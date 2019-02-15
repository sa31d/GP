using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrintNow.Models;
using System.IO;

namespace PrintNow.Controllers
{
    public class customersController : Controller
    {
        private PrintnowEntities2 db = new PrintnowEntities2();

        // GET: customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "custID,email,password,firstName,lastName,phone,city,address,rate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "custID,email,password,firstName,lastName,phone,city,address,rate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult RequstShipping()
        {
          // ViewBag.CustomerID = new SelectList(db.Customers, "custID", "email");
            ViewBag.ShippingID = new SelectList(db.Shipping_Company, "shippingID", "email");
            return View();
        }

        // POST: Customer_Request_Shipping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequstShipping(Customer_Request_Shipping customer_Request_Shipping,int shippingid)
        {
            if (ModelState.IsValid)
            {
                Customer_Request_Shipping CRS = new Customer_Request_Shipping();
                CRS.CustomerID= Convert.ToInt32(Session["custID"]);
                CRS.ShippingID = shippingid;
                CRS.shippingType = customer_Request_Shipping.shippingType;
                CRS.date = customer_Request_Shipping.date;
                CRS.amount = customer_Request_Shipping.amount;
                db.Customer_Request_Shipping.Add(CRS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "custID", "email", customer_Request_Shipping.CustomerID);
            ViewBag.ShippingID = new SelectList(db.Shipping_Company, "shippingID", "email", customer_Request_Shipping.ShippingID);
            return View(customer_Request_Shipping);
        }


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
                return RedirectToAction("Showorders");

            }
            
            return View();
        }
        public ActionResult Showorders( )
        {
            int cid = Convert.ToInt32(Session["custID"]);
           var custorder = db.Orders.Where(s => s.custID ==cid).ToList();
            return View(custorder);
        }
        public ActionResult ShowPrinterRespons(int ordid)
        {
            var PCR = db.PrintingCompany_Response.Where(s=>s.orderID==ordid).ToList();
            return View(PCR);
        }
        public ActionResult confirmPrinting(int id,int idp)
        {
          PrintingCompany_Response PCR = db.PrintingCompany_Response.SingleOrDefault(s=>s.printingID==idp && s.orderID==id);
            if (PCR != null)
            {
                PCR.confierm = 1;
                db.SaveChanges();
            }
            return RedirectToAction("Showorders");
        }
    }
}
