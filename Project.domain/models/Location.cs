using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class Location
    {
        public Location()
        {
            Events = new HashSet<Event>();
            Universities = new HashSet<University>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<University> Universities { get; set; }
    }
}
