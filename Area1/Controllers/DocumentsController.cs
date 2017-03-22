using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Area1.Models;
using System.Web.UI.WebControls;
using Area1.Helpers;
using System.IO;

namespace Area1.Controllers
{
    public class DocumentsController : Controller
    {
        private Area1Data db = new Area1Data();

        public ActionResult DocumentsIndex(int CatID)
        {
            DocumentViewModel dvm = new DocumentViewModel();
            dvm.docSubCategories = CommonProcs.dbAR.DocSubCategory.Where(x => x.dCatID == CatID).ToList();
            foreach(var subCat in dvm.docSubCategories)
            {
                List<Documents> docs = CommonProcs.dbAR.Documents.Where(x => x.docSubCat == subCat.subCatID).ToList();
                foreach (var doc in docs)
                {
                    dvm.documents.Add(doc);
                }
            }
            return View("DocumentsIndex",dvm);
        }

       public ActionResult DocumentsCreate(int CatID)
        {
            SelectList CatList = SelectListHelper.getDocCategories();
            SelectList SubCatList = SelectListHelper.getDocSubCategories(CatID);
            ViewBag.CatList = CatList;
            ViewBag.SubCatList = SubCatList;
            return View();
        }

        public ActionResult DocumentUploadAndSave(Documents document)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    System.IO.FileInfo fi = new FileInfo(file.FileName);
                    var ext = fi.Extension;
                    var fileName = fi.Name;
                    int docCatID = document.docCat;
                    string policyFolderName = CommonProcs.dbAR.DocCategory.FirstOrDefault(x => x.dCatID == docCatID).CategoryName;
                    var basePath = Path.Combine(Server.MapPath("~/Uploads"), policyFolderName);
                    if (!Directory.Exists(basePath))
                        Directory.CreateDirectory(basePath);
                    var docPath = Path.Combine(basePath, file.FileName);
                    try
                    {
                        file.SaveAs(docPath);
                        ViewBag.Success = true;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Success = false;
                        ViewBag.Error = ex.Message;
                    }
                    document.docPath = docPath;
                    CommonProcs.dbAR.Documents.Add(document);
                    CommonProcs.dbAR.SaveChanges();
                }
            }
            return View("DocumentsDetails");
        }

        // GET: Documents/Edit/5
        public ActionResult DocumentsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }
            return View(documents);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DocumentsEdit(Documents documents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DocumentsIndex");
            }
            return View(documents);
        }

        // GET: Documents/Delete/5
        public ActionResult DocumentsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documents documents = db.Documents.Find(id);
            if (documents == null)
            {
                return HttpNotFound();
            }
            return View(documents);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("DocumentsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Documents documents = db.Documents.Find(id);
            db.Documents.Remove(documents);
            db.SaveChanges();
            return RedirectToAction("DocumentsIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
