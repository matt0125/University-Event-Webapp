using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class Catagory
    {
        public Catagory()
        {
            Events = new HashSet<Event>();
        }

        public int CId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Entered { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
