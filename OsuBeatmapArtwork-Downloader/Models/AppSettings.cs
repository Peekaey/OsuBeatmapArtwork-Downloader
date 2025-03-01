using System;
using System.IO;

namespace OsuBeatmapArtwork_Downloader.Models;

public class AppSettings
{
    public string DefaultFolderPath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "BeatmapArtwork-Downloader");
    public Themes SelectedThemes { get; set; } = Themes.Light;
    public string OsuCookieValue { get; set; }
    public bool SaveSettingsToConfigFile { get; set; }
    public string ConfigFilePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "BeatmapArtwork-Downloader", "cookie.json");
}