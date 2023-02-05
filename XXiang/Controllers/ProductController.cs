using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
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
            //data = from t in _conetxt.TProducts                    //LinQ只select想要的
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
        public IActionResult ProductCreate(TProduct p)
        {
            TSupplier supplier = _conetxt.TSuppliers.FirstOrDefault(t => t.Name.Equals(p.SupplierName));
            if (supplier != null)
            {
                p.SupplierId = supplier.SupplierId;
                _conetxt.TProducts.Add(p);
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
        public IActionResult ProductEdit(TProduct p)
        {
            TProduct x = _conetxt.TProducts.FirstOrDefault(t => t.ProductId == p.ProductId);
            if (x != null)
            {
                x.Name = p.Name;
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

        //------------------------------PSite

        public IActionResult PSiteList(int? id)
        {
            IEnumerable<TPsite> data = null;
            data = from t in _conetxt.TPsites
                   where t.ProductId == id
                   select t;
            ViewBag.productID = id;
            return View(data);
        }
        public IActionResult PSiteCreate(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ProductList");
            }
            ViewBag.productID = id;
            return View();
        }
        [HttpPost]
        public IActionResult PSiteCreate(TPsite ps)
        {
            if (ps.photo != null)
            {
                string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".jpg";
                string path = Path.Combine(_environment.WebRootPath, "images", "PSite", photoName); //_environment.WebRootPath + "/images/" + photoName;
                ps.Image = photoName;
                ps.photo.CopyToAsync(new FileStream(path, FileMode.Create));
            }
            _conetxt.TPsites.Add(ps);
            _conetxt.SaveChanges();
            return RedirectToAction("PSiteList", new { id = ps.ProductId });
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
        public IActionResult PSiteEdit(TPsite ps)
        {
            TPsite x = _conetxt.TPsites.FirstOrDefault(t => t.SiteId == ps.SiteId);
            if (x != null)
            {
                if (ps.photo != null)
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
                    ps.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
                x.ProductId = ps.ProductId;
                x.Name = ps.Name;
                x.Address = ps.Address;
                x.Description = ps.Description;
                x.Latitude = ps.Latitude;
                x.Longitude = ps.Longitude;
                x.OpenTime = ps.OpenTime;
                _conetxt.SaveChangesAsync();
            }
            return RedirectToAction("PSiteList", new {id= x.ProductId });
        }
        public IActionResult PSiteDelete(int? id,int? productID)
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
            if (productID != null)
            {
                return RedirectToAction("PSiteList", new { id = productID });
            }
            return RedirectToAction("ProductList");
        }

        //------------------------------PSiteRoom

        public IActionResult PSiteRoomList(int? id,int? productID)
        {
            IEnumerable<TPsiteRoom> data = null;
            data = _conetxt.TPsiteRooms.Where(t=>t.SiteId==id).Select(t => new TPsiteRoom
            {
                RoomId = t.RoomId,
                SiteId = t.SiteId,
                CategoryId = t.CategoryId,
                HourPrice = t.HourPrice,
                DatePrice = t.DatePrice,
                Ping = t.Ping,
                Image = t.Image,
                Status = t.Status,
                Description = t.Description
            });
            ViewBag.siteID = id;
            ViewBag.productID = productID;
            return View(data);
        }
        public IActionResult PSiteRoomCreate(int? id,int? productID)         //看要不要用session存
        {
            if (id == null)
            {
                return RedirectToAction("PSiteList", new { id = productID });
            }
            ViewBag.siteID = id;
            return View();
        }
        [HttpOptions]
        public IActionResult PSiteRoomCreate(TPsiteRoom psr)       //做到這裡
        {
            return View();
        }
        public IActionResult PSiteRoomEdit()
        {
            return View();
        }
        [HttpOptions]
        public IActionResult PSiteRoomEdit(TPsiteRoom psr)
        {
            return View();
        }
        public IActionResult PSiteRoomDelete()
        {
            return View();
        }
    }
}
