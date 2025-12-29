using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonSatisTakipSistemi.Models.Entity;

namespace TelefonSatisTakipSistemi.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        // GET: Profil
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        [HttpGet]
        public ActionResult Index()
        {
            //Giriş yapan kullanıcının adı
            var kullaniciadi = User.Identity.Name;
            var model = db.TBLADMİN.FirstOrDefault(x => x.KULLANİCİADİ == kullaniciadi);
            return View(model);
        }
        [HttpPost]
        public ActionResult ProfilGuncelle(TBLADMİN p,string SifreTekrar)
        {
            if (p.SİFRE!=SifreTekrar)
            {
                ViewBag.hata = "Şifreler birbirleriyle uyuşmuyor!";
               
                var kullaniciadi = User.Identity.Name;
                var model = db.TBLADMİN.FirstOrDefault(x => x.KULLANİCİADİ == kullaniciadi);
                return View("Index",model);
            }
            var admin = db.TBLADMİN.Find(p.ID);
            admin.KULLANİCİADİ = p.KULLANİCİADİ;
            admin.SİFRE = p.SİFRE;
            db.SaveChanges();
            TempData["Mesaj"] = "Güncellendi";
            return RedirectToAction("Index");
        }
    }
}