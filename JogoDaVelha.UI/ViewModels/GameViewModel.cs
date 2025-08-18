using JogoDaVelha.Core.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JogoDaVelha.UI.ViewModels;
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
        PlayCommand = new RelayCommand(param => MakeMove((int)param));
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

    private void MakeMove(int position)
    {
        int row = position / 3;
        int col = position % 3;

        if (_gameManager.MakeMove(row, col))
        {
            UpdateBoard();

            if (_gameManager.IsGameOver)
            {
                CurrentPlayerText = _gameManager.CurrentPlayer == GameManager.Player.X ? "X wins!" : "O wins!";
            }
            else
            {
                CurrentPlayerText = _gameManager.CurrentPlayer == GameManager.Player.X ? "Player X's turn" : "Player O's turn";
            }
        }
    }

    private void NewGame()
    {
        _gameManager.ResetBoard();
        UpdateBoard();
        CurrentPlayerText = "Player X's turn";
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
