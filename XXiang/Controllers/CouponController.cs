using Microsoft.AspNetCore.Mvc;
using XXiang.Models;

namespace XXiang.Controllers
{
    public class CouponController : Controller
    {
        private readonly dbXContext db;
        public CouponController(dbXContext conetxt)
        {
            db = conetxt;
        }
        public IActionResult List()
        {
            IEnumerable<TCoupon> coupons = null;
            coupons = from c in db.TCoupons select c;
            return View(coupons);
        }
        public IActionResult Delete(int? id) {
         if (id != null)                                
            {
                TCoupon delCoupon = db.TCoupons.FirstOrDefault(t => t.CouponId == id);
                if(delCoupon != null)
                db.TCoupons.Remove(delCoupon);
                db.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TCoupon t)
        {
            db.TCoupons.Add(t);
            db.SaveChangesAsync();
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {

                TCoupon x = db.TCoupons.FirstOrDefault(t => t.CouponId == id);
                if (x != null)
                    return View(x);
            }
            return RedirectToAction("List");

        }
        [HttpPost]
        public IActionResult Edit(TCouponMetadata C)
        {
            TCoupon x = db.TCoupons.FirstOrDefault(t => t.CouponId == C.CouponId);
            if (x != null)
            {
                x.Code = C.Code;
                x.Quantity = C.Quantity;
                x.Discount = C.Discount;
                x.ExpiryDate = C.ExpiryDate;
                x.Available = C.Available;
                db.SaveChangesAsync();
            }
            return RedirectToAction("List");


        }

    }
}
