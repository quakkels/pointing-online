using Microsoft.AspNetCore.Mvc;

namespace PointingPoker.Controllers
{
    public class HomeController : Controller
    { 
        public ViewResult Index()
        {
            return View();
        }
    }
}
