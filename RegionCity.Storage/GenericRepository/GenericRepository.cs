using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionCity.Storage
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T: class,new()
    {

        DbContext context;
        DbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
           return dbSet;
        }

        //public abstract IEnumerable<T> GetByParentId(int id);
    }
}
