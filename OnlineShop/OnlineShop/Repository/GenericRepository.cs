using OnlineShop.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OnlineShop.Repository
{
    public class GenericRepository<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        private dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();
        DbSet<Tbl_Entity> dbSet;
        public GenericRepository(dbMyOnlineShoppingEntities db2)
        {
            db = db2;
            dbSet = db.Set<Tbl_Entity>();
        }

            
        public void Add(Tbl_Entity entity)
        {
            dbSet.Add(entity);
            db.SaveChanges();
        }

        public int GetAllrecordCount()
        {
            return dbSet.Count();
        }

        public IEnumerable<Tbl_Entity> GetAllRecords()
        {
            return dbSet.ToList();
        }

        public IQueryable<Tbl_Entity> GetAllRecordsIQueryable()
        {
            return dbSet;
        }

        public Tbl_Entity GetFirstorDefault(int recordId)
        {
            return dbSet.Find(recordId);
        }

        public Tbl_Entity GetFirstorDefaultByParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<Tbl_Entity> GetListParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return dbSet.Where(wherePredict).ToList();
        }

        public IEnumerable<Tbl_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Tbl_Entity, bool>> wherePredict, Expression<Func<Tbl_Entity, int>> orderByPredict)
        {
            if(wherePredict!=null)
            {
                return dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return dbSet.OrderBy(orderByPredict).ToList();
            }
        }

        public IEnumerable<Tbl_Entity> GetResultBySqlprocedure(string query, params object[] parameters)
        {
            if(parameters!=null)
            {
                return db.Database.SqlQuery<Tbl_Entity>(query, parameters).ToList();
            }
            else
            {
                return db.Database.SqlQuery<Tbl_Entity>(query).ToList();
            }
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);

        }

        public void Remove(Tbl_Entity entity)
        {
            if(db.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);
            dbSet.Remove(entity);


        }

        public void RemovebyWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            Tbl_Entity e = dbSet.Where(wherePredict).FirstOrDefault();
            dbSet.Remove(e);
        }

        public void RemoveRangeBywhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            List<Tbl_Entity> li_e = dbSet.Where(wherePredict).ToList();
            foreach(var i in li_e)
            {
                dbSet.Remove(i);
            }
        }

        public void Update(Tbl_Entity entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }
    }
}