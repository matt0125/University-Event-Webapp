using Project.domain.models;

namespace Project.web.Models
{
    public class SuperAdminNotifications
    {
        public SuperAdminNotifications()
        {
            Rsos = new List<Rso>();
            Events = new List<Event>();
        }
        public List<Rso> Rsos { get; set; }
        public List<Event> Events{ get; set; }
    }
}
