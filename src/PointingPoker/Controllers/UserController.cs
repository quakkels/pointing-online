using Microsoft.AspNetCore.Mvc;
using PointingPoker.Domain;
using PointingPoker.DataAccess.Models;
using PointingPoker.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PointingPoker.Controllers
{
    public class UserController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IUserService _userService;

        public UserController(
            ISecurityService securityService,
            IUserService userService)
        {
            _securityService = securityService;
            _userService = userService;
        }

        public ViewResult Index()
        {
            return View(_userService.GetUsers());
        }

        public ViewResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_userService.CreateUser(new User
            {
                Id = model.Id,
                Username = model.Username,
                Email = model.Email,
                PasswordHash = model.Password
            }))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Username", "This Username is taken.");
            return View(model);
        }

        [Authorize]
        public ViewResult Profile(string username)
        {
            var user = new ProfileViewModel(
                _userService.GetUserByUsername(username));

            return View(user);
        }

        [Authorize, HttpPost]
        public ViewResult Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!_userService.UpdateUserInfo(
                model.Id,
                model.Username,
                model.Email))
            {
                ModelState
                    .AddModelError(
                    "Username", 
                    "An error occurred while updating this profile information.");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.Password))
            {
                if(!_userService.UpdatePassword(model.Id, model.Password))
                {
                    ModelState
                        .AddModelError(
                        "Password", 
                        "An error occurred while updating the password.");
                    return View(model);
                }
            }

            return View(model);
        }

        public async Task<ActionResult> SignIn()
        {
            var user = _userService.GetUserByUsername("quakkels");
            await _securityService.SignIn(user.Id);

            return RedirectToAction(nameof(Profile));
        }

        public async Task<ActionResult> SignOut()
        {
            await _securityService.SignOut();
            return Redirect("/");
        }
    }
}
