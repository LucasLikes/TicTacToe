using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace JogoDaVelha.UI.ViewModels;
public class RankingViewModel : INotifyPropertyChanged
{
    public ICommand BackCommand { get; }
    private readonly Action _onBackToMenu;  // Armazenar a ação a ser executada quando o botão "Voltar" for pressionado

    public RankingViewModel(Action onBackToMenu)
    {
        _onBackToMenu = onBackToMenu;
        BackCommand = new RelayCommand(_ => _onBackToMenu());
    }

    public event PropertyChangedEventHandler PropertyChanged;

    // Aqui você pode adicionar outras propriedades, como o ranking
    private List<Ranking> _rankings;
    public List<Ranking> Rankings
    {
        get => _rankings;
        set
        {
            _rankings = value;
            OnPropertyChanged();
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

// Classe de exemplo para armazenar os dados do ranking
public class Ranking
{
    public string Player { get; set; }
    public int Wins { get; set; }
}