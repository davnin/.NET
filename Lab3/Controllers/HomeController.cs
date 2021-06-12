using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Razor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Count()
        {
            ViewData["bottles"] = Request.Form["bottles"];
            return View();
        }

        [HttpGet]
        public IActionResult CreatePerson()
        {

            return View();
        }

        [HttpPost]
        public IActionResult DisplayPerson()
        {
            ViewBag.fName = Request.Form["fName"];
            ViewBag.lName = Request.Form["lName"];
            ViewBag.age = Request.Form["age"];
            ViewBag.email = Request.Form["email"];
            ViewBag.dob = Request.Form["dob"];
            ViewBag.password = Request.Form["password"];
            ViewBag.description = Request.Form["description"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
