using Project.domain.models;

namespace Project.web.Models;

public class EventsAndUser
{
    public EventsAndUser()
    {
        publicEvents = new List<Event>();
        uniEvents = new List<Event>();
        rsoEvents = new List<Event>();
    }

    public List<Event> publicEvents { get; set; }
    public List<Event> uniEvents { get; set; }
    public List<Event> rsoEvents { get; set; }

    public int userId { get; set; }
}
