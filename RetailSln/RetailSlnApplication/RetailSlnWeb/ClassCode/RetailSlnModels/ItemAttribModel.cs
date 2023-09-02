using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemAttribModel : AuditInfoModel
    {
        public long? ItemAttribId { set; get; }

        public long ItemAttribMasterId { set; get; }

        [Required(ErrorMessage = "Select attribute unit")]
        public string ItemAttribUnitValue { set; get; }

        [Required(ErrorMessage = "Enter attribute value")]
        public string ItemAttribValue { set; get; }

        public long ItemId { set; get; }

        public float SeqNum { set; get; }

        public ItemAttribMasterModel ItemAttribMasterModel { set; get; }

        public ItemModel ItemModel { set; get; }
    }
}
