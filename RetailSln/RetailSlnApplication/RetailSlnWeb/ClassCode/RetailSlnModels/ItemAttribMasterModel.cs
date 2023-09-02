using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemAttribMasterModel : AuditInfoModel
    {
        public long? ItemAttribMasterId { set; get; }

        public string AttribDesc { set; get; }

        public string AttribName { set; get; }

        public string AttribUnitCategory { set; get; }

        public string AttribUnitType { set; get; }

        public string AttribValueType { set; get; }

        public bool IsMandatory { set; get; }

        public float SeqNum { set; get; }
    }
}
