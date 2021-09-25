using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;

namespace TicariOtomasyon.Controllers
{
    
    public class DepartmanController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            var list = context.Departmen.Where(x => x.Durum == true).ToList();
            return View(list);
        }

        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        
        int kontrol = 0;
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            context.Departmen.Add(d);
            kontrol = context.SaveChanges();
            if (kontrol > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "A")]
        public ActionResult DepartmanSil(int? id)
        {
            var deger = context.Departmen.Find(id);
            deger.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult DepartmanGuncelle(int? id)
        {
            var dep_id = context.Departmen.Find(id);
        
            return View("DepartmanGuncelle", dep_id);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult DepartmanGuncelle (Departman d)
        {
            var id = context.Departmen.Find(d.DepartmanID);
            id.DepartmanAd = d.DepartmanAd;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult DepartmanDetay (int? id)
        {
            var list = context.Personels.Where(x => x.Departmanid == id).ToList();
            var departman = context.Departmen.Where(j => j.DepartmanID == id).Select(k => k.DepartmanAd).FirstOrDefault();
            ViewBag.dept = departman;
            return View(list);
        }
        [Authorize(Roles = "A")]
        public ActionResult DepartmanPersonelSatis (int? id)
        {
            var personel = context.Personels.Where(x => x.PersonelID == id).Select(y => y.PersonelAd+" "+y.PersonelSoyad).FirstOrDefault();
            ViewBag.person = personel;
            var satis_list = context.SatisHarekets.Where(l => l.Personelid == id).ToList();
            return View(satis_list);
        }


    }
}