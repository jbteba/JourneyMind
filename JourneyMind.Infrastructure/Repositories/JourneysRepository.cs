using System.Collections.Generic;
using JourneyMind.Domain;

namespace JourneyMind.Infrastructure.Repositories
{
    public class JourneysRepository
    {
        public virtual List<Journey> GetAll()
        {
            return new List<Journey>();
        }
    }
}