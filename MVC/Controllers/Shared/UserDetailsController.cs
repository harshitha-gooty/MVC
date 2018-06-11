using Models;
using MVC.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.Shared
{
    public class UserDetailsController : Controller
    {
        public string APIURL
        {
            get
            {
                return ConfigurationManager.AppSettings["apiURL"].ToString();
            }
        }
        //
        // GET: /UserDetails/
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult GetUserDetails()
        {
            string apiURL = APIURL + "api/UserDetails/GetUserDetails/";
            var result = Helper.Instance.GetFromWebApi(apiURL);
            List<UserDetails> userDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserDetails>>(result);
            return PartialView("~/Views/Shared/_GetUserDetails.cshtml", userDetails);
        }
    }
}