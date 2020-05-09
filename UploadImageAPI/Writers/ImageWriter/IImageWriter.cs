using Microsoft.AspNetCore.Http;

namespace UploadImageAPI.Writers.ImageWriter
{
    public interface IImageWriter
    {
        bool UploadImage(IFormFile file, string filePath);
    }
}