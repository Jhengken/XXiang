using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using XXiang.Models;

namespace XiangXiang.Controllers
{
    public class AOrderController : Controller
    {
        private readonly dbXContext db;
        public AOrderController(dbXContext dbXContext) 
        {
            db = dbXContext;
        }
        public IActionResult List()
        {
            IEnumerable<TAorder> aorders = null;            
            aorders = from t in db.TAorders select new TAorder
                      {
                          AorderId = t.AorderId,
                          SupplierId = t.SupplierId,
                          AdvertiseId = t.AdvertiseId,
                          OrderDate = t.OrderDate,
                          EndDate = t.EndDate,
                          Clicks = t.Clicks,
                          Price= t.Price,
                          Advertise = t.Advertise,
                          Supplier = t.Supplier,
                      };
            return View(aorders);
        }
        //創建廣告訂單
        public IActionResult Create()
        {
            var advertiseOptions = db.TAdvertises.Select(a => new SelectListItem
            {
                Value = a.AdvertiseId.ToString(),
                Text = a.Name
            }).ToList();
            ViewBag.AdvertiseOptions = advertiseOptions;

            var supplierOptions = db.TSuppliers.Select(a => new SelectListItem
            {
                Value = a.SupplierId.ToString(),
                Text = a.Name
            }).ToList();
            ViewBag.SupplierOptions = supplierOptions;
            return View();
        }

        [HttpPost]
        public IActionResult Create(TAorder aorder)
        {
            var advertise = db.TAdvertises.FirstOrDefault(a => a.AdvertiseId == Convert.ToInt32(aorder.advertiseName));
            if (advertise == null)
            {
                // handle error - advertise not found
                return BadRequest("Advertise not found");
            }
            var supplier = db.TSuppliers.FirstOrDefault(a => a.SupplierId == Convert.ToInt32(aorder.supplierName));
            if (supplier == null)
            {
                // handle error - supplier not found
                return BadRequest("Supplier not found");
            }
            if (advertise != null && supplier != null)
            {
                aorder.AdvertiseId = Convert.ToInt32(advertise.AdvertiseId);
                aorder.SupplierId = Convert.ToInt32(supplier.SupplierId);
                aorder.OrderDate = aorder.OrderDate;
                aorder.EndDate = aorder.EndDate;
                aorder.Clicks = aorder.Clicks;
                aorder.Price = aorder.Price;
                db.Add(aorder);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            else
                return View("List");
        }

    }
}
