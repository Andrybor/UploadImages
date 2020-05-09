using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UploadImageAPI.Services.Interfaces
{
    public interface IUploadImageService
    {
        Task<bool> UploadImage(IFormFile file, string filePath);
    }
}