using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class RsoMember
    {
        public int MemberId { get; set; }
        public int RsoId { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public byte Status { get; set; }

        public virtual Rso Rso { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
