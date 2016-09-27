using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerceApp.Models;
using eCommerceApp.DAL;
using System.Net;

namespace eCommerceApp.Controllers
{
    public class AuthorsController : Controller
    {
//=====MANUAL========================================================================//
        // GET: Authors
        public ActionResult Index_()
        {
            using (AuthorDAL service = new AuthorDAL())
            {
                var authors = service.GetData().ToList();
                if (TempData["Message"] != null)
                {
                    ViewBag.msg = TempData["Message"].ToString();
                }
                return View(authors);
            }
               
        }   
        public ActionResult CreateAuthor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAuthor(Author au)
        {
            using (AuthorDAL service = new AuthorDAL())
            {
                try
                {
                    service.Add(au);
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data has been added");

                }
                catch (Exception ex)
                {

                    TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error", ex.Message);

                }

            }
            return RedirectToAction("Index");
        }
        public ActionResult EditAuthor(int id)
        {
            using (AuthorDAL service = new AuthorDAL())
            {
                var author = service.GetDataByID(id);
                return View(author);
            }
        }
        [HttpPost, ActionName("EditAuthor")]
        public ActionResult EditPost(int? id, Author au)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (AuthorDAL service = new AuthorDAL())
            {

                try
                {
                    service.Edits(id.Value, au);
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data  has been updated");

                }
                catch (Exception ex)
                {
                    TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error", ex.Message);

                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAuthor(int? id)
        {
            if (id != null)
            {
                using (AuthorDAL service = new AuthorDAL())
                    try
                    {
                        service.Delete(id.Value);
                        TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data has been removed");

                    }
                    catch (Exception ex)
                    {

                        TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error", ex.Message);

                    }
            }
            return RedirectToAction("Index");
        }

//==================================================================================//
        public ActionResult Index()
        {
            using (AuthorDAL service = new AuthorDAL())
            {
                var authors = service.GetData().ToList();
                if (TempData["Message"] != null)
                {
                    ViewBag.msg = TempData["Message"].ToString();
                }
                return View(authors);
            }

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Author au)
        {
            using (AuthorDAL service = new AuthorDAL())
            {
                try
                {
                    service.Add(au);
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data has been added");

                }
                catch (Exception ex)
                {

                    TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error !", ex.Message);

                }

            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            using (AuthorDAL service = new AuthorDAL())
            {
                var author = service.GetDataByID(id);
                return View(author);
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPosts( Author au)
        {
       
            using (AuthorDAL service = new AuthorDAL())
            {

                try
                {
                    service.Edit(au);
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data  has been updated");

                }
                catch (Exception ex)
                {
                    TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error", ex.Message);

                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (AuthorDAL service = new AuthorDAL())
                    try
                    {
                        service.Delete(id.Value);
                        TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data has been removed");

                    }
                    catch (Exception ex)
                    {

                        TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error", ex.Message);

                    }
            }
            return RedirectToAction("Index");
        }

    }
}