using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using OsuBeatmapArtwork_Downloader.Models;
using OsuBeatmapArtwork_Downloader.ViewModels;
using OsuBeatmapArtwork_Downloader.Views.Settings;

namespace OsuBeatmapArtwork_Downloader.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void TitleBar_OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            this.BeginMoveDrag(e);
        }
    }

    private void Minimize_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void Maximize_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
    private void OnSettingsButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs eventArgs)
    {
        // TODO Improve this and optimise
        var appSettings = new AppSettings();
        var settingsViewModel = new SettingsWindowViewModel(appSettings);
        var settingsWindow = new SettingsWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            DataContext = settingsViewModel
        };
        
        settingsWindow.ShowDialog(this);
        Console.WriteLine("Settings button clicked!");
    }
    
}
