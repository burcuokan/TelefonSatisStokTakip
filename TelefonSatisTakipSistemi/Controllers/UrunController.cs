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
    public class UrunController : Controller
    {
        // GET: Urun
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        public ActionResult Index(string p, int sayfa=1)
        {
            //Listeleme ve Arama İşlemi
            var urun = db.TBLURUNLER.Where(x => x.DURUM == true && (string.IsNullOrEmpty(p) || x.MARKA.Contains(p)))
                .ToList().ToPagedList(sayfa,4);
                /*.ToList().ToPagedList(sayfa,4);*/
            return View(urun);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> urun = (from k in db.TBLKATEGORİ.ToList()
                                         select new SelectListItem
                                         {
                                             Text = k.AD,
                                             Value=k.ID.ToString()
                                         }).ToList();
            ViewBag.urn = urun;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> urun = (from k in db.TBLKATEGORİ.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = k.AD,
                                                 Value = k.ID.ToString()
                                             }).ToList();
                ViewBag.urn = urun;
                return View("YeniUrun");
            }
            var urn = db.TBLKATEGORİ.Where(x => x.ID == p.TBLKATEGORİ.ID).FirstOrDefault();
            p.TBLKATEGORİ = urn;
            p.DURUM = true;
            db.TBLURUNLER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> urun = (from k in db.TBLKATEGORİ.ToList()
                                         select new SelectListItem
                                         {
                                             Text = k.AD,
                                             Value = k.ID.ToString()
                                         }).ToList();
            ViewBag.urn = urun;
            var urunbul = db.TBLURUNLER.Find(id);
            return View("UrunGetir", urunbul);
        }
        public ActionResult UrunGuncelle(TBLURUNLER p)
        {
            var kategori = db.TBLKATEGORİ.Where(x => x.ID == p.TBLKATEGORİ.ID).FirstOrDefault();
            var urun = db.TBLURUNLER.Find(p.ID);
            urun.MARKA = p.MARKA;
            urun.MODEL = p.MODEL;
            urun.KATEGORİ = kategori.ID;
            urun.STOK = p.STOK;
            urun.ALİSFİYATİ = p.ALİSFİYATİ;
            urun.SATİSFİYATİ = p.SATİSFİYATİ;
            urun.ACİKLAMA = p.ACİKLAMA;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.ID);
            urun.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}