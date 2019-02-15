using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrintNow.Models;

namespace PrintNow.Controllers
{
    public class UserAccountController : Controller
    {

        private PrintnowEntities2 db = new PrintnowEntities2();


      
        // GET: UserAccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterCustomer()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RegisterCustomer(Customer cust)
        {
            if (ModelState.IsValid)
            {
                cust.block = 0;
                db.Customers.Add(cust);
                db.SaveChanges();
                return RedirectToAction("CustomerHome");
            }
                 return View();
        }
        public ActionResult RegisterPrintCompany()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RegisterPrintCompany(Printing_Company print)
        {
           
            if (ModelState.IsValid)
              {
                print.block = 0;

               /* string fileName = Path.GetFileNameWithoutExtension(print.ImageFile.FileName);
                string extension = Path.GetExtension(print.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                print.image = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                print.ImageFile.SaveAs(fileName);
                */

                db.Printing_Company.Add(print);
                 db.SaveChanges();
                    return RedirectToAction("Index");
                
            }

            return View();
        }
        public ActionResult RegisterSuppCompany()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RegisterSuppCompany(Supplier supp)
        {

            if (ModelState.IsValid)
            {
                supp.block = 0;
                db.Suppliers.Add(supp);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();
        }
        public ActionResult RegisterShippCompany()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RegisterShippCompany(Shipping_Company shipp)
        {

            if (ModelState.IsValid)
            {
                shipp.block = 0;
                db.Shipping_Company.Add(shipp);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            
            var cust = db.Customers.Where(c => c.email == Email && c.password == Password).FirstOrDefault();
            var Supp = db.Suppliers.Where(s => s.email == Email && s.password == Password).FirstOrDefault();
            var Ship = db.Shipping_Company.Where(sh => sh.email == Email && sh.password == Password).FirstOrDefault();
            var Print = db.Printing_Company.Where(p => p.email == Email && p.password == Password).FirstOrDefault();
            
            if (cust != null)
            {
                Session["custID"] = cust.custID.ToString();
                Session["email"] = cust.email.ToString();
               return RedirectToAction("CustomerHome", "Customers");
            }else if (Supp != null)
            {
                Session["supplierID"] = Supp.supplierID.ToString();
                Session["email"] = Supp.email.ToString();
                @Session["companName"] = Supp.companyName.ToString();
                return RedirectToAction("Index", "Supplier");
            }
            else if (Ship != null)
            {
                Session["shippingID"] = Ship.shippingID.ToString();
                Session["email"] = Ship.email.ToString();
                @Session["companName"] = Ship.companyName.ToString();
                return RedirectToAction("Index", "ShippingCompany");

            }else if (Print != null)
            {
                Session["printingID"] = Print.printingID.ToString();
                Session["email"] = Print.email.ToString();
                @Session["companName"] = Print.companyName.ToString();
                return RedirectToAction("Index", "PrintingCompany");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is not Valid");
            }
            return View();
        }


    }
}