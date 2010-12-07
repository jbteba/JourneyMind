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
    public class SearcherControllerTest
    {
        [TestMethod]
        public void Searcher_CountriesRepositoryIsNotNull()
        {
            var searcherController = new SearcherController();

            Assert.IsNotNull(searcherController.GetFieldValue<CountriesRepository>("_countriesRepository"));
        }

        [TestMethod]
        public void Index_ReturnsAView()
        {
            
            var searcherController = new SearcherController();

            var searcherViewResult = searcherController.Index() as ViewResult;

            Assert.IsNotNull(searcherViewResult.ViewData);
        }

        [TestMethod]
        public void PostSearch_CallsToGetAllMethodFromCountriesRepository()
        {
            var mockCountriesRepository = MockRepository.GenerateMock<CountriesRepository>();

            var searcherController = new SearcherController(mockCountriesRepository);
            searcherController.Search();

            mockCountriesRepository.AssertWasCalled(m => m.GetAll());
        }

        [TestMethod]
        public void PostSearch_ReturnsACountryList()
        {
            var stubCountriesRepository = MockRepository.GenerateStub<CountriesRepository>();
            var countries = new List<Country>();
            stubCountriesRepository.Stub(s => s.GetAll()).Return(countries);

            var searcherController = new SearcherController(stubCountriesRepository);
            var searcherViewResult = searcherController.Search() as ViewResult;

            Assert.AreEqual(countries, searcherViewResult.ViewData.Model);
        }
    }
}
