using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.ViewModel;

namespace MyCourse.Customisations.ViewComponents
{
    public class PaginationBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke (IPaginationInfo model) //nome e tipo di return obbligatorio
        {

            return View();
        }
    }
}
