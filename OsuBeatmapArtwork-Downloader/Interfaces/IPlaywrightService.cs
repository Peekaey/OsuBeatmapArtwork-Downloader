using System.Threading.Tasks;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.Interfaces;

public interface IPlaywrightService
{
    Task<ServiceResult> DownloadBeatmap(string url, UserCookie userCookie, string downloadPath);
}