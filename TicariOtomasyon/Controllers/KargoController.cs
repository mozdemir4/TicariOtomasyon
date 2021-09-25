using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        Context context = new Context();
        public ActionResult Index(string p)
        {
            var kargo_listesi = from x in context.KargoDetays select x;
            if (!string.IsNullOrEmpty(p))
            {
                kargo_listesi = kargo_listesi.Where(k => k.TakipKodu.Contains(p));
            }
            return View("Index",kargo_listesi.ToList());
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D","E","F","G" };
            int k1, k2, k3;
            k1 = rnd.Next(0, karakterler.Length); // karakterler dizindeki harfleri karıştırmak için k1,k2,k3 
            k2 = rnd.Next(0, karakterler.Length); // harflerli karıştırıyoruz bu sayede
            k3 = rnd.Next(0, karakterler.Length);

            int s1, s2, s3;
            s1 = rnd.Next(100, 1000); 
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);

            string kod = s1 + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.takip = kod;
            return View();
        }
        
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay kargo)
        {
            if (ModelState.IsValid)
            {
                context.KargoDetays.Add(kargo);
                context.SaveChanges();
                return RedirectToAction("Index", "Kargo");
            }
            return View();
        }
        public ActionResult KargoTakip(string id)
        {         
            var kargo = context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(kargo);
        }
    }
}