using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TEvaluation
    {
        public int EvaluationId { get; set; }
        public int? CustomerId { get; set; }
        public int? RoomId { get; set; }
        public int? TitleId { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Response { get; set; }
        public int? Star { get; set; }

        public virtual TCustomer? Customer { get; set; }
        public virtual TPsiteRoom? Room { get; set; }
        public virtual TEtitle? Title { get; set; }
    }
}
