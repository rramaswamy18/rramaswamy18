using ArchitectureLibraryCacheData;
using ArchitectureLibraryImageLibrary;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryBusinessLayer
{
    public partial class ArchLibBL
    {
        public void CreateInitialSignatureImageFiles(long personId, string initialTextValue, long initialTextId, string signatureTextValue, long signatureTextId, string fontsDirectoryName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string documentImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName") + @"\";
            CodeDataModel codeDataModel;
            ImageDataModel imageDataModel;
            codeDataModel = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 35 && x.CodeDataNameId == initialTextId);
            imageDataModel = new ImageDataModel()
            {
                BlackColorBlue = 255,
                BlackColorGreen = 255,
                BlackColorRed = 255,
                FontFullFileName = fontsDirectoryName + codeDataModel.CodeDataDesc2,
                FontSize = 18,
                FontStyle = System.Drawing.FontStyle.Regular,
                ImageFormat = ImageFormat.Png,
                ImageHeight = 63,
                ImageOutputFullFileName = documentImagesDirectoryName + "Initial_" + personId + ".png",
                ImageWidth = 63,
                TextColorBlue = 0,
                TextColorGreen = 0,
                TextColorRed = 0,
                TextValue = initialTextValue,
            };
            ImageService imageService = new ImageService();
            imageService.CreateImageFileFromText(imageDataModel);
            codeDataModel = LookupCache.CodeDataModels.First(x => x.CodeTypeId == 35 && x.CodeDataNameId == initialTextId);
            imageDataModel = new ImageDataModel()
            {
                BlackColorBlue = 255,
                BlackColorGreen = 255,
                BlackColorRed = 255,
                FontFullFileName = fontsDirectoryName + codeDataModel.CodeDataDesc2,
                FontSize = 18,
                FontStyle = System.Drawing.FontStyle.Regular,
                ImageFormat = ImageFormat.Png,
                ImageHeight = 63,
                ImageOutputFullFileName = documentImagesDirectoryName + "Signature_" + personId + ".png",
                ImageWidth = 450,
                TextColorBlue = 0,
                TextColorGreen = 0,
                TextColorRed = 0,
                TextValue = signatureTextValue,
            };
            imageService.CreateImageFileFromText(imageDataModel);
            //imageDataModel = new ImageDataModel()
            //{
            //    BlackColorBlue = 255,
            //    BlackColorGreen = 255,
            //    BlackColorRed = 255,
            //    FontFullFileName = fontsDirectoryName + "KUNSTLER.TTF",
            //    FontSize = 18,
            //    FontStyle = System.Drawing.FontStyle.Regular,
            //    ImageFormat = ImageFormat.Png,
            //    ImageHeight = 81,
            //    ImageOutputFullFileName = documentImagesDirectoryName + "BusRep.png",
            //    ImageWidth = 630,
            //    TextColorBlue = 0,
            //    TextColorGreen = 0,
            //    TextColorRed = 0,
            //    TextValue = "School Bus Resp",
            //};
            //imageService.CreateImageFileFromText(imageDataModel);
        }
    }
}
