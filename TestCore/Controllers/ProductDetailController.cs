using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TestCore.Helper;

namespace TestCore.Controllers
{
    [Route("api/productDetail")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly PistisContext db;
        public ProductDetailController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getImages")]
        public async Task<IActionResult> getProductImage(int Id, int variantId)
        {
            var data = await db.ProductImages.Where(x => x.ProductVariantDetailId == variantId && x.IsActive == true).Take(6).ToListAsync();
            var result = new List<ProductImages>();
            var landingimg = data.Where(x => x.IsDefault == true).FirstOrDefault() == null ? data.FirstOrDefault().ImagePath450x450 : data.Where(x => x.IsDefault == true).FirstOrDefault().ImagePath450x450;
            foreach (var d in data)
            {
                var r = new ProductImages();
                r.Image150 = d.ImagePath150x150;
                r.Image450 = d.ImagePath450x450;
                r.ProductVariantDetailId = d.ProductVariantDetailId;
                r.LandingImage = landingimg;
                result.Add(r);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("getDetails")]
        public async Task<IActionResult> GetProductDetails(int Id, int variantId)
        {
            var data = db.ProductVariantDetails.Where(x => x.Id == variantId && x.IsActive == true).Include(x => x.Product).FirstOrDefault();
            var result = new ProductDetail();
            result.Description = data.Product.Description;
            result.Discount = data.Discount;
            result.InStock = data.InStock;
            result.Name = data.Product.Name;
            result.ProductId = data.ProductId;
            result.ProductVariantDetailId = data.Id;
            result.SellingPrice = data.Price;
            result.PriceAfterDiscount = data.PriceAfterdiscount;
            result.VendorId = data.Product.VendorId;
            result.ShipmentVendor = data.Product.ShipmentVendor ?? false;
            result = DealHelper.calculateDealForProductDetail(result, db);
            result = PriceIncrementHelper.calculatePriceForProductDetail(result, db);
            return Ok(result);

        }

        [HttpGet]
        [Route("getSpecifications")]
        public async Task<IActionResult> GetSpecifications(int variantId)
        {
            return Ok(db.ProductionSpecifications.Where(x => x.IsActive == true && x.VariantDetailId == variantId)
                    .Select(m => new ProductionSpecification
                    {
                        HeadingName = m.HeadingName,
                        Description = m.Description,
                    }).ToList());
        }

        [HttpGet]
        [Route("getVariants")]
        public async Task<IActionResult> GetVariants(int Id, int variantId)
        {
            var customModel1 = new List<customModel1>();
                 var productDetail = new ProductVariantModel();
            var qry = from pvd in db.ProductVariantDetails
                      where pvd.ProductId == Id && pvd.Id == variantId
                      join pvo in db.ProductVariantOptions on pvd.Id equals pvo.ProductVariantDetailId
                      join vo in db.VariantOptions on pvo.VariantOptionId equals vo.Id
                      join v in db.Variants on vo.VariantId equals v.Id
                      where pvd.IsActive == true && pvo.IsActive == true && vo.IsActive == true
                      && v.IsActive == true
                      select new customModel1
                      {
                          VariantModel = v,
                          VariantOption = vo,
                      };

            customModel1 = new List<customModel1>();
            customModel1 = qry.Distinct().ToList();
            var ProductVariantOption = await db.ProductVariantOptions.Where(c => c.IsActive == true && c.ProductVariantDetailId == variantId)
                                                            .Select(m => new TestCore.ProductVariantOption
                                                            {
                                                                Id = m.Id,
                                                                CategoryVariantId = m.CategoryVariantId,
                                                                VariantOptionId = m.VariantOptionId,
                                                                VariantOption = new VariantOptionModel
                                                                {
                                                                    Id = m.VariantOption.Id,
                                                                    Name = m.VariantOption.Name,
                                                                    VariantId = m.VariantOption.VariantId,
                                                                    IsActive = m.VariantOption.IsActive,
                                                                    Variant = new VariantsModel
                                                                    {
                                                                        Id = m.VariantOption.Variant.Id,
                                                                        Name = m.VariantOption.Variant.Name,
                                                                        IsMain = m.VariantOption.Variant.IsMain,
                                                                        IsActive = m.VariantOption.Variant.IsActive,
                                                                    },
                                                                }
                                                            }).ToListAsync();

            var optionQry = from pvd in db.ProductVariantDetails
                            where pvd.ProductId == Id && pvd.Id == variantId
                            join pvo in db.ProductVariantOptions on pvd.Id equals pvo.ProductVariantDetailId
                            join cv in db.CategoryVariants on pvo.CategoryVariantId equals cv.Id
                            join v in db.Variants on cv.VariantId equals v.Id
                            join vo in db.VariantOptions on v.Id equals vo.VariantId

                            where pvd.IsActive == true && pvo.IsActive == true && cv.IsActive == true
                            && v.IsActive == true
                            select new customModel1
                            {
                                VariantModel = v,
                                VariantOption = vo
                            };


            var allOptions = new List<Variant>();

            var variantData = await optionQry.Distinct().ToListAsync();
            foreach (var item in variantData)
            {
                if (!allOptions.Any(c => c.Id == item.VariantModel.Id))
                    allOptions.Add(item.VariantModel);
            }
            var optionData = new List<VariantsModel>();
            foreach (var varien in customModel1)
            {
                if (!productDetail.Variant.Any(x => x.Id == varien.VariantModel.Id))
                {
                    if (allOptions.Any(v => v.Id == varien.VariantModel.Id))
                    {
                        var data = allOptions.Where(v => v.Id == varien.VariantModel.Id).Select(c => new VariantsModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            SelectedValue = varien.VariantOption.Id,
                            IsMain = c.IsMain,
                            VariantOptions = c.VariantOptions.Where(v => v.IsActive == true).Select(b => new VariantOption
                            {
                                Id = b.Id,
                                Name = b.Name
                            }).ToList()
                        }).FirstOrDefault();
                        if (data.IsMain == true)
                            productDetail.Variant.Add(data);
                        else
                        {
                            if (!optionData.Any(c => c.Id == data.Id))
                                optionData.Add(data);
                        }
                    }
                }
            }
            var productVarientDetail = await db.ProductVariantDetails.Where(x => x.Id == variantId && x.IsActive == true)
                   .Include(c => c.ProductVariantOptions).FirstOrDefaultAsync();
            if (optionData.Count >= 0)
            {
                productDetail.Variant.AddRange(optionData);
                var options = productVarientDetail.ProductVariantOptions.Where(c => c.IsActive == true).ToList();

                var otherDetail = db.ProductVariantDetails.Where(c => c.ProductId == productVarientDetail.ProductId && c.IsActive == true).Include(v => v.ProductVariantOptions)
                .ToList();
                var data = new List<Models.ProductVariantOption>();
                foreach (var op in otherDetail)
                    data.AddRange(op.ProductVariantOptions.Where(b => b.IsActive == true).ToList());

                int count = 0;
                int CategoryVariantId = 0;
                int optionId = 0;

                var ExtraVariant = new List<VariantModel>();
                var variList = new List<VariantsModel>();

                foreach (var item in productDetail.Variant)
                {
                    if (count == 0)
                    {
                        if (options.Any(c => c.VariantOption.VariantId == item.Id))
                        {
                            item.SelectedValue = options.Where(c => c.VariantOption.VariantId == item.Id).Select(v => v.VariantOptionId).FirstOrDefault();
                            CategoryVariantId = options.Where(c => c.VariantOption.VariantId == item.Id).Select(v => v.CategoryVariantId).FirstOrDefault();
                            optionId = item.SelectedValue;
                            var listData = data.Where(c => c.VariantOption.Variant.Id == item.Id).ToList();
                            item.VariantOptions = new List<VariantOption>();
                            var opt = listData.Select(v => new VariantOption
                            {
                                Id = v.VariantOption.Id,
                                Name = v.VariantOption.Name,
                            }).ToList();
                            foreach (var op in opt)
                            {
                                if (!item.VariantOptions.Any(b => b.Id == op.Id))
                                    item.VariantOptions.Add(op);
                            }
                        }
                    }
                    if (count >= 1)
                    {
                        var vari = new VariantsModel();
                        if (options.Any(c => c.VariantOption.VariantId == item.Id))
                        {
                            item.SelectedValue = options.Where(c => c.VariantOption.VariantId == item.Id).Select(v => v.VariantOptionId).FirstOrDefault();
                        }
                        var listData = data.Where(c => c.CategoryVariantId == CategoryVariantId && c.VariantOptionId == optionId && c.IsActive == true).ToList();

                        //----------change id according to previous variant option
                        CategoryVariantId = options.Where(c => c.VariantOption.VariantId == item.Id).Select(v => v.CategoryVariantId).FirstOrDefault();
                        optionId = item.SelectedValue;

                        var other = new List<Models.ProductVariantOption>();
                        foreach (var li in listData)
                        {
                            var p = data.Where(b => b.ProductVariantDetailId == li.ProductVariantDetailId).ToList();
                            if (p.Any(c => c.VariantOption.VariantId == item.Id))
                            {
                                vari.Id = item.Id;
                                vari.Name = item.Name;
                                vari.SelectedValue = item.SelectedValue;
                                var op = p.Where(b => b.VariantOption.VariantId == item.Id).Select(b => new VariantOption
                                {
                                    Id = b.VariantOption.Id,
                                    Name = b.VariantOption.Name
                                }).FirstOrDefault();
                                if (vari.VariantOptions == null)
                                    vari.VariantOptions = new List<VariantOption>();
                                if (!vari.VariantOptions.Any(b => b.Id == op.Id))
                                    vari.VariantOptions.Add(op);
                            }
                        }
                        variList.Add(vari);
                    }
                    count++;
                }
                int index = 0;
                foreach (var item in productDetail.Variant)
                {
                    if (variList.Any(v => v.Id == item.Id) && index > 0)
                    {
                        item.VariantOptions = new List<VariantOption>();
                        item.VariantOptions = variList.Where(v => v.Id == item.Id).FirstOrDefault().VariantOptions;
                    }
                    index++;
                }
            }
            return Ok(productDetail);
        }
        [HttpGet]
        [Route("getReviews")]
        public async Task<IActionResult> GetReview(int Id,int variantId)
        {
            var prod = new Models.Product();
            var reviews = new productReview();
            var RatingCount = 0;
            var ReviewCount = 0;
            var ratingTotal = 0;
            var oneStar = 0;
            var TwoStar = 0;
            var ThreeStar = 0;
            var FourStar = 0;
            var FiveStar = 0;
            var allrates = await db.RatingReviews.Where(x => x.ProductId == Id && x.IsActive == true).ToListAsync();
           
            if (allrates.Count()>0)
            {

                var Users = await db.Users.Where(x => x.IsActive == true).ToListAsync();
                    foreach (var item in allrates)
                    {
                        var userRating = new UserRating();
                        var name = Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                        userRating.Username = name.FirstName;
                        userRating.UserReviews = item.Review;
                        userRating.UserRatings = item.Rating;
                        reviews.UserRatings.Add(userRating);
                        if (item.Rating > 0)
                        {
                            RatingCount++;
                            ratingTotal += item.Rating;
                        }
                        if (item.Review != null)
                        {
                            ReviewCount++;
                        }
                        if (item.Rating == 5)
                        {
                            FiveStar++;
                        }
                        else if (item.Rating == 4)
                        {
                            FourStar++;
                        }
                        else if (item.Rating == 3)
                        {
                            ThreeStar++;
                        }
                        else if (item.Rating == 2)
                        {
                            TwoStar++;
                        }
                        else if (item.Rating == 1)
                        {
                            oneStar++;
                        }

                    }
                
                if (RatingCount > 0)
                {
                    if (FiveStar > 0)
                        reviews.Fivestarper = (FiveStar * 100) / RatingCount;
                    if (FourStar > 0)
                        reviews.Fourstarper = (FourStar * 100) / RatingCount;
                    if (ThreeStar > 0)
                        reviews.Threestarper = (ThreeStar * 100) / RatingCount;
                    if (TwoStar > 0)
                        reviews.Twostarper = (TwoStar * 100) / RatingCount;
                    if (oneStar > 0)
                        reviews.Onestarper = (oneStar * 100) / RatingCount;
                }
                if (ratingTotal > 0 && RatingCount > 0)
                    reviews.RatingAvg = ratingTotal / RatingCount;

                reviews.ReviewCount = ReviewCount;
                reviews.RatingCount = RatingCount;
                reviews.Onestar = oneStar;
                reviews.Twostar = TwoStar;
                reviews.Threestar = ThreeStar;
                reviews.Fourstar = FourStar;
                reviews.Fivestar = FiveStar;
            }
            return Ok(reviews);

        }
    }

}