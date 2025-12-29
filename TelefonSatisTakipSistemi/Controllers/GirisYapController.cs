using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelefonSatisTakipSistemi.Models.Entity;
using System.Web.Security;

namespace TelefonSatisTakipSistemi.Controllers
{
    public class GirisYapController : Controller
    {
       
        // GET: GirisYap
        DbTelefonSatisEntities db = new DbTelefonSatisEntities();
        [AllowAnonymous]
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(TBLADMİN p)
        {
            var giris = db.TBLADMİN.FirstOrDefault(x => x.KULLANİCİADİ == p.KULLANİCİADİ && x.SİFRE == p.SİFRE);
            if (giris != null)
            {
                FormsAuthentication.SetAuthCookie(giris.KULLANİCİADİ, false);
                return RedirectToAction("Index", "Urun");
            }
            else
            {
                return View();
            }

        }
    }
}