using System;
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

        // Callbacks para a navegação entre as telas
        private readonly Action _onPlayPvp;
        private readonly Action _onPlayCpuEasy;
        private readonly Action _onPlayCpuHard;
        private readonly Action _onOpenRanking;
        private readonly Action _onExit;

        public MainMenuViewModel(
            Action onPlayPvp,
            Action onPlayCpuEasy,
            Action onPlayCpuHard,
            Action onOpenRanking,
            Action onExit)
        {
            _onPlayPvp = onPlayPvp;
            _onPlayCpuEasy = onPlayCpuEasy;
            _onPlayCpuHard = onPlayCpuHard;
            _onOpenRanking = onOpenRanking;
            _onExit = onExit;

            PlayPvpCommand = new RelayCommand(_ => _onPlayPvp());
            PlayCpuEasyCommand = new RelayCommand(_ => _onPlayCpuEasy());
            PlayCpuHardCommand = new RelayCommand(_ => _onPlayCpuHard());
            OpenRankingCommand = new RelayCommand(_ => _onOpenRanking());
            ExitCommand = new RelayCommand(_ => _onExit());
        }
    }
}
