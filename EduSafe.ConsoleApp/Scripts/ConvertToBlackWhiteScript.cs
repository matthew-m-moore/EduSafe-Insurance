using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using EduSafe.ConsoleApp.Interfaces;

namespace EduSafe.ConsoleApp.Scripts
{
    /// <summary>
    /// This is totally and completely code I stole from searching online.
    /// Thank you random kindness of strangers!
    /// </summary>
    public class ConvertToBlackWhiteScript : IScript
    {
        public List<string> GetArgumentsList()
        {
            return new List<string>
            {
                "[1] Enter path to the image to convert",
            };
        }

        public string GetFriendlyName()
        {
            return "Black & White Converter Tool";
        }

        public bool GetVisibilityStatus()
        {
            return false;
        }

        public void RunScript(string[] args)
        {
            var extension = args[1].Split('.').ToList().Last();
            var basePath = new string(args[1].Take(args[1].Length - extension.Length - 1).ToArray());

            var image = Image.FromFile(args[1]) as Bitmap;
            var newImage = ConvertBlackAndWhitePixelByPixel(image);
            newImage.Save(basePath + " - (B & W)." + extension);
        }

        /// <summary>
        /// https://stackoverflow.com/questions/2746103/what-would-be-a-good-true-black-and-white-colormatrix
        /// </summary>
        public static Bitmap ConvertBlackAndWhiteWithMatrix(Bitmap image)
        {
            var tempMatrix = new ColorMatrix
            {
                Matrix = new float[][]{
                    new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
                    new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
                    new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
                    new float[] { 0,      0,      0,      1, 0 },
                    new float[] { 0,      0,      0,      0, 1 }
                 }
            };

            return tempMatrix.Apply(image);
        }

        public static Bitmap ConvertBlackAndWhitePixelByPixel(Bitmap image)
        {
            var width = image.Width;
            var height = image.Height;
            var total = width * height;
            var arr = new int[225];
            var i = 1;
            Color p;

            //Grayscale
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    Console.WriteLine(string.Format("Converting pixel {0} of {1}", i, total));
                    p = image.GetPixel(x, y);
                    var a = p.A;
                    var r = p.R;
                    var g = p.G;
                    var b = p.B;
                    var avg = (r + g + b) / 3;
                    avg = avg < 128 ? 0 : 255;     // Converting gray pixels to either pure black or pure white
                    image.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                    i++;
                }
            }

            return image;
        }

        /// <summary>
        /// http://www.gutgames.com/post/Converting-Image-to-Black-and-White-in-C.aspx
        /// Helper class for setting up and applying a color matrix
        /// </summary>
        private class ColorMatrix
        {
            /// <summary>
            /// Matrix containing the values of the ColorMatrix
            /// </summary>
            public float[][] Matrix { get; set; }

            /// <summary>
            /// Applies the color matrix
            /// </summary>
            /// <param name="originalImage">Image sent in</param>
            /// <returns>An image with the color matrix applied</returns>
            public Bitmap Apply(Bitmap originalImage)
            {
                var newBitmap = new Bitmap(originalImage.Width, originalImage.Height);
                using (var newGraphics = Graphics.FromImage(newBitmap))
                {
                    System.Drawing.Imaging.ColorMatrix NewColorMatrix = new System.Drawing.Imaging.ColorMatrix(Matrix);
                    using (ImageAttributes Attributes = new ImageAttributes())
                    {
                        Attributes.SetColorMatrix(NewColorMatrix);
                        newGraphics.DrawImage(originalImage,
                            new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                            0, 0, originalImage.Width, originalImage.Height,
                            GraphicsUnit.Pixel,
                            Attributes);
                    }
                }
                return newBitmap;
            }
        }
    }
}
