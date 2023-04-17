using Microsoft.AspNetCore.Mvc;
using Project.domain.models;
using Project.web.Models;

namespace Project.web.Controllers;

public class SuperAdminController : Controller
{
    private readonly ProjectContext _context;
    private readonly CombinedUser _currentUser;

    public SuperAdminController(ProjectContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _currentUser = _context.CombinedUsers.FirstOrDefault(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
    }
    public IActionResult Notifications()
    {
        SuperAdminNotifications model = new SuperAdminNotifications();
        University myuni = _context.Universities.FirstOrDefault(x => x.UniId == _currentUser.UniId);
        model.Rsos = _context.Rsos.Where(x => x.Status == 1 && x.UniId == myuni.UniId).ToList();
        model.Events = _context.Events.Where(x => x.Status == 0 && x.UniId == myuni.UniId).ToList();
        return View(model);
    }

    public IActionResult ApproveRso(int id)
    {
        Rso rso = _context.Rsos.Find(id);
        rso.Status = 2;
        _context.Rsos.Update(rso);
        _context.SaveChanges();

        return RedirectToAction(nameof(Notifications));
    }
    public IActionResult DenyRso(int id)
    {
        Rso rso = _context.Rsos.Find(id);
        rso.Status = 3;
        _context.Rsos.Update(rso);
        _context.SaveChanges();
        return RedirectToAction(nameof(Notifications));
    }
    public IActionResult ApproveEvent(int id)
    {
        Event evt = _context.Events.Find(id);
        evt.Status = 1;
        _context.Events.Update(evt);
        _context.SaveChanges();

        return RedirectToAction(nameof(Notifications));
    }
    public IActionResult DenyEvent(int id)
    {
        Event evt = _context.Events.Find(id);
        evt.Status = 2;
        _context.Events.Update(evt);
        _context.SaveChanges();
        return RedirectToAction(nameof(Notifications));
    }
}
