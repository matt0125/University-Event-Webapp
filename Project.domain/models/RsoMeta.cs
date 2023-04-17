using Project.domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.domain.models
{
    public partial class Rso
    {
        [NotMapped] public List<StatusText> Statusi { get; set; }

        [NotMapped]
        public string StatusText
        {
            get
            {
                if (this.Status == 0)
                    return "Pending student approval";
                if (this.Status == 1)
                    return "Student approved, super admin pending";
                if (this.Status == 2)
                    return "Approved";
                if (this.Status == 3)
                    return "Denied";
                if (this.Status == 4)
                    return "Disbanded";
                else
                    return "This isn't right, don't trust anything.";
            }
        }
    }
}
