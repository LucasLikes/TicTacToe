using JogoDaVelha.UI;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class GameViewModel : INotifyPropertyChanged
{
    private readonly GameManager _gameManager;
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
        PlayCommand = new RelayCommand(param => MakeMove(Convert.ToInt32(param)));
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

    private void MakeMove(object param)
    {
        if (param is int position)
        {
            int row = position / 3;
            int col = position % 3;

            if (_gameManager.MakeMove(row, col))
            {
                UpdateBoard();

                if (_gameManager.IsGameOver)
                {
                    if (CheckForTie())
                    {
                        CurrentPlayerText = "Empateeeee!";
                    }
                    else
                    {
                        CurrentPlayerText = _gameManager.CurrentPlayer == GameManager.Player.X ? "X Venceu!" : "O Venceu!";
                    }
                }
                else
                {
                    CurrentPlayerText = _gameManager.CurrentPlayer == GameManager.Player.X ? "Vez Player X" : "Vez Player O";
                }
            }
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
