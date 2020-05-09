using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using UploadImageAPI.Controllers;
using UploadImageAPI.Services.Implementations;
using UploadImageAPI.Writers.ImageWriter;
using Xunit;

namespace UploadImageAPI.UnitTests
{
    public class ImagesControllerTests
    {
        [Fact]
        public async void UploadedImages()
        {
            // arrange
            var imageWriter = new ImageWriter();
            var imageService = new UploadImageService(imageWriter);
            var controller = new ImagesController(imageService);
            IActionResult result;
            // act
            using (var stream = File.OpenRead(Directory.GetCurrentDirectory() + @"/Resources/test.png"))
            {
                result = await controller.UploadImage(new FormCollection(new Dictionary<string, StringValues>(),
                    new FormFileCollection
                    {
                        new FormFile(stream, 0, stream.Length, null,
                            Path.GetFileName(Directory.GetCurrentDirectory() + @"/Resources/test.png"))
                        {
                            Headers = new HeaderDictionary(),
                            ContentType = "multipart/form-data"
                        }
                    }));
            }

            // assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var isSucceed = (bool) okObjectResult.Value;
            Assert.True(isSucceed);
        }
    }
}