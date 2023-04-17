using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class Rsoevent
    {
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string Expr2 { get; set; } = null!;
        public int Expr3 { get; set; }
        public int EId { get; set; }
        public int LocationId { get; set; }
        public int? RsoId { get; set; }
        public string Name { get; set; } = null!;
        public float Category { get; set; }
        public int Visibility { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte Status { get; set; }
    }
}
