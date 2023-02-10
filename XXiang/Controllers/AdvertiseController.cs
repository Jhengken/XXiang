using Microsoft.AspNetCore.Mvc;
using XXiang.Models;

namespace XXiang.Controllers
{
    public class AdvertiseController : Controller
    {
        //建立連線
        private readonly dbXContext db;
        public AdvertiseController(dbXContext dbXContext)
        {
            db = dbXContext;
        }

        //建立清單
        public IActionResult List()
        {
            IEnumerable<TAdvertise> ads = null;
            ads = from t in db.TAdvertises select t;
            return View(ads);
        }


        //新建廣告類型
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TAdvertise advertise)
        {
            db.TAdvertises.Add(advertise);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        //編輯廣告類型
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {

                TAdvertise ads = db.TAdvertises.FirstOrDefault(t => t.AdvertiseId == id);
                if (ads != null)
                    return View(ads);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Edit(TAdvertiseMetadata a)
        {
            TAdvertise ads = db.TAdvertises.FirstOrDefault(t => t.AdvertiseId == a.AdvertiseId);
            if (ads != null)
            {
                ads.Name = a.Name;
                ads.DatePrice = a.DatePrice;
                db.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
    }
}
