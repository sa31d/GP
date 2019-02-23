
using Microsoft.AspNet.SignalR;
using PrintNow.Models;
using PrintNow.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PrintNow.Controllers
{
    public class PrintingCompanyController : Controller
    {
        private PrintnowEntities2 db = new PrintnowEntities2();
      
        // GET: PrintingCompany
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getRequests()
        {
            var requests = db.Orders.ToList();
            requests = requests.OrderByDescending(x => x.Customer.rate).ToList();
            return View(requests);
        }

        [HttpGet]
        public ActionResult response(int id)
        {
            Order order = db.Orders.Find(id);
            var printing = db.Printing_Company.Find(1);
            PrintingCompany_Response response = new PrintingCompany_Response
            {
                orderID = order.orderID,
                printingID= 1
            };
            return View(response);
        }
        [HttpPost]
        public ActionResult response(PrintingCompany_Response response)
        {

            if (ModelState.IsValid)
            {
                response.confierm = 1;
                db.PrintingCompany_Response.Add(response);
                db.SaveChanges();
                return RedirectToAction("getRequests");
            }
            return View(response);
        }
        public ActionResult requestMaterial()
        {
            var materials = db.Materials.ToList();
            requestMaterial reqMaterial = new requestMaterial
            {
                materials = materials,
                request = new PrintingCompany_Request_Material { printingID = Convert.ToInt32(Session["printingID"]) }
            };
            return View(reqMaterial);
        }
        [HttpPost]
        public ActionResult requestMaterial(PrintingCompany_Request_Material request)
        {
            if (ModelState.IsValid)
            {
                db.PrintingCompany_Request_Material.Add(request);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(request);
        }
        [HttpGet]
        public ActionResult searchForRawMaterial()
        {
            var materials = db.Materials.ToList();
            searchMaterial vm = new searchMaterial
            {
                materials = materials
            };
            return View(vm);
        }
        
        [HttpGet]
        public ActionResult materialsOwner(int? id)
        {
            var supplyMaterial = db.Supply_Material.Where(x => x.MaterialID == id).ToList();
            if (supplyMaterial == null)
            {
                return Json(new { result =0},JsonRequestBehavior.AllowGet);
            }
            List<Object> suppliers = new List<Object>();
            foreach (var x in supplyMaterial)
            {
                suppliers.Add(new {
                    companyName = x.Supplier.companyName,
                    email = x.Supplier.email,
                    phone = x.Supplier.phone,
                    rate = x.Supplier.rate,
                    materialPrice = x.Price,
                    address = x.Supplier.city + "  " + x.Supplier.address
                });
            }
            
                return Json(suppliers,JsonRequestBehavior.AllowGet);

        }
        public ActionResult searchForShippingCompany()
        {
            return View();
        }
        [HttpGet]
        public ActionResult shippingOfCity(string city)
        {
            var companyList = db.Shipping_Company.Where(x => x.city.Contains(city)).ToList();
            if (companyList == null)
                return Json(new { response = 0 }, JsonRequestBehavior.AllowGet);
            return Json(companyList,JsonRequestBehavior.AllowGet);
        }
        public ActionResult requestShipping(int shippingID)
        {
            Printing_Request_Shipping request = new Printing_Request_Shipping { shippingID = shippingID,printingID = Convert.ToInt32(Session["printingID"])};
            return View(request);
        }
        [HttpPost]
        public ActionResult requestShipping(Printing_Request_Shipping request)
        {
            if (ModelState.IsValid)
            {
                db.Printing_Request_Shipping.Add(request);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(request);
        }
        public ActionResult Chat()
        {

            return View();
        }

    }
}