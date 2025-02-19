using Avalonia;
using Avalonia.Controls;
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
}