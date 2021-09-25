using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            var deger1 = context.Caris.Count().ToString();

            var deger2 = context.Uruns.Count().ToString();

            var deger3 = context.Personels.Count().ToString();

            var deger4 = context.Kategoris.Count().ToString();

            var deger5 = context.Uruns.Sum(x => x.Stok).ToString();

            var deger6 = (from x in context.Uruns select x.Marka).Distinct().Count().ToString();

            var deger7 = context.Uruns.Count(x => x.Stok < 20).ToString();

            var deger8 = (from x in context.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault().ToString();

            var deger9 = (from x in context.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault().ToString();

            var deger10 = context.Uruns.GroupBy(x => x.Marka).OrderByDescending(y => y.Count()).
                Select(z => z.Key).FirstOrDefault().ToString();

            var deger11 = context.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();

            var deger12 = context.Uruns.Count(x => x.UrunAd == "Dizüstü B").ToString();

            //!!!
            var deger13 = context.Uruns.Where(u=>u.UrunID == (context.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(x => x.Count())
                .Select(x => x.Key).FirstOrDefault())).Select(o=>o.UrunAd).FirstOrDefault();

            var deger14 = context.SatisHarekets.Sum(x => x.ToplamTutar).ToString();

            DateTime today = DateTime.Today;
            var deger15 = context.SatisHarekets.Count(x => x.Tarih == today).ToString();

            var deger16 = context.SatisHarekets.Where(x => x.Tarih == today).Sum(x => (decimal?)x.ToplamTutar).ToString();

            ViewBag.dg1 = deger1;
            ViewBag.dg2 = deger2;
            ViewBag.dg3 = deger3;
            ViewBag.dg4 = deger4;
            ViewBag.dg5 = deger5;
            ViewBag.dg6 = deger6;
            ViewBag.dg7 = deger7;
            ViewBag.dg8 = deger8;
            ViewBag.dg9 = deger9;
            ViewBag.dg10 = deger10;
            ViewBag.dg11 = deger11;
            ViewBag.dg12 = deger12;
            ViewBag.dg13 = deger13;
            ViewBag.dg14 = deger14;
            ViewBag.dg15 = deger15;
            ViewBag.dg16 = deger16;
            return View();
        }
        public ActionResult BasitTablolar()
        {
            var sorgu = from x in context.Caris
                        group x by x.CariSehir into g
                        select new SinifGroup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());
        }
        public PartialViewResult Partial1()
        {
            var query = from x in context.Personels
                        group x by x.Departman.DepartmanAd into veri
                        select new SinifGroup2
                        {
                            Departman = veri.Key,
                            Sayi = veri.Count()
                        };
            return PartialView(query.ToList());
        }
        public PartialViewResult Partial2()
        {
            var liste = context.Caris.Where(x=>x.Durum == true).ToList();
            return PartialView("Partial2",liste);
        }
        public PartialViewResult Partial3()
        {
            var listeler = context.Uruns.Where(k => k.Durum == true).ToList();
            return PartialView("Partial3", listeler);
        }
        public PartialViewResult Partial4()
        {
            var query2 = from x in context.Uruns
                         group x by x.Marka into k
                         select new SinifGroup3
                         {
                             sayi = k.Count(),
                             marka = k.Key
                         };
            return PartialView("Partial4", query2.ToList());

        }
    }
}