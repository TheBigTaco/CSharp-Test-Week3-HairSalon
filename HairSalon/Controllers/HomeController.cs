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
      [HttpGet("/stylist/{id}/customer")]
      public ActionResult AddCustomer(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(id);
        List<Customer> stylistCustomers = selectedStylist.GetCustomers();
        model.Add("stylist", selectedStylist);
        model.Add("customer", stylistCustomers);
        return View(model);
      }
      [HttpPost("/stylist/{id}/customer/new")]
      public ActionResult NewCustomer(int id)
      {
        int stylistId = int.Parse(Request.Form["stylist-id"]);
        Customer newCustomer = new Customer(Request.Form["customer-name"], stylistId);
        newCustomer.Save();
        return Redirect("/stylist/" + stylistId);
      }
      [HttpGet("/stylist/{sid}/customer/delete/{cid}")]
      public ActionResult Delete(int sid, int cid)
      {
        Customer.DeleteCustomer(cid);
        return Redirect("/stylist/"+sid);
      }
      [HttpGet("/stylist/{sid}/customer/update/{cid}")]
      public ActionResult Update(int sid, int cid)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(sid);
        Customer selectedCustomer = Customer.Find(cid);
        model.Add("stylist", selectedStylist);
        model.Add("customer", selectedCustomer);
        return View(model);
      }
      [HttpPost("/stylist/{id}/customer/update")]
      public ActionResult UpdateProccess(int id)
      {
        int customerId = int.Parse(Request.Form["customer-id"]);
        Customer updatedCustomer = Customer.Find(customerId);
        updatedCustomer.UpdateCustomer(Request.Form["new-name"], customerId);
        return Redirect("/stylist/"+id);
      }
    }
}
