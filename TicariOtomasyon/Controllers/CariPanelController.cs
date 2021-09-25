using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context context = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var session_mail = Session["CariMail"].ToString();
            var deger = context.Mesajlars.Where(x => x.Alici == session_mail).ToList();
            ViewBag.mail = session_mail;
            //sessiona atanan mailin idsini getiriyoruz ve sipariste kullanıyoruz.
            var mail_id = context.Caris.Where(x => x.CariMail == session_mail).Select(y => y.CariID).FirstOrDefault();
            
            var toplam_siparis = context.SatisHarekets.Where(x => x.Cariid == mail_id).Count();
            ViewBag.siparis = toplam_siparis;

            //toplam tutar
            var toplam_tutar = context.SatisHarekets.Where(x => x.Cariid == mail_id).Sum(y => y.ToplamTutar);
            ViewBag.toplam_tutar = toplam_tutar;

            //toplam alınan ürün adedi
            var toplam_adet = context.SatisHarekets.Where(x => x.Cariid == mail_id).Sum(y => y.Adet);
            ViewBag.toplam_adet = toplam_adet;

            //cari ad-soyad
            var adsoyad = context.Caris.Where(x => x.CariMail == session_mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad_ = adsoyad;

            //cari mail
            var mail = context.Caris.Where(x => x.CariMail == session_mail).Select(y => y.CariMail).FirstOrDefault();
            ViewBag.mail_adresi = mail;

            var sehir = context.Caris.Where(x => x.CariMail == session_mail).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.sehir = sehir;

            //cari telefon
            var telefon = context.Caris.Where(x => x.CariMail == session_mail).Select(y => y.CariTelefon).FirstOrDefault();
            ViewBag.telefon = telefon;
            return View("Index",deger);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var session_mail = (string)(Session["CariMail"]);
            var id = context.Caris.Where(x => x.CariMail == session_mail).Select(y => y.CariID).FirstOrDefault();

            var listele = context.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(listele);
        }

        public ActionResult GelenMesajlar()
        {

            var session_mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x=>x.Alici==session_mail).OrderByDescending(x=>x.MesajID).ToList();

            var gelensayisi = context.Mesajlars.Count(x => x.Alici == session_mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == session_mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }

        public ActionResult GidenMesajlar()
        {
            var session_mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x => x.Gonderici == session_mail).OrderByDescending(x=>x.MesajID).ToList();

            var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == session_mail).ToString();
            ViewBag.d2 = gidensayisi;

            var gelensayisi = context.Mesajlars.Count(x => x.Alici == session_mail).ToString();
            ViewBag.d1 = gelensayisi;
            return View(mesajlar);
        }

        public ActionResult MesajDetay(int? id)
        {
            var listeler = context.Mesajlars.Where(x => x.MesajID == id).ToList();

            var session_mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x => x.Alici == session_mail).ToList();

            var gelensayisi = context.Mesajlars.Count(x => x.Alici == session_mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == session_mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View("MesajDetay",listeler);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var session_mail = (string)Session["CariMail"];

            var gelensayisi = context.Mesajlars.Count(x => x.Alici == session_mail).ToString();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == session_mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {    
            if (ModelState.IsValid)
            {
                var session_mail = (string)Session["CariMail"];
                m.Gonderici = session_mail;

                m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());

                context.Mesajlars.Add(m);
                context.SaveChanges();
                return RedirectToAction("GelenMesajlar", "CariPanel");
            }
            return View();
        }
        public ActionResult KargoTakip(string p)
        {
            var kargo_listesi = from x in context.KargoDetays select x;
            kargo_listesi = kargo_listesi.Where(k => k.TakipKodu.Contains(p));  
            return View(kargo_listesi.ToList());
          
        }
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public PartialViewResult PartialDuyuru()
        {
            var listele = context.Mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView("PartialDuyuru",listele);
        }

        
        public PartialViewResult PartialForm()
        {
            var session = Session["CariMail"].ToString();
            var id = context.Caris.Where(x => x.CariMail == session).Select(y => y.CariID).FirstOrDefault();
            var cari_bul = context.Caris.Find(id);
            return PartialView("PartialForm",cari_bul);
        }
      
        public ActionResult CariBilgiGuncelle(Cari cari)
        {
            if (ModelState.IsValid)
            {
                var id = context.Caris.Find(cari.CariID);
                id.CariAd = cari.CariAd;
                id.CariSoyad = cari.CariSoyad;
                id.CariSehir = cari.CariSehir;
                id.CariSifre = cari.CariSifre;
                context.SaveChanges();
                return RedirectToAction("Index", "CariPanel");
            }
            return PartialView("PartialForm");
          
        }
       

    }

}