using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerceApp.DAL;
using eCommerceApp.Models;
using System.IO;

namespace eCommerceApp.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {

            using (AuthorDAL svAuthor = new AuthorDAL())
            using (BookDAL svBook = new BookDAL())
            {

                var result = svBook.GetData().ToList();
                if (TempData["Message"] != null)
                {
                    ViewBag.msg = TempData["Message"].ToString();
                }

                return View(result);

            }
        }
        public ActionResult Index_()
        {

            using (BookDAL svBook = new BookDAL())
            {

                var result = svBook.GetBookWithAuthors().ToList();
                if (TempData["Message"] != null)
                {
                    ViewBag.msg = TempData["Message"].ToString();
                }

                return View(result);

            }
        }
        
        public ActionResult Create()
        {
            //dropdown author
            var lstAuthor = new List<SelectListItem>();//buat nampilin di dropdown
            using (AuthorDAL svAuthor = new AuthorDAL())
            {
                foreach (var au in svAuthor.GetData())

                {
                    lstAuthor.Add(new SelectListItem
                    {
                        Value = au.AuthorID.ToString(),
                        Text = au.FirstName + " " + au.LastName
                    });
                    ViewBag.Authors = lstAuthor;
                }
            }

            //dropdown categories
            var lstCategories = new List<SelectListItem>();//buat nampilin di dropdown
            using (CategoriesDAL svAuthor = new CategoriesDAL())
            {
                foreach (var ca in svAuthor.GetData())

                {
                    lstCategories.Add(new SelectListItem
                    {
                        Value = ca.CategoryID.ToString(),
                        Text = ca.CategoryName
                    });
                    ViewBag.Categories = lstCategories;
                }
            }



            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book bk, HttpPostedFileBase coverimg)
        {
            string filePath = "";
            if (coverimg.ContentLength > 0)
            {
                TempData["Message"] = Helper.MsgBox.GetMsg("success", "IF ", "Your data has been added");

                string filename = Guid.NewGuid().ToString() + "_" + coverimg.FileName;
                filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/Images"), filename);
                bk.CoverImage = filename;
                coverimg.SaveAs(filePath);
            }
            using (BookDAL svBooks = new BookDAL())
            {

                try
                {
                    svBooks.Add(bk);
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data has been added");

                }
                catch (Exception ex)
                {

                    TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error !", ex.Message);

                }
            }
            return RedirectToAction("Index");

        }
        public ActionResult Details(int id)
        {
            using (BookDAL service = new BookDAL())
            {
                var bk = service.GetDataByID(id);
                var AuID = service.GetDataByID(id).AuthorID;
                var catID = service.GetDataByID(id).CategoryID;
                ViewBag.auti = AuID;
                using (AuthorDAL au = new AuthorDAL())
                using (CategoriesDAL cat = new CategoriesDAL())
                {

                    ViewBag.AuthorName = au.GetNameByID(AuID);
                    ViewBag.CategoryName = cat.GetNameByID(catID);
                }
                return View(bk);
            }
        }
        public ActionResult Find(string SelectKriteria, string txtSearch)
        {
            using (BookDAL svBooks = new BookDAL())
            {
                var result = svBooks.SearchByKriteria(SelectKriteria, txtSearch).ToList();
                return View("Index", result);
            }
        }
        //=====PAK ERICK=====
        public ActionResult Indeks()
        {
            using (BookDAL svBook = new BookDAL())
            {

                var result = svBook.GetBookWithAuthors().ToList();
                if (TempData["Message"] != null)
                {
                    ViewBag.msg = TempData["Message"].ToString();
                }

                return View(result);

            }
        }
        public ActionResult Detail(int id)
        {
            using (BookDAL service = new BookDAL())
            {
                var result = service.GetDetailWithAuthors(id);
                return View(result);
            }
        }
        public ActionResult Search (string SelectKriteria,string txtSearch )
        {
            using (BookDAL svBooks = new BookDAL())
            {
                var result = svBooks.SearchByKriteria(SelectKriteria, txtSearch).ToList();
                return View("Indeks", result);
            }
                
        }
    }
}