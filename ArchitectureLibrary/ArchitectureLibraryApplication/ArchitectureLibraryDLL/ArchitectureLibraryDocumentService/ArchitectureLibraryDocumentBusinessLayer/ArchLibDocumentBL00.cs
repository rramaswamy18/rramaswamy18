using ArchitectureLibraryDocumentCacheData;
using ArchitectureLibraryDocumentDataLayer;
using ArchitectureLibraryDocumentEnumerations;
using ArchitectureLibraryDocumentModels;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ArchitectureLibraryDocumentService
{
    public class ArchLibDocumentBL
    {
        public void CreateDocumentFile(string serverFileName, DocumentModel documentModel, string documentDirectoryName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string fullFileName = documentDirectoryName + documentModel.ServerFileName;
            if (!string.IsNullOrWhiteSpace(serverFileName))
            {
                try
                {
                    File.Copy(documentDirectoryName + serverFileName, fullFileName, true);
                }
                catch
                {
                    ;
                }
            }
            else
            {
                try
                {
                    BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(fullFileName));
                    binaryWriter.Write(documentModel.ContentByteData, 0, documentModel.ContentByteData.Length);
                    binaryWriter.Close();
                    binaryWriter.Dispose();
                    binaryWriter = null;
                    //using (FileStream fileStream = File.Create(fullFileName))
                    //{
                    //    fileStream.Write(documentModel.ContentByteData, 0, documentModel.ContentByteData.Length);
                    //    fileStream.Close();
                    //    fileStream.Dispose();
                    //}
                }
                catch
                {

                }
            }
        }
        public byte[] ReadAllBytes(Stream stream, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            if (stream is MemoryStream)
            {
                return ((MemoryStream)stream).ToArray();
            }
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public void ResizeImage(Stream stream, int resizedHeight, int resizedWidth, out byte[] imageResizedBytes, out int documentImageHeight, out int documentImageWidth, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                byte[] imageOriginalBytes = ReadAllBytes(stream, clientId, ipAddress, execUniqueId, loggedInUserId);
                var documentImage = Image.FromStream(stream, true, true);
                documentImageWidth = documentImage.Width;
                documentImageHeight = documentImage.Height;
                using (MemoryStream memoryStreamOriginal = new MemoryStream(imageOriginalBytes, 0, imageOriginalBytes.Length))
                {
                    using (Image imageOriginal = Image.FromStream(memoryStreamOriginal))
                    {
                        using (Bitmap bitmapOriginal = new Bitmap(imageOriginal, new Size(resizedWidth, resizedHeight)))
                        {
                            using (MemoryStream memoryStreamResized = new MemoryStream())
                            {
                                bitmapOriginal.Save(memoryStreamResized, System.Drawing.Imaging.ImageFormat.Jpeg);
                                imageResizedBytes = memoryStreamResized.ToArray();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public void CreateOrUpdateDocument(HttpPostedFileBase httpPostedFileBase, DocumentModel documentModel, string documentDirectoryName, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string fileExtension, serverFileName = documentModel.ServerFileName;
                try
                {
                    fileExtension = httpPostedFileBase.FileName.Substring(httpPostedFileBase.FileName.LastIndexOf('.') + 1);
                }
                catch
                {
                    fileExtension = "";
                }
                Stream stream = httpPostedFileBase.InputStream;
                ResizeImage(stream, documentModel.Height.Value, documentModel.Width.Value, out byte[] imageResizedBytes, out int documentImageHeight, out int documentImageWidth, clientId, ipAddress, execUniqueId, loggedInUserId);
                documentModel.ClientContentLength = httpPostedFileBase.ContentLength;
                documentModel.ClientFileName = httpPostedFileBase.FileName;
                documentModel.ClientHeight = documentImageHeight;
                documentModel.ClientHeightUnit = "px";
                documentModel.ClientWidth = documentImageWidth;
                documentModel.ClientWidthUnit = "px";
                documentModel.ContentLength = imageResizedBytes.Length;
                documentModel.ContentType = httpPostedFileBase.ContentType;
                documentModel.FileExtension = fileExtension;
                documentModel.ContentByteData = null;
                documentModel.ContentData = null;
                if (documentModel.DocumentId == 0)
                {
                    ArchLibDocumentDataContext.AddDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    documentModel.ServerFileName = documentModel.DocumentCategoryName + "_" + documentModel.DocumentId + "." + fileExtension;
                    ArchLibDocumentDataContext.UpdDocument2(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                else
                {
                    documentModel.ServerFileName = documentModel.DocumentCategoryName + "_" + documentModel.DocumentId + "." + fileExtension;
                    ArchLibDocumentDataContext.UpdDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                if (!string.IsNullOrWhiteSpace(serverFileName))
                {
                    try
                    {
                        File.Delete(documentDirectoryName + serverFileName);
                    }
                    catch (Exception exception)
                    {
                        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098000 :: Error while deleting fil " + documentDirectoryName + serverFileName + " ERROR???", exception);
                    }
                }
                documentModel.ContentByteData = imageResizedBytes;
                CreateDocumentFile(null, documentModel, documentDirectoryName, clientId, ipAddress, execUniqueId, loggedInUserId);
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public void CreateResizedImageFile(HttpPostedFileBase httpPostedFileBase, int[] resizedHeight, int[] resizedWidth, string[] serverFullFileName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                int i;
                Stream stream = httpPostedFileBase.InputStream;
                byte[] imageOriginalBytes = ReadAllBytes(stream, clientId, ipAddress, execUniqueId, loggedInUserId);
                using (var memoryStream = new MemoryStream(imageOriginalBytes))
                {
                    var originalImage = Image.FromStream(memoryStream);
                    for (i = 0; i < resizedHeight.Length; i++)
                    {
                        var resizedImage = new Bitmap(originalImage, resizedWidth[i], resizedHeight[i]);
                        ImageConverter imageConverter = new ImageConverter();
                        imageOriginalBytes = (byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
                        using (FileStream fileStream = File.Create(serverFullFileName[i]))
                        {
                            fileStream.Write(imageOriginalBytes, 0, imageOriginalBytes.Length);
                            fileStream.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public void CreateResizedImageFiles(string documentsImagesFullDirectoryName, List<HttpPostedFileBase> httpPostedFileBases, List<int> resizedHeights, List<int> resizedWidths, List<string> serverFileNames, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                int i;
                Stream stream;
                byte[] imageOriginalBytes;
                List<MemoryStream> memoryStreams = new List<MemoryStream>();
                for (i = 0; i < httpPostedFileBases.Count; i++)
                {
                    stream = httpPostedFileBases[i].InputStream;
                    imageOriginalBytes = ReadAllBytes(stream, clientId, ipAddress, execUniqueId, loggedInUserId);
                    memoryStreams.Add(new MemoryStream());
                    memoryStreams[i] = new MemoryStream(imageOriginalBytes);
                    var originalImage = Image.FromStream(memoryStreams[i]);
                    var resizedImage = new Bitmap(originalImage, resizedWidths[i], resizedHeights[i]);
                    ImageConverter imageConverter = new ImageConverter();
                    imageOriginalBytes = (byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
                    using (FileStream fileStream = File.Create(documentsImagesFullDirectoryName + serverFileNames[i]))
                    {
                        fileStream.Write(imageOriginalBytes, 0, imageOriginalBytes.Length);
                        fileStream.Close();
                    }
                    memoryStreams[i].Dispose();
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //public long CreateDocument(int resizedHeight, int resizedWidth, string documentCategoryName, string documentCategoryDesc, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //1. Build Document Model from NoImage Cache
        //    //2. Update some of the properties like server filename, height, width

        //    //1. Get Document row with serverFileName passed
        //    //2. Resize the file to get the correct content data
        //    //3. Add Document
        //    //4. Set Server File Name
        //    //5. Save file to disk
        //    //6. Update Document
        //    string documentImagesDirectoryName = ConfigurationManager.AppSettings["DocumentsImagesDirectoryName"];
        //    DocumentModel documentModel = CreateDocumentModelFromNoImage(clientId, ipAddress, execUniqueId, loggedInUserId);
        //    documentModel.ClientId = clientId;
        //    documentModel.ContentByteData = File.ReadAllBytes(documentImagesDirectoryName + documentModel.ServerFileName);
        //    documentModel.DocumentCategoryName = documentCategoryName;
        //    documentModel.DocumentDesc = documentCategoryDesc;
        //    documentModel.DocumentStatusId = StatusEnum.Active;
        //    using (var memoryStream = new MemoryStream(documentModel.ContentByteData))
        //    {
        //        var originalImage = Image.FromStream(memoryStream);
        //        var resizedImage = new Bitmap(originalImage, resizedWidth, resizedHeight);
        //        ImageConverter imageConverter = new ImageConverter();
        //        documentModel.ContentByteData = (byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
        //        documentModel.ContentLength = documentModel.ContentByteData.Length;
        //        documentModel.Height = resizedImage.Height;
        //        documentModel.HeightUnit = ArchLibDocumentCache.DocumentModelNoImage.HeightUnit;
        //        documentModel.Width = resizedImage.Width;
        //        documentModel.WidthUnit = ArchLibDocumentCache.DocumentModelNoImage.WidthUnit;
        //    }
        //    documentModel.ContentData = "data:" + documentModel.ContentType + "," + Convert.ToBase64String(documentModel.ContentByteData);
        //    documentModel.FileExtension = ArchLibDocumentCache.DocumentModelNoImage.FileExtension;
        //    documentModel.ServerFileName = "";
        //    ArchLibDocumentDataContext.CreateDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    documentModel.ServerFileName = documentModel.DocumentCategoryName.ToString() + "_" + documentModel.DocumentId + '.' + documentModel.FileExtension;
        //    CreateDocumentFile(null, documentModel, documentImagesDirectoryName, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    ArchLibDocumentDataContext.UpdDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    return (long)documentModel.DocumentId;
        //}
        //public long CreateEmptyDocument(string documentCategoryName, string documentCategoryDesc, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    DocumentModel documentModel = CreateEmptyDocumentModel(clientId, ipAddress, execUniqueId, loggedInUserId);
        //    documentModel.ClientId = clientId;
        //    documentModel.DocumentTypeId = DocumentTypeEnum.Upload;
        //    documentModel.DocumentTypeDesc = "Upload";
        //    documentModel.DocumentCategoryName = documentCategoryName;
        //    documentModel.DocumentDesc = documentCategoryDesc;
        //    documentModel.DocumentStatusId = StatusEnum.Active;
        //    ArchLibDocumentDataContext.CreateDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    return (long)documentModel.DocumentId;
        //}
        //public void CreateDocumentFile(DocumentModel documentModel, string documentDirectoryName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    int indexOf1;
        //    indexOf1 = documentModel.ContentData.IndexOf(",");
        //    documentModel.ContentType = documentModel.ContentData.Substring(0, indexOf1); //"data:image/png;base64"
        //    documentModel.ContentByteData = Convert.FromBase64String(documentModel.ContentData.Substring(indexOf1 + 1));
        //    //byte[] temp = File.ReadAllBytes(documentDirectoryName + "NoImage_0.png");
        //    //string contentData = documentModel.ContentData.Substring(22);
        //    //documentModel.ContentByteData = Encoding.ASCII.GetBytes(contentData);
        //    string fullFileName = documentDirectoryName + documentModel.ServerFileName;
        //    using (var memoryStream = new MemoryStream(documentModel.ContentByteData))
        //    {
        //        var originalImage = Image.FromStream(memoryStream);
        //        var resizedImage = new Bitmap(originalImage, (int)documentModel.Width, (int)documentModel.Height);
        //        ImageConverter imageConverter = new ImageConverter();
        //        documentModel.ContentByteData = (byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
        //        documentModel.ContentLength = documentModel.ContentByteData.Length;
        //        //documentModel.Height = resizedImage.Height;
        //        //documentModel.HeightUnit = ArchLibDocumentCache.DocumentModelNoImage.HeightUnit;
        //        //documentModel.Width = resizedImage.Width;
        //        //documentModel.WidthUnit = ArchLibDocumentCache.DocumentModelNoImage.WidthUnit;
        //    }
        //    using (FileStream fileStream = File.Create(fullFileName))
        //    {
        //        fileStream.Write(documentModel.ContentByteData, 0, documentModel.ContentByteData.Length);
        //    }
        //}
        //public DocumentModel CreateEmptyDocumentModel(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    DocumentModel documentModel = new DocumentModel
        //    {
        //        ClientId = clientId,
        //        ClientFileName = null,
        //        ClientHeight = null,
        //        ClientHeightUnit = null,
        //        ClientWidth = null,
        //        ClientWidthUnit = null,
        //        ContentByteData = null,
        //        ContentData = null,
        //        ContentLength = null,
        //        ContentType = null,
        //        DocumentCategoryName = null,
        //        DocumentDesc = null,
        //        DocumentId = null,
        //        DocumentStatusId = null,
        //        DocumentTypeId = null,
        //        DocumentTypeDesc = null,
        //        FileExtension = null,
        //        Height = null,
        //        HeightUnit = null,
        //        ServerFileName = null,
        //        Width = null,
        //        WidthUnit = null,
        //};
        //    return documentModel;
        //}
        //public DocumentModel CreateDocumentModelFromNoImage(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    DocumentModel documentModel = new DocumentModel
        //    {
        //        ClientId = ArchLibDocumentCache.DocumentModelNoImage.ClientId,
        //        ClientFileName = ArchLibDocumentCache.DocumentModelNoImage.ClientFileName,
        //        ClientHeight = ArchLibDocumentCache.DocumentModelNoImage.ClientHeight,
        //        ClientHeightUnit = ArchLibDocumentCache.DocumentModelNoImage.ClientHeightUnit,
        //        ClientWidth = ArchLibDocumentCache.DocumentModelNoImage.ClientWidth,
        //        ClientWidthUnit = ArchLibDocumentCache.DocumentModelNoImage.ClientWidthUnit,
        //        ContentType = ArchLibDocumentCache.DocumentModelNoImage.ContentType,
        //        DocumentTypeId = ArchLibDocumentCache.DocumentModelNoImage.DocumentTypeId,
        //        DocumentTypeDesc = ArchLibDocumentCache.DocumentModelNoImage.DocumentTypeDesc,
        //        ServerFileName = ArchLibDocumentCache.DocumentModelNoImage.ServerFileName,
        //    };
        //    return documentModel;
        //}
        //public void UpdateDocument(DocumentModel documentModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ArchLibDocumentDataContext.ModifyDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public void UpdateDocument(Stream stream, int resizedHeight, int resizedWidth, string serverFileName, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        byte[] imageOriginalBytes = ReadAllBytes(stream, clientId, ipAddress, execUniqueId, loggedInUserId), imageResizedBytes;
        //        using (MemoryStream memoryStreamOriginal = new MemoryStream(imageOriginalBytes, 0, imageOriginalBytes.Length))
        //        {
        //            using (Image imageOriginal = Image.FromStream(memoryStreamOriginal))
        //            {
        //                using (Bitmap bitmapOriginal = new Bitmap(imageOriginal, new Size(resizedWidth, resizedHeight)))
        //                {
        //                    using (MemoryStream memoryStreamResized = new MemoryStream())
        //                    {
        //                        bitmapOriginal.Save(memoryStreamResized, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                        imageResizedBytes = memoryStreamResized.ToArray();
        //                    }
        //                }
        //            }
        //        }
        //        var certificateDocumentImage = Image.FromStream(stream, true, true);
        //        int certificateDocumentImageWidth = certificateDocumentImage.Width;
        //        int certificateDocumentImageHeight = certificateDocumentImage.Height;
        //        if (string.IsNullOrWhiteSpace(serverFileName))
        //        {
        //            DocumentModel documentModel = new DocumentModel
        //            {
        //            };
        //            ArchLibDocumentDataContext.CreateDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        }
        //        //ArchLibDocumentDataContext.ModifyDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public void UpdateDocumentOld(Stream stream, int resizedHeight, int resizedWidth, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        byte[] imageOriginalBytes = new byte[stream.Length], imageResizedBytes;
        //        using (MemoryStream memoryStreamOriginal = new MemoryStream(imageOriginalBytes, 0, imageOriginalBytes.Length))
        //        {
        //            using (Image imageOriginal = Image.FromStream(memoryStreamOriginal))
        //            {
        //                using (Bitmap bitmapOriginal = new Bitmap(imageOriginal, new Size(resizedWidth, resizedHeight)))
        //                {
        //                    using (MemoryStream memoryStreamResized = new MemoryStream())
        //                    {
        //                        bitmapOriginal.Save(memoryStreamResized, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                        imageResizedBytes = memoryStreamResized.ToArray();
        //                    }
        //                }
        //            }
        //        }
        //        var certificateDocumentImage = Image.FromStream(stream, true, true);
        //        int certificateDocumentImageWidth = certificateDocumentImage.Width;
        //        int certificateDocumentImageHeight = certificateDocumentImage.Height;
        //        //ArchLibDocumentDataContext.ModifyDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public long CreateDocument(string serverFileName, int resizedHeight, int resizedWidth, string documentImagesDirectoryName, string documentCategoryName, string documentCategoryDesc, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //1. Get Document row with serverFileName passed
        //    //2. Resize the file to get the correct content data
        //    //3. Add Document
        //    //4. Set Server File Name
        //    //5. Save file to disk
        //    //6. Update Document
        //    DocumentModel documentModel = ArchLibDocumentDataContext.GetDocument(serverFileName, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    if (documentModel == null)
        //    {
        //        documentModel = new DocumentModel();
        //    }
        //    //2. Resize the file to get the correct content data
        //    documentModel.ClientId = clientId;
        //    documentModel.ContentByteData = File.ReadAllBytes(documentImagesDirectoryName + serverFileName);
        //    documentModel.DocumentCategoryName = documentCategoryName;
        //    documentModel.DocumentDesc = documentCategoryDesc;
        //    documentModel.DocumentStatusId = StatusEnum.Active;
        //    using (var memoryStream = new MemoryStream(documentModel.ContentByteData))
        //    {
        //        var originalImage = Image.FromStream(memoryStream);
        //        var resizedImage = new Bitmap(originalImage, resizedWidth, resizedHeight);
        //        ImageConverter imageConverter = new ImageConverter();
        //        documentModel.ContentByteData = (byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
        //        documentModel.ContentLength = documentModel.ContentByteData.Length;
        //        documentModel.Height = resizedImage.Height;
        //        documentModel.HeightUnit = "px";
        //        documentModel.Width = resizedImage.Width;
        //        documentModel.WidthUnit = "px";
        //    }
        //    documentModel.ContentData = "data:" + documentModel.ContentType + "," + Convert.ToBase64String(documentModel.ContentByteData);
        //    documentModel.FileExtension = "png";
        //    documentModel.ServerFileName = "";
        //    if (documentModel.DocumentId > 0)
        //    {
        //        ArchLibDocumentDataContext.UpdDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    else
        //    {
        //        //3. Add Document
        //        ArchLibDocumentDataContext.CreateDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        //4. Set Server File Name
        //        documentModel.ServerFileName = documentModel.DocumentCategoryName.ToString() + "_" + documentModel.DocumentId + '.' + documentModel.FileExtension;
        //        //5. Create file on disk
        //        //using (FileStream fileStream = File.Create(documentImagesDirectoryName + documentModel.ServerFileName))
        //        //{
        //        //    fileStream.Write(documentModel.ContentByteData, 0, documentModel.ContentByteData.Length);
        //        //}
        //        //6. Update Document
        //        documentModel.ContentByteData = null; //(byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
        //        ArchLibDocumentDataContext.UpdDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    CreateDocumentFile(serverFileName, documentModel, documentImagesDirectoryName, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    return (long)documentModel.DocumentId;
        //}
        //public void CreateDocumentFile(Stream stream, DocumentModel documentModel, string documentDirectoryName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        //    //SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
        //    //sqlConnection.Open();
        //    //DocumentModel documentModel = GetDocument(serverFileName, sqlConnection, execUniqueId);
        //    //documentModel.ContentByteData = File.ReadAllBytes(documentResumesDirectoryName + serverFileName);
        //    //string fullFileName = documentResumesDirectoryName + serverFileName;
        //    documentModel.ContentByteData = ReadAllBytes(stream, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    string fullFileName = documentDirectoryName + documentModel.ServerFileName;
        //    try
        //    {
        //        File.Delete(fullFileName);
        //    }
        //    catch
        //    {
        //        ;
        //    }
        //    try
        //    {
        //        using (FileStream fileStream = File.Create(fullFileName))
        //        {
        //            fileStream.Write(documentModel.ContentByteData, 0, documentModel.ContentByteData.Length);
        //        }
        //    }
        //    catch
        //    {

        //    }

        //    //return (long)documentModel.DocumentId;
        //}
        //public void UpdateDocument(long documentId, Stream stream, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    byte[] contentByteData = ReadAllBytes(stream, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    DocumentModel documentModel = new DocumentModel
        //    {
        //        DocumentId = documentId,
        //        ContentData = "data:" + "image/png;base64" + "," + Convert.ToBase64String(contentByteData),
        //    };
        //    ArchLibDocumentDataContext.UpdDocument1(sqlConnection, documentModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //}
        //public void CreateDocumentFile(string serverFileName, byte[] contentByteData, string documentDirectoryName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string fullFileName = documentDirectoryName + serverFileName;
        //    if (!string.IsNullOrWhiteSpace(serverFileName))
        //    {
        //        try
        //        {
        //            File.Copy(documentDirectoryName + serverFileName, fullFileName, true);
        //        }
        //        catch
        //        {
        //            ;
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            using (FileStream fileStream = File.Create(fullFileName))
        //            {
        //                fileStream.Write(contentByteData, 0, contentByteData.Length);
        //            }
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
    }
}
