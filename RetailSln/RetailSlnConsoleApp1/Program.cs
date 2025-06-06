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
            TestDivineBijaResizeItemMasterImages();
            //TestDivineBijaResizeProductsImages();
            Console.WriteLine("Process Completed");
            Console.ReadKey();
        }
        private static void TestResizeImage()
        {
            Class1 class1 = new Class1();
            string inputFullFileName, outputFullFileName;

            inputFullFileName = @"C:\Common\Images\DivineBija\DivineBija_20230927\UploadedImages\DB100474-2.jpg";
            outputFullFileName = @"C:\Common\Images\DivineBija\DivineBija_20230927\Items\Item166.png";
            class1.ResizeImage(inputFullFileName, outputFullFileName, 180, 189);

            inputFullFileName = @"C:\Common\Images\DivineBija\DivineBija_20230927\UploadedImages\DB100631-2.jpg";
            outputFullFileName = @"C:\Common\Images\DivineBija\DivineBija_20230927\Items\Item1.png";
            class1.ResizeImage(inputFullFileName, outputFullFileName, 180, 189);

            inputFullFileName = @"C:\Common\Images\DivineBija\DivineBija_20230927\UploadedImages\DB100920-2.jpg";
            outputFullFileName = @"C:\Common\Images\DivineBija\DivineBija_20230927\Items\Item2.png";
            class1.ResizeImage(inputFullFileName, outputFullFileName, 180, 189);
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
        private static void TestDivineBijaResizeItemMasterImages()
        {
            string inputDirectoryName = @"C:\Common\Images\DivineBija\DivineBija_20230927\UploadedImages\";
            string outputDirectoryName = @"C:\Common\Images\DivineBija\DivineBija_20230927\UploadedImages\ItemMaster\";
            string databaseConnectionString = "DATA SOURCE=.; INTEGRATED SECURITY=SSPI; INITIAL CATALOG=RetailSln;";
            Class1 class1 = new Class1();
            class1.DivineBijaResizeItemMasterImages(databaseConnectionString, inputDirectoryName, outputDirectoryName);
        }
        //private static void TestDivineBijaResizeProductsImages()
        //{
        //    string databaseConnectionString = "DATA SOURCE=.; INTEGRATED SECURITY=SSPI; INITIAL CATALOG=RetailSln;";
        //    Class1 class1 = new Class1();
        //    class1.DivineBijaResizeProductsImages(databaseConnectionString, @"C:\Common\Images\DivineBija_20230922\Products\", @"C:\Common\Images\DivineBija_20230922\Products\Items\");
        //}
    }
}
