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
      [HttpGet("/stylist")]
      public ActionResult AddStylist()
      {
        return View();
      }
      [HttpPost("/stylist/new")]
      public ActionResult NewStylist()
      {
        Stylist newStylist = new Stylist(Request.Form["new-stylist"]);
        newStylist.Save();
        return Redirect("/");
      }
    }
}
