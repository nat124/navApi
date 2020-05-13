using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace TestCore.Controllers
{
    [Route("api/banner")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class BannerImagesController : ControllerBase
    {
        private readonly PistisContext db;
        private readonly IHostingEnvironment environment;
        public BannerImagesController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getBannerImages")]
        public List<BannerImages> Images()
        {
            var data = db.BannerImages.Where(x => x.IsActive == true).ToList();
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
        [Route("getBannerImages1")]
        public async Task<IActionResult> Images1()
        {
            var banner = new List<BannerFrontEnd>();
            var data = new List<BannerImages>();
             data = await db.BannerImages.Where(x => x.IsActive == true && x.Side=="Left").OrderBy(x=>x.Position).ToListAsync();
            var ban = new BannerFrontEnd();
            if (data.Count > 0)
            {
                ban.Side = data[0].Side;
                ban.View = data[0].View;
                foreach (var item in data)
                {
                    ban.Images.Add(item.Image);
                    ban.Url.Add(item.Url);
                    ban.Position.Add(item.Position);
                }
                banner.Add(ban);
            }
            ///middle
            ban = new BannerFrontEnd();
            var dataM = await db.BannerImages.Where(x => x.IsActive == true && x.Side == "Middle").OrderBy(x => x.Position).ToListAsync();
            if (dataM.Count > 0)
            {
                ban.Side = dataM[0].Side;
                ban.View = dataM[0].View;
                foreach (var item in dataM)
                {
                    ban.Images.Add(item.Image);
                    ban.Url.Add(item.Url);
                    ban.Position.Add(item.Position);


                }
                banner.Add(ban);
            }
            ///Right
            ban = new BannerFrontEnd();
            var dataR = await db.BannerImages.Where(x => x.IsActive == true && x.Side == "Right").OrderBy(x => x.Position).ToListAsync();
            if (dataR.Count > 0)
            {
                ban.Side = dataR[0].Side;
                ban.View = dataR[0].View;
                foreach (var item in dataR)
                {
                    ban.Images.Add(item.Image);
                    ban.Url.Add(item.Url);
                    ban.Position.Add(item.Position);

                }

                banner.Add(ban);
            }
            try
            {
                return  Ok(banner);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                //  return await banner;
                return Ok(banner);

            }
        }
        [HttpPost]
        [Route("uploadFile")]
        public IActionResult UploadFile([FromBody]BannerImages model)
        {
            BannerImages bannerImages = new BannerImages();
            bannerImages.IsActive = true;
            bannerImages.Image = model.Image;
            try
            {

                db.BannerImages.Add(bannerImages);
                db.SaveChanges();

                return Ok(bannerImages);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Ok(ex);
            }

        }
        /// <summary>
        /// ye wala
        /// </summary>
        /// <param name="model1"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("uploadFile1")]
        public async Task<IActionResult> uploadFile([FromBody]BannerImagesModel1 model1)
        {
            var i = 0;
            var message = 0;
            if (model1 != null)
            {
                if (model1.Position.Count ==1) {
                var check = db.BannerImages
                         .Where(x => x.IsActive == true
                         && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                         && x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                         && x.Position==model1.Position[0]).FirstOrDefault();
                    if (check != null)
                    {
                        check.IsActive = false;
                        db.SaveChanges();
                    }
                }
                else if (model1.Position.Count == 2)
                {
                    var check = db.BannerImages
                         .Where(x => x.IsActive == true
                         && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                         && x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                         && x.Position == model1.Position[0]).FirstOrDefault();
                    if (check != null)
                    {
                        check.IsActive = false;
                        db.SaveChanges();
                    }
                    check = db.BannerImages
                         .Where(x => x.IsActive == true
                         && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                         && x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                         && x.Position == model1.Position[1]).FirstOrDefault();
                    if (check != null)
                    {
                        check.IsActive = false;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var check = db.BannerImages
                        .Where(x => x.IsActive == true
                       // && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                        && x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                      ).ToList();
                    if (check != null)
                    {

                        foreach (var item in check)
                        {
                            item.IsActive = false;
                            db.SaveChanges();

                        }
                    }
                    
                }
                var check1 = db.BannerImages
                       .Where(x => x.IsActive == true
                         &&
                       // && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                      x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                     ).ToList();
                if (check1 != null)
                {

                    foreach (var item in check1)
                    {
                        item.IsActive = false;
                        db.SaveChanges();

                    }
                }

                if (model1.ImageUrl.Count > 0) { 

                foreach (var item in model1.ImageUrl)
                {

                        if (item != null)
                        {
                            BannerImages bannerImages = new BannerImages();
                            var imageResponse = await S3Service.UploadObject(item);
                            var response = new JsonResult(new Object());
                            if (imageResponse.Success)
                            {
                                bannerImages.Image = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}"; ;
                                bannerImages.Side = model1.side.FirstOrDefault();
                                bannerImages.View = model1.view.FirstOrDefault();
                                bannerImages.IsActive = true;

                                bannerImages.Position = model1.Position[i];
                                if(model1.Position[i]==1)
                                    if (model1.ImageUrl1 != null && model1.ImageUrl1!="")
                                        bannerImages.Url = model1.ImageUrl1;
                                if (model1.Position[i] == 2)
                                    if (model1.ImageUrl2 != null && model1.ImageUrl2 != "")
                                        bannerImages.Url = model1.ImageUrl2;
                                if (model1.Position[i] == 3)
                                    if (model1.ImageUrl3 != null && model1.ImageUrl3 != "")
                                        bannerImages.Url = model1.ImageUrl3;
                                if (model1.Position[i] == 4)
                                    if (model1.ImageUrl4 != null && model1.ImageUrl4 != "")
                                        bannerImages.Url = model1.ImageUrl4;
                                
                           //     else if (i == 4)
                            //    {
                             //       if (model1.ImageUrl4 != null)
                                //        bannerImages.Url = model1.ImageUrl4;
                              //  }


                                i++;
                                db.BannerImages.Add(bannerImages);
                            }

                        }

                    }

                message = 1;
                }
            }
            try
            {
               
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }
        [HttpPost]
        [Route("uploadFile2")]
        public async Task<IActionResult> uploadFile2([FromBody]BannerImagesModel1 model1)
        {
            var i = 0;
            var message = 0;
            if (model1 != null)
            {
                if (model1.Position.Count == 1)
                {
                    var check = db.BannerImages
                             .Where(x => x.IsActive == true
                             && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                             && x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                             && x.Position == model1.Position[0]).FirstOrDefault();
                    if (check != null)
                    {
                        check.IsActive = false;
                        db.SaveChanges();
                    }
                }
                else if (model1.Position.Count == 2)
                {
                    var check = db.BannerImages
                         .Where(x => x.IsActive == true
                         && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                         && x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                         && x.Position == model1.Position[0]).FirstOrDefault();
                    if (check != null)
                    {
                        check.IsActive = false;
                        db.SaveChanges();
                    }
                    check = db.BannerImages
                         .Where(x => x.IsActive == true
                         && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                         && x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                         && x.Position == model1.Position[1]).FirstOrDefault();
                    if (check != null)
                    {
                        check.IsActive = false;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var check = db.BannerImages
                        .Where(x => x.IsActive == true
                        // && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                        && x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                      ).ToList();
                    if (check != null)
                    {

                        foreach (var item in check)
                        {
                            item.IsActive = false;
                            db.SaveChanges();

                        }
                    }

                }
                //var check1 = db.BannerImages
                //       .Where(x => x.IsActive == true
                //         &&
                //        && x.View.ToLower() == model1.view.FirstOrDefault().ToLower()
                //      x.Side.ToLower() == model1.side.FirstOrDefault().ToLower()
                //     ).ToList();
                //if (check1 != null)
                //{

                //    foreach (var item in check1)
                //    {
                //        item.IsActive = false;
                //        db.SaveChanges();

                //    }
                //}

                if (model1.ImageUrl.Count > 0)
                {

                    foreach (var item in model1.ImageUrl)
                    {

                        if (item != null)
                        {
                            BannerImages bannerImages = new BannerImages();
                            var imageResponse = await S3Service.UploadObject(item);
                            var response = new JsonResult(new Object());
                            if (imageResponse.Success)
                            {
                                bannerImages.Image = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}"; ;
                                bannerImages.Side = model1.side.FirstOrDefault();
                                bannerImages.View = model1.view.FirstOrDefault();
                                bannerImages.IsActive = true;

                                bannerImages.Position = model1.Position[i];
                                if (model1.Position[i] == 1)
                                    if (model1.ImageUrl1 != null && model1.ImageUrl1 != "")
                                        bannerImages.Url = model1.ImageUrl1;
                                if (model1.Position[i] == 2)
                                    if (model1.ImageUrl2 != null && model1.ImageUrl2 != "")
                                        bannerImages.Url = model1.ImageUrl2;
                                if (model1.Position[i] == 3)
                                    if (model1.ImageUrl3 != null && model1.ImageUrl3 != "")
                                        bannerImages.Url = model1.ImageUrl3;
                                if (model1.Position[i] == 4)
                                    if (model1.ImageUrl4 != null && model1.ImageUrl4 != "")
                                        bannerImages.Url = model1.ImageUrl4;

                                //     else if (i == 4)
                                //    {
                                //       if (model1.ImageUrl4 != null)
                                //        bannerImages.Url = model1.ImageUrl4;
                                //  }


                                i++;
                                db.BannerImages.Add(bannerImages);
                            }

                        }

                    }

                    message = 1;
                }
            }
            try
            {

                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }
        [HttpGet]
        [Route("editImages")]
        public IActionResult ImagesEdit(string Data,string side)
        { var data = new List<BannerImages>();
            try
            {
                if (Data != null && side!=null)
                {
                    data = db.BannerImages
                        .Where(x => x.IsActive == true && x.View.ToLower() == Data.ToLower()
                        && x.Side.ToLower() == side.ToLower())
                        .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("getLeftSideView")]
        public IActionResult getLeftSide(string CurrentSide)
        {
            try
            {
                var data = db.BannerImages.Where(x => x.Side.ToLower() == CurrentSide.ToLower() && x.IsActive == true).FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //needed
        //[HttpPost]
        //[Route("uploadFile")]
        //public HttpResponseMessage UploadFile()
        //{
        // BannerImages bannerImages = new BannerImages();
        // //var postedFile = HttpContext.Current.Request.Files["Icon"];
        // var postedFile = HttpContext.Request.Form.Files["Icon"];

        // if (postedFile.Length > 0)
        // {
        // var fileName = Path.GetFileName(postedFile.FileName);
        // //var path = Path.Combine(HttpContext.Current.Server.MapPath("~/uploads"), fileName);
        // var path = Path.Combine(environment.WebRootPath, "uploads", fileName);

        // postedFile.Saveas(path);
        // }
        // bannerImages.IsActive = true;
        // if (!string.IsNullOrEmpty(postedFile.FileName))
        // bannerImages.Image = "https://localhost:44304/uploads/" + postedFile.FileName;
        // try
        // {

        // db.BannerImages.Add(bannerImages);
        // db.SaveChanges();

        // return Request.CreateResponse(HttpStatusCode.Created);
        // }
        // catch (Exception ex)
        // {
        // Console.Write(ex);
        // return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        // }

        //}
        [HttpGet]
        [Route("Bannerdelete")]
        public BannerImages Image(int Id)
        {
            var obj = db.BannerImages.Find(Id);
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
        [HttpPost]
        [Route("updateUrl")]
        public IActionResult edit(string selectedSide, string selectedView, [FromBody]BannerImagesModel1 model1)
        {
            var message = 0;
            try
            {
                var data = db.BannerImages
                .Where(x => x.Side.ToLower() == selectedSide.ToLower() && x.View.ToLower() == selectedView.ToLower() && x.IsActive == true)
                .ToList();
                if (data != null)
                {
                    if (model1.ImageUrl1 != null)
                    {
                        data[0].Url = model1.ImageUrl1;
                    }
                    if (model1.ImageUrl2 != null && data.Count==2)
                    {
                        data[1].Url = model1.ImageUrl2;
                    }
                    if (model1.ImageUrl3 != null && data.Count == 3)
                    {
                        data[2].Url = model1.ImageUrl3;
                    }
                    if (model1.ImageUrl4 != null && data.Count == 4)
                    {
                        data[3].Url = model1.ImageUrl4;
                    }
                    db.SaveChanges();
                    message = 1;
                }




            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }
    }
    public class BannerImagesModel
    {
        public BannerImagesModel()
        {
            Images = new List<string>();
        }
        public string Place { get; set; }
        public List<string> Images { get; set; }
        public int Order { get; set; }
    }
    public class BannerImagesModel1
    {
        public BannerImagesModel1()
        {
            ImageUrl = new List<string>();
            view = new List<string>();
            side = new List<string>();
            Position = new List<int>();
        }
        public List<int> Position { get; set; }
        public string Place { get; set; }
        public List<string> ImageUrl { get; set; }
        public List<string> view { get; set; }
        public List<string> side { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string ImageUrl4 { get; set; }


    }
    public class BannerFrontEnd
    {
        public BannerFrontEnd()
        {
            Images = new List<string>();
            Url = new List<string>();
            Position = new List<int>();
        }
        
        public string Side { get; set; }
        public string View { get; set; }
        public List<string> Url { get; set; }
        public List<string> Images { get; set; }
        public List<int> Position { get; set; }
    }
}