 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            //throw new InvalidOperationException();
            return View();
        }

        [HttpGet("about")]
        public IActionResult AboutUs()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";

            //throw new InvalidOperationException("404");
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        { 
            return View();
        }
    }
}
