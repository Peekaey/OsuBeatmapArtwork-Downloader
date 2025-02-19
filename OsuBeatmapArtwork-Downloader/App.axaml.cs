using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using OsuBeatmapArtwork_Downloader.Models;
using OsuBeatmapArtwork_Downloader.ViewModels;
using OsuBeatmapArtwork_Downloader.Views;

namespace OsuBeatmapArtwork_Downloader;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    private StyleInclude? _lightTheme;
    private StyleInclude? _darkTheme;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        _lightTheme = new StyleInclude(new Uri("avares://OsuBeatmapArtwork-Downloader/"))
        {
            Source = new Uri("avares://OsuBeatmapArtwork-Downloader/Themes/LightTheme.axaml")
        };

        _darkTheme = new StyleInclude(new Uri("avares://OsuBeatmapArtwork-Downloader/"))
        {
            Source = new Uri("avares://OsuBeatmapArtwork-Downloader/Themes/DarkTheme.axaml")
        };

        SetTheme(Themes.Light);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    public void SetTheme(Themes themes)
    {
        if (_lightTheme != null && Styles.Contains(_lightTheme))
        {
            Styles.Remove(_lightTheme);
        }

        if (_darkTheme != null && Styles.Contains(_darkTheme))
        {
            Styles.Remove(_darkTheme);
        }

        if (themes == Themes.Light && _lightTheme != null)
        {
            Styles.Add(_lightTheme);
        }
        else if (themes == Themes.Dark && _darkTheme != null)
        {
            Styles.Add(_darkTheme);
        }
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}