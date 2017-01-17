using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Area1.Helpers
{
    public static class LoginHelpers
    {
        public static bool isLoggedIn()
        {
            if (HttpContext.Current.Session["LoginName"] == null) 
            {
                return false;
            }
            return true;
        }
    }
}