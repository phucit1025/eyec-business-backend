using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeC.Application.Common.Interfaces;
public interface IImageService
{
    Task<string> SaveImage(byte[] imageBytes, string fileName, int compressionQuality = 100);
}
