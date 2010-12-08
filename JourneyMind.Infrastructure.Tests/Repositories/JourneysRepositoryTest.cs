using System;
using System.Collections.Generic;
using System.ServiceModel;
using AtentoFramework2008.TestTools.Helpers;
using JourneyMind.Domain;
using JourneyMind.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using www.oorsprong.org.websamples.countryinfo;

namespace JourneyMind.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class CountriesRepositoryTest
    {
        [TestMethod]
        public void _CountryInfoServiceSoapTypeClientIsNotNull()
        {
            var countriesRepository = new CountriesRepository();

            Assert.IsNotNull(
                countriesRepository.GetFieldValue<CountryInfoServiceSoapTypeClient>("_countryInfoServiceSoapTypeClient"));
        }

        [TestMethod]
        public void GetAll_ReturnsANotEmptyList()
        {
            var countriesRepository = new CountriesRepository();

            List<Country> countries = countriesRepository.GetAll();

            Assert.AreNotEqual(0, countries.Count);
        }

        [TestMethod]
        public void GetAll_MethodFullCountryInfoAllCountriesIsCalled()
        {
            var stubBinding = new BasicHttpBinding();
            var stubEndpointAddress = new EndpointAddress("htt://stubEndPoint");
            var mockCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateStub<CountryInfoServiceSoapTypeClient>(stubBinding, stubEndpointAddress);
            mockCountryInfoServiceSoapTypeClient.Stub(s => s.FullCountryInfoAllCountries()).Return(new tCountryInfo[0]);

            var countriesRepository = new CountriesRepository(mockCountryInfoServiceSoapTypeClient);
            countriesRepository.GetAll();

            mockCountryInfoServiceSoapTypeClient.AssertWasCalled(m => m.FullCountryInfoAllCountries());
        }

        [TestMethod]
        public void GetAll_ReturnsACountriesList()
        {
            var stubCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateStub<CountryInfoServiceSoapTypeClient>(new BasicHttpBinding(),
                                                                              new EndpointAddress("http://stubEndPoint"));
            stubCountryInfoServiceSoapTypeClient.Stub(s => s.FullCountryInfoAllCountries()).Return(new[]
                                                                                                       {
                                                                                                           new tCountryInfo
                                                                                                               {
                                                                                                                   sISOCode
                                                                                                                       =
                                                                                                                       "CountryCode",
                                                                                                                   sName
                                                                                                                       =
                                                                                                                       "CountryName",
                                                                                                                   sCountryFlag
                                                                                                                       =
                                                                                                                       "flag"
                                                                                                               },
                                                                                                           new tCountryInfo
                                                                                                               {
                                                                                                                   sISOCode
                                                                                                                       =
                                                                                                                       "CountryCode2",
                                                                                                                   sName
                                                                                                                       =
                                                                                                                       "CountryName2",
                                                                                                                   sCountryFlag
                                                                                                                       =
                                                                                                                       "flag2"
                                                                                                               }
                                                                                                       });

            var countriesRepository = new CountriesRepository(stubCountryInfoServiceSoapTypeClient);
            List<Country> countries = countriesRepository.GetAll();

            Assert.AreEqual("CountryCode", countries[0].Code);
            Assert.AreEqual("CountryCode2", countries[1].Code);
            Assert.AreEqual("CountryName", countries[0].Name);
            Assert.AreEqual("CountryName2", countries[1].Name);
            Assert.AreEqual("flag", countries[0].Flag);
            Assert.AreEqual("flag2", countries[1].Flag);
        }

        [TestMethod]
        public void GetAll_WhenTheWebServiceIsNotAvailable_AnExceptionIsThrown()
        {
            var stubBinding = new BasicHttpBinding();
            var stubEndpointAddress = new EndpointAddress("http://stubEndPoint");
            var stubCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateStub<CountryInfoServiceSoapTypeClient>(stubBinding, stubEndpointAddress);
            stubCountryInfoServiceSoapTypeClient.Stub(s => s.FullCountryInfoAllCountries()).Throw(new Exception());

            var countriesRepository = new CountriesRepository(stubCountryInfoServiceSoapTypeClient);

            try
            {
                countriesRepository.GetAll();
                Assert.Fail("An exception should has been thrown");
            }
            catch (Exception exception)
            {
                var errorMessage = string.Format("The web service located in {0} is not available.",
                                                 stubEndpointAddress.Uri);
                Assert.AreEqual(errorMessage, exception.Message);
            }
        }

        [TestMethod]
        public void GetByCode_MethodFullCountryInfoIsCalled()
        {
            const string stubIsoCode = "isoCode";
            var mockCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateMock<CountryInfoServiceSoapTypeClient>(new BasicHttpBinding(),
                                                                              new EndpointAddress("htt://stubEndPoint"));
            mockCountryInfoServiceSoapTypeClient.Stub(s => s.FullCountryInfo(stubIsoCode)).Return(new tCountryInfo());

            var countriesRepository = new CountriesRepository(mockCountryInfoServiceSoapTypeClient);
            countriesRepository.GetByCode(stubIsoCode);

            mockCountryInfoServiceSoapTypeClient.AssertWasCalled(m => m.FullCountryInfo(stubIsoCode));
        }

        [TestMethod]
        public void GetByCode_WhenExistsACountryWithThisCode_ReturnsACountry()
        {
            const string stubIsoCode = "isoCode";
            var stubCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateStub<CountryInfoServiceSoapTypeClient>(new BasicHttpBinding(),
                                                                              new EndpointAddress("http://stubEndPoint"));
            stubCountryInfoServiceSoapTypeClient.Stub(s => s.FullCountryInfo(stubIsoCode)).Return(new tCountryInfo
                                                                                                      {
                                                                                                          sName =
                                                                                                              "CountryName",
                                                                                                          sCapitalCity =
                                                                                                              "CapitalCityName",
                                                                                                          sCountryFlag =
                                                                                                              "UrlFlag"
                                                                                                      });

            var countriesRepository = new CountriesRepository(stubCountryInfoServiceSoapTypeClient);
            Country country = countriesRepository.GetByCode(stubIsoCode);

            Assert.AreEqual("CountryName", country.Name);
            Assert.AreEqual("CapitalCityName", country.CapitalCity);
            Assert.AreEqual("UrlFlag", country.Flag);
        }

        [TestMethod]
        public void GetByCode_WhenNotExistsACountryWithThisCode_ReturnsNull()
        {
            const string nonExistentIsoCode = "isoCode";
            var stubCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateStub<CountryInfoServiceSoapTypeClient>(new BasicHttpBinding(),
                                                                              new EndpointAddress("http://stubEndPoint"));

            var countriesRepository = new CountriesRepository(stubCountryInfoServiceSoapTypeClient);
            Country country = countriesRepository.GetByCode(nonExistentIsoCode);

            Assert.IsNull(country);
        }

        [TestMethod]
        public void GetByCode_WhenTheWebServiceIsNotAvailable_AnExceptionIsThrown()
        {
            const string stubIsoCode = "isoCode";
            var stubEndpointAddress = new EndpointAddress("http://stubEndPoint");
            var stubCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateStub<CountryInfoServiceSoapTypeClient>(new BasicHttpBinding(),
                                                                              stubEndpointAddress);
            stubCountryInfoServiceSoapTypeClient.Stub(s => s.FullCountryInfo(stubIsoCode)).Throw(new Exception());

            var countriesRepository = new CountriesRepository(stubCountryInfoServiceSoapTypeClient);

            try
            {
                countriesRepository.GetByCode(stubIsoCode);
                Assert.Fail("An exception should has been thrown");
            }
            catch (Exception exception)
            {
                var errorMessage = string.Format("The web service located in {0} is not available.",
                                                 stubEndpointAddress.Uri);
                Assert.AreEqual(errorMessage, exception.Message);
            }
        }
    }
}
