using System.Collections.Generic;
using System.IO;
using Avalonia.Media.Imaging;
using OsuBeatmapArtwork_Downloader.Interfaces;

namespace OsuBeatmapArtwork_Downloader.Helpers;

public class ImageHelpers : IImageHelpers
{
    public List<Bitmap> ConvertMemoryStreamToBitmap(List<MemoryStream> beatmapImages)
    {
        var beatmapImagesBitmap = new List<Bitmap>();
        foreach (var image in beatmapImages)
        {
            image.Position = 0;
            var bitmap = new Bitmap(image);
            beatmapImagesBitmap.Add(bitmap);
        }
        return beatmapImagesBitmap;
    }
    
    public List<MemoryStream> ConvertBitmapToMemoryStream(List<Bitmap> beatmapImagesBitmap)
    {
        var beatmapImages = new List<MemoryStream>();
        foreach (var bitmap in beatmapImagesBitmap)
        {
            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream);
            memoryStream.Position = 0;
            beatmapImages.Add(memoryStream);
        }
        return beatmapImages;
    }
}