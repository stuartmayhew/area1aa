using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Area1.Models
{
    public class DocSubCategory
    {
        [Key]
        public int subCatID { get; set; }
        public int dCatID { get; set; }
        public string subCatTitle { get; set; }
    }
}