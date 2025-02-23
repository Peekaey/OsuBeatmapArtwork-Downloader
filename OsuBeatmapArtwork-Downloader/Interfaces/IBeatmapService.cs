using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OsuBeatmapArtwork_Downloader.Interfaces;

public interface IBeatmapService
{
    Task<List<MemoryStream>>? DownloadBeatmap(string textInput, bool autoSaveToDefaultFolderPath, string defaultFolderPath);
}