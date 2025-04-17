using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CategoryItemHierModel
    {
        public long? CategoryItemHierId { set; get; }
        public long ClientId { set; get; }
        public long CategoryItemMasterHierId { set; get; }
        public long ItemId { set; get; }
        public float SeqNum { set; get; }
        public ItemModel ItemModel { set; get; }
        public CategoryItemMasterHierModel CategoryItemMasterHierModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
