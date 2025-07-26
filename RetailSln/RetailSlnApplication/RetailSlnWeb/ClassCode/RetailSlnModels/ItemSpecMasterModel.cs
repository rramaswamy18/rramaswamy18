using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemSpecMasterModel
    {
        public long? ItemSpecMasterId { set; get; }

        public long ClientId { set; get; }

        public bool BookFlag { set; get; }

        public long? CodeTypeId { set; get; }

        public string SpecDesc { set; get; }

        public string SpecName { set; get; }

        public string SpecUnitType { set; get; }

        public string SpecValueType { set; get; }

        public string FormatString { set; get; }

        public bool IsMandatory { set; get; }

        public bool ItemMasterFlag { set; get; }

        public bool ProductFlag { set; get; }

        public float SeqNum { set; get; }

        public CodeTypeModel CodeTypeModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
