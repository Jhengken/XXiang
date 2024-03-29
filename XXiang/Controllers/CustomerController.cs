﻿using Microsoft.AspNetCore.Mvc;
using XXiang.Models;
using XXiang.VIewModels;
using System.Text.Json;

namespace XXiang.Controllers
{
    public class CustomerController : Controller
    {
        private readonly dbXContext _conetxt;
        public CustomerController(dbXContext conetxt)
        {
            _conetxt = conetxt;
        }
        public IActionResult List(KeywordViewModel vm)
        {
            IEnumerable<TCustomer> data = null;
            dbXContext db = new dbXContext();
            if (string.IsNullOrEmpty(vm.txtKeyword))
                data = from t in _conetxt.TCustomers
                       select t;
            else
                data = db.TCustomers.Where(t => t.Name.Contains(vm.txtKeyword) ||
                t.Phone.Contains(vm.txtKeyword) ||
                t.Email.Contains(vm.txtKeyword) ||
                t.Birth.ToString().Contains(vm.txtKeyword));
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TCustomer p)
        {
            dbXContext db = new dbXContext();
            _conetxt.TCustomers.Add(p);
            _conetxt.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbXContext db = new dbXContext();
                TCustomer delCustomer = _conetxt.TCustomers.FirstOrDefault(t => t.CustomerId == id);
                if (delCustomer != null)
                {
                    _conetxt.TCustomers.Remove(delCustomer);
                    _conetxt.SaveChangesAsync();
                }
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(TCustomer p)
        {
            dbXContext db = new dbXContext();
            TCustomer x = _conetxt.TCustomers.FirstOrDefault(t => t.CustomerId == p.CustomerId);
            if (x != null)
            {
                x.Name = p.Name;
                x.Email = p.Email;
                x.Phone = p.Phone;
                x.Password = p.Password;
                x.Birth = p.Birth;
                x.CreditCard = p.CreditCard;
                x.CreditPoints = p.CreditPoints;
                x.BlackListed = p.BlackListed;

                db.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                dbXContext db = new dbXContext();
                TCustomer x = _conetxt.TCustomers.FirstOrDefault(t => t.CustomerId == id);
                if (x != null)
                    return View(x);
            }
            return RedirectToAction("List");
        }
        public IActionResult Login(SLoginViewModel vm)
        {
            TCustomer user = (new dbXContext()).TCustomers.FirstOrDefault(
                t => t.Email.Equals(vm.txtAccount) && t.Password.Equals(vm.txtPassword));

            if (user != null && user.Password.Equals(vm.txtPassword))
            {
                string json = JsonSerializer.Serialize(user);
                HttpContext.Session.SetString(Dictionary.SK_LOGINED_USER, json);
                return RedirectToAction("Index");

            }
            return View();
        }

    }
}
