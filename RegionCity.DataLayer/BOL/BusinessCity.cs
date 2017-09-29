using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionCity.DataLayer.BOL
{
    public class BusinessCity
    {
        public int CityId { get; set; }
        public int RegionId { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
    }
}
