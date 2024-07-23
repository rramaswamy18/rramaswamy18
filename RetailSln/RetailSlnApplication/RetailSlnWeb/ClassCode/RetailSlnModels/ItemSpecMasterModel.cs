using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemSpecMasterModel : AuditInfoModel
    {
        public long? ItemSpecMasterId { set; get; }

        public long ClientId { set; get; }

        public long? CodeTypeId { set; get; }

        public string SpecDesc { set; get; }

        public string SpecName { set; get; }

        public string SpecUnitType { set; get; }

        public string SpecValueType { set; get; }

        public string FormatString { set; get; }

        public bool IsMandatory { set; get; }

        public float SeqNum { set; get; }
    }
}
