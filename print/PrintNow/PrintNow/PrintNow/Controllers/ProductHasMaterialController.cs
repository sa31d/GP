using PrintNow.Models;
using PrintNow.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PrintNow.Controllers
{
    public class ProductHasMaterialController : Controller
    {
        private PrintnowEntities2 db = new PrintnowEntities2();
        // GET: ProductHasMaterial
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult assignProductMaterial()
        {
            var products = db.Products.ToList();
            var materials = db.Materials.ToList();
            productMaterial vm = new productMaterial { materials = materials, products = products, prodMaterial = new Product_Has_Material { printingCompanyID = Convert.ToInt32(Session["printingID"]) } };
            return View(vm);
        }
        [HttpPost]
        public ActionResult assignProductMaterial(productMaterial productMaterial)
        {
            if (ModelState.IsValid)
            {
                var exist = db.Product_Has_Material.Where(x => x.printingCompanyID == productMaterial.prodMaterial.printingCompanyID && x.materialID == productMaterial.prodMaterial.materialID && x.productID == productMaterial.prodMaterial.productID).ToList();
                if (exist.Count == 0)
                {
                    db.Product_Has_Material.Add(productMaterial.prodMaterial);
                    db.SaveChanges();
                    return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = 2 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result =0},JsonRequestBehavior.AllowGet);
        }
        public ActionResult remove(int printingID, int productID)
        {
            var products = db.Product_Has_Material.Where(x => x.printingCompanyID == printingID && x.productID == productID).ToList();
            if (products != null)
            {
                db.Product_Has_Material.RemoveRange(products);
                db.SaveChanges();
                return RedirectToAction("index", "PrintingCompany", null);
            }
            else return Json(new { result = 2 });
        }
        public ActionResult Edit(int printingID, int productID, int materialID)
        {
            if (printingID == null || productID == null || materialID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prodMaterial = db.Product_Has_Material.Where(x => x.productID == productID && x.materialID == materialID && x.printingCompanyID == printingID);
            if (prodMaterial == null)
            {
                return HttpNotFound();
            }
            return View(prodMaterial);
        }
        public ActionResult Edit(Product_Has_Material prodMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodMaterial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(prodMaterial);
        }
        public ActionResult removeMaterial(int printingID, int productID, int materialID)
        {
            Product_Has_Material prodMaterial = (Product_Has_Material)db.Product_Has_Material.Where(x => x.productID == productID && x.materialID == materialID && x.printingCompanyID == printingID);
            db.Product_Has_Material.Remove(prodMaterial);
            db.SaveChanges();
            return RedirectToAction("index");
        }

    }
}