namespace RegionCity.DataLayer.DbLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("City")]
    public partial class City
    {
        public int CityId { get; set; }

        public int RegionId { get; set; }

        [Required]
        [StringLength(128)]
        public string CityName { get; set; }

        public virtual Region Region { get; set; }
    }
}
