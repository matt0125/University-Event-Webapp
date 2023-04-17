using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class EventPicture
    {
        public int PId { get; set; }
        public int EId { get; set; }
        public byte[] Picture { get; set; } = null!;

        public virtual Event EIdNavigation { get; set; } = null!;
    }
}
