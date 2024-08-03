using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemSpecModel : AuditInfoModel
    {
        public long? ItemSpecId { set; get; }

        public long ClientId { set; get; }

        public long ItemSpecMasterId { set; get; }

        [Required(ErrorMessage = "Select attribute unit")]
        public string ItemSpecUnitValue { set; get; }

        [Required(ErrorMessage = "Enter attribute value")]
        public string ItemSpecValue { set; get; }

        public string ItemSpecValueForDisplay { set; get; }

        public long ItemId { set; get; }

        public float SeqNum { set; get; }

        public float? SeqNumItem { set; get; }

        public float? SeqNumItemMaster { set; get; }

        public ItemSpecMasterModel ItemSpecMasterModel { set; get; }

        public ItemModel ItemModel { set; get; }
    }
}
