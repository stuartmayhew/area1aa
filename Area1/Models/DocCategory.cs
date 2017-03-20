using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Area1.Models
{
    public class DocCategory
    {
        [Key]
        public int dCatID { get; set; }
        public string CategoryName { get; set; }

    }
}