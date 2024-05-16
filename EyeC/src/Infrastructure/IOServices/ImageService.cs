using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeC.Application.Common.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace EyeC.Infrastructure.IOServices;
public class ImageService : IImageService
{
    public async Task<string> SaveImage(byte[] imageBytes, string fileName, int compressionQuality)
    {
        var path = $"../Web/wwwroot/images";
        var folderPath = Path.Combine(path);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var imagePath = Path.Combine($"{path}", fileName);
        await File.WriteAllBytesAsync(imagePath, imageBytes);

        if(compressionQuality > 0 && compressionQuality < 100)
        {
            var image = await Image.LoadAsync(imagePath);
            var encoder = new JpegEncoder() { Quality = compressionQuality };
            await image.SaveAsync(path, encoder);
        }
        //TODO: Build output path

        return "";
    }
}
