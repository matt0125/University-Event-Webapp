using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class Reaction
    {
        public int ReactionId { get; set; }
        public int EId { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; }
        public float? Rating { get; set; }
        public bool Save { get; set; }
        public bool Rsvp { get; set; }

        public virtual Event EIdNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
