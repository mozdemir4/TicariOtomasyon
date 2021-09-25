using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        Context context = new Context();
        Enumarable_ ortak_sinif = new Enumarable_();
        public ActionResult Index()
        {
            ortak_sinif.Tablo1 = context.Uruns.Where(x => x.UrunID == 1).ToList();
            ortak_sinif.Tablo2 = context.urunDetays.Where(x => x.DetayID == 2).ToList();
            return View("Index",ortak_sinif);
        }
    }
}