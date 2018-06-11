using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.Offers.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Offers/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}