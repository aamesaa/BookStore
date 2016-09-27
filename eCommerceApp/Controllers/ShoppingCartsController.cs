using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerceApp.DAL;
using eCommerceApp.Models;

using eCommerceApp.ViewModel;


namespace eCommerceApp.Controllers
{
    public class ShoppingCartsController : Controller
    {
        public bool isUser = true;
        // GET: ShoppingCarts
        public ActionResult Index()
        {
            //nampilin MSG BOX
            if (TempData["Message"] != null)
            {
                ViewBag.msg = TempData["Message"].ToString();
            }

            using (ShoppingCartsDAL service = new ShoppingCartsDAL())
            {
                string username = Session["username"] != null ? Session["username"].ToString() : string.Empty;
                return View(service.GetAllData(username).ToList());
            }
    
        }
        public ActionResult AddToCart(int id)
        {
            //cek sudah login blm
            if(Session["username"]==null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Session["username"] = User.Identity.Name;
                  
                }
                else
                {

                    var tempUser = Guid.NewGuid().ToString();
                    Session["username"] = tempUser;
                    
                    
                }
            }
            using (ShoppingCartsDAL scService = new ShoppingCartsDAL())
            {
                var newShoppingChart = new ShoppingCart
                {
                    CartID = Session["username"].ToString(),
                    Quantity = 1,
                    BookID=id,
                    DateCreated = DateTime.Now
                };
                
                scService.AddToCart(newShoppingChart);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {

                using (ShoppingCartsDAL service = new ShoppingCartsDAL())
                    try
                    {
                        service.Delete(id);
                        TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data has been removed from your Cart");

                    }
                    catch (Exception ex)
                    {

                        TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error", ex.Message);

                    
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit (int id)
        {
            using (ShoppingCartsDAL service = new ShoppingCartsDAL())
            {

                var sc = service.GetItemById(id);
                var s = service.GetDetailById(id);
                
                return View(sc);
            }
           
        }
        [HttpPost]
        public ActionResult Edit (ShoppingCart sc)
        {
            using (ShoppingCartsDAL service = new ShoppingCartsDAL())
            {
                try
                {
                    service.Edit(sc);
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Success! ", "Your data  has been updated");

                }
                catch (Exception ex)
                {
                    TempData["Message"] = Helper.MsgBox.GetMsg("danger", "Error", ex.Message);

                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult CheckOut()
        {
            //Cek Udah login blm
            if (User.Identity.IsAuthenticated)
            {
                
                using (ShoppingCartsDAL service = new ShoppingCartsDAL())
                {
                    string username = Session["username"] != null ? Session["username"].ToString() : string.Empty;
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Attention! ", "Please check your order below before submitting");
                    ViewBag.msg = TempData["Message"].ToString();
                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "User ", (Session["username"]).ToString());
                    ViewBag.msg = TempData["Message"].ToString();
                   
                    return View(service.GetAllData(username).ToList());
                }                
            }
            //Blm login
            else
            {
                TempData["AlertMessage"] = "You need to Login first to Continue";
                return RedirectToAction("Login", "Account");
            }

           
        }
       
    }
}