using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrintNow.Models;

namespace PrintNow.Controllers
{
    public class CustomersController : Controller
    {
        private PrintnowEntities3 db = new PrintnowEntities3();

        // GET: Customer
        public ActionResult CustomerHome()
        {

            return View(db.Products.ToList());
        }
        public ActionResult ProductDetails(int id)
        {

            var detail = db.Product_Has_Material.Where(p => p.productID == id).ToList();
            return View(detail);
        }

        [HttpGet]
        public ActionResult RequestProduct(int id, string material)
        {
            if (id == 1)
            {
                return View("RequestProductBook");
            }
            else if (id == 2)
            {
                return View("RequestProductCard");
            }

            return View();
        }
        [HttpPost]
        public ActionResult RequestProduct(Order order, int id, string material, HttpPostedFileBase imagefile1)
        {
            Order ord = new Order();
            ord.imageFile = imagefile1;
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(ord.imageFile.FileName);
                string extension = Path.GetExtension(ord.imageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                ord.imagePath = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);

                ord.imageFile.SaveAs(fileName);

                ord.custID = Convert.ToInt32(Session["custID"]);
                ord.prodID = id;
                ord.material = material;
                ord.printingSize = order.printingSize;
                ord.quantity = order.quantity;
                ord.orderDate = DateTime.Now;
                ord.NumberPages = order.NumberPages;
                ord.CollectionPapers = order.CollectionPapers;
                ord.BrowseBook = order.BrowseBook;
                ord.Cover = order.Cover;
                ord.note = order.note;
                ord.shape = order.shape;
                db.Orders.Add(ord);
                db.SaveChanges();
                return RedirectToAction("ViewOrder");

            }


            return View();
        }
        public ActionResult ViewOrder()
        {

           // int customerId = Convert.ToInt32(Session["custID"]);
            var orders = db.Orders.Where(o => o.custID == 1).ToList();

            return View(orders);
        }
    }
}