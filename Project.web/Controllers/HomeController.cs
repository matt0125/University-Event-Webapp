using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.domain;
using Project.domain.models;
using Project.web.Models;
using System.Diagnostics;

namespace Project.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly iRepo _repo;
        private readonly CombinedUser _currentUser;

        public HomeController(ProjectContext context, ILogger<HomeController> logger, iRepo repo, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _repo = repo;
            _currentUser = _context.CombinedUsers.FirstOrDefault(x => x.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Notifications()
        {
            StudentNotifications model = new StudentNotifications();
            model.RsoMembers = _context.RsoMembers.Where(x => x.Status == 0 && x.UserId == _currentUser.UserId).Include(x => x.Rso).ToList();
            return View(model);

        }

        public IActionResult ApproveRso(int id)
        {
            RsoMember rso = _context.RsoMembers.Find(id);
            rso.Status = 1;
            _context.RsoMembers.Update(rso);
            _context.SaveChanges();

            return RedirectToAction(nameof(Notifications));
        }
        public IActionResult DenyRso(int id)
        {
            RsoMember rso = _context.RsoMembers.Find(id);
            rso.Status = 2;
            _context.RsoMembers.Update(rso);
            _context.SaveChanges();

            return RedirectToAction(nameof(Notifications));
        }


        public IActionResult Events()
        {
            IEnumerable<Project.domain.models.Event> model = _repo.GetAllEvents();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}