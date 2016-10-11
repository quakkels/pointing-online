using Microsoft.AspNetCore.Mvc;
using PointingPoker.Domain;
using PointingPoker.DataAccess.Models;
using PointingPoker.Models;
using System;

namespace PointingPoker.Controllers
{
    public class PointinatingController : Controller
    {
        private readonly ICardService _cardService;

        public PointinatingController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Card(Guid currentUserId)
        {
            var model = new CardInfoViewModel
            {
                CreatedBy = currentUserId
            };
            return View(model);
        }

        [HttpPost]
        public ViewResult Card(CardInfoViewModel model)
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
                IsPointingClosed = model.IsPointingClosed
            };

            if (!_cardService.CreateCard(card))
            {
                ModelState.AddModelError("Description", "Could not create this card.");
                return View(model);
            }

            return View(model);
        }
    }
}
