using RegionCity.DataLayer.DbLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionCity.Storage
{
    public class CityRepository : GenericRepository<City>
    {
        public CityRepository(DbContext context) : base(context)
        {
        }
    }
}
