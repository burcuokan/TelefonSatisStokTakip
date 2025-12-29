using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonSatisTakipSistemi.Models.Entity;

namespace TelefonSatisTakipSistemi.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(TBLADMİN p)
        {
            db.TBLADMİN.Add(p);
            db.SaveChanges();
            return RedirectToAction("YeniAdmin");
        }
    }
}