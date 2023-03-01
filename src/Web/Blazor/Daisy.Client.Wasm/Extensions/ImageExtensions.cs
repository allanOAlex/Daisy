using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
//using static System.Net.Mime.MediaTypeNames;


namespace Daisy.Client.Wasm.Extensions
{
    public static class ImageExtensions
    {
        public static string ConvertImageToBase64(Image file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, file.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public static Image ConvertBase64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                return Image.FromStream(ms, true);
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            Image result;
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                result = Image.FromStream(ms);    
            }

            return result;
        }

        public static string ResizeImage(string base64, int width, int height, ImageFormat format)
        {
            string imageType = null;
            var image = Regex.Replace(base64, @"^data:image\/[a-zA-Z]+base64,", string.Empty);
            Image resizeImage = ResizeImage(Base64ToImage(image), width, height);
            using MemoryStream memoryStream = new MemoryStream();
            resizeImage.Save(memoryStream, format);
            return $"data:{imageType};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
        }

        public static string DoResizeImage(string base64, int width, int height)
        {
            try
            {
                Image image = ConvertBase64ToImage(base64);
                Bitmap bitMap = new Bitmap(image);
                image = DoResizeImage(bitMap, width, height);
                string resizedImage = ConvertImageToBase64(image);
                return resizedImage;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error resizing carousel image | {ex.Message}");
            }
            
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destinationRectangle = new Rectangle(0, 0,width, height);
            var resultImage = new Bitmap(width, height);
            resultImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(resultImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destinationRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return resultImage;
        }

        public static Image DoResizeImage(Image image, int width, int height)
        {
            var destinationRect = new Rectangle(0, 0, width, height);
            var destinationImage = new Bitmap(width, height);

            destinationImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destinationImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destinationRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destinationImage;
        }


    }
}
