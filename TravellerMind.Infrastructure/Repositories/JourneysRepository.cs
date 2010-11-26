using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravellerMind.Domain;

namespace TravellerMind.Infrastructure.Repositories
{
    public class JourneysRepository
    {
        public virtual List<Journey> GetJourneys(Search search)
        {
            return new List<Journey>();
        }
    }
}