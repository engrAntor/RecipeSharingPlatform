using DAL.EF;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repos
{
    internal class Repository<T, ID> : IRepository<T, ID> where T : class
    {
        protected RecipeContext db;

        public Repository()
        {
            db = new RecipeContext();
        }

        public void Create(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
        }

        public void Delete(ID id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
        }

        
        public IQueryable<T> GetAll()
        {
            return db.Set<T>();
        }
        

        public T GetById(ID id)
        {
            return db.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}