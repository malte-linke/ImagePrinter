using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePrinter
{

    class ImageUtil
    {
        public static ConsoleColor ToConsoleColor(Color c)
        {
            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
            index |= (c.R > 64) ? 4 : 0; // Red bit
            index |= (c.G > 64) ? 2 : 0; // Green bit
            index |= (c.B > 64) ? 1 : 0; // Blue bit

            return (ConsoleColor)index;
        }

        public static ConsoleColor[][] FromBitmap(Bitmap source)
        {
            var result = new ConsoleColor[source.Height][];
            for (int y = 0; y < result.Length; y++)
            {
                result[y] = new ConsoleColor[source.Width];
                for (int x = 0; x < result[y].Length; x++)
                {
                    var pixel = source.GetPixel(x, y);
                    result[y][x] = ToConsoleColor(pixel);
                }
            }
            return result;
        }

        public static void PrintData(ConsoleColor[][] data)
        {
            for (int y = 0; y < data.Length; y += 2)
            {
                var fg = Console.ForegroundColor;
                var bg = Console.BackgroundColor;
                for (int x = 0; x < data[y].Length; x++)
                {
                    Console.ForegroundColor = data[y + 1][x];
                    Console.BackgroundColor = data[y][x];
                    Console.Write("▄");
                }
                Console.WriteLine();
                Console.ForegroundColor = fg;
                Console.BackgroundColor = bg;
            }
        }

        public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        public static Bitmap LoadBitmap(string path)
        {
            return new Bitmap(path);
        }
    }
}
