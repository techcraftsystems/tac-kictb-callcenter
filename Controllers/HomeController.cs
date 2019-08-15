using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Models;

namespace tac_kictb_callcenter.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index() {
            return View();
        }

        [Route("privacy")]
        public IActionResult Privacy() {
            return View();
        }

        [Route("/messages/")]
        public IActionResult Messages() {
            return View();
        }

        [Route("/todo-list/")]
        public IActionResult TodoList() {
            return View();
        }

        [Route("/calendar/")]
        public IActionResult Calendar() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
