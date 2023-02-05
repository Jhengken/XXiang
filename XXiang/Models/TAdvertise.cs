using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TAdvertise
    {
        public TAdvertise()
        {
            TAorders = new HashSet<TAorder>();
        }

        public int AdvertiseId { get; set; }
        public string Name { get; set; } = null!;
        public decimal? DatePrice { get; set; }

        public virtual ICollection<TAorder> TAorders { get; set; }
    }
}
