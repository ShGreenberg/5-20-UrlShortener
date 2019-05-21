using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _5_20_URLShortener.data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace _5_20_URLShortener.web.Controllers
{
    public class AccountController : Controller
    {
        private string _connString;
        public AccountController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user, string password)
        {
            var rep = new UserRepository(_connString);
            string message = rep.AddUser(user, password);
            if (message != "")
            {
                TempData["message"] = message;
                return Redirect("/account/signup");
            }
            return Redirect("/account/login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            UserRepository rep = new UserRepository(_connString);
            User user = rep.Login(email, password);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var claims = new List<Claim>
            {
                new Claim("user", email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(
                claims, "Cookies", "user", "role"))).Wait();
            return Redirect("/");
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }


    }
}