using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using OsuBeatmapArtwork_Downloader.Interfaces;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IBeatmapService _beatmapService;
    private readonly IImageHelpers _imageHelpers;
    private readonly AppSettings _appSettings;
    private readonly IFileService _fileService;
    public MainWindowViewModel(IBeatmapService beatmapService, AppSettings appSettings, IImageHelpers imageHelpers, IFileService fileService)
    {
        _beatmapService = beatmapService;
        _appSettings = appSettings;
        _imageHelpers = imageHelpers;
        _fileService = fileService;
    }
    public string BeatmapId { get; set; }
    public ObservableCollection<MemoryStream> BeatmapImages { get; set; } 
    private ObservableCollection<Bitmap> _beatmapImagesBitmap = new ObservableCollection<Bitmap>();
    public ObservableCollection<Bitmap> BeatmapImagesBitmap 
    { 
        get => _beatmapImagesBitmap; 
        set
        {
            _beatmapImagesBitmap = value;
            OnPropertyChanged(nameof(BeatmapImagesBitmap));
        }
    }
        
    public async Task DownloadBeatmap(string beatmapId)
    {
        var beatmapImages = await _beatmapService.DownloadBeatmap(BeatmapId, _appSettings.AutoSaveToDefaultFolderPath, _appSettings.DefaultFolderPath);
        if (beatmapImages != null)
        {
            BeatmapImages = new ObservableCollection<MemoryStream>(beatmapImages);
            // Convert MemoryStream images to Bitmaps
            var convertedBitmaps = _imageHelpers.ConvertMemoryStreamToBitmap(beatmapImages);
            BeatmapImagesBitmap = new ObservableCollection<Bitmap>(convertedBitmaps);
        }
    }

}