using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "";
            var resize = false;
            var width = 0;
            var height = 0;
            foreach (var item in args)
            {
                if (item.ToUpper() == "--HELP" || item.ToUpper() == "-H") {
                    HelpPage();
                    return;
                }
                if (item.ToUpper().StartsWith("--RESIZE="))
                {
                    var raw = item.Substring("--resize=".Length);
                    width = Convert.ToInt32(raw.Split('x')[0]);
                    height = Convert.ToInt32(raw.Split('x')[1]);
                    resize = true;
                }
                else path = item;
            }

            var image = ImageUtil.LoadBitmap(path);
            if (resize)
            {
                image = ImageUtil.ResizeBitmap(image, width, height);
            }

            var data = ImageUtil.FromBitmap(image);
            ImageUtil.PrintData(data);

        }

        private static void HelpPage()
        {
            Console.WriteLine(
                "ImagePrinter  Copyright (C) 2021  Malte Linke\n" +
                "This program comes with ABSOLUTELY NO WARRANTY.\n" +
                "\n" +
                "Synthax\n" +
                " ImagePrinter [--resize=WIDTHxHEIGHT] <image>\n" +
                "\n" +
                "Examples\n" +
                " ImagePrinter --resize=64x64 C:\\CoolImage.png\n" +
                " ImagePrinter \"C:\\My awesome Directiry\\CoolImage.png\"\n"
            );
        }
    }
}
