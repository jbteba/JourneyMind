using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellerMind.Domain;
using TravellerMind.Infrastructure.Repositories;

namespace TravellerMind.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class JourneysRepositoryTest
    {
        [TestMethod]
        public void GetJourneys_ReturnsANotNullList()
        {
            var journeysRepository = new JourneysRepository();
            List<Journey> journeys = journeysRepository.GetJourneys(null);

            Assert.IsNotNull(journeys);
        }
    }
}
