using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Http;
using UploadImageAPI.Helpers;

namespace UploadImageAPI.Writers.ImageWriter
{
    public class ImageWriter : IImageWriter
    {
        public bool UploadImage(IFormFile file, string filePath)
        {
            var format = CheckIfImageFile(file);
            if (format != null) return WriteFile(file, filePath, format);

            return false;
        }

        /// <summary>
        ///     Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ImageFormat CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return ImageHelper.GetImageFormat(fileBytes);
        }

        public bool WriteFile(IFormFile file, string filePath, ImageFormat imageFormat)
        {
            if (string.IsNullOrEmpty(filePath)) return false;

            if (file == null || file.Length == 0) return false;

            try
            {
                var path = Path.Combine(filePath, file.FileName);

                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    using (var img = Image.FromStream(memoryStream))
                    {
                        var resized = img.Resize(100, 100);

                        resized.Save(path);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}