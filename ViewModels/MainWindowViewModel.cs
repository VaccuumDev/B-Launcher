using System;
using BW_Launcher.Models;
using System.Collections.ObjectModel;
using BW_Launcher.Models.Helpers;
using Avalonia.Media.Imaging;
//using BW_Launcher.Views;
using ReactiveUI;
using Avalonia.Markup.Xaml.MarkupExtensions;
using System.Diagnostics;
using Avalonia.Automation.Peers;

namespace BW_Launcher.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
  static sbyte osId = MainWindowModel.osId;

  public Bitmap? OSLogo { get; } = ImageHelper.LoadFromResource(new Uri((osId == 0) ? "avares://BW-Launcher/Assets/windows.png" : "avares://BW-Launcher/Assets/linux.png"));
  public string BWorldFolderLocation { get; } = (osId == 0) ? @"C:\ProgramFiles\B-World\" : @"~/.b-world/";

  // * 0_0
  ////const string URL = "https://127.0.0.0/";
  ////public static List<BW_Launcher.Models.Version> verDIsplayNames = BW_Launcher.Models.MainWindowModel.GetVersionsList();
  ////public static ObservableCollection<string> verDisplayNames { get; } = MainWindowModel.GetVersionsDisplayNames();

  private int _selectedItemIndex;
  public int SelectedItemIndex
  {
    get => _selectedItemIndex;
    set
    {
      if (_selectedItemIndex != value)
      {
        Log.Debug("Selected index changed");
        _selectedItemIndex = value;

        ID = BW_Launcher.Models.MainWindowModel.versionsList[
            _selectedItemIndex
        ].id;

        Description = BW_Launcher.Models.MainWindowModel.versionsList[
            _selectedItemIndex
        ].description;

        Link = (osId == 1) ? BW_Launcher.Models.MainWindowModel.versionsList[_selectedItemIndex].linkLinux : BW_Launcher.Models.MainWindowModel.versionsList[_selectedItemIndex].linkWindows;

        //Log.Information(Description);

        OnPropertyChanged();

        /*var selectedItem = SelectedItemIndex >= 0 && SelectedItemIndex < Items.Count 
            ? Items[SelectedItemIndex]
            : null;*/
      }
    }
  }

  private string _description = "# Выберите версию перед запуском\n## *(иначе ничего не произойдёт)*\n(да, обязательно **вручную** открыть список и выбрать необходимую версию. Это сосбенность архитектуры, возможно потом исправлю)";
  public string Description
  {
    get => _description;
    set => this.RaiseAndSetIfChanged(ref _description, value);
  }


  public static string Link = "empty";
  public static string ID = "z000";

  private ObservableCollection<string> _items = new ObservableCollection<string>();
  public ObservableCollection<string> Items
  {
    get { return _items; }
    set => this.RaiseAndSetIfChanged(ref _items, value);
  }

  public MainWindowViewModel()
  {
    Items = MainWindowModel.GetVersionsDisplayNames();
  }
}