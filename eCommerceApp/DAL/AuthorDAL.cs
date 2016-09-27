using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.Models;

namespace eCommerceApp.DAL
{
    public class AuthorDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public void Dispose()
        {
            db.Dispose();
        }

        public IQueryable<Author> GetData()
        {
            var result = from a in db.Authors orderby a.AuthorID select a;
            return result;
        }
        public Author GetDataByID(int auID)
        {
            var result = (from a in db.Authors where a.AuthorID == auID select a).SingleOrDefault();
            return result;
        }
        
        public string GetNameByID(int auID)
        {
            
            var first = (from a in db.Authors where a.AuthorID==auID select a.FirstName).SingleOrDefault();
            var last = (from a in db.Authors where a.AuthorID == auID select a.LastName).SingleOrDefault();

            return first+" "+last;
        }



        public void Add(Author obj)
        {
            try
            {
                db.Authors.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        //Edit cara 1
        public void Edits(int AuID, Author obj)
        {
            var result = GetDataByID(AuID);
            if (result != null)
            {
                result.FirstName = obj.FirstName;
                result.LastName = obj.LastName;
                result.Email = obj.Email;
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
        //Edit cara 2
        public void Edit(Author obj)
        {
            var model = GetDataByID(obj.AuthorID);
            if(model!=null)
            {
                model.FirstName = obj.FirstName;
                model.LastName = obj.LastName;
                model.Email = obj.Email;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception  ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
        
        public void Delete(int AuID)
        {
            var result = GetDataByID(AuID);
            if (result != null)
                db.Authors.Remove(result);
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
}