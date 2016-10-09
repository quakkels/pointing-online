using Microsoft.AspNetCore.Mvc;
using PointingPoker.Models;
using System;

namespace PointingPoker.Controllers
{
    public class PointinatingController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Card(Guid currentUserId)
        {
            return View(new CardInfoViewModel());
        }

        [HttpPost]
        public ViewResult Card(CardInfoViewModel model)
        {
            return View(model);
        }
    }
}
