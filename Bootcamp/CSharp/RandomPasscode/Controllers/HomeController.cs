﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;
namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

public class PasscodeController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            System.Console.WriteLine(TempData["passcode"]);
            int? attempt = HttpContext.Session.GetInt32("attempt");
            if(attempt == null)
            {
                HttpContext.Session.SetInt32("attempt", 1);
            }
            else
            {
                attempt += 1;
                HttpContext.Session.SetInt32("attempt", (int)attempt);
            }
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string passcode = "";
            Random rand = new Random();
            for(int i = 1; i < 15; i++)
            {
                int x = rand.Next(0,36);
                passcode += chars[x];
            }
            TempData["passcode"] = passcode;
            TempData["attempt"] = HttpContext.Session.GetInt32("attempt");
            
            return View();
        }
        [HttpGet]
        [Route("/clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }

}
