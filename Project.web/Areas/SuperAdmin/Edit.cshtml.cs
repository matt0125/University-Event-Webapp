using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.domain.models;

namespace Project.web.Areas.SuperAdmin
{
    public class EditModel : PageModel
    {
        private readonly Project.domain.models.ProjectContext _context;

        public EditModel(Project.domain.models.ProjectContext context)
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

            var @event =  await _context.Events.FirstOrDefaultAsync(m => m.EId == id);
            if (@event == null)
            {
                return NotFound();
            }
            Event = @event;
           ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
           ViewData["RsoId"] = new SelectList(_context.Rsos, "RsoId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.EId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EventExists(int id)
        {
          return (_context.Events?.Any(e => e.EId == id)).GetValueOrDefault();
        }
    }
}
