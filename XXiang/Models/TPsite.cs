using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TPsite
    {
        public TPsite()
        {
            TPsiteRooms = new HashSet<TPsiteRoom>();
        }

        public int SiteId { get; set; }
        public int? ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public string? OpenTime { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }

        public virtual TProduct? Product { get; set; }
        public virtual ICollection<TPsiteRoom> TPsiteRooms { get; set; }
    }
}
