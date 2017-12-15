using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected DbContext db;
        protected IDbSet<T> dbSet;

        public BaseRepository(DbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public virtual void Create(T obj)
        {
            dbSet.Add(obj);
            db.SaveChanges();
        }

        public void Delete(T obj)
        {
            dbSet.Remove(obj);
            db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public void Update(T obj)
        {            
            db.Entry(obj).State = EntityState.Modified;                           
            db.SaveChanges();
        }
    }
}
