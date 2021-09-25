using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            var liste = context.Personels.ToList();
            return View("Index", liste);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> degerler = (from x in context.Departmen.ToList()
                                             select new SelectListItem
                                             {
                                                 Value = x.DepartmanID.ToString(),
                                                 Text = x.DepartmanAd
                                             }).ToList();

            ViewBag.deger = degerler;
            return View("PersonelEkle");
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string dosya_adi = Path.GetFileName(Request.Files[0].FileName);
                    string dosya_uzantisi = Path.GetExtension(Request.Files[0].FileName);
                    string dosya_yolu = "~/Image1/" + dosya_adi + dosya_uzantisi;
                    Request.Files[0].SaveAs(Server.MapPath(dosya_yolu));
                    p.PersonelGorsel = "~/Image1/" + dosya_adi + dosya_uzantisi;
                  
                }
                context.Personels.Add(p);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpGet]
        public ActionResult PersonelGuncelle(int? id)
        {
            List<SelectListItem> liste = (from y in context.Departmen.ToList()
                                          select new SelectListItem
                                          {
                                              Text = y.DepartmanAd,
                                              Value = y.DepartmanID.ToString()
                                          }).ToList();
            ViewBag.deplist = liste;

            var personel_id = context.Personels.Find(id);
            return View("PersonelGuncelle", personel_id);
        }
        [HttpPost]
        public ActionResult PersonelGuncelle(Personel parametre)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    string dosya_adi = Path.GetFileName(Request.Files[0].FileName);
                    string dosya_uzantisi = Path.GetExtension(Request.Files[0].FileName);
                    string dosya_yolu = "~/Image/" + dosya_adi + dosya_uzantisi;
                    Request.Files[0].SaveAs(Server.MapPath(dosya_yolu));
                    parametre.PersonelGorsel = "~/Image/" + dosya_adi + dosya_uzantisi;

                }
                var personel = context.Personels.Find(parametre.PersonelID);
                personel.PersonelAd = parametre.PersonelAd;
                personel.PersonelSoyad = parametre.PersonelSoyad;
                personel.PersonelGorsel = parametre.PersonelGorsel;
                personel.Departmanid = parametre.Departmanid;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult PersonelDetay()
        {
            var liste = context.Personels.ToList();
            return View("PersonelDetay", liste);
        }
    }
}