using System;
using System.IO;
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
    private void Minimise(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void Maximise(object sender, RoutedEventArgs e)
    {
        this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    
    private void RenderSettingsPage(object? sender, Avalonia.Interactivity.RoutedEventArgs eventArgs)
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
    }

    private void GetBeatmapForDownload(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                string userInput = textBox.Text;
                var downloadResult = ViewModel.DownloadBeatmap(userInput);
            }
        }
    }

    
}
