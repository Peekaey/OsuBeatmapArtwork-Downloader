using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.Interfaces;

public interface IFileService
{
    ServiceResult CreateWorkingDirectory(string folderPath);
    List<MemoryStream>? GetImagesFromBeatmap(string beatmapFilePath);
    Task<FileSaveResult> SaveScreenshotToDisk(IStorageFile storageFile, Bitmap imageBitmap);
    Task<FileSaveResult> SaveSettingsToJsonConfig(string defaultSaveDirectory, string configSavePath, string cookie, string theme);
    Task<FileSaveResult> RemoveSettingsJsonConfig(string configSavePath);
}