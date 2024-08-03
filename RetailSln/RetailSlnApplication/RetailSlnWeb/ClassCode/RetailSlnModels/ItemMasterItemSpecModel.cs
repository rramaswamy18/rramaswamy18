using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemMasterItemSpecModel
    {
        public long ItemMasterItemSpecId { set; get; }
        public long ClientId { set; get; }
        public long ItemMasterId { set; get; }
        public long ItemSpecId { set; get; }
        public float SeqNumItemMaster { set; get; }
    }
}
