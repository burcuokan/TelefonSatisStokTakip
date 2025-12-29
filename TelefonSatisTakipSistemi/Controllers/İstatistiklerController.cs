using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonSatisTakipSistemi.Models.Entity;

namespace TelefonSatisTakipSistemi.Controllers
{
    [Authorize]
    public class İstatistiklerController : Controller
    {
        // GET: İstatistikler
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        public ActionResult Index()
        {
            //Toplam Ürün Sayısı
            ViewBag.toplamurun = db.TBLURUNLER.Where(x => x.DURUM == true).Count();

            //En Fazla Stoklu Ürün
            ViewBag.stok = db.TBLURUNLER.Where(x => x.DURUM == true).OrderByDescending(x => x.STOK)
                .Select(y => y.MARKA + " " + y.MODEL).FirstOrDefault();

            //En Az Stoklu Ürün
            ViewBag.dusukstok = db.TBLURUNLER.Where(x => x.DURUM == true).OrderBy(x => x.STOK)
                .Select(y => y.MARKA + " " + y.MODEL).FirstOrDefault();

            //En Çok Satılan Ürün
            ViewBag.stokurun = db.TBLSATİSDETAY.Where(x => x.TBLURUNLER.DURUM == true) .GroupBy(x => x.URUN).OrderByDescending(y => y.Count())
                .Select(z => z.FirstOrDefault().TBLURUNLER.MODEL).FirstOrDefault();

            //Toplam Müşteri
            ViewBag.toplammusteri = db.TBLMUSTERİ.Where(x => x.DURUM == true).Count();

            //Toplam Tutar
            ViewBag.toplamtutar = db.TBLSATİSLAR.Sum(x => x.TUTAR);

            //En Çok ürünü Olan Kategori
            ViewBag.urun = db.TBLURUNLER.Where(x => x.DURUM == true).GroupBy(x => x.TBLKATEGORİ.AD).OrderByDescending(y => y.Count())
                .Select(y => y.Key).FirstOrDefault();

            //En Çok Satış Yapan Persomel
            ViewBag.personel = db.TBLSATİSLAR.GroupBy(x => x.TBLPERSONEL.AD + " " + x.TBLPERSONEL.SOYAD)
                .OrderByDescending(y => y.Count()).Select(y => y.Key).FirstOrDefault();

            //En Çok Alışveriş Yapan Müşteri
            ViewBag.musteri = db.TBLSATİSLAR.GroupBy(x => x.TBLMUSTERİ.AD + " " + x.TBLMUSTERİ.SOYAD)
                .OrderByDescending(y => y.Count()).Select(z => z.Key).FirstOrDefault();

            return View();           
        }
    }
}