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
            var path = Directory.GetCurrentDirectory() + @"/Resources/test.png";
            IActionResult result;

            // act
            using (var stream = File.OpenRead(path))
            {
                result = await controller.UploadImage(new FormCollection(null,
                    new FormFileCollection
                    {
                        new FormFile(stream, 0, stream.Length, null,
                            Path.GetFileName(path))
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