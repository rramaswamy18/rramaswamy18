﻿using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemCatalogModel
    {
        public List<ItemMasterModel> ItemMasterModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
