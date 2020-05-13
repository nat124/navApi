using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;


namespace TestCore.Controllers
{
    [Route("api/helper")]
    [EnableCors("EnableCORS")]
    [ApiController]
    public class HelperController : ControllerBase
    {
        private readonly PistisContext db;
        private IOptions<AppSettings> _settings;
        public HelperController(PistisContext pistis, IOptions<AppSettings> settings)
        {
            db = pistis;
            _settings = settings;
        }



        [Route("UpdateProductImages")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductImages()
        {
            try
            {
                JsonResult response = null;

                var count = 0;
                var allImages = db.ProductImages.Where(x=>x.ImagePath150x150=="").ToList();
                foreach (var img in allImages)
                {
                    //if (img.ImagePath150x150 == null)
                    //{
                    try
                    {
                        var bytes = await S3Service.GetReaderFromS3(img.ImagePath);
                        var base64 = GetBase64StringForImage(bytes);
                        var imageResponse = await S3Service.updateUploadObject150(base64, img.ImagePath.Split('/')[3]);
                        var imageResponse1 = await S3Service.updateUploadObject450(base64, img.ImagePath.Split('/')[3]);
                        response = new JsonResult(new Object());
                        if (imageResponse.Success)
                        {
                            //img.ImagePath = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                            img.ImagePath150x150 = $"https://pistis150x150.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                            img.ImagePath450x450 = $"https://pistis450x450.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                            db.SaveChanges();
                            count++;
                        }
                    }
                    catch(Exception ext)
                    {
                        img.ImagePath150x150 = $"https://pistis150x150.s3.us-east-2.amazonaws.com/{img.ImagePath.Split('/')[3]}";
                        img.ImagePath450x450 = $"https://pistis450x450.s3.us-east-2.amazonaws.com/{img.ImagePath.Split('/')[3]}";
                        db.SaveChanges();
                        count++;
                    }
                    
                    //}        
                }

                //var products = db.Products.Include(m => m.ProductVariantDetails).ToList().Take(100).ToList();


                //var images = db.ProductImages.ToList();
                //foreach (var prod in products)
                //    foreach (var variant in prod.ProductVariantDetails)
                //    {
                //        variant.ProductImages = new List<ProductImage>();
                //        variant.ProductImages = images.Where(n => n.ProductVariantDetailId == variant.Id).ToList();

                //    }


                //foreach (var item in products)
                //{
                //    foreach (var variant in item.ProductVariantDetails)
                //    {
                //        foreach (var img in variant.ProductImages)
                //        {
                //            var bytes = await S3Service.GetReaderFromS3(img.ImagePath);
                //            var base64 = GetBase64StringForImage(bytes);
                //            var imageResponse = await S3Service.UploadObjectNew(base64, img.ImagePath.Split('/')[3]);

                //            response = new JsonResult(new Object());
                //            if (imageResponse.Success)
                //            {
                //                //img.ImagePath = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                //                img.ImagePath150x150 = $"https://pistis150x150.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                //                img.ImagePath450x450 = $"https://pistis450x450.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                //            }
                //        }
                //        db.SaveChanges();
                //    }
                //}




            }
            catch (Exception ex)
            {
                //return Ok(ex);
            }
            return Ok("Image updated successfully");
        }

        protected static string GetBase64StringForImage(byte[] imageBytes)
        {
            try
            {
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}