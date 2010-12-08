using System.Collections.Generic;
using System.Web.Mvc;
using JourneyMind.Domain;
using JourneyMind.Infrastructure.Repositories;

namespace JourneyMind.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly CountriesRepository _countriesRepository;

        public CountriesController(CountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public CountriesController()
        {
            _countriesRepository = new CountriesRepository();
        }

        public ActionResult Index()
        {
            return View(_countriesRepository.GetAll());
        }

        public ActionResult Details(string id)
        {
            return View(_countriesRepository.GetByCode(id));
        }
    }
}
