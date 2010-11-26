using System.Collections.Generic;
using JourneyMind.Domain;

namespace JourneyMind.Infrastructure.Repositories
{
    public class JourneysRepository
    {
        public virtual List<Journey> GetJourneys(Search search)
        {
            return new List<Journey>();
        }
    }
}