using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionCity.DataLayer.DbLayer;
using RegionCity.DataLayer.BOL;
using RegionCity.Storage;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Moq;

namespace RegionCity.Test
{
    [TestClass()]
    public class MoqRepositoryTest
    {
        private static Mock<RegionCityContext> mockContext;
        private static Mock<MockableDbSetWithExtensions<Region>> mockRegionSet;
        private static Mock<MockableDbSetWithExtensions<City>> mockCitySet;
        // Data provider for MockableDbSetWithExtensions<City> instance
        private static List<City> citiesProvider;
        // Data provider for MockableDbSetWithExtensions<Region> instance
        private static List<Region> regionsProvider;
        // CRUD Service instances
        private static CityDataService cityService;
        private static RegionDataService regionService;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext context)
        {
            mockContext = new Mock<RegionCityContext>();
            mockRegionSet = new Mock<MockableDbSetWithExtensions<Region>>();
            mockCitySet = new Mock<MockableDbSetWithExtensions<City>>();
            
            // Initialize data providers
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

            // Instantiate DataService objects
            cityService = new CityDataService(new CityRepository(mockContext.Object));
            regionService = new RegionDataService(new RegionRepository(mockContext.Object));
        }

        [TestMethod()]
        public void CityRepository_IsDbSetInitialized_True()
        {
            int expected = 5;
            int actual = (mockCitySet.Object).Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CityService_GetAll_All()
        {
            // Arrange
            //CityRepository cityRepo = new CityRepository(mockContext.Object);
            // Act
            List<City> listCities = cityService.GetAll().ToList();
            // Assert all objects from the List
            for (int i = 0; i < listCities.Count; i++)
            {
                Assert.AreEqual(citiesProvider[i].CityName, listCities[i].CityName);
                // Preview Test results in Test->Debug mode:
                Console.WriteLine($"Data Provider: {citiesProvider[i].CityName}, " +
                    $"Repository: {listCities[i].CityName}");
            }
        }
        [TestMethod]
        public void CityService_AddOrUpdate_Verify()
        {
            // Arrange
            City cityNew = new City()
            { CityName = "Stuttgart", Region = new Region() { RegionName = "Baden-Württemberg" } };
            City cityExisting = cityService.GetAll().FirstOrDefault();

            // Act
            cityService.AddOrUpdate(cityNew);
            cityService.AddOrUpdate(cityExisting);

            // Assert
            // 1. Verify AddOrUpdate method for new City instance
            mockCitySet.Verify(m => m.AddOrUpdate(It.IsAny<City>()),Times.Exactly(2));
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2));
        }

        [TestMethod()]
        public void RegionService_IsDbSetInitialized_True()
        {
            int expected = 5;
            int actual = (mockRegionSet.Object).Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RegionService_GetAll_All()
        {
            List<Region> listRegions = regionService.GetAll().ToList();
            for (int i = 0; i < listRegions.Count; i++)
            {
                Assert.AreEqual(regionsProvider[i].RegionName, listRegions[i].RegionName);
                // Preview Test results in Test->Debug mode:
                Console.WriteLine($"Data Provider: {regionsProvider[i].RegionName}, " +
                    $"Repository: {listRegions[i].RegionName}");
            }
        }

        [TestMethod]
        public void RegionService_AddOrUpdate_Verify()
        {
            // Arrange
            Region regionNew = new Region(){ RegionName = "Baden-Württemberg" };
            Region regionExisting = regionService.GetAll().FirstOrDefault();

            // Act
            regionService.AddOrUpdate(regionNew);
            regionService.AddOrUpdate(regionExisting);

            // Assert
            // 1. Verify AddOrUpdate method for new City instance
            mockRegionSet.Verify(m => m.AddOrUpdate(It.IsAny<Region>()), Times.AtLeastOnce());
            mockContext.Verify(m => m.SaveChanges(), Times.AtLeastOnce());
        }
    }
}
