using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionCity.Storage
{
    public abstract class DataServiceGeneric<T> : IDataServiceGeneric<T> 
        where T: class,new()
    {
        private IGenericRepository<T> _repository;

        public DataServiceGeneric(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T Get(int id)
        {
            return _repository.Get(id);
        }

        public T AddOrUpdate(T obj)
        {
            _repository.AddOrUpdate(obj);
            _repository.Save();
            return obj;
        }

        public T Delete(T obj)
        {
            _repository.Delete(obj);
            _repository.Save();
            return obj;
        }
    }
}
