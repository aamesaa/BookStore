using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCommerceApp.Models;
using eCommerceApp.ViewModel;

namespace eCommerceApp.DAL
{
    public class ShoppingCartsDAL :IDisposable
    {
        private CommerceModel db = new CommerceModel();
        public void Dispose()
        {
            db.Dispose();
        }
        public IQueryable<ShoppingCart> GetAllData(string username)
        {
            var result = from s in db.ShoppingCarts.Include("Book")
                         where s.CartID == username
                         orderby s.RecordID ascending
                         select s;
            return result;
        }
        //Cek item sudah ada atau blm
        public ShoppingCart GetItemByUser(string username, int bookId)
        {
            var result = (from sc in db.ShoppingCarts
                          where sc.CartID == username && sc.BookID == bookId
                          select sc).SingleOrDefault();
            return result;
        }
       
        public ShoppingCart GetItemById(int Id)
        {
            var result = (from sc in db.ShoppingCarts
                          where  sc.RecordID == Id
                          select sc).SingleOrDefault();
            return result;
        }

        public EditCartVM GetDetailById(int Id)
        {
            var result = (from sc in db.ShoppingCarts.Include("Book")
                          where sc.RecordID == Id
                          select new EditCartVM
                          {
                              BookID=sc.BookID,
                              CoverImage=sc.Book.CoverImage,
                              Quantity=sc.Quantity,
                              RecordID=sc.RecordID,
                              Title=sc.Book.Title
                          }).SingleOrDefault();
            return result;
        }
        public void AddToCart(ShoppingCart sc)
        {
            var result = GetItemByUser(sc.CartID, sc.BookID);
            if(result!=null)
            {
                //ditambahi klo sudah ada
                result.Quantity += 1;
            }
            else
            {
                db.ShoppingCarts.Add(sc);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message,ex.InnerException);
            }
        }
        /*public string GetCartID()
        {
            var result = from sc in db.ShoppingCarts select sc.CartID).
            return result;
        }*/
        public void UpdateCartID (string tempUser, string Username)
        {
            var result = from s in db.ShoppingCarts
                         where s.CartID == tempUser
                         select s;
            foreach(var sc in result)
            {
                sc.CartID = Username;
            }
            db.SaveChanges();
        }
        public void Edit(ShoppingCart obj)
        {
            var model = GetItemById(obj.RecordID);
            if (model!=null)
            {
                model.Quantity = obj.Quantity;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
        public void Delete(int id)
        {
            var result = GetItemById(id);
            if (result != null)
                db.ShoppingCarts.Remove(result);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        /*public DetailsVM GetDetail(int id, int book)
        {
            var result = (from sc in db.ShoppingCarts.Include("Book")
                          
                          where sc.BookID==book && sc.RecordID==id
                          select new DetailsVM // book VM buat nampung hasil
                          {
                              RecordID = sc.RecordID,
                              CartID = sc.CartID,
                              Quantity = sc.Quantity,
                              BookID = sc.BookID,
                             
                              AuthorID = b.AuthorID,
                              Title = b.Title,
                              CoverImage = sc.CoverImage,
                              Price = b.Price,
                              Description = b.Description,
                              Publisher = b.Publisher,
                              FirstName = b.Author.FirstName,
                              LastName = b.Author.LastName
                          }).SingleOrDefault();
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("Data tidak ditemukan");
            }

        }*/
    }
}