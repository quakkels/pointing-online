using Microsoft.AspNetCore.Mvc;
using PointingPoker.Domain;
using PointingPoker.DataAccess.Models;
using PointingPoker.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace PointingPoker.Controllers
{
    [Authorize]
    public class PointinatingController : Controller
    {
        private Guid _currentUserId;
        private readonly ICardService _cardService;
        private readonly ITeamService _teamService;

        public PointinatingController(
            ICardService cardService, 
            ITeamService teamService,
            ISecurityService securityService)
        {
            _cardService = cardService;
            _teamService = teamService;
            _currentUserId = securityService.GetCurrentUserId();
        }

        public ViewResult Dashboard()
        {
            var model = new DashboardViewModel
            {
                Teams = _teamService.GetTeamsByUser(_currentUserId)
            };

            return View(model);
        }

        public ViewResult Card(Guid id)
        {
            var model = new CardViewModel
            {
                CreatedBy = _currentUserId,
                TeamId = id
            };
            return View(model);
        }

        [HttpPost]
        public ViewResult Card(CardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var card = new Card
            {
                Id = Guid.NewGuid(),
                Description = model.Description,
                CreatedBy = model.CreatedBy,
                IsPointingClosed = model.IsPointingClosed,
                TeamId = model.TeamId
            };

            if (!_cardService.CreateCard(card))
            {
                ModelState.AddModelError("Description", "Could not create this card.");
                return View(model);
            }

            return View(model);
        }

        public ViewResult Point(Guid id)
        {
            var card = _cardService.GetCard(id);
            return View();
        }
    }
}
