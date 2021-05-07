using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly SignInManager<StoreUser> signInManager;

        public AccountController(ILogger<AccountController> logger, SignInManager<StoreUser> signInManager)
        {
            this.logger = logger;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if(this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginViewModel.Username,
                    loginViewModel.Password,
                    loginViewModel.RememberMe,
                    false);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        RedirectToAction("Shop", "App");
                    }
                }
            }
            ModelState.AddModelError("", "Failed to login");
            return View();
        }
    }
}
