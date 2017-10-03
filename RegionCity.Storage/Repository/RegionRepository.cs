using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionCity.DataLayer;
using RegionCity.DataLayer.BOL;
using RegionCity.DataLayer.DbLayer;

namespace RegionCity.Storage
{
    public class RegionRepository : GenericRepository<Region>
    {
        public RegionRepository(DbContext context) : base(context)
        {
        }
    }
}
