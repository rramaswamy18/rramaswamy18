using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RetailSlnConsoleApp1
{
    public class Class1
    {
        public void DivineBijaCreateImagesForItems(string outputDirectoryName)
        {
            string outputFullFileName;
            Font font;
            System.Drawing.Image img1 = null;
            Color textColor = Color.FromArgb(0, 0, 0);
            Color backColor = Color.FromArgb(255, 255, 255);
            font = new Font("Arial", (float)32);

            for (int i = 1; i < 213; i++)
            {
                outputFullFileName = outputDirectoryName + "Item" + i + ".png";
                img1 = DrawText1("Item " + i, font, textColor, backColor, 180, 180);
                img1.Save(outputFullFileName, ImageFormat.Png);
            }
            img1.Dispose();
        }
        public void DivineBijaCreateImagesForItemImages(string outputDirectoryName)
        {
            Font font;
            System.Drawing.Image img1 = null;
            Color textColor = Color.FromArgb(0, 0, 0);
            Color backColor = Color.FromArgb(255, 255, 255);
            font = new Font("Arial", (float)45);

            string inputFullFileName = outputDirectoryName + "Category.png";
            img1 = DrawText1("", font, textColor, backColor, 252, 252);
            img1.Save(inputFullFileName, ImageFormat.Png);

            string outputFullFileName = outputDirectoryName + "Category4.png";
            List<string> messages = new List<string>
            {
                "Item 1 Front",
                "450 x 450",
            };
            DrawTextMultipleLines(inputFullFileName, outputFullFileName, messages, 36f, 18f, 45f, 45);
        }
        public void ResizeImage(string inputFullFileName, string outputFullFileName, int resizedWidth, int resizedHeight)
        {
            var fileData = File.ReadAllBytes(inputFullFileName);
            using (var memoryStream = new MemoryStream(fileData))
            {
                var originalImage = System.Drawing.Image.FromStream(memoryStream);
                var resizedImage = new Bitmap(originalImage, resizedWidth, resizedHeight);
                ImageConverter imageConverter = new ImageConverter();
                var contentByteData = (byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
                using (FileStream fileStream = File.Create(outputFullFileName))
                {
                    fileStream.Write(contentByteData, 0, contentByteData.Length);
                    fileStream.Close();
                }
                memoryStream.Close();
            }
        }
        public System.Drawing.Image DrawText1(string text, Font font, Color textColor, Color backColor, int width, int height)
        {
            //first, create a dummy bitmap just to get a graphics object
            System.Drawing.Image img = new Bitmap(1, 1);
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
        public void DrawTextMultipleLines(string inputFullFileName, string outputFullFileName, List<string> messages, float fontSize, float x, float y, float yincrement)
        {
            Bitmap bitmap;
            Graphics graphics;

            bitmap = new Bitmap(inputFullFileName);
            graphics = Graphics.FromImage(bitmap);

            foreach (var message in messages)
            {
                graphics.DrawString(message, new Font("Arial", fontSize, FontStyle.Bold), Brushes.Black, new PointF(x, y));
                y += yincrement;
            }

            bitmap.Save(outputFullFileName);
            /*
            Bitmap bitmap;
            Graphics graphics;

            bitmap = new Bitmap(inputFullFileName);
            graphics = Graphics.FromImage(bitmap);

            float y = 9;
            foreach (var message in messages)
            {
                graphics.DrawString(message, new Font("Arial", fontSize, FontStyle.Regular), Brushes.Black, new PointF(x, y));
                y += yincrement;
            }

            bitmap.Save(outputFullFileName);
            */
        }
    }
}
