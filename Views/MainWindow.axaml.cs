using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using BW_Launcher.Models;
using BW_Launcher.ViewModels;
using System;

namespace BW_Launcher.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Log.Information("Initialized");
        RunButton.Click += RunButtonClickHandler;
        InstallButton.Click += InstallButtonClickHandler;
        CodeButton.Click += CodeButtonClickHandler;
        //Log.Information($"Avalonia ItemSource variabble elements count is {MainWindowViewModel.verDisplayNames.Count}", "DEBUG");
    }
    public async void InstallButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        Log.Debug("Install button clicked");

        try
        {
            await BW_Launcher.Models.VersionsInstaller.DownloadAsync(
                MainWindowViewModel.Link,
                MainWindowViewModel.ID
            );
            Log.Debug($"Downloading finished");
        }
        catch (Exception ex)
        {
            Log.Error($" while downloading file: {ex.Message}");
        }

    }

    public void PointerPressedHandler(object? sender, PointerPressedEventArgs e)
    {
        Log.Debug("Start Dragging window");
        if (e.GetCurrentPoint(null).Properties.IsLeftButtonPressed && this is Avalonia.Controls.Window win)
        {
            win.BeginMoveDrag(e);
        }
    }
    public void RunButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        Log.Debug("Play button clicked");
        ProcessRunner.RunGame(MainWindowViewModel.ID);
    }
    public async void CodeButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        Log.Debug("Code button clicked");
        var top = TopLevel.GetTopLevel(this);
        if (top?.Launcher is null)
            return;

        var uri = new Uri("https://github.com/VaccuumDev/");
        await top.Launcher.LaunchUriAsync(uri);
    }
}
