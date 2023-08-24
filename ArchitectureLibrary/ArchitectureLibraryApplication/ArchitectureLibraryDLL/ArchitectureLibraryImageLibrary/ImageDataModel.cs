using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryImageLibrary
{
    public class ImageDataModel
    {
        public int TextColorRed { set; get; }
        public int TextColorGreen { set; get; }
        public int TextColorBlue { set; get; }
        public int BlackColorRed { set; get; }
        public int BlackColorGreen { set; get; }
        public int BlackColorBlue { set; get; }
        public string FontName { set; get; }
        public string FontFullFileName { set; get; }
        public float FontSize { set; get; }
        public FontStyle FontStyle { set; get; }
        public string TextValue { set; get; }
        public int ImageWidth { set; get; }
        public int ImageHeight { set; get; }
        public string ImageOutputFullFileName { set; get; }
        public ImageFormat ImageFormat { set; get; }
    }
}
