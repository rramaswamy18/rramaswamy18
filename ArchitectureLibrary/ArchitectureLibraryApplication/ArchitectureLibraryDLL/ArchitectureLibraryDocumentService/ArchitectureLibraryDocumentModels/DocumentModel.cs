using ArchitectureLibraryDocumentEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDocumentModels
{
    public class DocumentModel : AuditInfoModel
    {
        public long? DocumentId { get; set; }
        public long ClientId { get; set; }
        public long? ClientContentLength { get; set; }
        public string ClientFileName { set; get; }
        public int? ClientHeight { set; get; }
        public string ClientHeightUnit { set; get; }
        public int? ClientWidth { set; get; }
        public string ClientWidthUnit { set; get; }
        public byte[] ContentByteData { get; set; }
        public string ContentData { get; set; }
        public long? ContentLength { get; set; }
        public string ContentType { set; get; }
        public string DocumentCategoryName { get; set; }
        public string DocumentDesc { get; set; }
        public StatusEnum? DocumentStatusId { get; set; }
        public DocumentTypeEnum? DocumentTypeId { get; set; }
        public string DocumentTypeDesc { get; set; }
        public string FileExtension { get; set; }
        public int? Height { set; get; }
        public string HeightUnit { set; get; }
        public string ServerFileName { get; set; }
        public int? Width { set; get; }
        public string WidthUnit { set; get; }
    }
}
