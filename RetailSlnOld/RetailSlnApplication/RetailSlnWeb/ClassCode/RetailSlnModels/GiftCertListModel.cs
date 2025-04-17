using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class GiftCertListModel
    {
        public List<GiftCertModel> GiftCertModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}