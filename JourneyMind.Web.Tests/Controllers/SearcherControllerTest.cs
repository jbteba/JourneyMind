using System.Collections.Generic;
using System.Web.Mvc;
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
            // Arrange
            var searcherController = new SearcherController();
            Assert.Inconclusive("Utilizar la dll TestTools del framework para obtener la propiedad privada");
        }

        [TestMethod]
        public void Index_ReturnsAView()
        {
            // Arrange
            var searcherController = new SearcherController();

            // Act
            var searcherViewResult = searcherController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(searcherViewResult.ViewData);
        }

        [TestMethod]
        public void PostSearch_CallsToGetJourneysMethodFromJourneysRepository()
        {
            // Arrange
            var mockJourneyRepository = MockRepository.GenerateMock<JourneysRepository>();
            var stubSearch = MockRepository.GenerateStub<Search>();

            // Act
            var searcherController = new SearcherController(mockJourneyRepository);
            searcherController.Search(stubSearch);

            // Assert
            mockJourneyRepository.AssertWasCalled(m => m.GetJourneys(stubSearch));
        }

        [TestMethod]
        public void PostSearch_ReturnsAJourneyList()
        {
            // Arrange
            var stubJourneyRepository = MockRepository.GenerateStub<JourneysRepository>();
            var stubSearch = MockRepository.GenerateStub<Search>();
            var journeys = new List<Journey>();

            stubJourneyRepository.Stub(s => s.GetJourneys(stubSearch)).Return(journeys);

            // Act
            var searcherController = new SearcherController(stubJourneyRepository);
            var searcherViewResult = searcherController.Search(stubSearch) as ViewResult;

            // Assert
            Assert.AreEqual(journeys, searcherViewResult.ViewData.Model);
        }
    }
}
