using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.domain.models;
using Project.web.Models;

namespace Project.web.Controllers
{
    public class LocationController : Controller
    {
        private readonly ProjectContext _context;

        public LocationController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Location
        public async Task<IActionResult> Index()
        {
            return _context.Locations != null ?
                        View(await _context.Locations.ToListAsync()) :
                        Problem("Entity set 'ProjectContext.Locations'  is null.");
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            SetupMap();
            return View();
        }
        private void SetupMap()
        {
            List<Location> locs = _context.Locations.ToList();
            LocationLists loclist = new LocationLists();
            var locations = new List<Locations>();


            foreach (Location item in locs)
            {
                locations.Add(new Locations(item.LocationId, item.Name, item.Description, (double)item.Lattitude, (double)item.Longitude));
            }
            loclist.LocationList = locations;
            ViewData["locationlist"] = loclist;
        }
        // POST: Location/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Location location)
        {
            SetupMap();
            if (location.Lattitude == 0 || location.Longitude == 0)
            {
                ModelState.Clear();
                ModelState.AddModelError("Lattitude", "Please select a location from the map");
                ModelState.AddModelError("Longitude", "Please select a location from the map");
                return View(location);
            }
            try
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("GeneralError", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }


            return View(location);
        }

        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Location/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Location location)
        {

            try
            {
                _context.Update(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("GeneralError", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }


            return View(location);
        }

        // GET: Location/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locations == null)
            {
                return Problem("Entity set 'ProjectContext.Locations'  is null.");
            }
            var location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return (_context.Locations?.Any(e => e.LocationId == id)).GetValueOrDefault();
        }
    }
}
