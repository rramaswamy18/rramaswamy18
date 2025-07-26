using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemMasterDataModel
    {
        public ItemMasterModel ItemMasterModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
