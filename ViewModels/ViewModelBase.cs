
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReactiveUI;

namespace BW_Launcher.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = "null") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}