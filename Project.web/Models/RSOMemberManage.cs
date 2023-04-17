using Project.domain.models;

namespace Project.web.Models
{
    public class RSOMemberManage
    {
        public RSOMemberManage()
        {
            RSOs = new List<Rso>();
        }
        public List<Rso> RSOs { get; set; }
        public CombinedUser User { get; set; }
    }
}
