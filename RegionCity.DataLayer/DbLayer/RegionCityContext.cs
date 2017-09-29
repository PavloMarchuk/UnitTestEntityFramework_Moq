namespace RegionCity.DataLayer.DbLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RegionCityContext : DbContext
    {
        public RegionCityContext()
            : base("name=RegionCityContext")
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>()
                .HasMany(e => e.Cities)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);
        }
    }
}
