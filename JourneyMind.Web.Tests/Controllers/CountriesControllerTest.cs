using System.Collections.Generic;
using System.Web.Mvc;
using AtentoFramework2008.TestTools.Helpers;
using JourneyMind.Domain;
using JourneyMind.Infrastructure.Repositories;
using JourneyMind.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace JourneyMind.WebTests.Controllers
{
    [TestClass]
    public class CountriesControllerTest
    {
        [TestMethod]
        public void Countries_CountriesRepositoryIsNotNull()
        {
            var countriesController = new CountriesController();

            Assert.IsNotNull(countriesController.GetFieldValue<CountriesRepository>("_countriesRepository"));
        }
        
        [TestMethod]
        public void Index_CallsToGetAllMethodFromCountriesRepository()
        {
            var mockCountriesRepository = MockRepository.GenerateMock<CountriesRepository>();

            var countriesController = new CountriesController(mockCountriesRepository);
            countriesController.Index();

            mockCountriesRepository.AssertWasCalled(m => m.GetAll());
        }

        [TestMethod]
        public void Index_ReturnsACountryList()
        {
            var stubCountriesRepository = MockRepository.GenerateStub<CountriesRepository>();
            var countries = new List<Country>();
            stubCountriesRepository.Stub(s => s.GetAll()).Return(countries);

            var countriesController = new CountriesController(stubCountriesRepository);
            var countriesViewResult = countriesController.Index() as ViewResult;

            Assert.AreEqual(countries, countriesViewResult.ViewData.Model);
        }

        [TestMethod]
        public void Details_CallsToGetByCodeMethodFromCountriesRepository()
        {
            const string isoCode = "countryCode";
            var mockCountriesRepository = MockRepository.GenerateMock<CountriesRepository>();

            var countriesController = new CountriesController(mockCountriesRepository);
            countriesController.Details(isoCode);

            mockCountriesRepository.AssertWasCalled(m => m.GetByCode(isoCode));
        }

        [TestMethod]
        public void Details_ReturnsACountry()
        {
            const string stubIsoCode = "countryCode";
            var stubCountry = new Country();
            var stubCountriesRepository = MockRepository.GenerateStub<CountriesRepository>();
            stubCountriesRepository.Stub(s => s.GetByCode(stubIsoCode)).Return(stubCountry);

            var countriesController = new CountriesController(stubCountriesRepository);
            var countryViewResult = countriesController.Details(stubIsoCode) as ViewResult;

            Assert.AreEqual(stubCountry, countryViewResult.ViewData.Model);
        }
    }
}
