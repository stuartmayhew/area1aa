using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;


namespace Area1.Models
{
    public class Login
    {
        [Key]
        public int login_id { get; set; }
        public string firstName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int accLevel { get; set; }
        public int DistKey { get; set; }
        public int positionKey { get; set; }
    }
}