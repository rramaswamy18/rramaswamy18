using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class SearchDataModel
    {
        public string SearchType { set; get; }
        public Dictionary<string, string> SearchKeyValuePairs { set; get; }
    }
}
