using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerceApp.DAL;
using eCommerceApp.Models;
using System.Net;
namespace eCommerceApp.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(int id)
        {
            using (OrderDAL service = new OrderDAL())
            {
                ViewBag.id = id;
                return View(service.GetOrder(id).ToList());
            }
        }
        //OrderNow = Checkout
        public ActionResult OrderNow()
        {
            //Cek Udah login blm
            if (User.Identity.IsAuthenticated)
            {

                using (ShoppingCartsDAL service = new ShoppingCartsDAL())
                {
                    //ViewBag.session = Session["username"].ToString();
                    //ViewBag.user = User.Identity.Name;
                    string username = Session["username"] != null ? Session["username"].ToString() : string.Empty;

                    TempData["Message"] = Helper.MsgBox.GetMsg("success", "Attention! ", "Please check your order below before submitting");
                    ViewBag.msg = TempData["Message"].ToString();
                    TempData["Message"] = Helper.MsgBox.GetMsg("info", "User ", User.Identity.Name);
                    ViewBag.msg2 = TempData["Message"].ToString();

                    //update cartID
                    if (Session["username"].ToString()!= User.Identity.Name)
                    {
                        service.UpdateCartID(Session["username"].ToString(), User.Identity.Name);
                        username = User.Identity.Name;
                    }
                    
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
        /*[HttpPost]
        public ActionResult OrderNow(ShoppingCart sc)
        {
            //ADD ORDER
            var newOrder = new Order
            {
                CustomerName = User.Identity.Name,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.Now.AddDays(3)
            };
            using (OrderDAL service = new OrderDAL())
            {
                service.AddOrder(newOrder);

                //ADD DETAIL
                foreach (var item in service.getData(sc.CartID))
                {
                    var newDetail = new OrderDetail
                    {
                        OrderID = newOrder.OrderID,
                        BookID = sc.BookID,
                        Quantity = sc.Quantity,
                        Price = sc.Book.Price
                    };

                    service.AddDetail(newDetail);
                }
            }
            return RedirectToAction("Index", new { id = newOrder.OrderID });
        }*/
        public ActionResult Final()
        {
            //ADD ORDER
            var newOrder = new Order
            {
               
                CustomerName = User.Identity.Name,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.Now.AddDays(5)
            };
            using (OrderDAL service = new OrderDAL())
            {
                service.AddOrder(newOrder);

                //ADD DETAIL
                foreach (var item in service.getData(User.Identity.Name).ToList())
                {
                    var newDetail = new OrderDetail
                    {
                        OrderID = newOrder.OrderID,
                        BookID = item.BookID,
                        Quantity = item.Quantity,
                        Price = item.Book.Price
                    };

                    service.AddDetail(newDetail);
                    service.removeCart(item);
                }
            }
            return RedirectToAction("Index", new { id = newOrder.OrderID });
        }
    }
}
