using System;
using System.Collections.Generic;

namespace XXiang.Models
{
    public partial class TManager
    {
        public int ManagerId { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
    }
}
