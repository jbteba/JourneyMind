using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using JourneyMind.Domain;

namespace JourneyMind.Infrastructure.Repositories
{
    public class JourneysRepository
    {
        private CountryInfoServiceSoapTypeClient _countryInfoServiceSoapTypeClient;

        public JourneysRepository()
        {
            _countryInfoServiceSoapTypeClient = new CountryInfoServiceSoapTypeClient(new BasicHttpBinding { MaxReceivedMessageSize = 9000000},
                                                                                     new EndpointAddress(
                                                                                         "http://www.oorsprong.org/websamples.countryinfo/CountryInfoService.wso"));
        }

        public JourneysRepository(CountryInfoServiceSoapTypeClient countryInfoServiceSoapTypeClient)
        {
            _countryInfoServiceSoapTypeClient = countryInfoServiceSoapTypeClient;
        }

        public virtual List<Journey> GetAll()
        {
            List<Journey> journeys;
            try
            {
                journeys = _countryInfoServiceSoapTypeClient.FullCountryInfoAllCountries().Select(
                                country => new Journey { Country = country.sName }).ToList();
            }
            catch (Exception)
            {
                throw new Exception(string.Format("The web service located in {0} is not available.",
                                                  _countryInfoServiceSoapTypeClient.Endpoint.ListenUri));
            }
            return journeys;
        }
    }
}