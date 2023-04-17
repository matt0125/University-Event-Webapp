using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class UniPicture
    {
        public int PId { get; set; }
        public int UniId { get; set; }
        public byte[] Picture { get; set; } = null!;

        public virtual University Uni { get; set; } = null!;
    }
}
