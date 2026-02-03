using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class UserProfListModel
    {
        public List<AspNetUserModel> AspNetUserModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
