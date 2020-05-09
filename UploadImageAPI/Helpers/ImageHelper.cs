using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace UploadImageAPI.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        ///     Get image format from array bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM"); // BMP
            var gif = Encoding.ASCII.GetBytes("GIF"); // GIF
            var png = new byte[] {137, 80, 78, 71}; // PNG
            var jpeg = new byte[] {255, 216, 255, 224}; // jpeg
            var jpeg2 = new byte[] {255, 216, 255, 225}; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.Bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.Gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.Png;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.Jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.Jpeg;

            return null;
        }

        /// <summary>
        ///     Resize Image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image Resize(this Image image, int width, int height)
        {
            var res = new Bitmap(width, height);
            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, width, height);
            }

            return res;
        }
    }
}