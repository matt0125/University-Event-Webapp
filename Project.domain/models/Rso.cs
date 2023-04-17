using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class Rso
    {
        public Rso()
        {
            Events = new HashSet<Event>();
            RsoMembers = new HashSet<RsoMember>();
        }

        public int RsoId { get; set; }
        public string Name { get; set; } = null!;
        public int UniId { get; set; }
        public byte Status { get; set; }
        public int CreatedBy { get; set; }
        public string Description { get; set; } = null!;

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual University Uni { get; set; } = null!;
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<RsoMember> RsoMembers { get; set; }
    }
}
