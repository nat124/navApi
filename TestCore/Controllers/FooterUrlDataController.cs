using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Localdb;
using Models;
using TestCore.Extension_Method;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;

namespace TestCore.Controllers
{
    [Route("api/FooterUrlData")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class FooterUrlDataController : ControllerBase
    {
        private readonly PistisContext db;
        public FooterUrlDataController(PistisContext pistis)
        {
            db = pistis;
        }
        [Route("FooterUrlData")]
        public int CheckFooterData(int Id)
        {
            var HaveData = 0;
            var data = db.FooterUrlDatas.Where(x => x.IsActive == true && x.FooterUrlId == Id).FirstOrDefault();
            if (data != null)
            {
               HaveData = 0;
            }
            else
            {
                HaveData = 1;

            }
            return HaveData;
        }
        [HttpGet]
        [Route("getFooterUrlDatas")]
        public IActionResult getFooterS()
        {
            var data = db.FooterUrlDatas.Where(x => x.IsActive == true).Include(x => x.FooterUrl).OrderByDescending(x => x.Id).ToList().RemoveReferences();
            try
            {
                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Ok(data);

            }
        }
        [Route("getFooterUrlData")]
        public List<FooterUrlData> getFooter()
        {
            var data = db.FooterUrlDatas.Where(x => x.IsActive == true).Include(x => x.FooterUrl).ToList().RemoveReferences();
            try
            {
                return data;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return data;

            }
        }

        [HttpGet]
        [Route("getOneFooterUrlData")]
        public FooterUrlData GetFooterUrlData(int Id)
        {
            var data = db.FooterUrlDatas.Where(x => x.IsActive == true && x.FooterUrlId == Id).FirstOrDefault();
            try
            {
                return data;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return data;

            }
        }
        [HttpGet]
        [Route("getOneFooterUrlData1")]
        public FooterUrlData GetFooterUrlData1(int Id)
        {
            var data = db.FooterUrlDatas.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault();
            try
            {
                return data;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return data;

            }
        }
        [HttpPost]
        [Route("saveFooterUrlData")]
        public FooterUrlData Footer([FromBody]FooterData formData)
        {
            Models.FooterUrlData Model = new Models.FooterUrlData();
            try
            {
                if (formData != null)
                {
                    if (formData.Id > 0)
                    {
                        var obj = db.FooterUrlDatas.Where(x => x.Id == formData.Id).FirstOrDefault();
                        obj.FooterUrlId = formData.FooterUrlId;
                        obj.Data = formData.Data;
                        Model.IsActive = true;
                    }
                    else
                    {
                        Model.FooterUrlId = formData.FooterUrlId;
                        Model.Data = formData.Data;
                        Model.IsActive = true;
                        db.FooterUrlDatas.Add(Model);

                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return Model;
        }
        [Route("updateFooterUrlData")]
        public FooterUrlData UpdateFooter(string data, int Id)
        {
            var obj = db.FooterUrlDatas.Where(x => x.Id == Id).FirstOrDefault();

            if (obj != null)
            {
                obj.Data = data;
                obj.FooterUrlId = Id;
                obj.IsActive = true;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return obj;
        }
        [HttpGet]
        [Route("deleteFooterUrlData")]
        public FooterUrlData deleteFooter(int Id)
        {
            var obj = db.FooterUrlDatas.Where(x => x.Id == Id).FirstOrDefault();

            if (obj != null)
            {
               
                obj.IsActive = false;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return obj;
        }
        [HttpPost]
        [Route("getLink")]
        public async Task<IActionResult> getLink(image obj)
        {
            try
            { var image = "";
                var imageResponse = await  S3Service.UploadObject(obj.imageUrl);
                var response = new JsonResult(new Object());
                var newImage = new image();

                if (imageResponse.Success)
                {
                    newImage.imageUrl = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                }
                return Ok(newImage);
                }
            catch (Exception ex)
            {

                throw;
            }
        }
        public class image
        {
            public string imageUrl { get; set; }
        }
    }
}
