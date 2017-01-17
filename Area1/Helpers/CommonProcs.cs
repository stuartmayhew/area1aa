using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Area1.Models;
using System.Configuration;

namespace Area1.Helpers
{
    public static class CommonProcs
    {
        public static clsDataGetter dg = new clsDataGetter(ConfigurationManager.ConnectionStrings["area1"].ConnectionString);

        public static int GetCurrUser()
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["area1"];
            Login login = new Login();
            login.login_id = -1;
            login.accLevel = 0;
            if (myCookie != null)
            {
                login.accLevel = int.Parse(myCookie.Values["accLevel"].ToString());
                login.login_id = int.Parse(myCookie.Values["login_id"].ToString());
                login.positionKey = int.Parse(myCookie.Values["positionKey"].ToString());
            }
            return login.login_id;
        }

    }
}