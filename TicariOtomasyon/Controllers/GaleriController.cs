using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicariOtomasyon.Models.Siniflar;
namespace TicariOtomasyon.Controllers
{
    public class GaleriController : Controller
    {
        Context context = new Context();   
        public ActionResult Index()
        {
            var liste = context.Uruns.ToList();
            return View("Index",liste);
        }
    }
}