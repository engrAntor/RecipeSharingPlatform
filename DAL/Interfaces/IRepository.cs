using System.Linq;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<T, ID>
    {
        void Create(T entity);
        IQueryable<T> GetAll();
        T GetById(ID id);
        void Update(T entity);
        void Delete(ID id); 
    }
}