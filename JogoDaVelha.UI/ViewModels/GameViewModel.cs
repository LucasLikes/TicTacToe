using JogoDaVelha.UI;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JogoDaVelha.Core.Logging;
using System.Text.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;


public class GameViewModel : INotifyPropertyChanged
{
    private readonly GameManager _gameManager;
    private List<MoveData> _gameHistory = new();
    private string[] _board;
    private string _currentPlayerText;

    public ICommand PlayCommand { get; }
    public ICommand NewGameCommand { get; }
    public ICommand BackCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public GameViewModel(Action onBackToMenu)
    {
        _gameManager = new GameManager();
        _board = new string[9];
        PlayCommand = new RelayCommand(async param =>
        {
            if (int.TryParse(param?.ToString(), out int pos))
            {
                await MakeMoveAsync(pos);
            }
        });
        NewGameCommand = new RelayCommand(param => NewGame());
        BackCommand = new RelayCommand(param => onBackToMenu());

        UpdateBoard();
    }

    public string[] Board => _board;
    public string CurrentPlayerText
    {
        get => _currentPlayerText;
        private set
        {
            _currentPlayerText = value;
            OnPropertyChanged();
        }
    }



    private async Task MakeMoveAsync(int position)
    {
        int row = position / 3;
        int col = position % 3;

        // Copia o estado atual do tabuleiro
        string[] stateSnapshot = new string[9];
        for (int i = 0; i < 9; i++)
        {
            var player = _gameManager.Board[i / 3, i % 3];
            stateSnapshot[i] = player == GameManager.Player.None ? " " : player.ToString();
        }

        if (_gameManager.MakeMove(row, col))
        {
            _gameHistory.Add(new MoveData
            {
                State = stateSnapshot,
                Action = position,
                Player = _gameManager.CurrentPlayer == GameManager.Player.X ? "O" : "X"
            });

            UpdateBoard();

            if (_gameManager.IsGameOver)
            {
                string winner = CheckForTie() ? "None" : (_gameManager.CurrentPlayer == GameManager.Player.X ? "O" : "X");

                CurrentPlayerText = winner == "None" ? "Empateeeee!" : $"{winner} Venceu!";

                var gameLog = new GameLog
                {
                    History = _gameHistory,
                    Winner = winner
                };

                await SendGameLogToPythonAsync(gameLog);

                var json = JsonSerializer.Serialize(gameLog, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("last_game.json", json);
            }
            else
            {
                CurrentPlayerText = _gameManager.CurrentPlayer == GameManager.Player.X ? "Vez Player X" : "Vez Player O";
            }
        }
    }


    private async Task SendGameLogToPythonAsync(GameLog gameLog)
    {
        using var client = new HttpClient();

        string json = JsonSerializer.Serialize(gameLog);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync("http://localhost:5000/api/logs", content); // ← Arrumar o URI...(Criar no Py)
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Log enviado com sucesso!");
            }
            else
            {
                Console.WriteLine($"Erro ao enviar log: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exceção ao enviar log: {ex.Message}");
        }
    }

    private bool CheckForTie()
    {
        // Verifica se todas as casas estão preenchidas e o jogo não acabou
        return !_gameManager.Board.Cast<GameManager.Player>().Any(cell => cell == GameManager.Player.None);
    }

    private void NewGame()
    {
        _gameManager.ResetBoard();
        _gameHistory.Clear();
        UpdateBoard();
        CurrentPlayerText = "Player X sua vez!";
    }

    private void UpdateBoard()
    {
        for (int i = 0; i < 9; i++)
            _board[i] = _gameManager.Board[i / 3, i % 3] == GameManager.Player.None ? "" : _gameManager.Board[i / 3, i % 3].ToString();

        OnPropertyChanged(nameof(Board));
    }

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
