using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSlnModels
{
    public class CategoryItemModel
    {
        public long? CategoryItemId { set; get; }
        public long? ClientId { set; get; }
        public long? CategoryId { set; get; }
        public long? ItemId { set; get; }
        public float? SeqNum { set; get; }
        public CategoryModel CategoryModel { set; get; }
        public ItemModel ItemModel { set; get; }
    }
}
