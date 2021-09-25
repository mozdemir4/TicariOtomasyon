using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TicariOtomasyon.Models.Siniflar;


namespace TicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        Context context = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        int kontrol = 0;
        [HttpPost]
        public PartialViewResult Partial1(Cari cari)
        {
            if (ModelState.IsValid)
            {              
                context.Caris.Add(cari);
                kontrol = context.SaveChanges();
                if(kontrol > 0)
                {
                    Response.Write("Kayıt Başarılı");
                   
                }
            }
            return PartialView("Partial1");
        }
        [HttpGet]
        public ActionResult CariLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariLogin(Cari p)
        {
            var kontrol = context.Caris.FirstOrDefault(x => x.CariMail == p.CariMail && x.CariSifre == p.CariSifre);
            if (kontrol !=null)
            {
                FormsAuthentication.SetAuthCookie(kontrol.CariMail, false);
                Session["CariMail"] = kontrol.CariMail;
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
                     
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var kontrol = context.Admins.FirstOrDefault(x => x.KullaniciAd == p.KullaniciAd && x.Sifre == p.Sifre);
            if(kontrol != null)
            {
                FormsAuthentication.SetAuthCookie(kontrol.KullaniciAd, false);
                Session["kad"] = kontrol.KullaniciAd;
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
          
        }
    }
}