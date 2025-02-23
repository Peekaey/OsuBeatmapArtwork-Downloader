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
    
    private void TitleBar_OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            this.BeginMoveDrag(e);
        }
    }
    
    private void Close_Click(object sender, RoutedEventArgs e)
    {
        // Close the settings window
        this.Close();
    }
}