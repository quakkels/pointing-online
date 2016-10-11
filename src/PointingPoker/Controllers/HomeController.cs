using Microsoft.AspNetCore.Mvc;
using PointingPoker.Domain;

namespace PointingPoker.Controllers
{

    public class HomeController : Controller
    {
        public readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public ViewResult Index()
        {
            var users = _userService.GetUsers();
            return View(users);
        }
    }
}
