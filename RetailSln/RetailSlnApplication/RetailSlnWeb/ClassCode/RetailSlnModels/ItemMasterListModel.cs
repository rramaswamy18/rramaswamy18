﻿using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemMasterListModel
    {
        public Dictionary<long, ItemDiscountModel> ItemDiscountModels { set; get; }
        public List<ItemMasterModel> ItemMasterModels { set; get; }
        public PaginationModel PaginationModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
