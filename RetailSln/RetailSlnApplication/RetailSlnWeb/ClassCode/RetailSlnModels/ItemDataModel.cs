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
    public class ItemDataModel
    {
        public ItemModel ItemModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
