using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TPsiteRoom
    {
        public TPsiteRoom()
        {
            TCorderDetails = new HashSet<TCorderDetail>();
            TEvaluations = new HashSet<TEvaluation>();
        }

        public int RoomId { get; set; }
        public int? SiteId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? HourPrice { get; set; }
        public decimal? DatePrice { get; set; }
        public int? Ping { get; set; }
        public string? Image { get; set; }
        public bool? Status { get; set; }
        public string? Description { get; set; }

        public virtual TCategory? Category { get; set; }
        public virtual TPsite? Site { get; set; }
        public virtual ICollection<TCorderDetail> TCorderDetails { get; set; }
        public virtual ICollection<TEvaluation> TEvaluations { get; set; }
    }
}
