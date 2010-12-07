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
        public void GetAll_ReturnsAListWithCountryNameInEachItem()
        {
            var stubCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateMock<CountryInfoServiceSoapTypeClient>(new BasicHttpBinding(),
                                                                              new EndpointAddress("http://stubEndPoint"));
            stubCountryInfoServiceSoapTypeClient.Stub(s => s.FullCountryInfoAllCountries()).Return(new[]
                                                                                                       {
                                                                                                           new tCountryInfo
                                                                                                               {
                                                                                                                   sName
                                                                                                                       =
                                                                                                                       "CountryName"
                                                                                                               },
                                                                                                           new tCountryInfo
                                                                                                               {
                                                                                                                   sName
                                                                                                                       =
                                                                                                                       "CountryName2"
                                                                                                               }
                                                                                                       });

            var countriesRepository = new CountriesRepository(stubCountryInfoServiceSoapTypeClient);
            List<Country> countries = countriesRepository.GetAll();

            Assert.AreEqual("CountryName", countries[0].Name);
            Assert.AreEqual("CountryName2", countries[1].Name);
        }

        [TestMethod]
        public void GetAll_ReturnsAListWithFlagSrcInEachItem()
        {
            var stubCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateMock<CountryInfoServiceSoapTypeClient>(new BasicHttpBinding(),
                                                                              new EndpointAddress("http://stubEndPoint"));
            stubCountryInfoServiceSoapTypeClient.Stub(s => s.FullCountryInfoAllCountries()).Return(new[]
                                                                                                       {
                                                                                                           new tCountryInfo
                                                                                                               {
                                                                                                                   sCountryFlag
                                                                                                                       =
                                                                                                                       "flag1"
                                                                                                               },
                                                                                                           new tCountryInfo
                                                                                                               {
                                                                                                                   sCountryFlag
                                                                                                                       =
                                                                                                                       "flag2"
                                                                                                               }
                                                                                                       });

            var countriesRepository = new CountriesRepository(stubCountryInfoServiceSoapTypeClient);
            List<Country> countries = countriesRepository.GetAll();

            Assert.AreEqual("flag1", countries[0].Flag);
            Assert.AreEqual("flag2", countries[1].Flag);
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
            
            mockCountryInfoServiceSoapTypeClient.AssertWasCalled(m=>m.FullCountryInfoAllCountries());
        }

        [TestMethod]
        public void GetAll_WhenTheWebServiceIsNotAvailable_AnExceptionIsThrown()
        {
            var stubBinding = new BasicHttpBinding();
            var stubEndpointAddress = new EndpointAddress("http://stubEndPoint");
            var stubCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateMock<CountryInfoServiceSoapTypeClient>(stubBinding, stubEndpointAddress);
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
    }
}
