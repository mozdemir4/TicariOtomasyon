using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
namespace TicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {

        Context context = new Context();
        public ActionResult Index(string p)
        {
            var urunler = from x in context.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.UrunAd.Contains(p));
            }
            return View("Index", urunler.ToList());

        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from x in context.Kategoris.ToList()
                                             select new SelectListItem
                                             {
                                                 Value = x.KategoriID.ToString(),
                                                 Text = x.KategoriAD
                                             }).ToList();
            ViewBag.deger = degerler;
            return View();
        }
        int kontrol = 0;
        [HttpPost]
        public ActionResult UrunEkle(Urun x)
        {
            context.Uruns.Add(x);
            kontrol = context.SaveChanges();
            if (kontrol > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult UrunSil(int? id)
        {
            var urun_id = context.Uruns.Find(id);
            urun_id.Durum = false;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UrunGuncelle(int? id)
        {
            var urun_id = context.Uruns.Find(id);
            List<SelectListItem> kategori_listesi = (from x in context.Kategoris.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Value = x.KategoriID.ToString(),
                                                         Text = x.KategoriAD
                                                     }).ToList();
            ViewBag.liste = kategori_listesi;
            return View("UrunGuncelle", urun_id);
        }
        [HttpPost]
        public ActionResult UrunGuncelle(Urun x)
        {
            var id = context.Uruns.Find(x.UrunID);
            id.UrunAd = x.UrunAd;
            id.Marka = x.Marka;
            id.Stok = x.Stok;
            id.AlisFiyat = x.AlisFiyat;
            id.SatisFiyat = x.SatisFiyat;
            id.Kategoriid = x.Kategoriid;
            id.Durum = x.Durum;
            id.UrunGorsel = x.UrunGorsel;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult UrunPdf()
        {
            var urun_list = context.Uruns.ToList();
            return View("UrunPdf", urun_list);
        }

        [HttpGet]
        public ActionResult SatisEkle(int? id)
        {
            var urun_id = context.Uruns.Find(id);
            ViewBag.urun = urun_id.UrunID;

            ViewBag.fiyat = urun_id.SatisFiyat;

            List<SelectListItem> personel_list = (from x in context.Personels.Where(x=>x.Departman.DepartmanAd == "Satış").ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.PersonelAd +" "+ x.PersonelSoyad,
                                                      Value = x.PersonelID.ToString()
                                                  }).ToList();
            ViewBag.pliste = personel_list;

            List<SelectListItem> cari_list = (from x in context.Caris.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CariAd,
                                                      Value = x.CariID.ToString()
                                                  }).ToList();
            ViewBag.cliste = cari_list;

            return View();
        }
        [HttpPost]
        public ActionResult SatisEkle(SatisHareket parametre)
        {
            if (ModelState.IsValid)
            {
                parametre.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
                context.SatisHarekets.Add(parametre);
                context.SaveChanges();
                return RedirectToAction("Index", "Satis");
            }
            return View();
          

        }
    }
}