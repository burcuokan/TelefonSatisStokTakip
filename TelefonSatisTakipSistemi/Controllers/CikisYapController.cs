using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TelefonSatisTakipSistemi.Controllers
{
    [Authorize]
    public class CikisYapController : Controller
    {
        // GET: CikisYap
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Giris", "GirisYap");

        }
    }
}