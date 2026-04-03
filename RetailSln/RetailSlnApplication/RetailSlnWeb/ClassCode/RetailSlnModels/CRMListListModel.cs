using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CRMListListModel
    {
        public long PageNum { set; get; }
        public long RowCountFrom { set; get; }
        public long RowCountTo { set; get; }
        public long TotalPageCount { set; get; }
        public long TotalRowCount { set; get; }
        public List<CRMListModel> CRMListModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
