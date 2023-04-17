using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class University
    {
        public University()
        {
            Events = new HashSet<Event>();
            Rsos = new HashSet<Rso>();
            UniPictures = new HashSet<UniPicture>();
            Users = new HashSet<User>();
        }

        public int UniId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int NumStudents { get; set; }
        public int CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual Location Location { get; set; } = null!;
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Rso> Rsos { get; set; }
        public virtual ICollection<UniPicture> UniPictures { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
