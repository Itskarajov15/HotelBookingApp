using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}