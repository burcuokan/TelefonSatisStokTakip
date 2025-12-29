using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonSatisTakipSistemi.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace TelefonSatisTakipSistemi.Controllers
{
    [Authorize]
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        public ActionResult Index(string p, int sayfa=1)
        {
            var musteri = db.TBLMUSTERİ.Where(x => x.DURUM == true &&(string.IsNullOrEmpty(p)||x.AD.Contains(p)||x.SOYAD.Contains(p)))
                .ToList().ToPagedList(sayfa, 3);
            return View(musteri);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERİ p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.DURUM = true;
            db.TBLMUSTERİ.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TBLMUSTERİ.Find(id);
            return View("MusteriGetir",musteri);
        }
        public ActionResult MusteriGuncelle(TBLMUSTERİ p)
        {
            var musteri = db.TBLMUSTERİ.Find(p.ID);
            musteri.AD = p.AD;
            musteri.SOYAD = p.SOYAD;
            musteri.SEHİR = p.SEHİR;
            musteri.TELEFON = p.TELEFON;
            musteri.EPOSTA = p.EPOSTA;
            musteri.ADRES = p.ADRES;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(TBLMUSTERİ p)
        {
            var musteri = db.TBLMUSTERİ.Find(p.ID);
            musteri.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}