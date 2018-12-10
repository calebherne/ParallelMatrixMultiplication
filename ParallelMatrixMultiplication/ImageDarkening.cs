using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace ParallelMatrixMultiplication
{
    public class ImageDarkening
    {
        Stopwatch stopwatch;
        Bitmap original;
        string path;
        int width;
        int height;

        public ImageDarkening(string path)
        {
            stopwatch = new Stopwatch();
            this.path = path;
            original = new Bitmap(path+".jpg");
            this.width = original.Width;
            this.height = original.Height;

        }

        public long DarkenSequential()
        {
            Bitmap pic = new Bitmap(this.path + ".jpg");
            Color[,] pixels = AllPixels(pic);

            stopwatch = Stopwatch.StartNew();
            for (int y = 0; y < this.height; y++)
            {
                this.DarkenOneRow(pixels,y);
            }
            stopwatch.Stop();

            SetAllPixels(pic, pixels);
            pic.Save(path + "Sequential.jpg");
            return stopwatch.ElapsedMilliseconds;
        }

        public long DarkenParallel()
        {
            Bitmap pic = new Bitmap(this.path + ".jpg");
            Color[,] pixels = AllPixels(pic);
            
            stopwatch = Stopwatch.StartNew();
            Parallel.For(0, this.height - 1, y =>
            {
                this.DarkenOneRow(pixels, y);
            });
            stopwatch.Stop();

            SetAllPixels(pic, pixels);
            pic.Save(path + "Parallel.jpg");
            return stopwatch.ElapsedMilliseconds;
        }

        private Color[,] AllPixels(Bitmap bitmap)
        {
            Color[,] pixels = new Color[this.width, this.height];
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    pixels[i,j] = bitmap.GetPixel(i, j);
                }
            }
            return pixels;
        }

        private void SetAllPixels(Bitmap pic, Color[,] pixels)
        {
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    pic.SetPixel(i, j, pixels[i,j]);
                }
            }
        }

        public void DarkenOneRow(Color[,] pixels,int y)
        {
            for (int x = 0; x < this.width; x++)
            {
                pixels[x,y] = ChangeColorBrightness(pixels[x,y], -0.5f);
            }
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
    }
}
