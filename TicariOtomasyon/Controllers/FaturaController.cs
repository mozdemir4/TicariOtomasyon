using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            var fatura_list = context.Faturalars.ToList();
            return View("Index",fatura_list);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        int sayac = 0;
        [HttpPost]
        public ActionResult FaturaEkle (Faturalar p)
        {
            if (ModelState.IsValid)
            {
                context.Faturalars.Add(p);
                sayac = context.SaveChanges();
                if(sayac > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult FaturaGuncelle (int? id)
        {
            var fatura_id = context.Faturalars.Find(id);    
            return View("FaturaGuncelle", fatura_id);
        }
        
        int kontrol = 0;
        [HttpPost]
        public ActionResult FaturaGuncelle (Faturalar p)
        {
            if (ModelState.IsValid)
            {
                var fatura = context.Faturalars.Find(p.FaturaID);
                fatura.FaturaSeriNo = p.FaturaSeriNo;
                fatura.FaturaSiraNo = p.FaturaSiraNo;
                fatura.VergiDairesi = p.VergiDairesi;
                fatura.Tarih = p.Tarih;
                fatura.Saat = p.Saat;
                fatura.TeslimEden = p.TeslimEden;
                fatura.TeslimAlan = p.TeslimAlan;
                kontrol = context.SaveChanges();
                if(kontrol > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult FaturaDetay (int? id)
        {
            var fatura_listes = context.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View("FaturaDetay", fatura_listes);
        }
        [HttpGet]
        public ActionResult FaturaKalemEkle(int? id)
        {
            var faturalist = context.FaturaKalems.Where(x => x.Faturaid == id).Select(x => x.Faturaid).FirstOrDefault();
            ViewBag.deger = faturalist;
            return View();
        }
        
        int say = 0;
        [HttpPost]
        public ActionResult FaturaKalemEkle (FaturaKalem p)
        {
            if (ModelState.IsValid)
            {
                context.FaturaKalems.Add(p);
                say = context.SaveChanges();
                if(say > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Dinamik()
        {
            IEnumerableFatura en = new IEnumerableFatura();
            en.deger1 = context.Faturalars.ToList();
            en.deger2 = context.FaturaKalems.ToList();
            return View(en);
        }

        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSiraNo, DateTime Tarih, string VergiDairesi,
            string Saat, string TeslimEden, string TeslimAlan, string ToplamTutar, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSiraNo = FaturaSiraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.ToplamTutar = decimal.Parse(ToplamTutar);
            context.Faturalars.Add(f);

            foreach(var x in kalemler)
            {
                FaturaKalem fatura_k = new FaturaKalem();
                fatura_k.Aciklama = x.Aciklama;
                fatura_k.BirimFiyat = x.BirimFiyat;
                fatura_k.Faturaid = x.FaturaKalemID;
                fatura_k.Miktar = x.Miktar;
                fatura_k.Tutar = x.Tutar;
                context.FaturaKalems.Add(fatura_k);
            }
         
            context.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}