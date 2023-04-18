using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class Rsoevent
    {
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string RsoName { get; set; } = null!;
        public int EId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; } = null!;
        public int CId { get; set; }
        public string Visibility { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte Status { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RsoId { get; set; }
    }
}
