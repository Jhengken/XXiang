using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TProduct
    {
        public TProduct()
        {
            TCorders = new HashSet<TCorder>();
            TPsites = new HashSet<TPsite>();
        }

        public int ProductId { get; set; }
        public int? SupplierId { get; set; }
        public string Name { get; set; } = null!;

        public virtual TSupplier? Supplier { get; set; }
        public virtual ICollection<TCorder> TCorders { get; set; }
        public virtual ICollection<TPsite> TPsites { get; set; }
    }
}
