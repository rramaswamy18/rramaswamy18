using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSlnConsoleApp1
{
    public  class Program
    {
        public static void Main(string[] args)
        {
            //TestResizeImage();
            TestDivineBijaCreateItemImages();
        }
        private static void TestResizeImage()
        {
            string inputFullFileName = @"C:\Users\rrama\OneDrive\Desktop\Image_Cropped.jpg";
            string outputFullFileName = @"C:\Users\rrama\OneDrive\Desktop\Image_Resized.jpg";
            Class1 class1 = new Class1();
            class1.ResizeImage(inputFullFileName, outputFullFileName, 180, 180);
        }
        private static void TestDivineBijaCreateItemImages()
        {
            string outputDirectoryName = @"C:\Code\rramaswamy18\RetailSln\RetailSlnApplication\RetailSlnWeb\ClientSpecific\3_RetailSln\Documents\Images_Original\Items\";
            Class1 class1 = new Class1();
            //class1.DivineBijaCreateImagesForItems(outputDirectoryName);
            class1.DivineBijaCreateImagesForItemImages(outputDirectoryName);
        }
    }
}
