using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.domain.models;

namespace Project.web.Areas.SuperAdmin
{
    public class CreateModel : PageModel
    {
        private readonly Project.domain.models.ProjectContext _context;

        public CreateModel(Project.domain.models.ProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
        ViewData["RsoId"] = new SelectList(_context.Rsos, "RsoId", "Name");
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Events == null || Event == null)
            {
                return Page();
            }

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
