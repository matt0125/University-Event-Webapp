using Project.domain.models;

namespace Project.web.Models
{
    public class LinkVM
    {

        public LinkVM()
        {
            User = new CombinedUser();
            University = new University();
        }

        public CombinedUser User { get; set; }

        public University University { get; set; }
        public int Notifications { get; set; }
        public int Invitations { get; set; }
    }
}
