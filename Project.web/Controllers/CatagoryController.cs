using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.domain.models;

namespace Project.web.Controllers
{
    public class CatagoryController : Controller
    {
        private readonly ProjectContext _context;

        public CatagoryController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Catagory
        public async Task<IActionResult> Index()
        {
              return _context.Catagories != null ? 
                          View(await _context.Catagories.ToListAsync()) :
                          Problem("Entity set 'ProjectContext.Catagories'  is null.");
        }

        // GET: Catagory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Catagories == null)
            {
                return NotFound();
            }

            var catagory = await _context.Catagories
                .FirstOrDefaultAsync(m => m.CId == id);
            if (catagory == null)
            {
                return NotFound();
            }

            return View(catagory);
        }

        // GET: Catagory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catagory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CId,Name,Entered")] Catagory catagory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catagory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catagory);
        }

        // GET: Catagory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Catagories == null)
            {
                return NotFound();
            }

            var catagory = await _context.Catagories.FindAsync(id);
            if (catagory == null)
            {
                return NotFound();
            }
            return View(catagory);
        }

        // POST: Catagory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CId,Name,Entered")] Catagory catagory)
        {
            if (id != catagory.CId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catagory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatagoryExists(catagory.CId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(catagory);
        }

        // GET: Catagory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Catagories == null)
            {
                return NotFound();
            }

            var catagory = await _context.Catagories
                .FirstOrDefaultAsync(m => m.CId == id);
            if (catagory == null)
            {
                return NotFound();
            }

            return View(catagory);
        }

        // POST: Catagory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Catagories == null)
            {
                return Problem("Entity set 'ProjectContext.Catagories'  is null.");
            }
            var catagory = await _context.Catagories.FindAsync(id);
            if (catagory != null)
            {
                _context.Catagories.Remove(catagory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatagoryExists(int id)
        {
          return (_context.Catagories?.Any(e => e.CId == id)).GetValueOrDefault();
        }
    }
}
