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

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ShowMainMenu();  // Chama o método para exibir o menu inicial
    }

    private void ShowMainMenu()
    {
        var mainMenu = new MainMenu();  // Substitua "MainMenu" com o nome correto da sua View
        MainContent.Content = mainMenu;  // Carrega o conteúdo no ContentControl
    }
}
