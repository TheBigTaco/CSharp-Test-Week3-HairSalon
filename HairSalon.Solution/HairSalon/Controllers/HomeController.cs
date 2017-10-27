using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Salon.Models;

namespace Salon.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        List<Stylist> stylists = Stylist.GetAll();
        return View(stylists);
      }
    }
}
