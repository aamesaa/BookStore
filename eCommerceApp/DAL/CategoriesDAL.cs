using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.Models;

namespace eCommerceApp.DAL
{
    public class CategoriesDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();
        public void Add(Category obj)
        {
            try
            {
                db.Categories.Add(obj);//menambahkan data
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IQueryable<Category>GetData()
        {
            var result = from c in db.Categories orderby c.CategoryName ascending select c;
            return result;
        }
        public void Dispose()
        {
           db.Dispose();
        }

        public void Edit(int CatID, Category obj)
        {

            var result = GetDataByID(CatID);
            if (result != null)
            {
                result.CategoryName = obj.CategoryName;
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
        public Category GetDataByID(int catID)
        {
            var result = (from c in db.Categories where c.CategoryID==catID select c).SingleOrDefault(); //ngambil satu data
            return result;
        }
        public string GetNameByID(int catID)
        {
            var result = (from c in db.Categories where c.CategoryID == catID select c.CategoryName).SingleOrDefault();
            return result;
        }


        public void Delete(int CatID)
        {
            var result = GetDataByID(CatID);
            if (result != null)
                db.Categories.Remove(result);
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