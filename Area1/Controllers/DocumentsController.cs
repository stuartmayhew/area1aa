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
            return View(db.Documents.Where(x => x.docCat == CatID).ToList());
        }

       public ActionResult DocumentsCreate()
        {
            SelectList CatList = SelectListHelper.getDocCategories();
            SelectList SubCatList = SelectListHelper.getDocSubCategories(1);
            ViewBag.CatList = CatList;
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
            return View("DocumentsIndex");
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
