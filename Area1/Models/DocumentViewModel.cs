using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Area1.Models
{
    public class DocumentViewModel
    {
        [Key]
        public int DocViewID { get; set; }
        public int docCategory { get; set; }
        public List<Documents> documents { get; set; }
        public List<DocSubCategory> docSubCategories { get; set; }
        public DocumentViewModel()
        {
            documents = new List<Documents>();
            docSubCategories = new List<DocSubCategory>();
        }
    }
}