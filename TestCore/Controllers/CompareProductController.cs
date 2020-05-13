using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TestCore.Helper;

namespace TestCore.Controllers
{
    [Route("api/compare")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class CompareProductController : ControllerBase
    {
        private readonly PistisContext db;
        private readonly IHostingEnvironment environment;
        public CompareProductController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("SaveCompareProduct")]
        public int CompareProduct(int variantId, int UserId, string IpAddress)
        {
            var message = 0;
            var counting = new List<CompareProduct>();
            try
            {
                if (variantId > 0)

                {
                    var obj = db.CompareProducts.Where(x => x.ProductVariantDetailId == variantId && x.IsActive==true && (x.UserId == UserId || x.IpAddress == IpAddress)).ToList();
                if (obj.Count==0)
                {
                   
                        var SaveProduct = new CompareProduct();
                        SaveProduct.ProductVariantDetailId = variantId;
                        SaveProduct.IsActive = true;
                        if (IpAddress != null)
                            SaveProduct.IpAddress = IpAddress;
                        if (UserId > 0)
                            SaveProduct.UserId = UserId;
                        db.Add(SaveProduct);
                        db.SaveChanges();
                        var actionResult = CompareProducts(UserId, IpAddress);
                        var okObjectResult = actionResult as OkObjectResult;

                        var model = okObjectResult.Value as  List<product1>;

                        message = model.Count;


                    }
                    //  counting = db.CompareProducts.Where(x => x.IsActive == true && (x.UserId == UserId || x.IpAddress == IpAddress)).ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return message;
        }

        //[HttpGet]
        //[Route("DeleteCompare")]
        //public int RemoveProduct(int variantId, int? UserId, string IpAddress)
        //{
        //    var message = 0;
        //    try
        //    {
        //        if (variantId > 0)
        //        {
        //            var obj = db.CompareProducts.Where(x => x.ProductVariantDetailId == variantId &&(x.UserId==UserId || x.IpAddress==IpAddress)).FirstOrDefault();
        //            obj.IsActive = false;
        //            db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //            db.SaveChanges();
        //            message = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    return message;
        //}
        [HttpGet]
        [Route("DeleteCompare")]
        public int RemoveProduct(int Id)
        {
            var message = 0;
            try
            {
                if (Id > 0)
                {
                    var obj = db.CompareProducts.Where(x => x.Id==Id).FirstOrDefault();
                    obj.IsActive = false;
                    db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    message = 1;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return message;
        }
        [HttpGet]
        [Route("getCompareProducts")]
        public IActionResult CompareProducts(int? UserId, string IpAddress)
        {
            var productDetail = new product1();
            var productDetaillist = new List<product1>();
            var obj = new List<CompareProduct>();
            var productDetails = new ProductionSpecification();
            try
            {
                if (UserId > 0 )
                {
                    obj = db.CompareProducts.Where(x => x.IsActive == true && x.UserId == UserId )
                    .Include(X => X.ProductVariantDetail)
                    .Include(X => X.ProductVariantDetail.Product)
                    .Include(X => X.ProductVariantDetail.Product.ProductImages)
                    .Include(x => x.ProductVariantDetail.Product.RatingReviews)
                    .Include(x => x.ProductVariantDetail.Product.ProductionSpecifications)
                    .Take(4)
                    .ToList();
                }else if( IpAddress != null)
                {
                    obj = db.CompareProducts.Where(x => x.IsActive == true && x.IpAddress == IpAddress)
                    .Include(X => X.ProductVariantDetail)
                    .Include(X => X.ProductVariantDetail.Product)
                    .Include(X => X.ProductVariantDetail.Product.ProductImages)
                    .Include(x => x.ProductVariantDetail.Product.RatingReviews)
                    .Include(x => x.ProductVariantDetail.Product.ProductionSpecifications)
                    .Take(4)
                    .ToList();
                }
                foreach (var item in obj)
                {
                    productDetail = new product1();
                    // productDetail.ProductSpecificationDescription = item.ProductVariantDetail.Product.ProductionSpecifications.Select(x => x.Description).FirstOrDefault();
                    // productDetail.ProductSpecificationHeading = item.ProductVariantDetail.Product.ProductionSpecifications.Select(x => x.HeadingName).FirstOrDefault();
                    foreach (var spec in item.ProductVariantDetail.Product.ProductionSpecifications)
                    {
                        productDetails = new ProductionSpecification();
                        productDetails.HeadingName = item.ProductVariantDetail.Product.ProductionSpecifications.Select(x => x.Description).FirstOrDefault();
                        productDetails.Description = item.ProductVariantDetail.Product.ProductionSpecifications.Select(x => x.HeadingName).FirstOrDefault();


                    }

                    productDetail.ProductionSpecification.Add(productDetails);
                    productDetail.ProductId = item.ProductVariantDetail.ProductId;
                    productDetail.VariantDetailId = item.ProductVariantDetailId;
                    productDetail.CompareProductId = item.Id;
                    productDetail.Name = item.ProductVariantDetail.Product.Name;
                    productDetail.ShipmentVendor = item.ProductVariantDetail.Product.ShipmentVendor??false;
                    productDetail.ShipmentTime = item.ProductVariantDetail.Product.ShipmentTime??0;
                    productDetail.ShipmentCost = item.ProductVariantDetail.Product.ShipmentCost??0;
                    productDetail.Description = item.ProductVariantDetail.Product.Description.Length>=255? item.ProductVariantDetail.Product.Description.Substring(0, 255): item.ProductVariantDetail.Product.Description;
                    productDetail.SellingPrice = item.ProductVariantDetail.Price;
                    productDetail.Discount = item.ProductVariantDetail.Discount;
                    productDetail.PriceAfterdiscount = item.ProductVariantDetail.PriceAfterdiscount;
                    productDetail.InStock = item.ProductVariantDetail.InStock;
                    if (item.ProductVariantDetail.Product.ProductImages.Count > 0)
                        if (item.ProductVariantDetail.Product.ProductImages.Any(c => c.IsDefault == true))
                            productDetail.LandingImage150 = item.ProductVariantDetail.Product.ProductImages.Where(c => c.IsDefault == true && c.IsActive == true).FirstOrDefault().ImagePath150x150;
                        else
                            productDetail.LandingImage150 = item.ProductVariantDetail.Product.ProductImages.Where(c => c.IsActive == true).FirstOrDefault().ImagePath150x150;
                    var reviewCount = item.ProductVariantDetail.Product.RatingReviews.Where(X => X.Review != null).Count();
                    var ratingCount = item.ProductVariantDetail.Product.RatingReviews.Where(x => x.Rating > 0).Count();
                    var ratingSum = item.ProductVariantDetail.Product.RatingReviews.Where(x => x.Rating > 0).Sum(x => x.Rating);

                    productDetail.ReviewCount = reviewCount;
                    if (ratingCount > 0 && ratingSum > 0)
                    {
                        var ratingAvg = Convert.ToSingle(ratingSum) / Convert.ToSingle(ratingCount);
                        productDetail.RatingAvg = ratingAvg;
                    }
                    if (productDetail.ReviewCount > 0 && productDetail.RatingAvg > 0)
                    {
                        productDetail.ReviewCount = 0;
                        productDetail.RatingAvg = 0;
                    }
                    if (productDetail.LandingVariant == null)
                        productDetail.LandingVariant = new ProductVariantDetailModel();

                    productDetail.LandingVariant.Id = Convert.ToInt32(productDetail.VariantDetailId);
                    productDetaillist.Add(productDetail);
                }
                productDetaillist = DealHelper.calculateDealForProductsList(productDetaillist, db);
                productDetaillist = PriceIncrementHelper.calculatePriceForProductsList(productDetaillist, db);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return Ok(productDetaillist);
        }
    }
}