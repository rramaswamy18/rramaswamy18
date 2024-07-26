using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class FestivalListImageModel : AuditInfoModel
    {
        public long FestivalListImageId { set; get; }
        public long FestivalListId { set; get; }
        public float SeqNum { set; get; }
        public string ImageExtension { set; get; }
        public string ImageName { set; get; }
        public string ImageNotes { set; get; }
        public string UploadImageFileName { set; get; }
        public FestivalListModel FestivalListItemModel { set; get; }
    }
}
