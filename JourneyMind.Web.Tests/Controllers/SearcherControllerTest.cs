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
        public void Searcher_JourneyRepositoryIsNotNull()
        {
            var searcherController = new SearcherController();

            Assert.IsNotNull(searcherController.GetFieldValue<JourneysRepository>("_journeysRepository"));
        }

        [TestMethod]
        public void Index_ReturnsAView()
        {
            
            var searcherController = new SearcherController();

            var searcherViewResult = searcherController.Index() as ViewResult;

            Assert.IsNotNull(searcherViewResult.ViewData);
        }

        [TestMethod]
        public void PostSearch_CallsToGetJourneysMethodFromJourneysRepository()
        {
            var mockJourneyRepository = MockRepository.GenerateMock<JourneysRepository>();
            
            var searcherController = new SearcherController(mockJourneyRepository);
            searcherController.Search();

            mockJourneyRepository.AssertWasCalled(m => m.GetAll());
        }

        [TestMethod]
        public void PostSearch_ReturnsAJourneyList()
        {
            var stubJourneyRepository = MockRepository.GenerateStub<JourneysRepository>();
            var journeys = new List<Journey>();
            stubJourneyRepository.Stub(s => s.GetAll()).Return(journeys);

            var searcherController = new SearcherController(stubJourneyRepository);
            var searcherViewResult = searcherController.Search() as ViewResult;

            Assert.AreEqual(journeys, searcherViewResult.ViewData.Model);
        }
    }
}
