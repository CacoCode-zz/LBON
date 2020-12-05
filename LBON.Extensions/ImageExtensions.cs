using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace LBON.Extensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Convert image to base64
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToBase64(this Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                var arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
        }

        /// <summary>
        /// Scales a Image to make it fit inside of a Height/Width
        /// </summary>
        /// <param name="image"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Image ScaleImage(this Image image, int height, int width)
        {
            if (image == null || height <= 0 || width <= 0)
            {
                return null;
            }
            int newWidth = (image.Width * height) / (image.Height);
            int newHeight = (image.Height * width) / (image.Width);
            int x = 0;
            int y = 0;

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

            // use this when debugging.
            //g.FillRectangle(Brushes.Aqua, 0, 0, bmp.Width - 1, bmp.Height - 1);
            if (newWidth > width)
            {
                // use new height
                x = (bmp.Width - width) / 2;
                y = (bmp.Height - newHeight) / 2;
                g.DrawImage(image, x, y, width, newHeight);
            }
            else
            {
                // use new width
                x = (bmp.Width / 2) - (newWidth / 2);
                y = (bmp.Height / 2) - (height / 2);
                g.DrawImage(image, x, y, newWidth, height);
            }
            // use this when debugging.
            //g.DrawRectangle(new Pen(Color.Red, 1), 0, 0, bmp.Width - 1, bmp.Height - 1);
            return bmp;
        }

        /// <summary>
        /// Convert image to byte array
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Image image, ImageFormat format)
        {
            if (image == null)
                throw new ArgumentNullException("image");
            if (format == null)
                throw new ArgumentNullException("format");

            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, format);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Create a new Image from a byte array
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Image ToImage(this byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            return Image.FromStream(new MemoryStream(bytes));
        }

        private static byte[] Pngiconheader =
            { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// Convert image to ICO stream
        /// </summary>
        /// <param name="image"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static MemoryStream ToIcoStream(Image image, Size s)
        {
            using (Bitmap bmp = new Bitmap(image, s))
            {
                byte[] png;
                using (MemoryStream fs = new MemoryStream())
                {
                    bmp.Save(fs, ImageFormat.Png);
                    fs.Position = 0;
                    png = fs.ToArray();
                }

                MemoryStream outs = new MemoryStream();
                Pngiconheader[6] = (byte)s.Width;
                Pngiconheader[7] = (byte)s.Height;
                Pngiconheader[14] = (byte)(png.Length & 255);
                Pngiconheader[15] = (byte)(png.Length / 256);
                Pngiconheader[18] = (byte)(Pngiconheader.Length);

                outs.Write(Pngiconheader, 0, Pngiconheader.Length);
                outs.Write(png, 0, png.Length);
                outs.Position = 0;
                return outs;
            }
        }
    }
}
