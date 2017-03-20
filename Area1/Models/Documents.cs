using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Area1.Models
{
    public class Documents
    {
        [Key]
        public int docID { get; set; }
        public int docCat { get; set; }
        public int docSubCat { get; set; }
        public string docTitle { get; set; }
        public string docPath { get; set; }
        public string docAuthor { get; set; }
        public DateTime docDate { get; set; }
    }
}