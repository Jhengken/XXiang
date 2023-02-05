using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TAorder
    {
        public int AorderId { get; set; }
        public int? SupplierId { get; set; }
        public int? AdvertiseId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Clicks { get; set; }
        public decimal? Price { get; set; }

        public virtual TAdvertise? Advertise { get; set; }
        public virtual TSupplier? Supplier { get; set; }
    }
}
