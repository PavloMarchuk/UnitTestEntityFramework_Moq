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
        void AddOrUpdate(T obj);
        void Delete(T obj);
        void Save();
    }
}
