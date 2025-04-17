using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class UserListModel
    {
        public List<PersonExtn1Model> PersonExtn1Models { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
