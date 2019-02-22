using Gp_project.Models;
using Gp_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gp_project.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier

        PrintnowEntities db = new PrintnowEntities();

        // GET: Supplier

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageMaterials()

        {

            var materials = getMaterials().Where(x => x.SupplierID == Convert.ToInt32(Session["supplierID"]));
            return View(materials);

        }

        public IEnumerable<Supply_Material> getMaterials()
        {
            var materials = db.Supply_Material.ToList();
            return materials;
        }
        [HttpGet]
        public ActionResult AddMaterials()
        {
            var materials = db.Material.ToList();
            SuppliedMaterials supm = new SuppliedMaterials
            {

                materials = materials,

            };
            return View(supm);
        }
        public bool IsExist(int supid, int matid)
        {

            var result = db.Supply_Material.Any(x => x.SupplierID == supid && x.MaterialID == matid);

            return result;

        }
        [HttpPost]
        public ActionResult AddMaterials(SuppliedMaterials supm)
        {
            if (ModelState.IsValid)
            {
                supm.supply_material.SupplierID = Convert.ToInt32(Session["supplierID"]);

                //supm.supply_material.SupplierID = 3;
                if (IsExist(supm.supply_material.SupplierID, supm.supply_material.MaterialID))
                {

                    return Json(new { result = 2 });
                }
                else
                {

                    db.Supply_Material.Add(supm.supply_material);

                    db.SaveChanges();

                    return Json(new { result = 1 });

                }



            }
            return Json(new { result = 0 });


        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var material = db.Supply_Material.Single(x => x.ID == id);
            var materials = db.Material.ToList();
            SuppliedMaterials supm = new SuppliedMaterials
            {

                materials = materials,
                supply_material = material
            };
            return View(supm);
        }
        [HttpPost]
        public ActionResult Edit(SuppliedMaterials supm,int id)
        {
            if (!ModelState.IsValid)
            {
                var materials = db.Material.ToList();
                supm.materials = materials;
                return View("Edit", supm);
            }
            var material = db.Supply_Material.Single(x => x.ID == id);
            material.MaterialID = supm.supply_material.MaterialID;
            material.Price = supm.supply_material.Price;
            material.amount = supm.supply_material.amount;
            db.SaveChanges();

            return RedirectToAction("ManageMaterials");
        }





        public ActionResult Details(int id)
        {

            var material = getMaterials().SingleOrDefault(x => x.ID == id);
            if (material == null)
                return HttpNotFound();

            return View(material);
        }
        public ActionResult Delete(int id)
        {
            var material = db.Supply_Material.Single(x => x.ID == id);
            db.Supply_Material.Remove(material);
            db.SaveChanges();

            return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowPrintingRequest()
        {
            return View();
        }
        public IEnumerable<PrintingCompany_Request_Material> getrequests()
        {
            var requests = db.PrintingCompany_Request_Material.ToList();
            return requests;
        }
        public ActionResult ShowRequests()
        {
            var requests = getrequests();
            return View(requests);

        }

    }

}
