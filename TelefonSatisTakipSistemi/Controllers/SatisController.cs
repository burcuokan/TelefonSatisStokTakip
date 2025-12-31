using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonSatisTakipSistemi.Models.Entity;
using TelefonSatisTakipSistemi.Models;

namespace TelefonSatisTakipSistemi.Controllers
{
    [Authorize]
    public class SatisController : Controller
    {
        // GET: Satis
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        public ActionResult Index()
        {
            var satis = db.TBLSATİSLAR.ToList();
            return View(satis);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            //Müşteri
            List<SelectListItem> musteri = (from x in db.TBLMUSTERİ
                                            where x.DURUM == true
                                            select new SelectListItem
                                            {
                                                Text = x.AD + " " + x.SOYAD,
                                                Value = x.ID.ToString()

                                            }).ToList();
            ViewBag.mst = musteri;

            //Personel
            List<SelectListItem> personel = (from x in db.TBLPERSONEL.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.AD + " " + x.SOYAD,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.prs = personel;

            //Ürün
            List<SelectListItem> urun = (from x in db.TBLURUNLER
                                         where x.DURUM==true
                                         select new SelectListItem
                                         {
                                             Text = x.MARKA + " " + x.MODEL,
                                             Value = x.ID.ToString()
                                         }).ToList();
            ViewBag.urn = urun;

            // Sayfa ilk kez yüklendiğinde veya sepet henüz oluşturulmamışsa, 
            // Session üzerinde boş bir sepet listesi başlatıyoruz.
            if (Session["Sepet"] == null)
            {

                Session["Sepet"] = new List<SepetUrun>();
            }

            // Session'daki veriyi SepetUrun listesi tipine dönüştürerek değişkene alıyoruz.
            List<SepetUrun> sepet = (List<SepetUrun>)Session["Sepet"];
            return View(sepet);

        }
        [HttpPost]
        public ActionResult SepetEkle(int UrunID, int Adet)
        {
            var urun = db.TBLURUNLER.Find(UrunID);
            // Sepeti hafızadan (Session) çağırıyoruz
            var sepet = (List<SepetUrun>)Session["Sepet"];
            if (sepet == null)
            {
                sepet = new List<SepetUrun>();
            }
            //Yeni bir ürün kutusu (nesne) oluşturup içini dolduruyoruz
            SepetUrun yeniItem = new SepetUrun();
            yeniItem.UrunID = urun.ID;
            yeniItem.UrunTamAd = urun.MARKA + " " + urun.MODEL;
            yeniItem.Adet = Adet;

            // Fiyat kontrolü: Eğer fiyat null ise 0 kabul et
            if (urun.SATİSFİYATİ == null)
            {
                yeniItem.BirimFiyat = 0;
            }
            else
            {
                yeniItem.BirimFiyat = (decimal)urun.SATİSFİYATİ;
            }
            //Hesaplama
            yeniItem.ToplamTutar = yeniItem.Adet * yeniItem.BirimFiyat;
            sepet.Add(yeniItem);
            Session["Sepet"] = sepet;
            return RedirectToAction("YeniSatis");
        }

        [HttpPost]
        public ActionResult SatisYap(int MUSTERIID, int PERSONELID)
        {
            //Hafızadaki sepeti al
            //Sepet boş değilse işlemlere başla
            var sepet = (List<SepetUrun>)Session["Sepet"];
            if (sepet != null && sepet.Count > 0)
            {
                //Ana Fiş
                TBLSATİSLAR yeniSatis = new TBLSATİSLAR();
                yeniSatis.MUSTERİ = MUSTERIID;
                yeniSatis.PERSONEL = PERSONELID;
                yeniSatis.TARİH = DateTime.Now;
                yeniSatis.TUTAR = sepet.Sum(x => x.ToplamTutar);
                db.TBLSATİSLAR.Add(yeniSatis);
                db.SaveChanges();

                //Fiş Detayları
                foreach (var x in sepet)
                {
                    TBLSATİSDETAY detay = new TBLSATİSDETAY();
                    detay.SATİSID = yeniSatis.ID;
                    detay.URUN = x.UrunID;
                    detay.ADET = (short)x.Adet;
                    detay.FİYAT = x.BirimFiyat;
                    db.TBLSATİSDETAY.Add(detay);

                    //Stok Güncelleme
                    var UrunStokGuncelle = db.TBLURUNLER.Find(x.UrunID);
                    if (UrunStokGuncelle!=null)
                    {
                        UrunStokGuncelle.STOK = (int)(UrunStokGuncelle.STOK - x.Adet);
                    }
                }
                db.SaveChanges();
                Session["Sepet"] = null;

            }
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = db.TBLSATİSDETAY.Where(x => x.SATİSID == id).ToList();
            return View(degerler);
        }
    }
}