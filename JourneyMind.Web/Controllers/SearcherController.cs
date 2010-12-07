using System.Collections.Generic;
using System.Web.Mvc;
using JourneyMind.Domain;
using JourneyMind.Infrastructure.Repositories;

namespace JourneyMind.Web.Controllers
{
    public class SearcherController : Controller
    {
        private readonly JourneysRepository _journeysRepository;

        public SearcherController(JourneysRepository journeysRepository)
        {
            _journeysRepository = journeysRepository;
        }

        public SearcherController()
        {
            _journeysRepository = new JourneysRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search()
        {
            List<Country> retreivedCountries =_journeysRepository.GetAll();
            return View(retreivedCountries);
        }
    }
}
