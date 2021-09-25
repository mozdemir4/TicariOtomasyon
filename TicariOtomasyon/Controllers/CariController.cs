using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class CariController : Controller
    {

        Context context = new Context();
        public ActionResult Index()
        {
            var list = context.Caris.Where(x => x.Durum == true).ToList();
            return View("Index", list);
        }
        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariEkle(Cari cari)
        {
            if (ModelState.IsValid)
            {
                cari.Durum = true;
                context.Caris.Add(cari);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult CariSil(int? id)
        {
            var cari = context.Caris.Find(id);
            cari.Durum = false;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CariGuncelle (int? id)
        {
            var cari_id = context.Caris.Find(id);
            return View("CariGuncelle", cari_id);
        }
        [HttpPost]
        public ActionResult CariGuncelle (Cari cari)
        {
            if (ModelState.IsValid)
            {
                var c = context.Caris.Find(cari.CariID);
                c.CariAd = cari.CariAd;
                c.CariSoyad = cari.CariSoyad;
                c.CariSehir = cari.CariSehir;
                c.CariMail = cari.CariMail;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult CariDetay (int? id)
        {
            var liste = context.SatisHarekets.Where(k => k.Cariid == id).ToList();
            var cari_ad = context.Caris.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = cari_ad;
            return View("CariDetay", liste);
        }
    }
}