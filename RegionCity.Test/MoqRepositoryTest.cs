using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionCity.DataLayer.DbLayer;
using RegionCity.DataLayer.BOL;
using RegionCity.Storage;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace RegionCity.Test
{
    [TestClass()]
    public class MoqRepositoryTest
    {
        private static Mock<RegionCityContext> mockContext;
        private static Mock<DbSet<Region>> mockRegionSet;
        private static Mock<DbSet<City>> mockCitySet;
        // Data provider for DbSet<City> instance
        private static List<City> citiesProvider;
        // Data provider for DbSet<Region> instance
        private static List<Region> regionsProvider;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext context)
        {
            mockContext = new Mock<RegionCityContext>();
            mockRegionSet = new Mock<DbSet<Region>>();
            mockCitySet = new Mock<DbSet<City>>();
            
            // Arrange data providers
            citiesProvider = new List<City>()
            {
                new City{CityName="Berlin",Region=new Region{RegionName="Berlin" } },
                new City{CityName="Hamburg",Region=new Region{RegionName="Hamburg" } },
                new City{CityName="Munich",Region=new Region{RegionName="Bavaria" } },
                new City{CityName="Cologne",Region=new Region{RegionName="North Rhine-Westphalia" } },
                new City{CityName="Frankfurt am Main",Region=new Region{RegionName="Hesse" } }
            };
            regionsProvider = new List<Region>()
            {
                new Region{RegionName="Berlin"},
                new Region{RegionName="Hamburg"},
                new Region{RegionName="Bavaria"},
                new Region{RegionName="North Rhine-Westphalia"},
                new Region{RegionName="Hesse"}
            };


            // mockCitySet setup
            mockCitySet.As<IQueryable<City>>().Setup(m => m.Provider).Returns(citiesProvider.AsQueryable().Provider);
            mockCitySet.As<IQueryable<City>>().Setup(m => m.Expression).Returns(citiesProvider.AsQueryable().Expression);
            mockCitySet.As<IQueryable<City>>().Setup(m => m.ElementType).Returns(citiesProvider.AsQueryable().ElementType);
            mockCitySet.As<IQueryable<City>>().Setup(m => m.GetEnumerator()).Returns(citiesProvider.AsQueryable().GetEnumerator());

            // mockRegionSet setup
            mockRegionSet.As<IQueryable<Region>>().Setup(m => m.Provider).Returns(regionsProvider.AsQueryable().Provider);
            mockRegionSet.As<IQueryable<Region>>().Setup(m => m.Expression).Returns(regionsProvider.AsQueryable().Expression);
            mockRegionSet.As<IQueryable<Region>>().Setup(m => m.ElementType).Returns(regionsProvider.AsQueryable().ElementType);
            mockRegionSet.As<IQueryable<Region>>().Setup(m => m.GetEnumerator()).Returns(regionsProvider.AsQueryable().GetEnumerator());

            // mockContext setup
            mockContext.Setup(c => c.Regions).Returns(mockRegionSet.Object);
            mockContext.Setup(c => c.Set<Region>()).Returns(mockRegionSet.Object);
            mockContext.Setup(c => c.Cities).Returns(mockCitySet.Object);
            mockContext.Setup(c => c.Set<City>()).Returns(mockCitySet.Object);
        }

        [TestMethod()]
        public void CityRepository_IsDbSetInitialized_True()
        {
            int expected = 5;
            int actual = (mockCitySet.Object).Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CityRepository_GetAll_All()
        {
            // Arrange
            CityRepository cityRepo = new CityRepository(mockContext.Object);
            // Act
            List<City> listCities = cityRepo.GetAll().ToList();
            // Assert all objects from the List
            for (int i = 0; i < listCities.Count; i++)
            {
                Assert.AreEqual(citiesProvider[i].CityName, listCities[i].CityName);
                // Preview Test results in Test->Debug mode:
                Console.WriteLine($"Data Provider: {citiesProvider[i].CityName}, " +
                    $"Repository: {listCities[i].CityName}");
            }
        }

        [TestMethod()]
        public void RegionRepository_IsDbSetInitialized_True()
        {
            int expected = 5;
            int actual = (mockRegionSet.Object).Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RegionRepository_GetAll_All()
        {
            RegionRepository regionRepo = new RegionRepository(mockContext.Object);
            List<Region> listRegions = regionRepo.GetAll().ToList();
            for (int i = 0; i < listRegions.Count; i++)
            {
                Assert.AreEqual(regionsProvider[i].RegionName, listRegions[i].RegionName);
                // Preview Test results in Test->Debug mode:
                Console.WriteLine($"Data Provider: {regionsProvider[i].RegionName}, " +
                    $"Repository: {listRegions[i].RegionName}");
            }
        }
    }
}
