using JogoDaVelha.UI.ViewModels;
using JogoDaVelha.UI.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JogoDaVelha.UI;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        var vm = new MainMenuViewModel(
            onPlayPvp: () => ShowGame("PvP"),  // Quando "Jogar PvP" é clicado, chama o método ShowGame
            onPlayCpuEasy: () => ShowGame("CpuEasy"),
            onPlayCpuHard: () => ShowGame("CpuHard"),
            onOpenRanking: ShowRanking,
            onExit: () => Close()
        );

        MainContent.Content = new MainMenu { DataContext = vm };
    }

    private void ShowGame(string mode)
    {
        var vm = new GameViewModel(
            onBackToMenu: ShowMainMenu
        );

        // Aqui você pode passar o "mode" para o ViewModel se quiser
        MainContent.Content = new GameView { DataContext = vm };
    }

    private void ShowRanking()
    {
        var vm = new RankingViewModel(
            onBackToMenu: ShowMainMenu
        );

        MainContent.Content = new RankingView { DataContext = vm };
    }
}