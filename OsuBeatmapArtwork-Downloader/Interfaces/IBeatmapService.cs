using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.Interfaces;

public interface IBeatmapService
{
    Task<DownloadResult> DownloadBeatmap(string textInput, string defaultFolderPath, string cookie);
}