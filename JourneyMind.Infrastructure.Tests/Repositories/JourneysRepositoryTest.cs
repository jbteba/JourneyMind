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
    public class JourneysRepositoryTest
    {
        [TestMethod]
        public void JourneyRepository_CountryInfoServiceSoapTypeClientIsNotNull()
        {
            var journeyRepository = new JourneysRepository();

            Assert.IsNotNull(
                journeyRepository.GetFieldValue<CountryInfoServiceSoapTypeClient>("_countryInfoServiceSoapTypeClient"));
        }

        [TestMethod]
        public void GetAll_ReturnsANotEmptyList()
        {
            var journeysRepository = new JourneysRepository();

            List<Journey> journeys = journeysRepository.GetAll();

            Assert.AreNotEqual(0, journeys.Count);
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
            
            var journeysRepository = new JourneysRepository(stubCountryInfoServiceSoapTypeClient);
            List<Journey> journeys = journeysRepository.GetAll();

            Assert.AreEqual("CountryName", journeys[0].Country);
            Assert.AreEqual("CountryName2", journeys[1].Country);
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

            var journeysRepository = new JourneysRepository(stubCountryInfoServiceSoapTypeClient);
            List<Journey> journeys = journeysRepository.GetAll();

            Assert.AreEqual("flag1", journeys[0].Flag);
            Assert.AreEqual("flag2", journeys[1].Flag);
        }

        [TestMethod]
        public void GetAll_MethodFullCountryInfoAllCountriesIsCalled()
        {
            var stubBinding = new BasicHttpBinding();
            var stubEndpointAddress = new EndpointAddress("htt://stubEndPoint");
            var mockCountryInfoServiceSoapTypeClient =
                MockRepository.GenerateStub<CountryInfoServiceSoapTypeClient>(stubBinding, stubEndpointAddress);
            mockCountryInfoServiceSoapTypeClient.Stub(s => s.FullCountryInfoAllCountries()).Return(new tCountryInfo[0]);

            var journeysRepository = new JourneysRepository(mockCountryInfoServiceSoapTypeClient);
            journeysRepository.GetAll();
            
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

            var journeysRepository = new JourneysRepository(stubCountryInfoServiceSoapTypeClient);
            
            try
            {
                journeysRepository.GetAll();
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
