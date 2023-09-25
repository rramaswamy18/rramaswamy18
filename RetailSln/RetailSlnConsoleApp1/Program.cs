using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSlnConsoleApp1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Process Started");
            //TestResizeImage();
            //TestDivineBijaCreateImagesForItems();
            TestDivineBijaResizeBooksImages();
            TestDivineBijaResizeProductsImages();
            Console.WriteLine("Process Completed");
            //Console.ReadKey();
        }
        private static void TestResizeImage()
        {
            string inputFullFileName = @"C:\Users\rrama\OneDrive\Desktop\Image_Cropped.jpg";
            string outputFullFileName = @"C:\Users\rrama\OneDrive\Desktop\Image_Resized.jpg";
            Class1 class1 = new Class1();
            class1.ResizeImage(inputFullFileName, outputFullFileName, 180, 180);
        }
        private static void TestDivineBijaCreateImagesForItems()
        {
            string outputDirectoryName = @"C:\Code\rramaswamy18\RetailSln\RetailSlnApplication\RetailSlnWeb\ClientSpecific\3_RetailSln\Documents\Images\Items\";
            Class1 class1 = new Class1();
            class1.DivineBijaCreateImagesForItems(outputDirectoryName);
        }
        private static void TestDivineBijaCreateImagesForItemImagess()
        {
            string outputDirectoryName = @"C:\Code\rramaswamy18\RetailSln\RetailSlnApplication\RetailSlnWeb\ClientSpecific\3_RetailSln\Documents\Images\Items\";
            Class1 class1 = new Class1();
            class1.DivineBijaCreateImagesForItemImages(outputDirectoryName);
        }
        private static void TestDivineBijaResizeBooksImages()
        {
            string databaseConnectionString = "DATA SOURCE=.; INTEGRATED SECURITY=SSPI; INITIAL CATALOG=RetailSln;";
            Class1 class1 = new Class1();
            class1.DivineBijaResizeBooksImages(databaseConnectionString, @"C:\Common\Images\DivineBija_20230922\Books\", @"C:\Common\Images\DivineBija_20230922\Books\Items\");
        }
        private static void TestDivineBijaResizeProductsImages()
        {
            string databaseConnectionString = "DATA SOURCE=.; INTEGRATED SECURITY=SSPI; INITIAL CATALOG=RetailSln;";
            Class1 class1 = new Class1();
            class1.DivineBijaResizeProductsImages(databaseConnectionString, @"C:\Common\Images\DivineBija_20230922\Products\", @"C:\Common\Images\DivineBija_20230922\Products\Items\");
        }
    }
}
