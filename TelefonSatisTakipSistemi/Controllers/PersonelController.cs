using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonSatisTakipSistemi.Models.Entity;

namespace TelefonSatisTakipSistemi.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        // GET: Personel
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        public ActionResult Index()
        {
            var personel = db.TBLPERSONEL.ToList();
            return View(personel);
        }
        [HttpGet]
        public ActionResult YeniPersonel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniPersonel(TBLPERSONEL p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniPersonel");
            }
            db.TBLPERSONEL.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var personel = db.TBLPERSONEL.Find(id);
            return View("PersonelGetir", personel);
        }
        public ActionResult PersonelGuncelle(TBLPERSONEL p)
        {
            var personel = db.TBLPERSONEL.Find(p.ID);
            personel.AD = p.AD;
            personel.SOYAD = p.SOYAD;
            personel.DEPARTMAN = p.DEPARTMAN;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelSil(TBLPERSONEL p)
        {
            var personel = db.TBLPERSONEL.Find(p.ID);
            db.TBLPERSONEL.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}