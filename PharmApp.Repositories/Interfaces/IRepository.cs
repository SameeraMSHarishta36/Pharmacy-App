using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmApp.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity:class
    {
        TEntity Find(object id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        int SaveChanges();
    }
}
