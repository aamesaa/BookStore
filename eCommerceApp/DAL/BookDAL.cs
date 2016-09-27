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
    public class BookDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();
        public IQueryable<Book> GetData()
        {
            var result = from b in db.Books orderby b.BookID select b;
            return result;
        }
        public IQueryable<int> GetID()
        {
            var result = from b in db.Books select b.AuthorID;
            return result;
        }
        public IQueryable<BookVM> GetBookWithAuthors()
        {
            var result = from b in db.Books.Include("Authors")
                         orderby b.Title
                         select new BookVM // book VM buat nampung hasil
                         {
                             BookID = b.BookID,
                             AuthorID = b.AuthorID,
                             Title = b.Title,
                             CoverImage = b.CoverImage,
                             Price = b.Price,
                             ISBN = b.ISBN,
                             PublicationDate = b.PublicationDate,
                             Description = b.Description,
                             Publisher = b.Publisher,
                             FirstName = b.Author.FirstName,
                             LastName = b.Author.LastName
                         };

            return result;

        }
        public BookVM GetDetailWithAuthors(int id)
        {
            var result = (from b in db.Books.Include("Authors")
                          orderby b.Title
                          where b.BookID == id
                          select new BookVM // book VM buat nampung hasil
                          {
                              BookID = b.BookID,
                              AuthorID = b.AuthorID,
                              Title = b.Title,
                              CoverImage = b.CoverImage,
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

        }
        public void Dispose()
        {
            db.Dispose();
        }
        public Book GetDataByID(int bkID)
        {
            var result = (from b in db.Books
                          where b.BookID == bkID
                          select b).SingleOrDefault();
            return result;
        }
        public void Add(Book obj)
        {
            try
            {
                db.Books.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void Edit(Book bk)
        {
            var model = GetDataByID(bk.AuthorID);
            if (model != null)
            {
                model.AuthorID = bk.AuthorID;
                model.CategoryID = bk.CategoryID;
                model.Title = bk.Title;
                model.ISBN = bk.ISBN;
                model.CoverImage = bk.CoverImage;
                model.Price = bk.Price;
                model.Description = bk.Description;
                model.PublicationDate = bk.PublicationDate;
                model.Description = bk.Description;
                model.Publisher = bk.Publisher;
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
        public void Delete(int bkID)
        {
            var result = GetDataByID(bkID);
            if (result != null)
                db.Books.Remove(result);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IQueryable<BookVM> SearchByKriteria(string selectKriteria, string txtSearch)
        {
            IQueryable<BookVM> result;
            if (selectKriteria == "Title")
            {
                //Pencarian berdasar title
                result = from b in db.Books.Include("Authors")
                         orderby b.Title
                         where b.Title.ToLower().Contains(txtSearch.ToLower())
                         select new BookVM // book VM buat nampung hasil
                         {
                             BookID = b.BookID,
                             AuthorID = b.AuthorID,
                             Title = b.Title,
                             CoverImage = b.CoverImage,
                             Price = b.Price,
                             Description = b.Description,
                             Publisher = b.Publisher,
                             FirstName = b.Author.FirstName,
                             LastName = b.Author.LastName
                         };
            }
            else
            {
                //Berdasar author
                result = from b in db.Books.Include("Authors")
                         orderby b.Title
                         where b.Author.FirstName.ToLower().Contains(txtSearch.ToLower()) ||
                         b.Author.LastName.ToLower().Contains(txtSearch.ToLower())
                         select new BookVM // book VM buat nampung hasil
                         {
                             BookID = b.BookID,
                             AuthorID = b.AuthorID,
                             Title = b.Title,
                             CoverImage = b.CoverImage,
                             Price = b.Price,
                             Description = b.Description,
                             Publisher = b.Publisher,
                             FirstName = b.Author.FirstName,
                             LastName = b.Author.LastName
                         };

            }
            return result;
        }
    }
}
