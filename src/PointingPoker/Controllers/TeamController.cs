using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointingPoker.DataAccess.Models;
using PointingPoker.Domain;
using PointingPoker.Models;
using System;

namespace PointingPoker.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly ISecurityService _securityService;
        private Guid _currentUserId;

        public TeamController(ITeamService teamService, ISecurityService securityService)
        {
            _teamService = teamService;
            _securityService = securityService;
            _currentUserId = _securityService.GetCurrentUserId();
        }

        public ViewResult Create()
        {
            var model = new CreateTeamViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateTeamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _teamService.CreateTeam(new Team
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                CreatedBy = _currentUserId
            });

            return RedirectToAction("Index", "Pointinating");
        }
    }
}
