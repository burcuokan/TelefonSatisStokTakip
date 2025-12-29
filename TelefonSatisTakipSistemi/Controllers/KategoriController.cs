using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonSatisTakipSistemi.Models.Entity;

namespace TelefonSatisTakipSistemi.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        public ActionResult Index()
        {
            var kategori = db.TBLKATEGORİ.ToList();
            return View(kategori);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORİ p)
        {
            db.TBLKATEGORİ.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
           var kategori= db.TBLKATEGORİ.Find(id);
            return View("KategoriGetir",kategori);
        }
        public ActionResult KategoriGuncelle(TBLKATEGORİ p)
        {
            var kategori = db.TBLKATEGORİ.Find(p.ID);
            kategori.AD = p.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBLKATEGORİ.Find(id);
            db.TBLKATEGORİ.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}