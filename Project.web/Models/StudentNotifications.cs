using Project.domain.models;

namespace Project.web.Models
{
    public class StudentNotifications
    {

        public StudentNotifications()
        {
            RsoMembers = new List<RsoMember>();
        }
        public List<RsoMember> RsoMembers { get; set; }
    }
}
