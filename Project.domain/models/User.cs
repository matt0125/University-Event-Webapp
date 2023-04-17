using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class User
    {
        public User()
        {
            Reactions = new HashSet<Reaction>();
            RsoMembers = new HashSet<RsoMember>();
            Rsos = new HashSet<Rso>();
            Universities = new HashSet<University>();
        }

        public string AspNetUserId { get; set; } = null!;
        public int UserId { get; set; }
        public int UniId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsStudent { get; set; }

        public virtual AspNetUser AspNetUser { get; set; } = null!;
        public virtual University Uni { get; set; } = null!;
        public virtual ICollection<Reaction> Reactions { get; set; }
        public virtual ICollection<RsoMember> RsoMembers { get; set; }
        public virtual ICollection<Rso> Rsos { get; set; }
        public virtual ICollection<University> Universities { get; set; }
    }
}
