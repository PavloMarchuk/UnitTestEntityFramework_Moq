using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionCity.Storage
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetByParenyId(int id);
        T Get(int id);
        T AddOrUpdate(T obj);
        T Delete(T obj);
        void Save();
    }
}
