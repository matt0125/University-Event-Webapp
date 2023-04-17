using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using Project.domain.models;
using Project.domain.Models;
using Project.web.Models;

namespace Project.web.Controllers;

public class RsoController : Controller
{
    private readonly ProjectContext _context;
    private readonly CombinedUser _currentUser;

    public RsoController(ProjectContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _currentUser = _context.CombinedUsers.FirstOrDefault(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
    }

    // GET: Rso
    public async Task<IActionResult> Index()
    {
        List<StatusText> statusTexts = StatusTexts();

        List<Rso> projectContext = await _context.Rsos.Include(r => r.CreatedByNavigation).Include(r => r.Uni).ToListAsync();

        foreach (Rso rso in projectContext)
        {
            rso.Statusi = statusTexts;
        }
        
        return View(projectContext);
    }

    public async Task<IActionResult> Uni()
    {

        if (HttpContext.User == null)
        {
            return View(new CombinedUser { IsAdmin = false, IsSuperAdmin = false });
        }
        else
        {
            RSOMemberManage model = new RSOMemberManage();
            model.User = _currentUser;


            if (model.User == null)
            {
                model.User = new CombinedUser { IsAdmin = false, IsStudent = false, IsSuperAdmin = false };
                return View(model);
            }


            University _university = _context.Universities.FirstOrDefault(x => x.UniId == model.User.UniId);

            List<StatusText> statusTexts = StatusTexts();

            model.RSOs = await _context.Rsos.Where(r => r.Status == 2 && r.UniId == _university.UniId).Include(r => r.RsoMembers).Include(r => r.CreatedByNavigation).Include(r => r.Uni).ToListAsync();

            foreach (Rso rso in model.RSOs)
            {
                rso.Statusi = statusTexts;
            }

            return View(model);
        }

    }

    public async Task<IActionResult> Join(int id)
    {
        RsoMember newMember = new RsoMember();

        newMember.RsoId = id;
        newMember.IsAdmin = false;
        newMember.Status = 1;

        LinkVM model = new LinkVM();
        model.User = _context.CombinedUsers.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);

        newMember.UserId = model.User.UserId;

        if(_context.RsoMembers.FirstOrDefault(x => x.UserId == newMember.UserId && x.RsoId == id) != null)
        {
            _context.RsoMembers.FirstOrDefault(x => x.UserId == newMember.UserId && x.RsoId == id).Status = 1;
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());

        }

        _context.Add(newMember);
        await _context.SaveChangesAsync();


        return Redirect(Request.Headers["Referer"].ToString());
    }

    public async Task<IActionResult> Leave(int id)
    {
        RsoMember member = _context.RsoMembers.FirstOrDefault(x => x.UserId == _currentUser.UserId && x.RsoId == id);
        if(member != null)
        {
            _context.RsoMembers.Remove(member);
        }
        await _context.SaveChangesAsync();


        return Redirect(Request.Headers["Referer"].ToString());
    }

    private List<StatusText> StatusTexts()
    {
        List<StatusText> statusTexts = new();

        statusTexts.Add(new StatusText { ID = 0, Text = "Student approval pending" });
        statusTexts.Add(new StatusText { ID = 1, Text = "Student approved, super admin pending" });
        statusTexts.Add(new StatusText { ID = 2, Text = "Approved" });
        statusTexts.Add(new StatusText { ID = 3, Text = "Denied" });

        return statusTexts;
    }

    // GET: Rso/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Rsos == null)
        {
            return NotFound();
        }

        var rso = await _context.Rsos
            .Include(r => r.CreatedByNavigation)
            .Include(r => r.Uni)
            .FirstOrDefaultAsync(m => m.RsoId == id);
        if (rso == null)
        {
            return NotFound();
        }

        return View(rso);
    }

    // GET: Rso/Create
    public IActionResult Create()
    {
        SetupForm();

        return View();
    }

    private void SetupForm()
    {
 
        if (_currentUser != null)
            ViewData["Members"] = new SelectList(_context.CombinedUsers.Where(u => u.UniId == _currentUser.UniId), "UserId", "FullName");
    }

    // POST: Rso/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RsoVM rso)
    {

        if (CheckUsers(rso))
        {
            Rso newRso = new Rso();

            
            newRso.CreatedBy = _currentUser.UserId;
            newRso.UniId = _currentUser.UniId;
            newRso.Name = rso.Name;
            newRso.Description = rso.Description;

            _context.Add(newRso);
            newRso.RsoMembers.Add(new RsoMember { UserId = rso.User1 });
            newRso.RsoMembers.Add(new RsoMember { UserId = rso.User2 });
            newRso.RsoMembers.Add(new RsoMember { UserId = rso.User3 });
            newRso.RsoMembers.Add(new RsoMember { UserId = rso.User4 });
            newRso.RsoMembers.Add(new RsoMember { UserId = _currentUser.UserId, IsAdmin = true, Status = 2 });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("User1", "Please ensure all students are unique");
        SetupForm();
        return View(rso);
    }

    public bool CheckUsers(RsoVM rso)
    {
        if(_currentUser.UserId ==  rso.User1 || _currentUser.UserId == rso.User2 || _currentUser.UserId == rso.User3 || _currentUser.UserId == rso.User4)
        {
            return false;
        }
        if (rso.User1 == rso.User2 || rso.User1 == rso.User3 || rso.User1 == rso.User4)
            return false;
        if (rso.User2 == rso.User3 || rso.User2 == rso.User4)
            return false;
        if (rso.User3 == rso.User4)
            return false;

        return true;
    }

    // GET: Rso/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Rsos == null)
        {
            return NotFound();
        }

        var rso = await _context.Rsos.FindAsync(id);
        if (rso == null)
        {
            return NotFound();
        }
        ViewData["CreatedBy"] = new SelectList(_context.Users, "UserId", "AspNetUserId", rso.CreatedBy);
        ViewData["UniId"] = new SelectList(_context.Universities, "UniId", "Description", rso.UniId);
        return View(rso);
    }

    // POST: Rso/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Rso rso)
    {
        if (id != rso.RsoId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rso);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RsoExists(rso.RsoId))
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
        ViewData["CreatedBy"] = new SelectList(_context.Users, "UserId", "AspNetUserId", rso.CreatedBy);
        ViewData["UniId"] = new SelectList(_context.Universities, "UniId", "Description", rso.UniId);
        return View(rso);
    }

    // GET: Rso/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Rsos == null)
        {
            return NotFound();
        }

        var rso = await _context.Rsos
            .Include(r => r.CreatedByNavigation)
            .Include(r => r.Uni)
            .FirstOrDefaultAsync(m => m.RsoId == id);
        if (rso == null)
        {
            return NotFound();
        }

        return View(rso);
    }

    // POST: Rso/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Rsos == null)
        {
            return Problem("Entity set 'ProjectContext.Rsos'  is null.");
        }
        var rso = await _context.Rsos.FindAsync(id);
        if (rso != null)
        {
            _context.Rsos.Remove(rso);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RsoExists(int id)
    {
      return (_context.Rsos?.Any(e => e.RsoId == id)).GetValueOrDefault();
    }
}
