using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
using System.Web.Security;

namespace TicariOtomasyon.Controllers
{

    public class KategoriController : Controller
    {
        Context context = new Context();


        public ActionResult Index(int sayfa = 1)
        {
            var list = context.Kategoris.ToList().ToPagedList(sayfa, 5);
            return View(list);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            var kategori_ekle = context.Kategoris.Add(k);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int? id)
        {
            var k_id = context.Kategoris.Find(id);
            context.Kategoris.Remove(k_id);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KategoriGuncelle(int? id)
        {
            var k_id = context.Kategoris.Find(id);
            return View(k_id);
        }
        int sayac = 0;
        [HttpPost]
        public ActionResult KategoriGuncelle(Kategori kt)
        {
            var kategori_id = context.Kategoris.Find(kt.KategoriID);
            kategori_id.KategoriAD = kt.KategoriAD;
            sayac = context.SaveChanges();
            if (sayac > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult CascadingIslemi()
        {
            Cascading cs = new Cascading();
            cs.Kategoriler = new SelectList(context.Kategoris, "KategoriID", "KategoriAD");
            cs.Urunler = new SelectList(context.Uruns, "UrunID", "UrunAd");
            return View(cs);
        }
        public JsonResult UrunGetir(int p)
        {
            var urunlistesi = (from x in context.Uruns
                              join y in context.Kategoris
                              on x.Kategori.KategoriID equals y.KategoriID
                              where x.Kategori.KategoriID == p
                              select new
                              {
                                  Text = x.UrunAd,
                                  Value = x.UrunID.ToString()
                              }
                              ).ToList();
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
    }
}