namespace RegionCity.DataLayer.DbLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Region")]
    public partial class Region
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Region()
        {
            Cities = new HashSet<City>();
        }

        public int RegionId { get; set; }

        [Required]
        [StringLength(32)]
        public string RegionName { get; set; }

        public int UserCreateId { get; set; }

        public DateTime DateCreate { get; set; }

        public int UserModifyId { get; set; }

        public DateTime DateModify { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<City> Cities { get; set; }
    }
}
