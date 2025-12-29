using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelefonSatisTakipSistemi.Models
{
    // Bu sınıf, admin 'Satışı Tamamla' diyene kadar ürünleri hafızada tutar.
    public class SepetUrun
    {
        public int UrunID { get; set; }
        public string UrunTamAd { get; set; }
        public int Adet { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal ToplamTutar { get; set; }

    }
}