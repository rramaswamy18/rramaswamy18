using iText.Forms;
using iText.Forms.Fields;
using iText.Html2pdf;
//using iText.IO.Font;
//using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Font;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryPDFLibrary
{
    public partial class PDFUtility
    {
        public void PopulatePDFData(string inputPDFFullFileName, string outputPDFFullFileName, List<PDFDataModel> pDFDataModels)
        {
            PdfDocument pdfDocument = null;
            try
            {
                PdfReader pdfReader = new PdfReader(inputPDFFullFileName);
                pdfReader.SetUnethicalReading(true);
                pdfDocument = new PdfDocument(pdfReader, new PdfWriter(outputPDFFullFileName));
                PdfAcroForm pdfAcroForm = PdfAcroForm.GetAcroForm(pdfDocument, true);
                IDictionary<String, PdfFormField> pdfFormFields = pdfAcroForm.GetAllFormFields();
                PdfFormField toSet;
                Document document = new Document(pdfDocument);
                Paragraph paragraph;
                foreach (var pDFDataModel in pDFDataModels)
                {
                    try
                    {
                        if (pDFDataModel.PageNumber == null)
                        {
                            pdfFormFields.TryGetValue(pDFDataModel.FormFieldName, out toSet);
                            if (pDFDataModel.FontSize != null)
                            {
                                toSet.SetFontSize((float)pDFDataModel.FontSize);
                            }
                            toSet.SetValue(pDFDataModel.FormFieldValue);
                        }
                        else
                        {
                            paragraph = new Paragraph(pDFDataModel.FormFieldValue);
                            //paragraph.SetFixedPosition(1, 405, 230, 198);
                            paragraph.SetFixedPosition((int)pDFDataModel.PageNumber, (float)pDFDataModel.LeftCoord, (float)pDFDataModel.BottomCoord, (float)pDFDataModel.Width);
                            if (pDFDataModel.FontSize == null)
                            {
                                pDFDataModel.FontSize = 9;
                            }
                            paragraph.SetFontSize((float)pDFDataModel.FontSize);
                            paragraph.SetBold();
                            //paragraph.SetFont(calibri);
                            document.Add(paragraph);
                        }
                    }
                    catch
                    {
                        ;
                    }
                }
                pdfDocument.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                try
                {
                    pdfDocument.Close();
                }
                catch
                {

                }
                try
                {
                    pdfDocument = null;
                }
                catch
                {

                }
            }
        }
        public void GeneratePDFFromHtmlString(string inputHtmlString, string outputPDFFullFileName)
        {
            try
            {
                PdfWriter pdfWriter = new PdfWriter(outputPDFFullFileName);
                ConverterProperties converterProperties = new ConverterProperties();
                FontProvider fontProvider = new FontProvider();
                fontProvider.AddSystemFonts();
                converterProperties.SetFontProvider(fontProvider);

                PdfDocument pdfDocument = new PdfDocument(pdfWriter);

                //For setting the PAGE SIZE
                pdfDocument.SetDefaultPageSize(new PageSize(PageSize.LETTER));

                Document document = HtmlConverter.ConvertToDocument(inputHtmlString, pdfDocument, converterProperties);
                document.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                //e.printStackTrace();
            }
        }
        //public void PopulatePDFData(string inputPDFFullFileName, string outputPDFFullFileName, Dictionary<string, string> formFieldNamesValues)
        //{
        //    PdfDocument pdfDoc = null;
        //    try
        //    {
        //        PdfReader reader = new PdfReader(inputPDFFullFileName);
        //        reader.SetUnethicalReading(true);
        //        pdfDoc = new PdfDocument(reader, new PdfWriter(outputPDFFullFileName));
        //        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
        //        IDictionary<String, PdfFormField> fields = form.GetFormFields();
        //        PdfFormField toSet;
        //        foreach (var formFieldNamesValue in formFieldNamesValues)
        //        {
        //            try
        //            {
        //                fields.TryGetValue(formFieldNamesValue.Key, out toSet);
        //                toSet.SetValue(formFieldNamesValue.Value);
        //            }
        //            catch
        //            {
        //                ;
        //            }
        //        }
        //        pdfDoc.Close();
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception.Message);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            pdfDoc.Close();
        //        }
        //        catch
        //        {

        //        }
        //        try
        //        {
        //            pdfDoc = null;
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        //public void GeneratePDFFromHtml(String inputHtmlFullFileName, string outputPDFFullFileName)
        //{
        //    try
        //    {
        //        PdfWriter pdfWriter = new PdfWriter(outputPDFFullFileName);
        //        ConverterProperties converterProperties = new ConverterProperties();
        //        PdfDocument pdfDocument = new PdfDocument(pdfWriter);

        //        //For setting the PAGE SIZE
        //        pdfDocument.SetDefaultPageSize(new PageSize(PageSize.LETTER));

        //        StreamReader streamReader = new StreamReader(inputHtmlFullFileName);
        //        string inputHtmlString = streamReader.ReadToEnd();
        //        streamReader.Close();

        //        Document document = HtmlConverter.ConvertToDocument(inputHtmlString, pdfDocument, converterProperties);
        //        document.Close();
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception.Message);
        //        //e.printStackTrace();
        //    }
        //}
        //public void PopulatePDFData(string inputPDFFullFileName, string outputPDFFullFileName, List<dynamic> pDFDataModels)
        //{
        //    List<PDFDataModel> pDFDataModelsTemp = new List<PDFDataModel>();
        //}
        //public void PopulatePDFDataSave(string inputPDFFullFileName, string outputPDFFullFileName, List<PDFDataModel> pDFDataModels)
        //{
        //    //, List<PDFDataModel> pDFFonts
        //    PdfDocument pdfDocument = null;
        //    try
        //    {
        //        //Register the fonts to be used for this PDF
        //        //The same font can be used in multiple places
        //        //foreach (var pDFFont in pDFFonts)
        //        //{
        //        //    FontProgramFactory.RegisterFont(pDFFont.FontFamily, pDFFont.FontFamily);
        //        //}
        //        PdfReader pdfReader = new PdfReader(inputPDFFullFileName);
        //        pdfReader.SetUnethicalReading(true);
        //        pdfDocument = new PdfDocument(pdfReader, new PdfWriter(outputPDFFullFileName));
        //        PdfAcroForm pdfAcroForm = PdfAcroForm.GetAcroForm(pdfDocument, true);
        //        IDictionary<String, PdfFormField> pdfFormFields = pdfAcroForm.GetFormFields();
        //        PdfFormField toSet;
        //        Document document = new Document(pdfDocument);
        //        Paragraph paragraph;
        //        foreach (var pDFDataModel in pDFDataModels)
        //        {
        //            try
        //            {
        //                if (pDFDataModel.PageNumber == null)
        //                {
        //                    pdfFormFields.TryGetValue(pDFDataModel.FormFieldName, out toSet);
        //                    toSet.SetValue(pDFDataModel.FormFieldValue);
        //                }
        //                else
        //                {
        //                    //FontProgramFactory.RegisterFont(pDFDataModel.FontFamily, pDFDataModel.FontFamily);
        //                    //PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLDITALIC);
        //                    //paragraph.SetFixedPosition(1, 405, 230, 198);

        //                    var pdfFont = PdfFontFactory.CreateRegisteredFont(pDFDataModel.FontFamily);
        //                    paragraph = new Paragraph(pDFDataModel.FormFieldValue);
        //                    paragraph.SetFixedPosition((int)pDFDataModel.PageNumber, (float)pDFDataModel.LeftCoord, (float)pDFDataModel.BottomCoord, (float)pDFDataModel.Width);
        //                    paragraph.SetFontSize((float)pDFDataModel.FontSize);
        //                    paragraph.SetBold();
        //                    paragraph.SetFont(pdfFont);
        //                    document.Add(paragraph);
        //                }
        //            }
        //            catch
        //            {
        //                ;
        //            }
        //        }
        //        pdfDocument.Close();
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception.Message);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            pdfDocument.Close();
        //        }
        //        catch
        //        {

        //        }
        //        try
        //        {
        //            pdfDocument = null;
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
    }
}
