using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TCustomer
    {
        public TCustomer()
        {
            TCorders = new HashSet<TCorder>();
            TEvaluations = new HashSet<TEvaluation>();
        }

        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public bool? Sex { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public DateTime? Birth { get; set; }
        public string? CreditCard { get; set; }
        public int? CreditPoints { get; set; }
        public bool? BlackListed { get; set; }

        public virtual ICollection<TCorder> TCorders { get; set; }
        public virtual ICollection<TEvaluation> TEvaluations { get; set; }
    }
}
