﻿using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderCategoryItemListModel
    {
        public long ParentCategoryId { set; get; }

        public int PageNum { set; get; }

        public int RowCount { set; get; }

        public List<ItemMasterModel> ItemMasterModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}