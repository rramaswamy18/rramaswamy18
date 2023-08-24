using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryImageLibrary
{
    public class ImageService
    {
        public void CreateImageFileFromText(ImageDataModel imageDataModel)
        {
            Color textColor = Color.FromArgb(imageDataModel.TextColorRed, imageDataModel.TextColorGreen, imageDataModel.TextColorBlue);
            Color backColor = Color.FromArgb(imageDataModel.BlackColorRed, imageDataModel.BlackColorGreen, imageDataModel.BlackColorBlue);
            var myFonts = new System.Drawing.Text.PrivateFontCollection();
            myFonts.AddFontFile(imageDataModel.FontFullFileName);
            Font font = new Font(myFonts.Families[0], imageDataModel.FontSize,  imageDataModel.FontStyle);
            Image img1 = DrawText1(imageDataModel.TextValue, font, textColor, backColor, imageDataModel.ImageWidth, imageDataModel.ImageHeight);
            img1.Save(imageDataModel.ImageOutputFullFileName, imageDataModel.ImageFormat);
            img1.Dispose();
        }
        public Image DrawText1(string text, Font font, Color textColor, Color backColor, int width, int height)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image image = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(image);

            //measure the string to see how big the image needs to be
            SizeF sizeF = graphics.MeasureString(text, font);

            //free up the dummy image and old graphics object
            image.Dispose();
            graphics.Dispose();

            //create a new image of the right size
            image = new Bitmap(width, height);

            graphics = Graphics.FromImage(image);

            //paint the background
            graphics.Clear(backColor);

            //create a brush for the text
            Brush brush = new SolidBrush(textColor);

            StringFormat stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            graphics.DrawString(text, font, brush, (width - sizeF.Width) / 2, (height - sizeF.Height) / 2);

            graphics.Save();

            brush.Dispose();
            graphics.Dispose();

            return image;
        }
    }
}
