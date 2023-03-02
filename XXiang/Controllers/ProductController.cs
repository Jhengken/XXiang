using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.Json;
using System.Xml.Linq;
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
            Response.Cookies.Append("currentPSiteID", "");

            IEnumerable<TProduct> data = null;

            data = _conetxt.TProducts.Select(x => new TProduct      //Lambda select
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
            Response.Cookies.Append("currentProductID", $"{id}");
            ViewBag.productID = Request.Cookies["currentProductID"];

            IEnumerable<TPsite> data = null;
            data = from t in _conetxt.TPsites
                   where t.ProductId == id
                   select t;
            return View(data);
        }
        public IActionResult PSiteCreate(int? id)
        {
            ViewBag.productID = Request.Cookies["currentProductID"];

            if (id == null)
            {
                return RedirectToAction("ProductList");
            }
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
            ViewBag.productID = Request.Cookies["currentProductID"];

            if (id != null)
            {
                TPsite x = _conetxt.TPsites.FirstOrDefault(t => t.SiteId.Equals(id));
                if (x != null)
                {
                    return View(x);
                }
            }
            return RedirectToAction("PSiteList", new { id = Request.Cookies["currentProductID"] });
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
                        string del = Path.Combine(_environment.WebRootPath, "images", "PSite", x.Image.ToString());
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
            return RedirectToAction("PSiteList", new { id = x.ProductId });
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
            if (Request.Cookies["currentProductID"] != null)
            {
                return RedirectToAction("PSiteList", new { id = Request.Cookies["currentProductID"] });
            }
            return RedirectToAction("ProductList");
        }

        //------------------------------PSiteRoom

        public IActionResult PSiteRoomList(int? id)
        {
            Response.Cookies.Append("currentSiteID", $"{id}");
            ViewBag.productID = Request.Cookies["currentProductID"];
            ViewBag.siteID = Request.Cookies["currentSiteID"];

            IEnumerable<TPsiteRoom> data = null;
            data = _conetxt.TPsiteRooms.Where(t => t.SiteId == id).Select(t => new TPsiteRoom
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
            return View(data);
        }
        public IActionResult PSiteRoomCreate(int? id)
        {
            ViewBag.productID = Request.Cookies["currentProductID"];
            ViewBag.siteID = Request.Cookies["currentSiteID"];

            if (id == null)
            {
                return RedirectToAction("PSiteList", new { id = Request.Cookies["currentProductID"] });
            }
            IEnumerable<SelectListItem> items = new List<SelectListItem>();
            items = _conetxt.TCategories.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.CategoryId.ToString(),
                Selected = t.CategoryId.Equals(3)
            });

            ViewBag.CategoryId = items;
            return View();
        }
        [HttpPost]
        public IActionResult PSiteRoomCreate(TPsiteRoom psr)
        {
            if (psr.photo != null)
            {
                string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".jpg";
                string path = Path.Combine(_environment.WebRootPath, "images", "PSiteRoom", photoName);
                psr.Image = photoName;
                psr.photo.CopyToAsync(new FileStream(path, FileMode.Create));
            }
            _conetxt.TPsiteRooms.Add(psr);
            _conetxt.SaveChanges();
            return RedirectToAction("PSiteRoomList", new { id = psr.SiteId });
        }
        public IActionResult PSiteRoomEdit(int? id)
        {
            ViewBag.siteID = Request.Cookies["currentSiteID"];

            if (id != null)
            {
                TPsiteRoom x = _conetxt.TPsiteRooms.FirstOrDefault(t => t.RoomId == id);
                if (x != null)
                {
                    IEnumerable<SelectListItem> items = new List<SelectListItem>();
                    items = _conetxt.TCategories.Select(t => new SelectListItem
                    {
                        Text = t.Name,
                        Value = t.CategoryId.ToString(),
                        Selected = t.CategoryId.Equals(3)
                    });
                    ViewBag.CategoryId = items;
                    return View(x);
                }
            }
            return RedirectToAction("PSiteRoomList", new { id = Request.Cookies["currentSiteID"] });
        }
        [HttpPost]
        public IActionResult PSiteRoomEdit(TPsiteRoom psr)
        {
            TPsiteRoom x = _conetxt.TPsiteRooms.FirstOrDefault(t => t.RoomId == psr.RoomId);
            if (x != null)
            {
                //x.Image = "";   //試試看能不能null
                if (psr.photo != null)
                {
                    string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ".jpg";
                    string path = Path.Combine(_environment.WebRootPath, "images", "PSiteRoom", photoName);
                    if (x.Image != null)
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        string del = Path.Combine(_environment.WebRootPath, "images", "PSiteRoom", x.Image.ToString());
                        //ContorllerBase也有定義File所以要加System.IO.來準確使用
                        System.IO.File.Delete(del);
                    }
                    x.Image = photoName;
                    psr.photo.CopyTo(new FileStream(path, FileMode.Create));
                }
                x.SiteId = psr.SiteId;
                x.CategoryId = psr.CategoryId;
                x.HourPrice = psr.HourPrice;
                x.DatePrice = psr.DatePrice;
                x.Ping = psr.Ping;
                x.Status = psr.Status;
                x.Description = psr.Description;
                _conetxt.SaveChangesAsync();
            }
            return RedirectToAction("PSiteRoomList", new { id = Request.Cookies["currentSiteID"] });
        }
        public IActionResult PSiteRoomDelete(int? id)
        {
            if (id != null)
            {
                TPsiteRoom del = _conetxt.TPsiteRooms.FirstOrDefault(t => t.RoomId == id);
                if (del != null)
                {
                    _conetxt.TPsiteRooms.Remove(del);
                    _conetxt.SaveChangesAsync();
                }
            }

            if (Request.Cookies["currentSiteID"] != null)
            {
                return RedirectToAction("PSiteRoomList", new { id = Request.Cookies["currentSiteID"] });
            }
            if (Request.Cookies["currentProductID"] != null)
            {
                return RedirectToAction("PSiteList", new { id = Request.Cookies["currentProductID"] });
            }
            return View("ProductList");
        }
    }
}
