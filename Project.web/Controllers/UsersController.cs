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
    public class UsersController : Controller
    {
        private readonly ProjectContext _context;

        public UsersController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.Users.Include(u => u.AspNetUser).Include(u => u.Uni);
            return View(await projectContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.AspNetUser)
                .Include(u => u.Uni)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            ViewData["UniId"] = new SelectList(_context.Universities, "UniId", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AspNetUserId,UserId,UniId,FirstName,LastName,IsAdmin,IsSuperAdmin,IsStudent")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", user.AspNetUserId);
            ViewData["UniId"] = new SelectList(_context.Universities, "UniId", "Name", user.UniId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(x => x.Uni).Include(x => x.AspNetUser).FirstOrDefaultAsync(x => x.UserId == id.Value);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", user.AspNetUserId);
            ViewData["UniId"] = new SelectList(_context.Universities, "UniId", "Name", user.UniId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AspNetUserId,UserId,UniId,FirstName,LastName,IsAdmin,IsSuperAdmin,IsStudent")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            ViewData["AspNetUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", user.AspNetUserId);
            ViewData["UniId"] = new SelectList(_context.Universities, "UniId", "Name", user.UniId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.AspNetUser)
                .Include(u => u.Uni)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ProjectContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
