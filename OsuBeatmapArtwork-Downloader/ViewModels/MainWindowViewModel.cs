﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using OsuBeatmapArtwork_Downloader.Interfaces;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.ViewModels;

public partial class MainWindowViewModel : ViewModelBase , INotifyPropertyChanged
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
        IsBusy = false;
    }
    public string BeatmapId { get; set; }
    
    private string _statusMessage;
    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            if (_statusMessage != value)
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy != value)
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
    }
    public ObservableCollection<MemoryStream> BeatmapImages { get; set; }
    private ObservableCollection<Bitmap> _beatmapImagesBitmap;
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
        StatusMessage = "Downloading Beatmap...";
        IsBusy = true;
        var beatmapImagesResult = await _beatmapService.DownloadBeatmap(BeatmapId, _appSettings.DefaultFolderPath, _appSettings.OsuCookieValue);

        if (beatmapImagesResult.Success == false)
        {
            StatusMessage = $"Error downloading beatmap: {beatmapImagesResult.StatusMessage}";
        }

        if (beatmapImagesResult.Success == true && !beatmapImagesResult.Artworks.Any())
        {
            StatusMessage = $"No Artwork found for Beatmap: {beatmapImagesResult.StatusMessage}";
        }
        
        if (beatmapImagesResult.Success == true && beatmapImagesResult.Artworks.Any())
        {
            BeatmapImages = new ObservableCollection<MemoryStream>(beatmapImagesResult.Artworks);
            // Convert MemoryStream images to Bitmaps so Avalonia UI can Render them
            var convertedBitmaps = _imageHelpers.ConvertMemoryStreamToBitmap(beatmapImagesResult.Artworks);
            BeatmapImagesBitmap = new ObservableCollection<Bitmap>(convertedBitmaps);
            StatusMessage = $"Showing Artwork for : {beatmapImagesResult.StatusMessage}";
        }
    }
    
    public async Task SaveImageToDisk(IStorageFile saveFilePath, Bitmap bitmap)
    {
        _fileService.SaveScreenshotToDisk(saveFilePath, bitmap);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        Console.WriteLine($"Property changed: {propertyName} to {GetType().GetProperty(propertyName)?.GetValue(this)}");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}