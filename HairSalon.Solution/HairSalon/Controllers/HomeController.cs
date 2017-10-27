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
      [HttpGet("/stylist/{id}")]
      public ActionResult StylistDetail(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(id);
        List<Customer> stylistCustomers = selectedStylist.GetCustomers();
        model.Add("stylist", selectedStylist);
        model.Add("customer", stylistCustomers);
        return View(model);
      }

    }
}
