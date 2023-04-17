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
    public class DeleteModel : PageModel
    {
        private readonly Project.domain.models.ProjectContext _context;

        public DeleteModel(Project.domain.models.ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FirstOrDefaultAsync(m => m.EId == id);

            if (@event == null)
            {
                return NotFound();
            }
            else 
            {
                Event = @event;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }
            var @event = await _context.Events.FindAsync(id);

            if (@event != null)
            {
                Event = @event;
                _context.Events.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
