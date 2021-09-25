using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        Context context = new Context();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GrafikList()
        {
            var grafik = new Chart(350, 350);
            grafik.AddTitle(text: "Kategori - Ürün Stokları");
            grafik.AddLegend(title: "Stok");
            grafik.AddSeries(
                    name: "Değerler",
                    xValue: new[] { "Telefon", "Bilgisayar", "Küçük Ev Aletleri" },
                    yValues: new[] { 85, 65, 75 }
                );
            grafik.Write();
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult GrafikListesi()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var degerler = context.Uruns.ToList();
            degerler.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            degerler.ToList().ForEach(y => yvalue.Add(y.Stok));
            var grafik = new Chart(500, 500).AddTitle("Stoklar").AddSeries(chartType: "Column", name: "Stok"
                , xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }


        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult()
        {
            return Json(UrunListesi(), JsonRequestBehavior.AllowGet);
        }

        // manuel 
        public List<GoogleCharts> UrunListesi()
        {
            List<GoogleCharts> charts = new List<GoogleCharts>();
            charts.Add(new GoogleCharts()
            {
                urunad = "Bilgisayar",
                stok = 150
            });
            charts.Add(new GoogleCharts()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 60
            });
            charts.Add(new GoogleCharts()
            {
                urunad = "Mobilya",
                stok = 80
            });

            charts.Add(new GoogleCharts()
            {
                urunad = "Mobil Cihazlar",
                stok = 170
            });

            return charts;
        }



        public ActionResult Index3()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult2()
        {
            return Json(UrunListesi2(), JsonRequestBehavior.AllowGet);
        }
        public List<GoogleCharts2> UrunListesi2()
        {
            List<GoogleCharts2> charts2s = new List<GoogleCharts2>();
            using (var context = new Context())
            {
                charts2s = context.Uruns.Select(x => new GoogleCharts2
                {
                    urunadi = x.UrunAd,
                    stoksayisi = x.Stok
                }).ToList();
            }
            return charts2s;
        }

        public ActionResult Index4()
        {
            return View();
        }
        public ActionResult Index5()
        {
            return View();
        }

    }
}