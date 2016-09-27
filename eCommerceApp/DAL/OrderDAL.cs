using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.Models;
using eCommerceApp.ViewModel;

namespace eCommerceApp.DAL
{
    public class OrderDAL:IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public void Dispose()
        { db.Dispose(); }
        public void AddOrder(Order obj)
        {
            try
            {
                db.Orders.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void removeCart(ShoppingCart sc)
        {
            db.ShoppingCarts.Remove(sc);
            db.SaveChanges();
        }
        
        public void AddDetail(OrderDetail obj)
        {
          
                db.OrderDetails.Add(obj);
                db.SaveChanges();
           
        }
        public IQueryable<ShoppingCart> getData (string id)
        {
            var result = (from sc in db.ShoppingCarts
                          where sc.CartID == id
                          select sc);
            return result;
        }
        public IQueryable<OrderVM> GetOrder(int id)
        {
            var result = from o in db.OrderDetails.Include("Orders")
                          where o.OrderID == id
                          select new OrderVM
                         {
                             OrderID= o.Order.OrderID,
                             CustomerName = o.Order.CustomerName,
                             OrderDate = o.Order.OrderDate,
                             ShipDate = o.Order.ShipDate,
                             BookID = o.BookID,
                             Quantity = o.Quantity,
                             Price = o.Price ,
                             Title = o.Book.Title            
                                                                        

                         };
            return result;
        }
    }
}