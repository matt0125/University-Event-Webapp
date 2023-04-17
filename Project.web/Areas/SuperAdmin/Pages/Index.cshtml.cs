using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project.domain.models;

namespace Project.web.Areas.SuperAdmin
{
    public class IndexModel : PageModel
    {
        private readonly Project.domain.models.ProjectContext _context;

        public IndexModel(Project.domain.models.ProjectContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Events != null)
            {
                Event = await _context.Events
                .Include(x => x.Location)
                .Include(x => x.Rso).ToListAsync();
            }
        }
    }
}
