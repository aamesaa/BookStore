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
    public class CategoriesController : Controller
    {
        private CommerceModel db = new CommerceModel();
        // GET: Categories
        public ActionResult Index()
        {
            using (CategoriesDAL service = new CategoriesDAL())
            {
                var categories = service.GetData().ToList();
                if (TempData["Message"] != null)
                {
                    ViewBag.msg = TempData["Message"].ToString();
                }
                return View(categories);
            }
                
        }
        public ActionResult CreateCategories()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategories(Category cat)
        {
            using (CategoriesDAL service = new CategoriesDAL())
            {
                
                try
                {
                    service.Add(cat);
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data " + cat.CategoryName + " has been added");

                }
                catch (Exception ex)
                {
                    TempData["Message"] = Helper.MsgBox.GetMsg("danger","Error", ex.Message);
                    
                }
                

            }
            return RedirectToAction("Index");
        }

        public ActionResult EditCategories(int id)
        {
            using (CategoriesDAL service = new CategoriesDAL()) 
            {
                var category = service.GetDataByID(id);
                return View(category);
            }
        }
        [HttpPost,ActionName("EditCategories")] //blm bisa
        public ActionResult EditPost(int? id,Category cat ) //int? --> bisa diisi nilai NULL
        {
          if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (CategoriesDAL service = new CategoriesDAL())
            {
                
                try
                {
                    service.Edit(id.Value, cat);
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data " + cat.CategoryName + " has been updated");

                }
                catch (Exception ex)
                {
                    TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error", ex.Message);

                }
            }
            return RedirectToAction("Index");

        }

        public ActionResult DeleteCategories(int? id)
        {
            if(id!=null)
            {
                using (CategoriesDAL service = new CategoriesDAL())
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