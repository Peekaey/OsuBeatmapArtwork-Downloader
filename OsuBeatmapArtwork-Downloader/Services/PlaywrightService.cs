using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Playwright;
using OsuBeatmapArtwork_Downloader.Interfaces;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.Services;

public class PlaywrightService : IPlaywrightService
{

    public async Task<ServiceResult> DownloadBeatmap(string url, UserCookie userCookie, string downloadPath)
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        // Create a new context
        var context = await browser.NewContextAsync(new BrowserNewContextOptions { AcceptDownloads = true });

        var cookie = new Cookie
        {
            Name = "osu_session",
            Domain = ".ppy.sh",
            Path = "/",
            Value = userCookie.Value,
            SameSite = SameSiteAttribute.Lax,
            Secure = true,
            HttpOnly = true

        };

        // Add the cookie to the context
        await context.AddCookiesAsync(new[] { cookie });
        var page = await context.NewPageAsync();


        // Attach event handlers to observe all network requests/responses:
        List<string> networkRequestUrls = new List<string>();
        page.Request += (_, request) =>
        {
            Console.WriteLine($"[Request] {request.Method} {request.Url}");
            networkRequestUrls.Add(request.Url);
        };
        page.Response += async (_, response) => { Console.WriteLine($"[Response] {response.Status} {response.Url}"); };
        
        var initialResponse = await page.GotoAsync(url);

        if (initialResponse.Status == 404)
        {
            return ServiceResult.AsFailure("Beatmap not found");
        }
        
        await page.WaitForSelectorAsync("a.btn-osu-big.btn-osu-big--beatmapset-header");

        // Click Download Button to Trigger
        await page.ClickAsync("a.btn-osu-big.btn-osu-big--beatmapset-header");

        // Scan Network Requests/Responses To Find Mirror Url
        var downloadUrl = networkRequestUrls.Find(x => x.Contains("osumirror.idle.host"));

        using var handler = new HttpClientHandler();
        using var client = new HttpClient(handler);
        
        using var response = await client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead);
        if (!response.IsSuccessStatusCode)
        {
            return ServiceResult.AsFailure("Failed to download beatmap");
        }
            
        var headerFileName = response.Content.Headers.ContentDisposition?.FileName.Replace("\"", "") ?? "beatmap.zip";
        var combinedDownloadPath = Path.Combine(downloadPath, headerFileName);
        try 
        {
            await using (var fileStream = new FileStream(combinedDownloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await response.Content.CopyToAsync(fileStream);
                await fileStream.FlushAsync(); // Ensure the file stream is flushed
                await browser.CloseAsync();
                return new ServiceResult(true, combinedDownloadPath);
            }
        }
        catch (Exception e)
        {
            await browser.CloseAsync();
            return new ServiceResult(false, e.Message);
        }
    }
}
