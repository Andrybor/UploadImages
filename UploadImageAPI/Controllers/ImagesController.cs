using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadImageAPI.Extensions;
using UploadImageAPI.Services.Interfaces;

namespace UploadImageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IUploadImageService _imageService;

        public ImagesController(IUploadImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] IFormCollection collection)
        {
            var path = Path.GetFullPath(@"..\..\..\..\");
            var uploadedFiles = collection.Files;
            if (uploadedFiles.Count > 0)
            {
                var isSucceed = await uploadedFiles.ToList()
                    .ForEachAsync(async i => await _imageService.UploadImage(i, path));
                return Ok(isSucceed);
            }

            return NotFound(false);
        }
    }
}