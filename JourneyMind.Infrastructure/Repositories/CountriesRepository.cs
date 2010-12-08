using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using JourneyMind.Domain;
using www.oorsprong.org.websamples.countryinfo;

namespace JourneyMind.Infrastructure.Repositories
{
    public class CountriesRepository
    {
        private readonly CountryInfoServiceSoapTypeClient _countryInfoServiceSoapTypeClient;

        public CountriesRepository()
        {
            _countryInfoServiceSoapTypeClient =
                new CountryInfoServiceSoapTypeClient(new BasicHttpBinding {MaxReceivedMessageSize = 9000000},
                                                     new EndpointAddress(
                                                         "http://www.oorsprong.org/websamples.countryinfo/CountryInfoService.wso"));
        }

        public CountriesRepository(CountryInfoServiceSoapTypeClient countryInfoServiceSoapTypeClient)
        {
            _countryInfoServiceSoapTypeClient = countryInfoServiceSoapTypeClient;
        }

        public virtual List<Country> GetAll()
        {
            List<Country> countries;
            try
            {
                countries = _countryInfoServiceSoapTypeClient.FullCountryInfoAllCountries().Select(
                    country => new Country {Code = country.sISOCode, Name = country.sName, Flag = country.sCountryFlag})
                    .ToList();
            }
            catch (Exception)
            {
                throw new Exception(string.Format("The web service located in {0} is not available.",
                                                  _countryInfoServiceSoapTypeClient.Endpoint.ListenUri));
            }
            return countries;
        }

        public virtual Country GetByCode(string code)
        {
            tCountryInfo country;
            try
            {
                country = _countryInfoServiceSoapTypeClient.FullCountryInfo(code);
            }
            catch (Exception)
            {
                throw new Exception(string.Format("The web service located in {0} is not available.",
                                                  _countryInfoServiceSoapTypeClient.Endpoint.ListenUri));
            }

            if (country!=null)
            {
                return new Country
                           {
                               Code = country.sISOCode,
                               Name = country.sName,
                               CapitalCity = country.sCapitalCity,
                               Flag = country.sCountryFlag
                           };
            }
            return null;
        }
    }
}