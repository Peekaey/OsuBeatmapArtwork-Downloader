using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.Interfaces;

public interface IFileService
{
    ServiceResult CheckDirectoryExists(string folderPath);
    List<MemoryStream>? GetImagesFromBeatmap(string beatmapFilePath);
}