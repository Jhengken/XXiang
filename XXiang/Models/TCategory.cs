using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TCategory
    {
        public TCategory()
        {
            TPsiteRooms = new HashSet<TPsiteRoom>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<TPsiteRoom> TPsiteRooms { get; set; }
    }
}
