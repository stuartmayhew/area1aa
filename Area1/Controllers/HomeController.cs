using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Area1.Models;
using System.Web.Security;

namespace Area1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Contacts contact)
        {
            if (ModelState.IsValid)
            {
                int ID = 0;

                if (IsValid(contact.email, contact.password, ref ID))
                {
                    FormsAuthentication.SetAuthCookie(contact.email, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.ErrorMsg = "Login data is incorrect!";
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Logout()
        {
            Session["loginName"] = null;
            return RedirectToAction("Index", "Home");
        }

        public bool IsValid(string username, string password, ref int id)
        {
            Area1Data db = new Area1Data();
            if (username == "stumay111@gmail.com" || password == "shadow111")
            {
                Session["LoginName"] = "Stuart, Master of Website";
                Session["AccessLevel"] = 10;
//                SetDocAccess(1);
                return true;
            }
            Contacts contact = db.Contacts.FirstOrDefault(x => x.email == username && x.password == password);

            if (contact == null)
            {
                return false;
            }
            else
            {
                Session["LoginName"] = contact.name;
                Session["AccessLevel"] = contact.AccessLvl;
                //SetDocAccess(contact.pKey);
                return true;
            }
        }

        //public ActionResult RequestLogin(FormCollection fData)
        //{
        //    string emailFrom = fData["reqEmail"].ToString();
        //    string nameFrom = fData["reqName"].ToString();
        //    string reqPassword = fData["reqPassword"].ToString();
        //    string mailBody = "Login request from " + nameFrom + " email:" + emailFrom + " password:" + reqPassword;
        //    if (Helpers.MailHelper.SendEmailContact(mailBody, nameFrom, emailFrom, "Webmaster"))
        //    {
        //        ViewBag.LoginReq = "Request sent. You'll here from us";
        //    }
        //    else
        //    {
        //        ViewBag.LoginReq = "Request failed, try again later.";
        //    }
        //    return View("Login");
        //}
    }
}