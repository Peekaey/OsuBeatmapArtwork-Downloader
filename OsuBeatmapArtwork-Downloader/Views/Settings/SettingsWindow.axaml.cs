using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using OsuBeatmapArtwork_Downloader.ViewModels;

namespace OsuBeatmapArtwork_Downloader.Views.Settings;

public partial class SettingsWindow : Window
{
    private SettingsWindowViewModel WindowViewModel => DataContext as SettingsWindowViewModel;

    public SettingsWindow()
    {
        InitializeComponent();
    }
    
    private void StatusBar_OnBarPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            this.BeginMoveDrag(e);
        }
    }
    
    private void Close(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}