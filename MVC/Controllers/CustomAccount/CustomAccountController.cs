using Models.CustomAccount;
using MVC.AppCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.CustomAccount
{
    public class CustomAccountController : Controller
    {
        public string APIURL
        {
            get
            {
                return ConfigurationManager.AppSettings["apiURL"].ToString();
            }
        }
        //
        // GET: /CustomAccount/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register details)
        {
            if (ModelState.IsValid)
            {
                string apiURL =APIURL+ "api/CustomAccount/Register/";
                var result = Helper.Instance.PostToWebApi(apiURL, details);
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Login()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                string apiURL = APIURL + "api/CustomAccount/Login";
                var result = Helper.Instance.PostToWebApi(apiURL, model);
                var stringResult = JsonConvert.DeserializeObject<string>(result);
                if (stringResult != "Success")
                    ModelState.AddModelError("", stringResult);
                else
                {
                    System.Web.HttpContext.Current.Session["CurrentUser"] = model.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Manage(ManageMessageId message)
        {
            //ViewBag.StatusMessage =
               // message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
               // : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                //: message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                //: message == ManageMessageId.Error ? "An error has occurred."
                //: "";
            //ViewBag.HasLocalPassword = HasPassword();
            //ViewBag.ReturnUrl = Url.Action("Manage");
            return RedirectToAction("Index", "Home");

        }
        private bool HasPassword()
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //if (user != null)
            //{
            //  return user.PasswordHash != null;
            //}
            return false;
        }
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
    }
}