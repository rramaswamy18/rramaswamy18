using ArchitectureLibraryEnumerations;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderHeaderModel
    {
        public long OrderHeaderId { set; get; }

        public DimensionUnitEnum DimensionUnitId { set; get; }

        [Display(Name = "Order Date")]
        public string OrderDate { set; get; }

        //public decimal? OrderNumber { set; get; }

        //public string OrderNum { set; get; }

        [Display(Name = "Order Status")]
        public OrderStatusEnum? OrderStatusId { set; get; }

        public long PersonId { set; get; }
    
        public long VolumeValue { set; get; }

        public long WeightValue { set; get; }

        public WeightUnitEnum WeightUnitId { set; get; }

        public List<OrderDetailModel> OrderDetailModels { set; get; }

        public List<OrderDetailModel> OrderSummaryModels { set; get; }
    }
}
