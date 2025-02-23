using System.Collections.Generic;
using System.IO;
using Avalonia.Media.Imaging;

namespace OsuBeatmapArtwork_Downloader.Interfaces;

public interface IImageHelpers
{
    List<Bitmap> ConvertMemoryStreamToBitmap(List<MemoryStream> beatmapImages);
    List<MemoryStream> ConvertBitmapToMemoryStream(List<Bitmap> beatmapImagesBitmap);
}