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
    [Route("api/state")]
    public class StatesController : ControllerBase
    {
        private readonly PistisContext db ;
        public StatesController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getAll")]
        public List<State> GetAll()
        {
            var data = db.States.Where(x => x.IsActive == true).ToList();
            return data;
        }
        [HttpGet]
        [Route("getById")]
        public State GetById(int id)
        {
            var data = db.States.Where(x => x.IsActive == true && x.Id==id).FirstOrDefault();
            return data;
        }
        [HttpGet]
        [Route("getByCountryId")]
        public List<State> GetByCountryId(int id)
        {
            var data = db.States.Where(x => x.IsActive == true && x.CountryId==id).ToList();
            return data;
        }
    }
}
