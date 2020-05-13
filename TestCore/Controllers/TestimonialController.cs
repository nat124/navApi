using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.Controllers
{
    [Route("api/testimonial")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class TestimonialController : ControllerBase
    {
        private readonly PistisContext db;

        public TestimonialController(PistisContext pistis)
        {
            db = pistis;
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetList()
        {
            try
            {
                var result = db.Testimonial.Where(x => x.IsActive == true).Include(x => x.User)
                    .Select(x=> new
                    {
                        Id =x.Id,
                        Rating = x.Rating,
                        Description = x.Description,
                        IsApproved = x.IsApproved,
                        UserName = x.User.Email,
                        CreatedOn = x.CreatedOn
                    }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("Searchval")]
        public IActionResult GetSearchList(string val)
        {
            try
            {
                var result = db.Testimonial.Where(x => x.IsActive == true).Include(x => x.User)
                      .Select(x => new
                      {
                          Id = x.Id,
                          Rating = x.Rating,
                          Description = x.Description,
                          IsApproved = x.IsApproved,
                          UserName = x.User.Email,
                          CreatedOn = x.CreatedOn
                      }).ToList();
                var abc = result.Where(x => x.UserName.Contains(val)).ToList();
                return Ok(abc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("get-all/approved")]
        public async Task<IActionResult> GetApprovedList()
        {
       //     var result1 = db.Testimonial.Where(x => x.IsActive == true && x.IsApproved == true).Include(x => x.User).ToList();
            try
            {
                var result = await db.Testimonial.Where(x => x.IsActive == true && x.IsApproved == true).Include(x => x.User)
                    .Select(x => new
                    {
                        Id = x.Id,
                        Rating = x.Rating,
                        Description = x.Description,
                        IsApproved = x.IsApproved,
                        UserName = x.User.FirstName,
                        CreatedOn = x.CreatedOn
                    }).ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpPost]
        [Route("save")]
        public IActionResult Add(testi model)
        {
            int result = 0;
            if (model.UserId > 0)
            {
                var testimonial = db.Testimonial.Where(x => x.IsActive == true && x.UserId == model.UserId).FirstOrDefault();
                if (testimonial == null)
                {
                    var data = new Models.Testimonial();
                    data.Description = model.Description;
                    data.CreatedOn = DateTime.Now;
                    data.IsApproved = false;
                    
                    data.Rating = model.Rating;
                    data.UpdatedOn = DateTime.Now;
                    data.UserId = model.UserId;
                    data.IsActive = true;
                    db.Testimonial.Add(data);
                    result = db.SaveChanges();
                }
                else
                {
                    testimonial.Description = model.Description;
                    testimonial.CreatedOn = testimonial.CreatedOn;
                    testimonial.IsApproved = false;
                    if (model.Rating != 0)
                        testimonial.Rating = model.Rating;
                    testimonial.UpdatedOn = DateTime.Now;
                    testimonial.UserId = model.UserId;
                    testimonial.IsActive = true;
                     result = db.SaveChanges();
                }
            }
           
            try
            {
                    return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(int id, bool IsApproved)
        {
            var result = db.Testimonial.Where(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                result.IsApproved = IsApproved;
                result.UpdatedOn = DateTime.UtcNow;
                db.Testimonial.Update(result);
                db.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var result = db.Testimonial.Where(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                db.Testimonial.Update(result);
                db.SaveChanges();
            }
            return Ok("Success");
        }
        [HttpGet]
        [Route("getUserTestimonial")]
        public IActionResult getUsertest(int Id)
        {
            try
            {
                var testimonial = db.Testimonial.Where(x => x.IsActive == true  && x.UserId == Id).FirstOrDefault();
                return Ok(testimonial);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
    public class testi
    {
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}