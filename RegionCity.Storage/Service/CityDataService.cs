using RegionCity.DataLayer.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionCity.Storage
{
    public class CityDataService : DataServiceGeneric<City>
    {
        public CityDataService(IGenericRepository<City> repository) : base(repository)
        {
        }
    }
}
