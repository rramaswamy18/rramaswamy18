using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryTools
{
    public class ImagesLibrary
    {
        public Image DrawText1(string text, Font font, Color textColor, Color backColor, int width, int height)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            //img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            img = new Bitmap(width, height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            //drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
            //drawing.DrawString(text, font, textBrush, (width - textSize.Width) / 2, 0);
            drawing.DrawString(text, font, textBrush, (width - textSize.Width) / 2, (height - textSize.Height) / 2);
            //drawing.DrawString(text, font, textBrush, (1341 - textSize.Width) / 2 , (225 - textSize.Height) / 2);
            //drawing.DrawString(text, font, textBrush, 0, 0, drawFormat);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
        public void ResizeImage(string fullFileNameInput, string fullFileNameOutput, int width, int height)
        {
            byte[] contentByteData = File.ReadAllBytes(fullFileNameInput);
            using (var memoryStream = new MemoryStream(contentByteData))
            {
                var originalImage = Image.FromStream(memoryStream);
                var resizedImage = new Bitmap(originalImage, width, height);
                ImageConverter imageConverter = new ImageConverter();
                contentByteData = (byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
                using (FileStream fileStream = File.Create(fullFileNameOutput))
                {
                    fileStream.Write(contentByteData, 0, contentByteData.Length);
                    fileStream.Close();
                }
            }

        }
    }
}
