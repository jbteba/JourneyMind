﻿using System.Collections.Generic;
using System.Web.Mvc;
using JourneyMind.Domain;
using JourneyMind.Infrastructure.Repositories;

namespace JourneyMind.Web.Controllers
{
    public class SearcherController : Controller
    {
        private readonly CountriesRepository _countriesRepository;

        public SearcherController(CountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public SearcherController()
        {
            _countriesRepository = new CountriesRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search()
        {
            List<Country> retreivedCountries = _countriesRepository.GetAll();
            return View(retreivedCountries);
        }
    }
}
