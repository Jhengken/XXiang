using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TEtitle
    {
        public TEtitle()
        {
            TEvaluations = new HashSet<TEvaluation>();
        }

        public int TitleId { get; set; }
        public string TitleName { get; set; } = null!;

        public virtual ICollection<TEvaluation> TEvaluations { get; set; }
    }
}
