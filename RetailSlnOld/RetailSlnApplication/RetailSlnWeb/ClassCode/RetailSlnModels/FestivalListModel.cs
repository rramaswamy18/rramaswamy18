using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class FestivalListModel : AuditInfoModel
    {
        public long FestivalListItemId { set; get; }
        public long FestivalListId { set; get; }
        public string StartDate { set; get; }
        public string StartTime { set; get; }
        public string FinishDate { set; get; }
        public string FinishTime { set; get; }
        public string EventDate { set; get; }
        public string EventTime { set; get; }
        public string EventDesc { set; get; }
        public float SeqNum { set; get; }
        public long ItemMasterId { set; get; }
        public ItemMasterModel ItemMasterModel { set; get; }
        public List<FestivalListImageModel> FestivalListImageModels { set; get; }
    }
}
