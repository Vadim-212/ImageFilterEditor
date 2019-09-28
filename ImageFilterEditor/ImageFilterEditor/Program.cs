using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyProject_Image
{
    class Program
    {
        static void Main(string[] args)
        {
            string errorMessage = System.AppDomain.CurrentDomain.FriendlyName + " [param] [attr]\n\nParams:\n-i - path to image (in \"\")\n\n-f - filter type:\nn - negative filter\nbw - black and white filter\n\nExample: \"" + System.AppDomain.CurrentDomain.FriendlyName + " -i \"./images/img.bmp\" -f n\" makes image/img.bmp in negative filter";
            if (args.Length < 4 || args[0] == "/?" || args[0] == "-help")
            {
                Console.WriteLine(errorMessage);
                return;
            }
            string imagePath = "", filterType = "";
            if (args[0] == "-i")
                imagePath = args[1];
            if (args[2] == "-f")
                filterType = args[3];
            if (imagePath == "" || filterType == "")
            {
                Console.WriteLine(errorMessage);
                return;
            }
            if (filterType != "n" && filterType != "bw")
            {
                Console.WriteLine("This filter doesn't exists\n" + errorMessage);
                return;
            }
            if (imagePath.Substring(0, 2) == "./")
                imagePath = Path.Combine(Environment.CurrentDirectory, imagePath.Substring(2));

            if (!File.Exists(imagePath))
            {
                Console.WriteLine("This image doesn't exists\n" + errorMessage);
                return;
            }

            FileInfo imageFile = new FileInfo(imagePath);
            if (imageFile.Extension != ".bmp")
            {
                Console.WriteLine("Image must have a \".bmp\" extension\n" + errorMessage);
                return;
            }


            Bitmap img = new Bitmap(Environment.CurrentDirectory + "\\img.bmp");
            Color[][] colors = GetBitMapColorMatrix(img);
            decimal percent = 0;
            decimal t1 = img.Width * img.Height;
            decimal percentStep = 1 / (t1);
            switch (filterType)
            {
                case "bw":
                    for (int i = 0; i < img.Width; i++)
                    {
                        for (int j = 0; j < img.Height; j++)
                        {
                            Color color = Color.FromArgb(255, 255, 255, 255);
                            if (colors[i][j].R > colors[i][j].G)
                            {
                                if (colors[i][j].R > colors[i][j].B)
                                    color = Color.FromArgb(255, colors[i][j].R, colors[i][j].R, colors[i][j].R);
                                else
                                    color = Color.FromArgb(255, colors[i][j].B, colors[i][j].B, colors[i][j].B);
                            }
                            else if (colors[i][j].G > colors[i][j].B)
                                color = Color.FromArgb(255, colors[i][j].G, colors[i][j].G, colors[i][j].G);
                            else
                                color = Color.FromArgb(255, colors[i][j].B, colors[i][j].B, colors[i][j].B);
                            colors[i][j] = color;
                            percent += percentStep;
                        }
                        Console.Clear();
                        Console.Write(decimal.ToDouble(percent * 100) + "% |");
                        for (int k = 0; k < decimal.ToInt32(percent * 100); k++)
                        {
                            Console.Write("#");
                        }
                        for (int k = 0; k < 100 - decimal.ToInt32(percent * 100); k++)
                        {
                            Console.Write(" ");
                        }
                        Console.Write("|");
                        Console.WriteLine();
                    }
                    break;
                case "n":
                    for (int i = 0; i < img.Width; i++)
                    {
                        for (int j = 0; j < img.Height; j++)
                        {
                            Color color = Color.FromArgb(255, 255 - colors[i][j].R, 255 - colors[i][j].G, 255 - colors[i][j].B);
                            colors[i][j] = color;
                            percent += percentStep;
                        }
                        Console.Clear();
                        Console.Write(decimal.ToDouble(percent * 100) + "% |");
                        for (int k = 0; k < decimal.ToInt32(percent * 100); k++)
                        {
                            Console.Write("#");
                        }
                        for (int k = 0; k < 100 - decimal.ToInt32(percent * 100); k++)
                        {
                            Console.Write(" ");
                        }
                        Console.Write("|");
                        Console.WriteLine();
                    }
                    break;
            }
            //for (int i = 0; i < img.Width; i++)
            //{
            //    for (int j = 0; j < img.Height; j++)
            //    {
            //        // изображение -> изображение в стандартных цветах консоли Windows
            //        //ConsoleColor cColor = Helper.GetConsoleColor(colors[i][j]);
            //        //colors[i][j] = GetColorFromConsoleColor(cColor);

            //        // изображение -> чёрно-белое изображение 
            //        //if((colors[i][j].R + colors[i][j].G + colors[i][j].B) < 383)
            //        //{
            //        //    colors[i][j] = GetColorFromConsoleColor(ConsoleColor.Black);
            //        //}
            //        //else
            //        //{
            //        //    colors[i][j] = GetColorFromConsoleColor(ConsoleColor.White);
            //        //}

            //        // изображение -> изображение серых оттенков
            //        //Color color = Color.FromArgb(255, 255, 255, 255);
            //        //if (colors[i][j].R > colors[i][j].G)
            //        //{
            //        //    if (colors[i][j].R > colors[i][j].B)
            //        //        color = Color.FromArgb(255, colors[i][j].R, colors[i][j].R, colors[i][j].R);
            //        //    else
            //        //        color = Color.FromArgb(255, colors[i][j].B, colors[i][j].B, colors[i][j].B);
            //        //}
            //        //else if (colors[i][j].G > colors[i][j].B)
            //        //    color = Color.FromArgb(255, colors[i][j].G, colors[i][j].G, colors[i][j].G);
            //        //else
            //        //    color = Color.FromArgb(255, colors[i][j].B, colors[i][j].B, colors[i][j].B);
            //        //colors[i][j] = color;

            //        // изображение -> изображение в негативе
            //        Color color = Color.FromArgb(255, 255 - colors[i][j].R, 255 - colors[i][j].G, 255 - colors[i][j].B);
            //        colors[i][j] = color;
            //        percent += percentStep;
            //    }
            //    Console.Clear();
            //    Console.Write(decimal.ToDouble(percent * 100) + "% |");
            //    for (int k = 0; k < decimal.ToInt32(percent * 100); k++)
            //    {
            //        Console.Write("#");
            //    }
            //    for (int k = 0; k < 100 - decimal.ToInt32(percent * 100); k++)
            //    {
            //        Console.Write(" ");
            //    }
            //    Console.Write("|");
            //    Console.WriteLine();
            //}
            Bitmap newImg = ColorsToBitmap(colors, img.Width, img.Height);
            newImg.Save(Environment.CurrentDirectory + "\\out.bmp");
        }
        public static Color[][] GetBitMapColorMatrix(string bitmapFilePath)
        {
            Bitmap b1 = new Bitmap(bitmapFilePath);

            int hight = b1.Height;
            int width = b1.Width;

            Color[][] colorMatrix = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j);
                }
            }
            return colorMatrix;
        }
        public static Color[][] GetBitMapColorMatrix(Bitmap image)
        {
            int hight = image.Height;
            int width = image.Width;

            Color[][] colorMatrix = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = image.GetPixel(i, j);
                }
            }
            return colorMatrix;
        }
        public static Bitmap ColorsToBitmap(Color[][] colors, int bitmapWidth, int bitmapHeight)
        {
            Bitmap bm = new Bitmap(bitmapWidth, bitmapHeight);
            for (int i = 0; i < bitmapWidth; i++)
            {
                for (int j = 0; j < bitmapHeight; j++)
                {
                    bm.SetPixel(i, j, colors[i][j]);
                }
            }

            return bm;
        }
        public static Color GetColorFromConsoleColor(ConsoleColor color)
        {
            Color newColor = new Color();
            switch (color)
            {
                case ConsoleColor.Black:
                    newColor = Color.Black;
                    break;
                case ConsoleColor.DarkBlue:
                    newColor = Color.DarkBlue;
                    break;
                case ConsoleColor.DarkGreen:
                    newColor = Color.DarkGreen;
                    break;
                case ConsoleColor.DarkCyan:
                    newColor = Color.DarkCyan;
                    break;
                case ConsoleColor.DarkRed:
                    newColor = Color.DarkRed;
                    break;
                case ConsoleColor.DarkMagenta:
                    newColor = Color.DarkMagenta;
                    break;
                case ConsoleColor.DarkYellow:
                    newColor = Color.FromArgb(255, 155, 136, 12);
                    break;
                case ConsoleColor.Gray:
                    newColor = Color.Gray;
                    break;
                case ConsoleColor.DarkGray:
                    newColor = Color.DarkGray;
                    break;
                case ConsoleColor.Blue:
                    newColor = Color.Blue;
                    break;
                case ConsoleColor.Green:
                    newColor = Color.Green;
                    break;
                case ConsoleColor.Cyan:
                    newColor = Color.Cyan;
                    break;
                case ConsoleColor.Red:
                    newColor = Color.Red;
                    break;
                case ConsoleColor.Magenta:
                    newColor = Color.Magenta;
                    break;
                case ConsoleColor.Yellow:
                    newColor = Color.Yellow;
                    break;
                case ConsoleColor.White:
                    newColor = Color.White;
                    break;
            }
            return newColor;
        }
    }
    public static class Helper
    {
        public static ConsoleColor GetConsoleColor(this Color color)
        {
            if (color.GetSaturation() < 0.5)
            {
                // we have a grayish color
                switch ((int)(color.GetBrightness() * 3.5))
                {
                    case 0: return ConsoleColor.Black;
                    case 1: return ConsoleColor.DarkGray;
                    case 2: return ConsoleColor.Gray;
                    default: return ConsoleColor.White;
                }
            }
            int hue = (int)Math.Round(color.GetHue() / 60, MidpointRounding.AwayFromZero);
            if (color.GetBrightness() < 0.4)
            {
                // dark color
                switch (hue)
                {
                    case 1: return ConsoleColor.DarkYellow;
                    case 2: return ConsoleColor.DarkGreen;
                    case 3: return ConsoleColor.DarkCyan;
                    case 4: return ConsoleColor.DarkBlue;
                    case 5: return ConsoleColor.DarkMagenta;
                    default: return ConsoleColor.DarkRed;
                }
            }
            // bright color
            switch (hue)
            {
                case 1: return ConsoleColor.Yellow;
                case 2: return ConsoleColor.Green;
                case 3: return ConsoleColor.Cyan;
                case 4: return ConsoleColor.Blue;
                case 5: return ConsoleColor.Magenta;
                default: return ConsoleColor.Red;
            }
        }
    }
}

