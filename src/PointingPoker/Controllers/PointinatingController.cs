﻿using Microsoft.AspNetCore.Mvc;
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
        private int _currentUserId;
        private readonly ICardService _cardService;
        private readonly ITeamService _teamService;
        private readonly IPointService _pointService;

        public PointinatingController(
            ICardService cardService, 
            ITeamService teamService,
            ISecurityService securityService,
            IPointService pointService)
        {
            _cardService = cardService;
            _teamService = teamService;
            _currentUserId = securityService.GetCurrentUserId();
            _pointService = pointService;
        }

        public ViewResult Dashboard()
        {
            var model = new DashboardViewModel
            {
                Teams = _teamService.GetTeamsByUser(_currentUserId)
            };

            return View(model);
        }

        public ViewResult Card(int id)
        {
            var model = new CardViewModel
            {
                CreatedBy = _currentUserId,
                TeamId = id
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Card(CardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var card = new Card
            {
                Description = model.Description,
                CreatedBy = model.CreatedBy,
                ClosedBy = model.ClosedBy,
                TeamId = model.TeamId
            };

            if (_cardService.CreateCard(card) == 0)
            {
                ModelState.AddModelError("Description", "Could not create this card.");
                return View(model);
            }

            return RedirectToAction("Summary", "Team", new { id =  card.TeamId});
        }

        public ViewResult Point(int id)
        {
            var model = new PointViewModel
            {
                Card = _cardService.GetCard(id),
                PointValue = _pointService.GetCardPoint(id, _currentUserId)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Point(PointViewModel model)
        {
            model.Card = _cardService.GetCard(model.Card.Id);

            if (ModelState.IsValid)
            {
                View(model);
            }

            var point = new Point
            {
                PointedBy = _currentUserId,
                CardId = model.Card.Id,
                Points = model.PointValue
            };
            var success = _pointService.PointCard(point);

            if (!success)
            {
                return Redirect("/");
            }

            return RedirectToAction("Summary", "Team", new { id = model.Card.TeamId });
        }

        public ViewResult Close(int id)
        {
            var card = _cardService.GetCard(id);
            return View(card);
        }

        [HttpPost]
        public ActionResult ClosePointing(int id)
        {
            var card = _cardService.GetCard(id);
            _cardService.ClosePointing(id, _currentUserId);
            return RedirectToAction("Summary", "Team", new { id = card.TeamId });
        }

        public ViewResult CardScore(int id)
        {
            var model = new CardScoreViewModel
            {
                Card = _cardService.GetCard(id),
                CardScores = _pointService.GetCardScore(id)
            };

            return View(model);
        }

        public ViewResult Reopen(int id)
        {
            var model = _cardService.GetCard(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Reopen(Card model, bool shouldReopen)
        {
            if (shouldReopen)
            {
                _cardService.OpenPointing(_currentUserId, model.Id);
            }
            else
            {
                return View(model);
            }

            return RedirectToAction(nameof(TeamController.Summary), "Team", new { id = model.TeamId });
        }
    }
}
