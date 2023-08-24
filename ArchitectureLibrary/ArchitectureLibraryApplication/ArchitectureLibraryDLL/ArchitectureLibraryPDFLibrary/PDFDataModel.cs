using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryPDFLibrary
{
    public class PDFDataModel
    {
        public string FormFieldName { set; get; }
        public string FormFieldValue { set; get; }
        public int? PageNumber { set; get; }
        public float? LeftCoord { set; get; }
        public float? BottomCoord { set; get; }
        public float? Width { set; get; }
        public string FontFileName { set; get; }
        public string FontFamily { set; get; }
        public float? FontSize { set; get; }
        public string FontWeight { set; get; }
    }
}
