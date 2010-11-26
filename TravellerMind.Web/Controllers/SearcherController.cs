using System.Collections.Generic;
using System.Web.Mvc;
using TravellerMind.Domain;
using TravellerMind.Infrastructure.Repositories;

namespace TravellerMind.Web.Controllers
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

        //
        // GET: /Searcher/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(Search search)
        {
            List<Journey> retreivedJourneys =_journeysRepository.GetJourneys(search);
            return View(retreivedJourneys);
        }
    }
}
