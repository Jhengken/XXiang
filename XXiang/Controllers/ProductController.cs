using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using XXiang.Models;

namespace XXiang.Controllers
{
    public class ProductController : Controller
    {
        private readonly dbXContext _conetxt;
        IWebHostEnvironment _environment;
        public ProductController(dbXContext conetxt, IWebHostEnvironment environment)
        {
            _conetxt = conetxt;
            _environment = environment;
        }
        public IActionResult ProductList()
        {
            IEnumerable<TProduct> data = null;
            //data = from t in _conetxt.TProducts      //只select想要的
            //       select new TProduct 
            //       { 
            //           ProductId=t.ProductId,
            //           Name=t.Name,
            //           SupplierId = t.SupplierId
            //       };
            data = _conetxt.TProducts.Select(x => new TProduct      //Lambda只select想要的
            {
                ProductId = x.ProductId,
                Name = x.Name,
                SupplierId = x.SupplierId
            });

            return View(data);
        }
        public IActionResult ProductCreate()
        {
            IEnumerable<string> dl = from t in _conetxt.TSuppliers
                                    select t.Name;
            ViewBag.datalist = dl;
            return View();
        }
        [HttpPost]
        public IActionResult ProductCreate(TProduct tp)
        {
            TSupplier supplier = _conetxt.TSuppliers.FirstOrDefault(t => t.Name.Equals(tp.SupplierName));
            if (supplier != null)
            {
                tp.SupplierId = supplier.SupplierId;
                _conetxt.TProducts.Add(tp);
                _conetxt.SaveChangesAsync();
                return RedirectToAction("ProductList");
            }
            return RedirectToAction("ProductCreate");
        }
        public IActionResult ProductEdit(int? id)
        {
            if (id != null)
            {
                TProduct x = _conetxt.TProducts.FirstOrDefault(t => t.ProductId == id);
                if (x != null)
                return View(x);
            }
            return RedirectToAction("ProductList");
        }
        [HttpPost]
        public IActionResult ProductEdit(TProduct tp)
        {
            TProduct x = _conetxt.TProducts.FirstOrDefault(t => t.ProductId == tp.ProductId);
            if (x != null)
            {
                x.Name = tp.Name;
                _conetxt.SaveChangesAsync();
            }
            return RedirectToAction("ProductList");
        }
        public IActionResult ProductDelete(int? id)
        {
            if (id != null)
            {
                TProduct del = _conetxt.TProducts.FirstOrDefault(t => t.ProductId == id);
                if (del != null)
                {
                    _conetxt.TProducts.Remove(del);
                    _conetxt.SaveChangesAsync();
                }
            }
            return RedirectToAction("ProductList");
        }


        //PSite
        public IActionResult PSiteList()
        {
            IEnumerable<TPsite> data = null;
            data = from t in _conetxt.TPsites
                   select t;
            return View(data);
        }
        public IActionResult PSiteCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PSiteCreate(TPsite tp)
        {
            if (tp.photo != null)
            {
                string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".jpg";
                string path = Path.Combine(_environment.WebRootPath, "images", "PSite", photoName); //_environment.WebRootPath + "/images/" + photoName;
                tp.Image = photoName;
                tp.photo.CopyToAsync(new FileStream(path, FileMode.Create));
            }
            _conetxt.TPsites.Add(tp);
            _conetxt.SaveChanges();
            return RedirectToAction("PSiteList");
        }
        public IActionResult PSiteEdit(int? id)
        {
            if (id != null)
            {
                TPsite x = _conetxt.TPsites.FirstOrDefault(t => t.SiteId.Equals(id));
                if (x != null)
                {
                    return View(x);
                }
            }
            return View("PSiteList");
        }
        [HttpPost]
        public IActionResult PSiteEdit(TPsite tp)
        {
            TPsite x = _conetxt.TPsites.FirstOrDefault(t => t.SiteId == tp.SiteId);
            if (x != null)
            {
                if (tp.photo != null)
                {
                    string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".jpg";
                    string path = Path.Combine(_environment.WebRootPath, "images", "PSite", photoName);
                    if (x.Image != null)                        //刪除原有的檔案
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        string del = _environment.WebRootPath + "/images/" + x.Image.ToString();
                        //ContorllerBase也有定義File所以要加System.IO.來準確使用
                        System.IO.File.Delete(del);            //靜態，所以沒有dispose
                    }
                    x.Image = photoName;
                    tp.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
                x.Name = tp.Name;
                x.Address = tp.Address;
                x.Description = tp.Description;
                x.Latitude = tp.Latitude;
                x.Longitude = tp.Longitude;
                x.OpenTime = tp.OpenTime;
                _conetxt.SaveChangesAsync();
            }
            return RedirectToAction("PSiteList");
        }
        public IActionResult PSiteDelete(int? id)
        {
            if (id != null)
            {
                TPsite del = _conetxt.TPsites.FirstOrDefault(t => t.SiteId == id);
                if (del != null)
                {
                    _conetxt.TPsites.Remove(del);
                    _conetxt.SaveChangesAsync();
                }
            }
            return RedirectToAction("PSiteList");
        }




        //PSiteRoom
        public IActionResult PSiteRoomList()
        {
            return View();
        }
        public IActionResult PSiteRoomCreate()
        {
            return View();
        }
        public IActionResult PSiteRoomEdit()
        {
            return View();
        }
        public IActionResult PSiteRoomDelete()
        {
            return View();
        }
    }
}
