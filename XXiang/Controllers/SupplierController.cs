using Microsoft.AspNetCore.Mvc;
using XXiang.Models;

namespace XXiang.Controllers
{
    public class SupplierController : Controller
    {
        private readonly dbXContext _conetxt;
        public SupplierController(dbXContext conetxt)
        {
            _conetxt = conetxt;
        }
        public IActionResult List()
        {
            IEnumerable<TSupplier> data=null;
            dbXContext db = new dbXContext();
            data = from t in _conetxt.TSuppliers
                   select t;
            return View(data);
        }

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(TSupplier p)
        {
            _conetxt.TSuppliers.Add(p);
            _conetxt.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(TSupplier p)
        {
            dbXContext db = new dbXContext();
            TSupplier x = _conetxt.TSuppliers.FirstOrDefault(t => t.SupplierId == p.SupplierId);
            if (x != null)
            {

                x.Name = p.Name;
                x.Email = p.Email;
                x.Phone = p.Phone;
                x.Password = p.Password;
                x.Address = p.Address;
                x.CreditPoints = p.CreditPoints;
                _conetxt.SaveChangesAsync();

            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                dbXContext db = new dbXContext();
                TSupplier x = _conetxt.TSuppliers.FirstOrDefault(t => t.SupplierId == id);
                if (x != null)
                    return View(x);
            }
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbXContext db = new dbXContext();
                TSupplier delSupplier = _conetxt.TSuppliers.FirstOrDefault(t => t.SupplierId == id);
                if (delSupplier != null)
                {
                    _conetxt.TSuppliers.Remove(delSupplier);
                    _conetxt.SaveChangesAsync();
                }
            }
            return RedirectToAction("List");
        }

    }
}
