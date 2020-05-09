using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UploadImageAPI.Services.Interfaces;
using UploadImageAPI.Writers.ImageWriter;

namespace UploadImageAPI.Services.Implementations
{
    public class UploadImageService : IUploadImageService
    {
        private readonly IImageWriter _imageWriter;

        public UploadImageService(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<bool> UploadImage(IFormFile file, string filePath)
        {
            return await Task.FromResult(_imageWriter.UploadImage(file, filePath));
        }
    }
}