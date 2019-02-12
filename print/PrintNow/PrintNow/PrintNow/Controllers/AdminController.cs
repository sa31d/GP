using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrintNow.Models;
using PrintNow.Models.ViewModel;
using System.Net;
using static System.Net.WebRequestMethods;

namespace PrintNow.Controllers
{
    public class AdminController : Controller
    {
        PrintnowEntities6 db = new PrintnowEntities6();

        // GET: Admin
        public ActionResult Index()
        {
            Users user = new Users {
                customers = db.Customers.ToList(),
                printing = db.Printing_Company.ToList(),
                shipping = db.Shipping_Company.ToList(),
                suppliers=db.Suppliers.ToList()
                            };
           
            
            return View(user);
        }

        
        public ActionResult searchForCustomer(String name, string Email)
        {
            List<String> userType = new List<string> { "Customer", "Printing Company", "Supplier", "Shipping" };
            

            ViewBag.name=new SelectList(userType);
                 if (Email !=null)
                 {
                     if (name == "Customer")
                     {
                    var user = db.Customers.ToList();
                    user = user.Where(e => e.email.Contains(Email)).ToList();
                       return View("searchForUser",user);
                     }
                     else if (name == "Printing Company")
                     {
                        var  user = db.Printing_Company.ToList();
                         user = user.Where(e => e.email.Contains(Email)).ToList();
                      return View("View",user);
                     }
                     else if (name == "Shipping")
                     {
                         var user = db.Shipping_Company.ToList();
                         user = user.Where(e => e.email.Contains(Email)).ToList();
                       return View("shippingmange", user);
                     }
                     else if (name == "Supplier")
                     {
                         var user = db.Suppliers.ToList();
                         user = user.Where(e => e.email.Contains(Email)).ToList();
                         return View("suppliermange", user);
                     }
                 }

                return View();
        }

        public ActionResult Block(int id)
        {

            Customer c = db.Customers.Find(id);
            Printing_Company pc = db.Printing_Company.Find(id);
            Shipping_Company Shic = db.Shipping_Company.Find(id);
            Supplier sup = db.Suppliers.Find(id);
            if (c != null)
            {
                if (c.block == 0)
                    c.block = 1;
                else
                    c.block = 0;
            }
            else if (pc != null)
            {
                if (pc.block == 0)
                    pc.block = 1;
                else
                    pc.block = 0;
            }
           else if (sup != null)
            {
                if (sup.block == 0)
                    sup.block = 1;
                else
                    sup.block = 0;
            }
            
            else if (Shic != null)
            {
                if (Shic.block == 0)
                    c.block = 1;
                else
                    Shic.block = 0;
            }
            db.SaveChanges();
            return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
        }
    }
}