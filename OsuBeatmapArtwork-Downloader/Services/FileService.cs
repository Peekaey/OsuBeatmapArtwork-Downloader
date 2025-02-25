using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using OsuBeatmapArtwork_Downloader.Interfaces;
using OsuBeatmapArtwork_Downloader.Models;

namespace OsuBeatmapArtwork_Downloader.Services;

public class FileService : IFileService
{
    public async Task<FileSaveResult> SaveFileToDisk()
    {
        return FileSaveResult.AsFileSaveSuccess("");
    }

    public ServiceResult CheckDirectoryExists(string folderPath)
    {
        try
        {
            // Ensure download directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return ServiceResult.AsSuccess();
        }
        catch (IOException e)
        {
            return ServiceResult.AsFailure(e.Message);
        }
    }

    public List<MemoryStream>? GetImagesFromBeatmap(string beatmapFilePath)
    {
        //Chang file extension from .osz to .zip
        try
        {
            // Add Fallsafe to keep executing if the file exists
            var zipFilePath = beatmapFilePath.Replace(".osz", ".zip");
            
            if (!File.Exists(zipFilePath))
            {
                File.Move(beatmapFilePath, zipFilePath);
            }
            
            var images = GetImageContentsFromZip(zipFilePath);
            return images;
        }
        catch (IOException e)
        {
            return null;
        }
        
    }
        
    public List<MemoryStream> GetImageContentsFromZip(string zipFilePath)
    {
        // Look through zip path for files with .png or jpg extension
        var imageStreams = new List<MemoryStream>();
        
        using (var zipArchive = ZipFile.OpenRead(zipFilePath))
        {
            foreach (var entry in zipArchive.Entries)
            {
                // Check if the entry's name ends with .png or .jpg (case-insensitive)
                // Skip Lyric Folder
                // TODO Check if more efficient way to skip lyric folder

                if (entry.FullName.ToLower().Contains("sb/") && !entry.FullName.ToLower().Contains("bg/"))
                {
                    continue;
                }
                
                if (entry.FullName.ToLower().Contains("lyric"))
                {
                    continue;
                }
                if (IsImageFile(entry.FullName))
                {
                    var memoryStream = new MemoryStream();
    
                    using (var entryStream = entry.Open())
                    {
                        entryStream.CopyTo(memoryStream);
                    }
                    memoryStream.Position = 0;
                    imageStreams.Add(memoryStream);
                }
            }
        }
        return imageStreams;
    }
    
    private bool IsImageFile(string fileName)
    {
        return fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
               fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
               fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase);
    }

}