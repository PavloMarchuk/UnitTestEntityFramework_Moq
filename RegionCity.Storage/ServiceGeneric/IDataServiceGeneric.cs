using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionCity.Storage
{
    public interface IDataServiceGeneric<T> where T: class, new()
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T AddOrUpdate(T obj);
        T Delete(T obj);
    }
}
