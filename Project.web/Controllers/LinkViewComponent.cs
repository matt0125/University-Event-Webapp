using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.domain.models;
using Project.web.Models;

namespace Project.web.Controllers
{
    [ViewComponent(Name = "Link")]
    public class LinkViewComponent : ViewComponent
    {
        private readonly ProjectContext _context;
        public LinkViewComponent(ProjectContext context)
        {
            _context = context;

        }
        public IViewComponentResult Invoke()
        {
            if(HttpContext.User== null)
            {
                return View(new CombinedUser { IsAdmin = false, IsSuperAdmin = false });
            }
            else
            {
                LinkVM model = new LinkVM();
                model.User =  _context.CombinedUsers.FirstOrDefault(x => x.UserName == HttpContext.User.Identity.Name);
                
                
                if(model.User == null)
                {
                    model.User = new CombinedUser { IsAdmin = false, IsStudent = false, IsSuperAdmin = false};
                    model.User = new CombinedUser { IsAdmin = false, IsStudent = false, IsSuperAdmin = false };
                    return View(model);
                }
                

                model.University = _context.Universities.FirstOrDefault(x => x.UniId == model.User.UniId);
                int notices = 0;
                notices += _context.Rsos.Where(x => x.Status == 1 && x.UniId == model.University.UniId).Count();
                notices += _context.Events.Where(x => x.Status == 0 && x.UniId == model.University.UniId).Count();

                model.Notifications = notices;
                
                model.Invitations = _context.RsoMembers.Where(x => x.Status == 0 && x.UserId == model.User.UserId).Count();
                return View(model);
            }
        }
    }
}
