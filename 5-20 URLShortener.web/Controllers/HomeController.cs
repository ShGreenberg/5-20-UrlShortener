using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _5_20_URLShortener.web.Models;
using _5_20_URLShortener.data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace _5_20_URLShortener.web.Controllers
{
    public class HomeController : Controller
    {
        private string _connString;
        public HomeController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShortenUrl(string url)
        {
            UrlRepository rep = new UrlRepository(_connString);
            Url fullUrl = new Url();
            if (User.Identity.IsAuthenticated)
            {
                fullUrl = rep.Shorten(url, User.Identity.Name);
            }
            else
            {
                fullUrl = rep.Shorten(url);
            }
            return Json(fullUrl);
        }

        [Route("{urlShort}")]
        public IActionResult ViewUrl(string urlShort)
        {
            UrlRepository rep = new UrlRepository(_connString);
            string url = rep.GetUrl(urlShort).OrgUrl;
            return Redirect(url);
        }

        [Authorize]
        public IActionResult MyUrls()
        {
            UrlRepository rep = new UrlRepository(_connString);
            var urls = rep.GetMyUrls(User.Identity.Name);
            return View(urls);
        }

    }
}
