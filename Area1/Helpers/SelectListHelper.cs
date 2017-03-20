using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Area1.Models;
using System.Web.Mvc;
using System.Configuration;

namespace Area1.Helpers
{
	public static class SelectListHelper
	{
		public static SelectList getDocCategories()
		{
			var optionsList = CommonProcs.dbAR.DocCategory
											.Select(x => new
											{
												DisplayText = x.CategoryName,
												Value = x.dCatID
											});
			return new SelectList(optionsList, "Value", "DisplayText");
		}

        internal static SelectList getDocSubCategories(int CatID)
        {
            var optionsList = CommonProcs.dbAR.DocSubCategory.Where(x => x.dCatID == CatID)
                                            .Select(x => new
                                            {
                                                DisplayText = x.subCatTitle,
                                                Value = x.subCatID
                                            });
            return new SelectList(optionsList, "Value", "DisplayText");
        }
    }
}