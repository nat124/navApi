using Localdb;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestCore.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly PistisContext db;

        public CountryController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getAll")]
        public List<Country> GetAll()
        {

            var data = db.Countries.Where(x => x.IsActive == true).ToList();
            return data;
        }

        [HttpGet]
        [Route("getById")]
        public Country GetById(int id)
        {
            var data = db.Countries.Where(x => x.IsActive == true && x.Id==id).FirstOrDefault();
            return data;
        }

        [HttpGet]
        [Route("getStateByCountry")]
        public IActionResult getStateByCountry(int id)
        {
            return Ok(db.States.Where(x => x.IsActive == true && x.CountryId == id).ToList());
        }
    }
}
