using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemSpecMasterListModel
    {
        public List<ItemSpecMasterModel> ItemSpecMasterModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
