using Microsoft.EntityFrameworkCore;
using Project.domain.models;

namespace Project.domain
{
    public class Repo:iRepo
    {
        private ProjectContext _context;

        public Repo(ProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _context.Events.Include(x => x.Location);
        }
    }
}