using System.Windows.Input;

namespace JogoDaVelha.UI.ViewModels
{
    public class MainMenuViewModel
    {
        public ICommand PlayPvpCommand { get; }
        public ICommand PlayCpuEasyCommand { get; }
        public ICommand PlayCpuHardCommand { get; }
        public ICommand OpenRankingCommand { get; }
        public ICommand ExitCommand { get; }

        public MainMenuViewModel(
            Action onPlayPvp,
            Action onPlayCpuEasy,
            Action onPlayCpuHard,
            Action onOpenRanking,
            Action onExit)
        {
            PlayPvpCommand = new RelayCommand(_ => onPlayPvp());
            PlayCpuEasyCommand = new RelayCommand(_ => onPlayCpuEasy());
            PlayCpuHardCommand = new RelayCommand(_ => onPlayCpuHard());
            OpenRankingCommand = new RelayCommand(_ => onOpenRanking());
            ExitCommand = new RelayCommand(_ => onExit());
        }
    }
}
