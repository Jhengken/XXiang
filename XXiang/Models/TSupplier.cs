using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TSupplier
    {
        public TSupplier()
        {
            TAorders = new HashSet<TAorder>();
            TProducts = new HashSet<TProduct>();
        }

        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public int? CreditPoints { get; set; }
        public bool? BlackListed { get; set; }

        public virtual ICollection<TAorder> TAorders { get; set; }
        public virtual ICollection<TProduct> TProducts { get; set; }
    }
}
