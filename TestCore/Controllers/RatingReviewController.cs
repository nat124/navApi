using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Models;
using Microsoft.EntityFrameworkCore;
using TestCore.Extension_Method;
using Newtonsoft.Json;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace TestCore.Controllers
{
    [Route("api/Rating")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class RatingReviewController : ControllerBase
    {
        private readonly PistisContext db;
        private readonly IHostingEnvironment environment;


        public RatingReviewController(PistisContext pistis)
        {
            db = pistis;
        }

        [HttpGet]
        [Route("getRating")]
        public IActionResult getRating(int UserId, int ProductId)
        {
            var data = new Models.RatingReview();
            try
            {
                data = db.RatingReviews.Where(x => x.UserId == UserId && x.ProductId == ProductId && x.IsActive == true)
                .FirstOrDefault();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(data);
        }
        [HttpPost]
        [Route("saveRating")]
        public IActionResult save(Review Review)
        {
            try
            {
                var data = new Models.RatingReview();
                var check = db.RatingReviews.Where(x => x.ProductId == Review.ProductId && x.UserId == Review.UserId)
                       .FirstOrDefault();
                if (check != null)
                {

                    check.ProductId = Review.ProductId;
                    check.UserId = Review.UserId;
                    check.Review = Review.review;
                    check.IsActive = true;
                    check.ReviewDate = DateTime.Now;
                    check.Rating = Review.Rating;
                    check.IsDefault = true;
                    if(Review.ReviewStatusId!=0)
                    check.ReviewStatusId = Review.ReviewStatusId;
                    db.SaveChanges();
                }
                else
                {
                    data.ProductId = Review.ProductId;
                    data.UserId = Review.UserId;
                    data.Review = Review.review;
                    data.IsActive = true;
                    data.ReviewDate = DateTime.Now;
                    data.Rating = Review.Rating;
                    db.RatingReviews.Add(data);
                    db.SaveChanges();
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }

        [HttpGet]
        [Route("CustomerReviews")]
        public IActionResult customerReview(int page, int pageSize)
        {
            
            var skipData = pageSize * (page - 1);
            var customerReviewsList = new List<RatingReview>();
            var customerReviewList = new List<CustomerReviews>();
            try
            {
                var ratingReviews = db.RatingReviews.Where(x => x.IsActive == true)
                    .Include(x => x.ReviewStatus)
                    .ToList();
                var customerReviewsList1 = db.RatingReviews.Where(x => x.IsActive == true)
                    .GroupBy(x => x.UserId)

                   .Select(x => new
                   {
                       UserId = x.Key,
                       NoOfReviews = x.Count(),
                       ids = x.Select(n => n.ProductId).Distinct().ToList()
                      // IsApproved = x
                   }).ToList();
                //var customerReviewsList1 = from ra in db.RatingReviews
                //                           group ra by new { ra.UserId, isapproved = ra.ReviewStatus } into ul
                //                           select new
                //                           {
                //                               UserId = ul.Key.UserId,
                //                               NoOfReviews = ul.Count(),
                //                               ids = ul.Select(n => n.ProductId).Distinct().ToList(),
                //                               IsApproved = ul.Key.isapproved.RatingReviews
                //                               //       NoOfReviews = x.Count(),
                //                               //       ids = x.Select(n => n.ProductId).Distinct().ToList()
                //                               //       IsApproved = x
                //                           };
                

                var users = db.Users.ToList();
                foreach (var item in customerReviewsList1)
                {
                    var customerReviews = new CustomerReviews();
                    customerReviews.NoOfreviews = item.NoOfReviews;
                    customerReviews.ReviewsList = ratingReviews.Where(x => x.IsActive == true && x.UserId == item.UserId).Select(x => x.Review).ToList();
                    customerReviews.customerName = users.Where(x => x.Id == item.UserId).FirstOrDefault().FirstName;
                    customerReviews.UserId = item.UserId;
               //     customerReviews.IsApproved = ratingReviews.Where(x => x.IsActive == true && x.UserId == item.UserId).Select(x => x.ReviewStatusId).FirstOrDefault();
                    foreach (var ids in item.ids)
                    {
                        var productInfo = allProducts(ids);

                        customerReviews.product2.Add(productInfo);

                    }
                    var abc = customerReviews.product2.ToList();
                    //customerReviews.product1s=allProducts()
                    customerReviewList.Add(customerReviews);
                }



                //if (customerReviewsList != null)
                //{
                //    var dataNeeded = from reviews in customerReviewsList
                //                     group reviews by reviews.UserId into ratings
                //                     select new
                //                     {
                //                         UserId = ratings.Key,
                //                         NoOfReviews = ratings.Count(),
                //                         productId=ratings.FirstOrDefault().ProductId
                //                     };
                //    //   var gotData = dataNeeded.ToList();


                //    //foreach (var item in gotData)
                //    //{
                //    //    var customerReviews = new CustomerReviews();
                //    //    customerReviews.NoOfreviews = item.NoOfReviews;
                //    //    customerReviews.ReviewsList = db.RatingReviews.Where(x => x.IsActive == true && item.UserId == item.UserId).Select(x => x.Review).ToList();
                //    //    customerReviews.customerName = db.Users.Where(x => x.Id == item.UserId).FirstOrDefault().FirstName;
                //    //    customerReviews.product1s = allProducts(item.productId);
                //    //    //customerReviews.product1s=allProducts()
                //    //    customerReviewList.Add(customerReviews);
                //    //}
                //}


            }
            catch (Exception ex)
            {

                throw ex;
            }

            var response = new
            {
                data = customerReviewList.Skip(skipData).Take(pageSize).ToList(),
                count = customerReviewList.Count(),
            };

            return Ok(response);
        }


        [HttpGet]
        [Route("deActivate")]
        public IActionResult deActivateCust(int Id)
        {
            try
            {
                int? ratingId;
                var rating = db.RatingReviews.Where(x => x.ProductId == Id && x.IsActive==true).FirstOrDefault();
                    if (rating.ReviewStatusId == 1)
                {
                 rating.ReviewStatusId = 3;
                }
                else
                {
                    rating.ReviewStatusId = 1;
                }
                db.SaveChanges();
                return Ok(rating.ReviewStatusId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public product2 allProducts(int Id)
        {
            var eachPrd = new product2();
            try
            {
                var data = db.Products.Where(x => x.IsActive == true && x.Id == Id)
                    .Include(c => c.ProductVariantDetails)
                    .Include(x => x.RatingReviews)
                    .Include(x => x.ProductImages).FirstOrDefault();
                eachPrd.Id = data.Id;
                var UserId = data.RatingReviews.Select(x => x.UserId).FirstOrDefault();
                eachPrd.UserName = db.Users.Where(x => x.Id == UserId).FirstOrDefault().FirstName;
                eachPrd.Review = data.RatingReviews.Select(x => x.Review).FirstOrDefault();
                eachPrd.ReviewDate = data.RatingReviews.Select(x => x.ReviewDate.Date).FirstOrDefault().ToShortDateString();
                var ReviewStatus = data.RatingReviews.Select(x => x.ReviewStatusId).FirstOrDefault();
                if (ReviewStatus == 1)
                {
                    eachPrd.Status = "Approved";
                }
                else if (ReviewStatus == 2)
                    eachPrd.Status = "Pending";
                else if (ReviewStatus == 3)
                    eachPrd.Status = "Not approved";
                if (data.ProductImages.Count() != 0)
                    eachPrd.Url = data.ProductImages?.FirstOrDefault().ImagePath;
                eachPrd.Description = data.Description;
                eachPrd.Name = data.Name;
                eachPrd.ProductCategoryId = data.ProductCategoryId;
                if (data.ProductVariantDetails.Where(c => c.IsActive == true).Any(c => c.IsDefault == true))
                {
                    eachPrd.LandingVariant = data.ProductVariantDetails.Where(c => c.IsActive == true).Where(c => c.IsDefault == true && c.IsActive == true).Select(d => new ProductVariantDetailModel
                    {
                        Id = d.Id,
                        Price = d.Price,
                        Discount = d.Discount,
                        PriceAfterdiscount = d.PriceAfterdiscount,
                        InStock = d.InStock,
                    }).FirstOrDefault();
                }
                else
                {
                    eachPrd.LandingVariant = data.ProductVariantDetails.Where(c => c.IsActive == true).Select(d => new ProductVariantDetailModel
                    {
                        Id = d.Id,
                        Price = d.Price,
                        Discount = d.Discount,
                        PriceAfterdiscount = d.PriceAfterdiscount,
                        InStock = d.InStock,
                    }).FirstOrDefault();
                }
                if (eachPrd.LandingVariant != null)
                {
                    eachPrd.SellingPrice = eachPrd.LandingVariant.Price;
                    eachPrd.Discount = eachPrd.LandingVariant.Discount;
                    eachPrd.PriceAfterdiscount = eachPrd.LandingVariant.PriceAfterdiscount;
                    eachPrd.InStock = eachPrd.LandingVariant.InStock;

                    var img = data.ProductImages.Where(c => c.ProductId == eachPrd.Id && c.ProductVariantDetailId == eachPrd.LandingVariant.Id && c.IsActive == true).ToList();
                    if (img.Any(c => c.IsDefault == true))
                        eachPrd.Url = img.Where(c => c.IsDefault == true).FirstOrDefault().ImagePath;
                    else
                        eachPrd.Url = img.FirstOrDefault().ImagePath;
                }
                //if (_prd.Count < 12)
                //    _prd.Add(eachPrd);
                //else
                //    break;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return eachPrd;
        }

        [HttpGet]
        [Route("FilterCustomerReviews")]
        public IActionResult FilterReviews(string firstDate, string lastDate, int UserId, int ReviewSatusId)
        {
            var product2List = new List<product2>();
            try
            {
                if (firstDate != null && lastDate != null)
                {
                    var first = Convert.ToDateTime(firstDate);
                    var Seconed = Convert.ToDateTime(lastDate);
                    var data = db.RatingReviews.Where(x => x.ReviewDate.Date >= first.Date && x.ReviewDate.Date <= Seconed.Date).ToList();
                    var UserData = data.Where(x => x.UserId == UserId).ToList();
                    foreach (var item in UserData)
                    {
                        var product2 = new product2();
                        product2 = allProducts(item.ProductId);
                        product2List.Add(product2);
                    }

                }
                else if (ReviewSatusId > 0)
                {

                    var data = db.RatingReviews.Where(x => x.ReviewStatusId == ReviewSatusId).ToList();
                    var UserData = data.Where(x => x.UserId == UserId).ToList();
                    foreach (var item in UserData)
                    {
                        var product2 = new product2();
                        product2 = allProducts(item.ProductId);
                        product2List.Add(product2);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(product2List);
        }


        [HttpGet]
        [Route("FilterCustomerReviewsStatus")]
        public IActionResult FilterReviews(int UserId, int ReviewSatusId)
        {
            var product2List = new List<product2>();
            try
            {
                if (ReviewSatusId > 0 && UserId > 0)
                {
                    var data = db.RatingReviews.Where(x => x.ReviewStatusId == ReviewSatusId).ToList();
                    var UserData = data.Where(x => x.UserId == UserId).ToList();
                    foreach (var item in UserData)
                    {
                        var product2 = new product2();
                        product2 = allProducts(item.ProductId);
                        product2List.Add(product2);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(product2List);
        }
        [HttpGet]
        [Route("getRatingperUser")]
        public IActionResult getRatingperUser(int UserId, int ProductId)
        {
            var data = new Models.RatingReview();
            try
            {
                data = db.RatingReviews.Where(x => x.UserId == UserId && x.ProductId == ProductId && x.IsActive == true)

                    .FirstOrDefault();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("productReview")]
        [EnableCors("EnableCORS")]
        public IActionResult productReview(int page,int pageSize)
        {
            var skipData = pageSize * (page - 1);
            // var customerReviewsList = new List<RatingReview>();
            //var customerReviewList = new List<CustomerReviews>();
            try
            {

                //var customerReviewsList = db.RatingReviews.Where(x => x.IsActive == true)
                //      .Include(x => x.Product).Select(x => new Models.RatingReview
                //      {
                //          Product = new Product
                //          {
                //              Name = x.Product.Name,

                //          }

                //      }).ToList();

                var result = from u in db.RatingReviews
                             join p in db.Products on u.ProductId equals p.Id
                             join a in db.Users on u.UserId equals a.Id

                             // join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                             group u by new { u.ProductId, p.Name, p.CostPrice, p.SellingPrice, p.Discount, u.ReviewDate, a.FirstName, a.Id,u.ReviewStatusId } into ul
                             select new
                             {
                                 ProductName = ul.Key.Name,
                                 //  ProductCategory = ul.Key.c.Name,
                                 CostPrice = ul.Key.CostPrice,
                                 SellingPrice = ul.Key.SellingPrice,
                                 Discount = ul.Key.Discount,
                                 //  PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                 Rating = ul.Average(x => x.Rating),
                                 approvedrating = ul.Where(x => x.ReviewStatusId == 1).Average(x => x.ReviewStatusId) ?? 0,
                                 productId = ul.Key.ProductId,
                                 lastreviewDate = ul.Key.ReviewDate.ToShortDateString(),
                                 UserName = ul.Key.FirstName,
                                 ReviewCount = ul.Count(),
                                 UserId = ul.Key.Id,
                                 ReviewStatusId=ul.Key.ReviewStatusId
                             };


                // var data = db.Products.Where(x => x.RatingReviews != null).ToList();

                var response = new
                {
                    data = result.Skip(skipData).Take(pageSize).ToList(),
                    count=result.Count()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        [HttpGet]
        [Route("productReviewUpdate")]
        public IActionResult productReviewUpdate(int Id)
        {
            try
            {
                var productReview = db.RatingReviews.Where(x => x.ProductId == Id).FirstOrDefault();
                if (productReview.ReviewStatusId == 1)
                {
                    productReview.ReviewStatusId = 3;
                }
                else
                {
                    productReview.ReviewStatusId = 1;

                }
                db.SaveChanges();
                return Ok(productReview.ReviewStatusId);
                    
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("Orders")]
        [EnableCors("EnableCORS")]
        public IActionResult orders(string FirstDate, string scndDate, string DOY, string Orderstatus)
        {
            var checkouts = new List<Checkout>();
            //   var Oredrs = new Oredrs();
            var orderList = new List<Oredrs>();
            try
            {
                //decimal totalAmount = 0;
                //decimal shippingPrice = 0;

                //var totalorders = checkouts.Count();

                //foreach (var item in checkouts)
                //{
                //    var Oredrs = new Oredrs();
                //    Oredrs.Orders = totalorders;

                //    totalAmount += item.TotalAmount;
                //    shippingPrice += item.ShippingPrice;
                //    var data = item.CheckoutItems.Select(x => x.OrderStatusId).FirstOrDefault();
                //    if (data == 1)
                //        Oredrs.orderStatus = "Ordered";
                //    else if (data == 2)
                //        Oredrs.orderStatus = "Packed";
                //    else if (data == 3)
                //        Oredrs.orderStatus = "Shipped";
                //    else if (data == 4)
                //        Oredrs.orderStatus = "Delivered";

                //    orderList.Add(Oredrs);
                //}

                var checkouts1 = db.Checkouts.Where(x => x.IsActive == true && x.IsPaid == true)
                      .ToList();
                //foreach (var item in checkouts)
                //{
                //    foreach (var list in item.CheckoutItems)
                //    {
                //        list.Checkout = null;
                //    }
                //}

                //var checkouts1 = db.Checkouts.Where(x => x.IsActive == true && x.IsPaid == true)
                //    .Select(x => new
                //    {
                //        Orders = x.CheckoutItems.Select(y => new {
                //            y.ProductVariantDetail.Product.Name
                //        })

                //    })
                //   .ToList();




            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(checkouts);
        }
        [HttpGet]
        [Route("FilterProductReviews")]
        public IActionResult FilterProdReviews(string firstDate, string lastDate, int ProductId, int ReviewSatusId)
        {
            var product2List = new List<product2>();
            try
            {
                if (firstDate != null && lastDate != null)
                {
                    var first = Convert.ToDateTime(firstDate);
                    var Seconed = Convert.ToDateTime(lastDate);
                    var data = db.RatingReviews.Where(x => x.ReviewDate.Date >= first.Date && x.ReviewDate.Date <= Seconed.Date).ToList();
                    var UserData = data.Where(x => x.ProductId == ProductId).ToList();
                    foreach (var item in UserData)
                    {
                        var product2 = new product2();
                        product2 = allProducts(item.ProductId);
                        product2List.Add(product2);
                    }

                }
                else if (ReviewSatusId > 0)
                {

                    var data = db.RatingReviews.Where(x => x.ReviewStatusId == ReviewSatusId).ToList();
                    var UserData = data.Where(x => x.ProductId == ProductId).ToList();
                    foreach (var item in UserData)
                    {
                        var product2 = new product2();
                        product2 = allProducts(item.ProductId);
                        product2List.Add(product2);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(product2List);
        }
        [HttpGet]
        [Route("FilterProductIdReviews")]
        public IActionResult FilterProdReviews(int ProductId)
        {
            var product2List = new List<product2>();
            try
            {
                if (ProductId > 0)
                {

                    var UserData = db.RatingReviews.Where(x => x.ProductId == ProductId).ToList();
                    foreach (var item in UserData)
                    {
                        var product2 = new product2();
                        product2 = allProducts(item.ProductId);
                        product2List.Add(product2);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(product2List);
        }

        [HttpGet]
        [Route("FilterProductReviewsStatus")]
        public IActionResult reviewsofproduct(int productId, int ReviewSatusId)
        {
            var product2List = new List<product2>();
            try
            {
                if (ReviewSatusId > 0 && productId > 0)
                {
                    var data = db.RatingReviews.Where(x => x.ReviewStatusId == ReviewSatusId).ToList();
                    var UserData = data.Where(x => x.ProductId == productId).ToList();
                    foreach (var item in UserData)
                    {
                        var product2 = new product2();
                        product2 = allProducts(item.ProductId);
                        product2List.Add(product2);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(product2List);

        }
    }
    public class ProductInfo
    {
        public string ProductName { get; set; }
        public int NoOfReviews { get; set; }
        public int AvgRating { get; set; }
        public int AvgApprovedRating { get; set; }
        public int LastReview { get; set; }

    }
    public class Oredrs
    {
        public Oredrs()
        {
            checkoutItems = new List<Models.CheckoutItem>();
        }
        public string Period { get; set; }
        public int Orders { get; set; }
        public int SalesTotal { get; set; }
        public int SalesShipping { get; set; }
        public string orderStatus { get; set; }
        public List<Models.CheckoutItem> checkoutItems { get; set; }

    }
    public class Review
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string review { get; set; }
        public int Rating { get; set; }
        public int ReviewStatusId { get; set; }

    }
    public class CustomerReviews
    {
        public CustomerReviews()
        {
            ReviewsList = new List<string>();
            product2 = new List<product2>();

        }
     //  public int? IsApproved { get; set; }
        public int UserId { get; set; }
        public int NoOfreviews { get; set; }
      
        public List<string> ReviewsList { get; set; }
        public string customerName { get; set; }
        public string ProductName { get; set; }
        public List<product2> product2 { get; set; }
    }
    public class product2
    {
        public product2()
        {
            Images = new List<string>();
            Variant = new List<VariantsModel>();
            VariantOption = new HashSet<VariantOption>();
            ProductionSpecification = new List<ProductionSpecification>();
            ControlType = new HashSet<string>();
            ProductVariantOption = new List<ProductVariantOption>();
        }
        public string ReviewDate { get; set; }
        public int WishListId { get; set; }
        public string Status { get; set; }
        public string Review { get; set; }
        public string UserName { get; set; }
        public string ProductSpecificationHeading { get; set; }
        public string ProductSpecificationDescription { get; set; }
        public int CompareProductId { get; set; }
        public string LandingImage { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public int UnitId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Discount { get; set; }
        public decimal PriceAfterdiscount { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public float RatingAvg { get; set; }
        public int ReviewCount { get; set; }
        public int RatingCount { get; set; }
        public int Onestar { get; set; }
        public int Twostar { get; set; }
        public int Threestar { get; set; }
        public int Fourstar { get; set; }
        public int Fivestar { get; set; }
        public string ParentName { get; set; }
        public int? VariantDetailId { get; set; }
        public int InStock { get; set; }
        public ProductVariantDetailModel LandingVariant { get; set; }
        public HashSet<string> ControlType { get; set; }
        public List<ProductionSpecification> ProductionSpecification { get; set; }
        public List<string> Images { get; set; }
        public List<VariantsModel> Variant { get; set; }
        public HashSet<VariantOption> VariantOption { get; set; }
        public List<ProductVariantOption> ProductVariantOption { get; set; }
    }
}