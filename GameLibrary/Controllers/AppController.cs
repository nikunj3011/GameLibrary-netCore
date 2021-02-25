 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibrary.Services;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }
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
            if (ModelState.IsValid)
            {
                //send email
                _mailService.SendMessage("a@a.com", model.Name, $"from: { model.Email} , Message: {model.message}");
                ViewBag.UserMessage = "Sent mail";
                ModelState.Clear();
            }
            else
            {
                //show errors
            }
            return View();
        }
    }
}
