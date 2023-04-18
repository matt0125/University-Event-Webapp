using Project.domain.models;

namespace Project.web.Models;


public class RSOUniVM
{
    public RSOUniVM(University Uni)
    {
        RSO = new RSOMemberManage();
        this.Uni = Uni;
    }
    public RSOMemberManage RSO { get; set; }
    public University Uni { get; set; }
}
