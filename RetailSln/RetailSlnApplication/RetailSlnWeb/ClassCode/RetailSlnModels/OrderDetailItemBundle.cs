using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderDetailItemBundle : AuditInfoModel
    {
        public long? OrderDetailItemBundleId { set; get; }

        public long ClientId { set; get; }

        public long BundledItemId { set; get; }

        public string ItemDesc { set; get; }

        public long ItemId { set; get; }

        public string ItemShortDesc { set; get; }

        public long OrderDetailTypeId { set; get; }

        public long OrderDetailId { set; get; }

        public long OrderQty { set; get; }

        public float SeqNum { set; get; }
    }
}