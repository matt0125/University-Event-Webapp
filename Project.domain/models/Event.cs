using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class Event
    {
        public Event()
        {
            EventPictures = new HashSet<EventPicture>();
            Reactions = new HashSet<Reaction>();
        }

        public int EId { get; set; }
        public int LocationId { get; set; }
        public int? RsoId { get; set; }
        public int UniId { get; set; }
        public string Name { get; set; } = null!;
        public int CId { get; set; }
        public string Visibility { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public byte Status { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual Catagory CIdNavigation { get; set; } = null!;
        public virtual Location Location { get; set; } = null!;
        public virtual Rso? Rso { get; set; }
        public virtual University Uni { get; set; } = null!;
        public virtual ICollection<EventPicture> EventPictures { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }
    }
}
