using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicariOtomasyon.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageError ()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Page400()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 400;
            return View("PageError");
        }
        public ActionResult Page403()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 403;
            return View("PageError");
        }
        public ActionResult Page404()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;
            return View("PageError");
        }
    }
}