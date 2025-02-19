using System;

namespace OsuBeatmapArtwork_Downloader.Models;

public class AppSetting
{
    public string DefaultFolderPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
    public bool AutoSaveToDefaultFolderPath { get; set; } = false;
    public bool AutoSaveToClipboard { get; set; } = false;

    public Themes SelectedThemes { get; set; } = Themes.Light;
    public OperatingSystem CurrentOs { get; set; } = Environment.OSVersion;
}