using System;

namespace OsuBeatmapArtwork_Downloader.Models;

public class AppSettings
{
    public string DefaultFolderPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
    public bool AutoSaveToDefaultFolderPath { get; set; } = false;
    public bool CreateSubfolderPerBeatmap { get; set; } = false;

    public Themes SelectedThemes { get; set; } = Themes.Light;
    public OperatingSystem CurrentOs { get; set; } = Environment.OSVersion;
}