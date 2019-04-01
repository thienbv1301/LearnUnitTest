using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);
        void Add(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        IEnumerable<TEntity> GetAll();
    }
}
