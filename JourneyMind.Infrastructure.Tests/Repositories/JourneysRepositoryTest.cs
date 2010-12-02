using System.Collections.Generic;
using JourneyMind.Domain;
using JourneyMind.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JourneyMind.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class JourneysRepositoryTest
    {
        [TestMethod]
        public void GetAll_ReturnsANotNullList()
        {
            var journeysRepository = new JourneysRepository();
            List<Journey> journeys = journeysRepository.GetAll();

            Assert.IsNotNull(journeys);
        }
    }
}
