using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestCore.Helper;
using Models;
using TestCore.Extension_Method;
using System.Web;
using System.IO;
using Localdb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;

namespace TestCore.Controllers
{
    [Route("api/sliders")]
    [EnableCors("EnableCORS")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly PistisContext db;
        private readonly IHostingEnvironment environment;

        public SliderController(PistisContext pistis, IHostingEnvironment IHostingEnvironment)
        {
            db = pistis;
            environment = IHostingEnvironment;
        }
        [Route("getsliderImages")]
        public IActionResult getsliderImages(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);
            var data = new List<Slider>();
            var query = db.Sliders.Where(x => x.IsActive == true).OrderByDescending(x => x.Id);
            try
            {
                if (search == null)
                    data = query.ToList();
                else
                    data = query.Where(v => v.Url.Contains(search)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            var response = new
            {
                data = data.Skip(skipData).Take(pageSize).ToList(),
                count = data.Count
            };
            return Ok(response);
        }

        
            [Route("getsliderImagesForBackend")]
        public IActionResult getsliderImagesForBackend()
        {
            var data = new List<Slider>();
            try
            {
                data = db.Sliders.Where(x => x.IsActive == true )
                    .OrderByDescending(x => x.Id).ToList();
                // data = query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(data);
        }
        [Route("getsliderImages1")]
        public async Task <IActionResult> getsliderImages()
        {
            var data = new List<Slider>();

            try
            {
                data = await db.Sliders.Where(x => x.IsActive == true && x.ToDateTime > DateTime.Now)
                    .OrderByDescending(x => x.Id).ToListAsync();

                // data = query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(data);
        }



        [HttpPost]
        [Route("addSlider")]
        public async Task<IActionResult> addSlider([FromBody]Slider model)
        {
            JsonResult response = null;

            try
            {

                //----uploaded to s3
                if (model.Image.Contains("https://pistis.s3.us-east-2.amazonaws.com/"))
                {
                    Slider slider = new Slider();
                    slider.FromDateTime = model.FromDateTime;
                    slider.ToDateTime = model.ToDateTime;
                    slider.IsActive = true;
                    slider.Url = model.Url;
                    slider.Image = model.Image;
                    if (model.Id > 0)
                    {
                        var sliderUpdate = db.Sliders.Where(x => x.Id == model.Id).FirstOrDefault();
                        sliderUpdate.FromDateTime = model.FromDateTime;
                        sliderUpdate.ToDateTime = model.ToDateTime;
                        slider.Image = model.Image;
                        sliderUpdate.IsActive = true;
                        sliderUpdate.Url = model.Url;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Sliders.Add(slider);
                        db.SaveChanges();
                    }


                }
                else
                {

                    if (model.Image != null)
                    {
                        var imageResponse = await S3Service.UploadObject(model.Image);
                        response = new JsonResult(new Object());
                        if (imageResponse.Success)
                        {

                            Slider slider = new Slider();

                            slider.FromDateTime = model.FromDateTime;
                            slider.ToDateTime = model.ToDateTime;
                            slider.Image = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                            slider.IsActive = true;
                            slider.Url = model.Url;
                            if (model.Id > 0)
                            {
                                var sliderUpdate = db.Sliders.Where(x => x.Id == model.Id).FirstOrDefault();
                                sliderUpdate.FromDateTime = model.FromDateTime;
                                sliderUpdate.ToDateTime = model.ToDateTime;
                                sliderUpdate.Image = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                                sliderUpdate.IsActive = true;
                                slider.Url = model.Url;
                                db.Entry(sliderUpdate).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                db.Sliders.Add(slider);
                                db.SaveChanges();
                            }

                            response.StatusCode = 0;
                            response.Value = "Uploaded done!";
                        }
                        else
                        {
                            response.StatusCode = -1;
                            response.Value = "Upload failed!";
                        }
                    }

                    //return Request.CreateResponse(HttpStatusCode.Created);

                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                //return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                return null;
            }
        }

        //add slider action is done inplace of this.
        //[HttpPost]
        //[Route("UploadFile")]

        //public IActionResult UploadFile()
        //{
        // Slider slider = new Slider();
        // var Id = HttpContext.Request.Form["Id"];
        // int sliderId = Convert.ToInt32(Id);
        // if (sliderId > 0)
        // {
        // slider = db.Sliders.AsNoTracking().Where(x => x.Id == sliderId).FirstOrDefault();

        // }
        // var postedFile = HttpContext.Request.Form.Files["Image"];
        // var FromDateTime = HttpContext.Request.Form["FromDateTime"];
        // var ToDateTime = HttpContext.Request.Form["ToDateTime"];

        // if (postedFile.Length > 0)
        // {
        // var fileName = Path.GetFileName(postedFile.FileName);
        // var path = Path.Combine(environment.WebRootPath, "uploads", fileName);
        // postedFile.CopyTo(new FileStream(path, FileMode.Create));



        // }

        // if (!string.IsNullOrEmpty(FromDateTime))
        // slider.FromDateTime = Convert.ToDateTime(FromDateTime);
        // if (!string.IsNullOrEmpty(ToDateTime))
        // slider.ToDateTime = Convert.ToDateTime(ToDateTime);
        // slider.IsActive = true;
        // if (!string.IsNullOrEmpty(postedFile.FileName))
        // slider.Image = "~/uploads/" + postedFile.FileName;
        // try
        // {
        // if (sliderId > 0)
        // {
        // db.Entry(slider).State = EntityState.Modified;
        // db.SaveChanges();
        // }
        // else
        // {
        // db.Sliders.Add(slider);
        // db.SaveChanges();
        // }
        // //return Request.CreateResponse(HttpStatusCode.Created);
        // return Ok(slider);
        // }
        // catch (Exception ex)
        // {
        // Console.Write(ex);
        // //return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        // return null;
        // }

        //}
        [Route("delete")]
        public Slider DeleteCategory(int id)
        {
            var obj = db.Sliders.Find(id);
            if (obj != null)
            {
                obj.IsActive = false;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            { }
            return obj;

        }
    }
}