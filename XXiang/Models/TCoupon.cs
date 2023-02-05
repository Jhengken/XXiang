using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TCoupon
    {
        public TCoupon()
        {
            TCorderDetails = new HashSet<TCorderDetail>();
        }

        public int CouponId { get; set; }
        public string? Code { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? Quantity { get; set; }
        public bool? Available { get; set; }

        public virtual ICollection<TCorderDetail> TCorderDetails { get; set; }
    }
}
