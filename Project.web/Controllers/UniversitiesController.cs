using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.domain.models;
using Project.domain.Models;
using Project.web.Models;

namespace Project.web.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly ProjectContext _context;
        private readonly CombinedUser _currentUser;

        public UniversitiesController(ProjectContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _currentUser = _context.CombinedUsers.FirstOrDefault(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        private void SetupForm(University university = null)
        {
            var names = _context.Users.Select(x => new { UserId = x.UserId, Name = $"{x.FirstName} {x.LastName}" }).ToList();
            ViewData["CreatedBy"] = new SelectList(names, "UserId", "Name", university?.CreatedBy);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", university?.LocationId);
        }

        // GET: Universities
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.Universities.Include(u => u.CreatedByNavigation).Include(u => u.Location);
            return View(await projectContext.ToListAsync());
        }

        // GET: Universities/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Universities == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .Include(u => u.CreatedByNavigation)
                .Include(u => u.Location)
                .Include(u => u.UniPictures)
                .FirstOrDefaultAsync(m => m.UniId == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // GET: Universities/Home/
        public async Task<IActionResult> Home(int? id)
        {
            if (id == null || _context.Universities == null)
            {
                return NotFound();
            }
            ViewData["currentuser"] = _currentUser;
            var university = await _context.Universities
                .Include(u => u.CreatedByNavigation)
                .Include(u => u.Location)
                .Include(u => u.UniPictures)
                .Include(u => u.Events).ThenInclude(x => x.EventPictures)
                .Include(x => x.Events).ThenInclude(u => u.Location)
                .Include(x => x.Events).ThenInclude(u => u.Reactions)
                .Include(u => u.Rsos)
                
                .FirstOrDefaultAsync(m => m.UniId == id);
            if (university == null)
            {
                return NotFound();
            }
            RSOUniVM model = new RSOUniVM(university);

            RSOMemberManage rso = new RSOMemberManage();
            rso.User = _currentUser;


            if (rso.User == null)
            {
                rso.User = new CombinedUser { IsAdmin = false, IsStudent = false, IsSuperAdmin = false };
                return View(model);
            }


            University _university = _context.Universities.FirstOrDefault(x => x.UniId == rso.User.UniId);


            rso.RSOs = await _context.Rsos.Where(r => r.Status == 2 && r.UniId == _university.UniId).Include(r => r.RsoMembers).Include(r => r.CreatedByNavigation).Include(r => r.Uni).ToListAsync();

            model.RSO = rso;

            return View(model);
        }



        // GET: Universities/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Users, "UserId", "AspNetUserId");
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
            return View();
        }

        // POST: Universities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UniId,LocationId,Name,Description")] University university)
        {
            AspNetUser user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
            User user1 = await _context.Users.FirstOrDefaultAsync(x => x.AspNetUserId == user.Id);

            university.CreatedBy = user1.UserId;
            _context.Add(university);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["CreatedBy"] = new SelectList(_context.Users, "UserId", "AspNetUserId", university.CreatedBy);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", university.LocationId);
            return View(university);
        }

        // GET: Universities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Universities == null)
            {
                return NotFound();
            }

            var university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "UserId", "AspNetUserId", university.CreatedBy);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", university.LocationId);
            return View(university);
        }

        // POST: Universities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, University university)
        {
            if (id != university.UniId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(university);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GeneralError", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            SetupForm(university);
            return View(university);
        }
        public async Task<IActionResult> Picture(int id)
        {
            UniPicture model = new UniPicture { UniId = id };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Picture(List<IFormFile> myfiles, int _UniId)
        {
            try
            {
                foreach (IFormFile file in myfiles)
                {

                    if (file.Length > 0)
                    {
                        UniPicture _thispicture = new UniPicture { UniId = _UniId };
                        byte[] p1 = null;
                        using (var fs1 = file.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        _thispicture.Picture = p1;
                        _context.UniPictures.Add(_thispicture);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GeneralError", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            return View(new UniPicture { UniId = _UniId });
        }

        // GET: Universities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Universities == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .Include(u => u.CreatedByNavigation)
                .Include(u => u.Location)
                .FirstOrDefaultAsync(m => m.UniId == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // POST: Universities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Universities == null)
            {
                return Problem("Entity set 'ProjectContext.Universities'  is null.");
            }
            var university = await _context.Universities.FindAsync(id);
            if (university != null)
            {
                _context.Universities.Remove(university);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversityExists(int id)
        {
            return (_context.Universities?.Any(e => e.UniId == id)).GetValueOrDefault();
        }
    }
}
