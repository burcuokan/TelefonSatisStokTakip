using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonSatisTakipSistemi.Models.Entity;
using System.Data.Entity;

namespace TelefonSatisTakipSistemi.Controllers
{
    [Authorize]
    public class RaporlarController : Controller
    {
        // GET: Raporlar
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        public ActionResult Index(DateTime? tarih)
        {
            //Eğer kullanıcı takvimden bir tarih seçmişse sonucu getir
            //Eğer kullanıcı takvimden bir tarih seçmediyse bugünün satışlarını getir
            if (tarih!=null)
            {
                var degerler = db.TBLSATİSLAR
                         .Where(x => DbFunctions.TruncateTime(x.TARİH) == DbFunctions.TruncateTime(tarih)).ToList();
                ViewBag.secilen = tarih.Value.ToString("yyyy-MM-dd");
                return View(degerler);
            }
            else
            {
                var bugun = DateTime.Today;
                var degerler = db.TBLSATİSLAR
                                 .Where(x => DbFunctions.TruncateTime(x.TARİH) == DbFunctions.TruncateTime(bugun)).ToList();
                ViewBag.secilen = bugun.ToString("yyyy-MM-dd");
                return View(degerler);
            }

        }
    }
}