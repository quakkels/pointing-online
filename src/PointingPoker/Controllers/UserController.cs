using Microsoft.AspNetCore.Mvc;
using PointingPoker.Domain;
using PointingPoker.DataAccess.Models;
using PointingPoker.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace PointingPoker.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IUserService _userService;
        private Guid _currentUserId;

        public UserController(
            ISecurityService securityService,
            IUserService userService)
        {
            _securityService = securityService;
            _userService = userService;
            _currentUserId = _securityService.GetCurrentUserId();
        }

        public ViewResult Index()
        {
            return View(_userService.GetUsers());
        }

        [AllowAnonymous]
        public ViewResult Register()
        {
            return View(new RegisterViewModel());
        }

        [AllowAnonymous, HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_userService.CreateUser(new User
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.Password
            }))
            {
                return RedirectToAction(nameof(SignIn));
            }

            ModelState.AddModelError("UserName", "This user name is taken.");
            return View(model);
        }
        
        public ViewResult Profile()
        {
            var user = new ProfileViewModel(
                _userService.GetUserById(_currentUserId));

            return View(user);
        }

        [HttpPost]
        public ViewResult Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!_userService.UpdateUserInfo(
                model.Id,
                model.UserName,
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

        [AllowAnonymous]
        public ViewResult SignIn()
        {
            return View(new SignInViewModel());
        }

        [AllowAnonymous, HttpPost]
        public async Task<ActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(!_securityService
                .VerifyUserPassword(model.UserName, model.Password))
            {
                ModelState.AddModelError("UserName", "These credentials aren't valid.");
                return View(model);
            }

            var user = _userService.GetUserByUsername(model.UserName);
            await _securityService.SignIn(user.Id);
            
            return Redirect("/");
        }

        [AllowAnonymous]
        public async Task<ActionResult> SignOut()
        {
            await _securityService.SignOut();
            return Redirect("/");
        }
    }
}
