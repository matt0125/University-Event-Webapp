using System;
using System.Collections.Generic;

namespace Project.domain.models
{
    public partial class CombinedUser
    {
        public int UserId { get; set; }
        public int UniId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsStudent { get; set; }
        public string AspNetUserId { get; set; } = null!;
        public string? UserName { get; set; }
        public string FullName { get; set; } = null!;
    }
}
