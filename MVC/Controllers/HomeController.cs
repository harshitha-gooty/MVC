using MVC.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorization("Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult FirstPage(string UserName, string Password)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            var s = UserName;
            var p = Password;
            return null;
        }
    }
}