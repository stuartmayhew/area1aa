using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Area1.Models;
using System.Web.Security;
using Area1.Helpers;
namespace Area1.Controllers
{
    public class HomeController : Controller
    {
        public Area1Data db = new Area1Data();

        public ActionResult Login()
        {
            Login login = ReadCookie();
            if (login.login_id > 0)
            {
                login.firstName = CommonProcs.dg.GetScalarString("SELECT firstName FROM login WHERE login_id=" + login.login_id.ToString());
                return View("Welcome",login);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            Login newLogin = db.Login.FirstOrDefault(x => x.username == login.username && x.password == login.password);
            if (newLogin == null)
            {
                ViewBag.ErrorMsg = "Login data is incorrect!";
                return View("Login");
            }
            else
            {
                WriteUserCookie(newLogin);
                return View("Welcome", newLogin);
            }
        }

        public ActionResult Logout()
        {
            Session["loginName"] = null;
            return RedirectToAction("Index", "Home");
        }

        private void WriteUserCookie(Login login)
        {
            HttpCookie myCookie = new HttpCookie("area1");
            myCookie.Values.Add("login_id", login.login_id.ToString());
            myCookie.Values.Add("accLevel", login.accLevel.ToString());
            myCookie.Values.Add("positionKey", login.positionKey.ToString());
            myCookie.Expires = DateTime.Now.AddYears(100);
            Response.Cookies.Add(myCookie);
        }


        public Login ReadCookie()
        {
            HttpCookie myCookie = Request.Cookies["area1"];
            Login login = new Login();
            login.login_id = -1;
            login.accLevel = 0;
            if (myCookie != null)
            {
                login.accLevel = int.Parse(myCookie.Values["accLevel"].ToString());
                login.login_id = int.Parse(myCookie.Values["login_id"].ToString());
            }
            return login;
        }

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
