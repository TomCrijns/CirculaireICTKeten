using CirculaireICTKeten.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;


namespace CirculaireICTKeten.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var accounttype = Convert.ToInt32(HttpContext.Session.GetInt32("AccountType"));
            ViewData["AccountOverzicht"] = accounttype;
            var ProfileName = HttpContext.Session.GetString("ProfileName");
            ViewData["WelcomeName"] = ProfileName;
            if (accounttype == 1)
            {
                return View();
            }
            else if (accounttype == 2)
            {
                return View();
            }
            else if (accounttype == 3)
            {
                return View();
            }
            else if (accounttype == 4)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}
