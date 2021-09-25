using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;
namespace TicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            var urun_sayisi = context.Uruns.Distinct().Count();
            ViewBag.urun = urun_sayisi;
            var musteri_sayisi = context.Caris.Count().ToString();
            ViewBag.musteri = musteri_sayisi;

            var yapilacak_list = context.yapilacaks.Where(x=>x.Durum == true).ToList();
            return View("Index",yapilacak_list);
        }
    }
}