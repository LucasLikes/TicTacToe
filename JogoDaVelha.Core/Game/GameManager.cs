public class GameManager
{
    public enum Player { None, X, O }

    private Player[,] board = new Player[3, 3];  // Tabuleiro 3x3 privado

    // Propriedade pública para acessar o tabuleiro
    public Player[,] Board => board;

    public Player CurrentPlayer { get; private set; } = Player.X;
    public bool IsGameOver { get; private set; } = false;

    public GameManager()
    {
        ResetBoard();
    }

    // Resetar o tabuleiro
    public void ResetBoard()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = Player.None;

        IsGameOver = false;
        CurrentPlayer = Player.X;
    }

    // Fazer uma jogada
    public bool MakeMove(int row, int col)
    {
        if (board[row, col] == Player.None && !IsGameOver)
        {
            board[row, col] = CurrentPlayer;
            if (CheckForWinner())
            {
                IsGameOver = true;
                return true;
            }

            // Trocar o jogador
            CurrentPlayer = (CurrentPlayer == Player.X) ? Player.O : Player.X;
            return true;
        }
        return false;
    }

    // Verificar se alguém venceu
    private bool CheckForWinner()
    {
        // Verificar linhas, colunas e diagonais
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != Player.None)
                return true;

            if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != Player.None)
                return true;
        }

        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != Player.None)
            return true;

        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != Player.None)
            return true;

        return false;
    }
}
