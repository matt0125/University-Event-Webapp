using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.domain.models;
using Project.web.Models;

namespace Project.web.Controllers
{
    public class EventController : Controller
    {
        private readonly ProjectContext _context;
        private readonly CombinedUser _user;

        public EventController(ProjectContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = context.CombinedUsers.FirstOrDefault(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            List<Event> currentevents = await _context.Events.
                   AsNoTracking()
                   .Include(x => x.CIdNavigation)
                   .Include(x => x.Location)
                    .Include(x => x.Uni)
                   .Include(x => x.Rso)
                   .ToListAsync();
            return View(currentevents);
        }

        // GET: Event
        public async Task<IActionResult> Search(int lID = -1, int CId = -1, string vis = null, string name = null)
        {
            List<SelectListItem> locs = _context.Locations.Select(x => new SelectListItem { Value = x.LocationId.ToString(), Text = x.Name }).ToList();
            locs.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            if (lID != -1)
            {
                foreach (var item in locs)
                {
                    if (item.Value == lID.ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
            ViewData["LocationId"] = locs;

            List<SelectListItem> cats = _context.Catagories.Select(x => new SelectListItem { Value = x.CId.ToString(), Text = x.Name }).ToList();
            cats.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            if (CId != -1)
            {
                foreach (var item in cats)
                {
                    if (item.Value == CId.ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
            ViewData["Catagories"] = cats;


            List<SelectListItem> visis = Enum.GetValues(typeof(Enums.Visability)).Cast<Enums.Visability>().Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList();
            visis.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            if (!string.IsNullOrEmpty(vis))
            {
                foreach (var item in visis)
                {
                    if (item.Value == vis)
                    {
                        item.Selected = true;
                    }
                }
            }
            ViewData["Visabilities"] = visis;

            List<Event> currentevents = await _context.Events.
                   AsNoTracking()
                   .Include(x => x.CIdNavigation)
                   .Include(x => x.Location)
                   .Include(x => x.Uni)
                   .Include(x => x.Rso)
                   .ToListAsync();
            if (lID != -1)
            {
                currentevents = currentevents.Where(x => x.LocationId == lID).ToList();
            }
            if (CId != -1)
            {
                currentevents = currentevents.Where(x => x.CId == CId).ToList();
            }
            if (!string.IsNullOrEmpty(vis) && vis != "-1")
            {
                currentevents = currentevents.Where(x => x.Visibility == vis).ToList();
            }
            if (!string.IsNullOrEmpty(name))
            {
                currentevents = currentevents.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }

            return View(currentevents);
        }

        public async Task<IActionResult> Picture(int id)
        {
            EventPicture model = new EventPicture { EId = id };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Picture(List<IFormFile> myfiles, int _EId)
        {
            try
            {
                foreach (IFormFile file in myfiles)
                {

                    if (file.Length > 0)
                    {
                        EventPicture _thispicture = new EventPicture { EId = _EId };
                        byte[] p1 = null;
                        using (var fs1 = file.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        _thispicture.Picture = p1;
                        _context.EventPictures.Add(_thispicture);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GeneralError", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            return View(new EventPicture { EId = _EId });
        }

        public async Task<IActionResult> Current()
        {
            ViewData["currentuser"] = _user;
            List<Event> currentevents = await _context.Events.
                AsNoTracking()
                .Include(x => x.EventPictures)
                .Include(x => x.CIdNavigation)
                .Include(x => x.Location)
                .Include(x => x.Uni)
                .Include(x => x.Rso)
                .Include(x => x.Reactions).ThenInclude(x => x.User)
                .ToListAsync();
            return View(currentevents);
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(x => x.Location)
                .Include(x => x.EventPictures)
                .Include(x => x.Rso)
                .FirstOrDefaultAsync(m => m.EId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            SetupMap();

            Event model = new Event { StartTime = DateTime.Now.Date.AddHours(12), EndTime = DateTime.Now.Date.AddHours(13) };
            SetupForm(model);
            return View(model);
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

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,Email,Phone,CId,RsoId,Name,Category,Visibility,Description,StartTime,EndTime,Status")] Event @event)
        {
            SetupMap();
            SetupForm(@event);
            try
            {
                if (_context.Events.Where(e => e.LocationId == @event.LocationId)
                    .Where(e => (e.StartTime > @event.StartTime && e.StartTime < @event.EndTime) ||
                    (e.EndTime > @event.StartTime && e.EndTime < @event.EndTime) ||
                    (e.StartTime < @event.StartTime && e.EndTime > @event.StartTime) ||
                    (e.StartTime < @event.EndTime && e.EndTime > @event.EndTime))
                    .Count() == 0)
                {
                    if (@event.LocationId == 0)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError("LocationId", "Please select a location from the map");

                        return View(@event);
                    }
                    @event.UniId = _user.UniId;
                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.Clear();
                ModelState.AddModelError("GeneralError", "Timefram overlaps with other event(s) for location selected");
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ModelState.AddModelError("GeneralError", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            SetupForm(@event);
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event)
        {

            try
            {
                _context.Update(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GeneralError", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }


            SetupForm(@event);
            return View(@event);
        }

        [HttpPost]
        public IActionResult Comment()
        {
            string? eid = Request.Form["eid"];
            string? comment = Request.Form["comment"];
            string? rating = Request.Form["rating"];

            if (!string.IsNullOrEmpty(eid) && !string.IsNullOrEmpty(comment))
            {
                Reaction rec = new Reaction();
                rec.Comment = comment;
                rec.EId = Convert.ToInt32(eid);
                rec.Rating = Convert.ToInt32(rating);
                rec.UserId = _user.UserId;

                _context.Reactions.Add(rec);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Current));
          
        }
        public IActionResult EditComment(int id)
        {
            Reaction rec = _context.Reactions.FirstOrDefault(x => x.ReactionId == id);
            return View(rec);
        }
        [HttpPost]
        public IActionResult EditComment(Reaction rec)
        {
                _context.Reactions.Update(rec);
                _context.SaveChanges();
            
            return RedirectToAction(nameof(Current));

        }

        private void SetupForm(Event @event)
        {
            if (_context.Rsos.Count(x => x.CreatedBy == _user.UserId) > 1)
            {
                ViewData["Rsos"] = new SelectList(_context.Rsos.Where(x => x.CreatedBy == _user.UserId), "RsoId", "Name", @event?.RsoId);
            }
            else
            {
                if (_context.Rsos.Count(x => x.CreatedBy == _user.UserId) == 1)
                {
                    @event.RsoId = _context.Rsos.FirstOrDefault(x => x.CreatedBy == _user.UserId).RsoId;
                }
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", @event?.LocationId);
            ViewData["Catagories"] = new SelectList(_context.Catagories, "CId", "Name", @event?.CId);
            ViewData["RsoId"] = new SelectList(_context.Rsos, "RsoId", "Name", @event?.RsoId);
            ViewData["Visabilities"] = Enum.GetValues(typeof(Enums.Visability)).Cast<Enums.Visability>().Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() });
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(x => x.Location)
                .Include(x => x.Rso)
                .FirstOrDefaultAsync(m => m.EId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ProjectContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.EId == id)).GetValueOrDefault();
        }
    }
}
