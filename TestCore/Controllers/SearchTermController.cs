using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TestCore.Controllers
{
    [Route("api/searchTerm")]
    [EnableCors("EnableCORS")]
    [ApiController]
    public class SearchTermController : ControllerBase
    {
        private readonly PistisContext db;
        public SearchTermController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll(int page, int size)
        {
            var skipData = size * (page - 1);
            var data = db.SearchTerm.Where(x => x.IsActive == true).ToList();
            var response = new {
                data= data.Skip(skipData).Take(size).OrderByDescending(c => c.Id),
                count=data.Count,
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("getbById")]
        public IActionResult getbById(int id)
        {
            var data = db.SearchTerm.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault();
            return Ok(data);
        }
        [HttpPost]
        [Route("save")]
        public IActionResult Save(SearchTerm model)
        {
            model.IsActive = true;
            model.IsDisplay = false;
            var data = db.SearchTerm.Where(x => x.Name == model.Name && x.IsActive == true).FirstOrDefault();
            if (data == null)
            {
                db.SearchTerm.Add(model);
            }
            else
            {
                data.UserCount = data.UserCount + 1;
                data.ProductCount = model.ProductCount;
            }
            try
            {
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(SearchTerm model)
        {

            var data = db.SearchTerm.Where(x => x.Id == model.Id && x.IsActive == true).FirstOrDefault();
            if (data != null)
            {
                data.Name = model.Name;
                data.IsDisplay = model.IsDisplay;
            }
            try
            {
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        [Route("getSearchtermdata")]
        public IActionResult searchTerm()
        {
            try
            {
                var response = from srch in db.SearchTerm
                               where srch.IsActive == true && srch.Name!=null
                               select new
                               {
                                   
                                   name=srch.Name
                               };
                var data = response.ToList();
                return Ok(data);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}