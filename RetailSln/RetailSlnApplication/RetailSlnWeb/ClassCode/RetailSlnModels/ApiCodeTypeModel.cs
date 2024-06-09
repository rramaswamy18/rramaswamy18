using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiCodeTypeModel
    {
        public long CodeTypeId { get; set; }

        public CodeTypeEnum CodeTypeNameId { get; set; }

        public string CodeTypeNameDesc { get; set; }

        public string CodeTypeDesc { get; set; }
    }
}
