using System.Threading.Tasks;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.Interfaces;

public interface IPlaywrightService
{
    Task<PlaywrightServiceResult> DownloadBeatmap(string url, UserCookie userCookie, string downloadPath);
}