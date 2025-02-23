using System.Net;
using System.Threading.Tasks;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.Interfaces;

public interface IApiManagerService
{
    Task<T> Get<T>(string url);
    void AddUserCookie(UserCookie userCookie);
}