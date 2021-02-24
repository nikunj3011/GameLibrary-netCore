﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult AboutUs()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }
    }
}
