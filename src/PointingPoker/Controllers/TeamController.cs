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
        private readonly ICardService _cardService;
        private readonly IUserService _userService;

        private int _currentUserId;

        public TeamController(
            ITeamService teamService, 
            ISecurityService securityService,
            ICardService cardService,
            IUserService userService)
        {
            _teamService = teamService;
            _securityService = securityService;
            _currentUserId = _securityService.GetCurrentUserId();
            _cardService = cardService;
            _userService = userService;
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

            var team = new Team
            {
                Id = 1,
                Name = model.Name,
                CreatedBy = _currentUserId
            };

            _teamService.CreateTeam(
                team,
                model.MemberEmails?.Split(' '));

            return RedirectToAction("Summary", "Team", new { id = team.Id });
        }

        public ActionResult Summary(int id)
        {
            if (!_teamService.IsUserInTeam(_currentUserId, id))
            {
                return Redirect("/");
            }

            var model = new TeamSummaryViewModel
            {
                Team = _teamService.GetTeam(id),
                CardsToPoint = _cardService.GetCardsToPointForTeam(_currentUserId, id),
                PointedCards = _cardService.GetOpenCardsForTeam(id),
                TeamUserNames = _userService.GetUserNamesByTeam(id),
                Teams = _teamService.GetTeamsByUser(_currentUserId),
                ClosedCards = _cardService.GetClosedCardsForTeam(id, _currentUserId)
            };

            return View(model);
        }

        public ViewResult InviteMembers(int id)
        {            
            return View(new InviteTeamMembersViewModel { TeamId = id });
        }

        [HttpPost]
        public ActionResult InviteMembers(InviteTeamMembersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _teamService.AddMembersByEmail(_currentUserId, model.TeamId, model.GetParsedInvitedEmails);
            
            return RedirectToAction(nameof(Summary), new { id = model.TeamId });
        }
    }
}
