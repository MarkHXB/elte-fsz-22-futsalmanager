﻿using Microsoft.AspNetCore.Mvc;

namespace FutsalManager.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}