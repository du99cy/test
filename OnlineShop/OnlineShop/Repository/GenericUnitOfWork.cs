using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShop.Database;
namespace OnlineShop.Repository
{
    public class GenericUnitOfWork:IDisposable
    {
        public dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();
        public GenericRepository<Tbl_EntityType> GetRepositoryInstance<Tbl_EntityType>() where Tbl_EntityType : class
        {
            return new GenericRepository<Tbl_EntityType>(db);
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
        ~GenericUnitOfWork() => Dispose(false);
        public virtual void Dispose(bool disposing)
        {
            if(_disposed)
            {
                return;
            }
            if(disposing)
            {
                db.Dispose();
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        
    }
}