using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OsuBeatmapArtwork_Downloader.Interfaces;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.Services;

public class BeatmapService : IBeatmapService
{
    private readonly string _beatmapUrl = "";
    private readonly string _baseURL = "";
    private readonly string beatmapId = "";
    

    private readonly IApiManagerService _apiManagerService;
    private readonly IPlaywrightService _playwrightService;
    private readonly IFileService _fileService;
    
    
    public BeatmapService(IApiManagerService apiManagerService, IPlaywrightService playwrightService, IFileService fileService)
    {
        _apiManagerService = apiManagerService;
        _playwrightService = playwrightService;
        _fileService = fileService;
    }
    
    public async Task<List<MemoryStream>>? DownloadBeatmap(string textInput, bool autoSaveToDefaultFolderPath, string defaultFolderPath)
    {
        var downloadUrl = CreateDownloadUrl(textInput);
        
        var directoryResult = _fileService.CheckDirectoryExists(defaultFolderPath);
        
        if (directoryResult.Success == false)
        {
            return null;
        }
        
        var userCookie = new UserCookie
        {
            Expires = FormatExpireDateTime(""),
            Value = "",
        };
        
        var downloadResult = await _playwrightService.DownloadBeatmap(downloadUrl, userCookie,  defaultFolderPath);

        if (downloadResult.Success)
        {
            var images = _fileService.GetImagesFromBeatmap(downloadResult.ErrorMessage);
            return images;
        }
        else
        {
            return null;
        }
    }
    
    private string CreateDownloadUrl(string textInput)
    {
        return $"{_beatmapUrl}{textInput}/download";
    }
    
    private DateTime FormatExpireDateTime(string expireDate)
    {
        return DateTime.Parse(expireDate);
    }
}