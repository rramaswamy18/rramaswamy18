using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiCodeDataModel
    {
        public long CodeDataId { get; set; }

        public long CodeTypeId { get; set; }

        public CodeTypeEnum CodeTypeNameId { get; set; }

        public long CodeDataNameId { get; set; }

        public string CodeDataNameDesc { get; set; }

        public string CodeDataDesc0 { get; set; }

        public string CodeDataDesc1 { get; set; }

        public string CodeDataDesc2 { get; set; }

        public string CodeDataDesc3 { get; set; }

        public string CodeDataDesc4 { get; set; }

        public ApiCodeTypeModel CodeTypeModel { get; set; }
    }
}
