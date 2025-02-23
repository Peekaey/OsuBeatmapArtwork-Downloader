using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Net;
using System.Net.Http;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OsuBeatmapArtwork_Downloader.Helpers;
using OsuBeatmapArtwork_Downloader.Interfaces;
using OsuBeatmapArtwork_Downloader.Models;
using OsuBeatmapArtwork_Downloader.Services;
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
        var services = new ServiceCollection();
        services.AddSingleton(new CookieContainer());
        services.AddSingleton<IApiManagerService, ApiManagerService>();
        services.AddHttpClient<ApiManagerService>()
            .ConfigurePrimaryHttpMessageHandler(sp =>
            {
                return new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    CookieContainer = sp.GetRequiredService<CookieContainer>(),
                    UseCookies = true,
                };
            });
        services.AddSingleton<IBeatmapService, BeatmapService>();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IOsPlatformHelpers, OsPlatformHelpers>();
        services.AddSingleton<IValidationHelper, ValidationHelper>();
        services.AddSingleton<IPlaywrightService, PlaywrightService>();
        services.AddSingleton<IImageHelpers, ImageHelpers>();
        
        services.AddSingleton<AppSettings>();
        
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<SettingsWindowViewModel>();
        
        
        _serviceProvider = services.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>()
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