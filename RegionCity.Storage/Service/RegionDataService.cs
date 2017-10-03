using RegionCity.DataLayer.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionCity.Storage
{
    public class RegionDataService : DataServiceGeneric<Region>
    {
        public RegionDataService(IGenericRepository<Region> repository) : base(repository)
        {
        }
    }
}
