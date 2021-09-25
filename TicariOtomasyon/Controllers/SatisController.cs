using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context context = new Context();

        public ActionResult Index()
        {
            var list = context.SatisHarekets.ToList();
            return View("Index", list);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> liste = (from x in context.Uruns.Where(y => y.Durum == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.UrunAd,
                                              Value = x.UrunID.ToString()
                                          }).ToList();

            List<SelectListItem> liste2 = (from x in context.Caris.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd,
                                               Value = x.CariID.ToString()
                                           }).ToList();


            List<SelectListItem> liste3 = (from x in context.Personels.Where(x=>x.Departman.DepartmanAd == "Satış").ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.urunlist = liste;
            ViewBag.carilist = liste2;
            ViewBag.personellist = liste3;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket satis)
        {
            if (ModelState.IsValid)
            {
                satis.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
                context.SatisHarekets.Add(satis);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult SatisGuncelle (int? id)
        {
            List<SelectListItem> liste = (from x in context.Uruns.Where(y => y.Durum == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.UrunAd,
                                              Value = x.UrunID.ToString()
                                          }).ToList();

            List<SelectListItem> liste2 = (from x in context.Caris.Where(x => x.Durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd,
                                               Value = x.CariID.ToString()
                                           }).ToList();


            List<SelectListItem> liste3 = (from x in context.Personels.Where(x => x.Departman.DepartmanAd == "Satış").ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.urunlist = liste;
            ViewBag.carilist = liste2;
            ViewBag.personellist = liste3;
            var satis_id = context.SatisHarekets.Find(id);
            return View("SatisGuncelle", satis_id);
        }
        [HttpPost]
        public ActionResult SatisGuncelle (SatisHareket p)
        {
            if (ModelState.IsValid)
            {
                var satis_id = context.SatisHarekets.Find(p.SatisID);
                satis_id.Urunid = p.Urunid;
                satis_id.Personelid = p.Personelid;
                satis_id.Cariid = p.Cariid;
                satis_id.Adet = p.Adet;
                satis_id.Fiyat = p.Fiyat;
                satis_id.ToplamTutar = p.ToplamTutar;
                satis_id.Tarih = p.Tarih;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult SatisDetay (int? id)
        {
            var list = context.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View("SatisDetay", list);
        }

    }
}