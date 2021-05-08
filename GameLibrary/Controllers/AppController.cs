 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.Services;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly GameContext _context;
        private readonly UserManager<StoreUser> userManager;

        public AppController(IMailService mailService, GameContext context,UserManager<StoreUser> userManager)
        {
            _mailService = mailService;
            _context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            //StoreUser user = await userManager.FindByEmailAsync("nikunj@rathod.com");

            //if (user == null)
            //{
            //    user = new StoreUser()
            //    {
            //        FirstName = "Nikunj",
            //        LastName = "Rathod",
            //        Email = "nikunj@rathod.com",
            //        UserName = "nikunj3011"
            //    };

            //    var result = await userManager.CreateAsync(user, "P@ssw0rd!");
            //    if (result != IdentityResult.Success)
            //    {
            //        throw new InvalidOperationException("Could not create new user in Seeder");
            //    }
            //}
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

        [Authorize]
        public IActionResult Shop()
        {
            var results = from p in _context.GameLibraries
                          orderby p.Name
                          select p; //linq syntax


            //var results =_context.GameLibraries.OrderBy(p=>p.Name).ToList();  //fluid syntax we can do either
            return View(results.ToList()); 
        }
    }
}
