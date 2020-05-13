using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestCore.Controllers
{
    [Route("api/vendorReport")]
    [ApiController]
    public class VendorReportController : ControllerBase
    {
        private readonly PistisContext db;
        public VendorReportController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpPost]
        [Route("low-stock")]
        public IActionResult LowStock([FromBody] ReportFilter search)
        {
            var skipData = search.size * (search.page - 1);
            try
            {
                if (search.CategoryId == 0)
                {
                    var result = from pv in db.ProductVariantDetails
                                 join p in db.Products on pv.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                                 where p.VendorId == search.VendorId
                                 orderby pv.InStock ascending
                                 select new
                                 {
                                     Id = pv.Id,
                                     ProductName = p.Name,
                                     ProductCategory = pc.Name,
                                     PriceAfterDiscount = pvd.CostPrice - (pvd.Discount / 100) * pvd.CostPrice,
                                     CostPrice = pvd.CostPrice,
                                     SellingPrice = pvd.Price,
                                     Discount = pvd.Discount,
                                     InStock = pv.InStock
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }
                else
                {
                    var result = from pv in db.ProductVariantDetails
                                 join p in db.Products on pv.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                                 where p.ProductCategoryId == search.CategoryId
                                 where p.VendorId == search.VendorId
                                 orderby pv.InStock ascending
                                 select new
                                 {
                                     Id = pv.Id,
                                     ProductName = p.Name,
                                     ProductCategory = pc.Name,
                                     PriceAfterDiscount = pvd.CostPrice - (pvd.Discount / 100) * pvd.CostPrice,
                                     CostPrice = pvd.CostPrice,
                                     SellingPrice = pvd.Price,
                                     Discount = pvd.Discount,
                                     InStock = pv.InStock
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("most-viewed")]
        public IActionResult MostViewed([FromBody] ReportFilter search)
        {
            var skipData = search.size * (search.page - 1);
            try
            {
                if (search.CategoryId <= 0 || search.FromDate == null || search.ToDate == null)
                {
                    var result = from u in db.UserLogs
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 group u by new { u.ProductId, p.VendorId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     
                                     VendorId = ul.Key.VendorId,
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var filteredresult = result.Where(x => x.VendorId == search.VendorId);
                    var response = getPaging(filteredresult, skipData, search.size);
                    return Ok(response);
                }
                else
                {
                    var result = from u in db.UserLogs
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 where p.ProductCategoryId == search.CategoryId && u.LogInDate >= search.FromDate &&
                                 u.LogInDate <= search.ToDate
                                 group u by new { u.ProductId, p.VendorId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     VendorId = ul.Key.VendorId,
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Sum(x => x.ProductId),
                                 };

                    var filteredresult = result.Where(x => x.VendorId == search.VendorId);
                    var response = getPaging(filteredresult, skipData, search.size);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("reviewed-products")]
        public IActionResult ReviewedProducts([FromBody] ReportFilter search)
        {
            var skipData = search.size * (search.page - 1);
            try
            {
                if (search.CategoryId <= 0 || search.FromDate == null || search.ToDate == null)
                {
                    var result = from u in db.RatingReviews
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 group u by new { u.ProductId, p.VendorId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     VendorId = ul.Key.VendorId,
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var filteredresult = result.Where(x => x.VendorId == search.VendorId);
                    var response = getPaging(filteredresult, skipData, search.size);
                    return Ok(response);
                }
                else
                {
                    var result = from u in db.RatingReviews
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 where p.ProductCategoryId == search.CategoryId && u.ReviewDate >= search.FromDate &&
                                 u.ReviewDate <= search.ToDate
                                 group u by new { u.ProductId, p.VendorId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     VendorId = ul.Key.VendorId,
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var filteredresult = result.Where(x => x.VendorId == search.VendorId);
                    var response = getPaging(filteredresult, skipData, search.size);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("order-report")]
        public IActionResult OrderReport([FromBody] ReportFilter search)
        {
            var skipData = search.size * (search.page - 1);
            try
            {
                if (search.CategoryId <= 0 || search.FromDate == null || search.ToDate == null)
                {
                    var result = from ci in db.CheckoutItems
                                 join pvd in db.ProductVariantDetails on ci.ProductVariantDetailId equals pvd.Id
                                 join p in db.Products on pvd.ProductId equals p.Id
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                                 join v in db.Users on p.VendorId equals v.Id
                                 group ci by new { ci.ProductVariantDetailId, p.Name, CategoryName = pc.Name, pvd.CostPrice, pvd.Price, pvd.Discount, p.VendorId, v.UserName } into bs
                                 select new
                                 {
                                     VendorId = bs.Key.VendorId,
                                     ProductName = bs.Key.Name,
                                     ProductCategory = bs.Key.CategoryName,
                                     CostPrice = bs.Key.CostPrice,
                                     SellingPrice = bs.Key.Price,
                                     Discount = bs.Key.Discount,
                                     PriceAfterDiscount = bs.Key.CostPrice - (bs.Key.Discount / 100) * bs.Key.CostPrice,
                                     QuantityOrdered = bs.Count(),
                                 };

                    var filteredresult = result.Where(x => x.VendorId == search.VendorId);
                    var response = getPaging(filteredresult, skipData, search.size);
                    return Ok(response);
                }
                else
                {

                    var result = from ci in db.CheckoutItems.Include(x => x.Checkout)
                                 join pvd in db.ProductVariantDetails on ci.ProductVariantDetailId equals pvd.Id
                                 join p in db.Products on pvd.ProductId equals p.Id
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                                 join v in db.Users on p.VendorId equals v.Id
                                 where p.ProductCategoryId == search.CategoryId && ci.Checkout.CheckoutDate >= search.FromDate &&
                                              ci.Checkout.CheckoutDate <= search.ToDate
                                 group ci by new { ci.ProductVariantDetailId, p.Name, CategoryName = pc.Name, pvd.CostPrice, pvd.Price, pvd.Discount, p.VendorId, v.UserName } into bs
                                 select new
                                 {
                                     VendorId = bs.Key.VendorId,
                                     ProductName = bs.Key.Name,
                                     ProductCategory = bs.Key.CategoryName,
                                     CostPrice = bs.Key.CostPrice,
                                     SellingPrice = bs.Key.Price,
                                     Discount = bs.Key.Discount,
                                     PriceAfterDiscount = bs.Key.CostPrice - (bs.Key.Discount / 100) * bs.Key.CostPrice,
                                     QuantityOrdered = bs.Count(),
                                 };

                    var filteredresult = result.Where(x => x.VendorId == search.VendorId);
                    var response = getPaging(filteredresult, skipData, search.size);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("out-stock")]
        public IActionResult OutStock([FromBody] ReportFilter search)
        {
            var skipData = search.size * (search.page - 1);
            try
            {
                if (search.CategoryId == 0)
                {
                    var result = from pv in db.ProductVariantDetails
                                 join p in db.Products on pv.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                                 where p.VendorId == search.VendorId && pv.InStock == 0

                                 select new
                                 {
                                     Id = pv.Id,
                                     ProductName = p.Name,
                                     ProductCategory = pc.Name,
                                     PriceAfterDiscount = pvd.CostPrice - (pvd.Discount / 100) * pvd.CostPrice,
                                     CostPrice = pvd.CostPrice,
                                     SellingPrice = pvd.Price,
                                     Discount = pvd.Discount,
                                     InStock = pv.InStock
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }
                else
                {
                    var result = from pv in db.ProductVariantDetails
                                 join p in db.Products on pv.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                                 where p.ProductCategoryId == search.CategoryId
                                 where p.VendorId == search.VendorId && pv.InStock == 0

                                 select new
                                 {
                                     Id = pv.Id,
                                     ProductName = p.Name,
                                     ProductCategory = pc.Name,
                                     PriceAfterDiscount = pvd.CostPrice - (pvd.Discount / 100) * pvd.CostPrice,
                                     CostPrice = pvd.CostPrice,
                                     SellingPrice = pvd.Price,
                                     Discount = pvd.Discount,
                                     InStock = pv.InStock
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("high-demand")]
        public IActionResult HighDemand([FromBody] ReportFilter search)
        {
            var skipData = search.size * (search.page - 1);
            try
            {
                if (search.CategoryId == 0)
                {
                    var result = from pv in db.ProductVariantDetails
                                 join p in db.Products on pv.ProductId equals p.Id
                                 join w in db.WishLists on pv.Id equals w.ProductVariantDetailId
                                 where w.IsActive == true
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id

                                 where p.VendorId == search.VendorId && pv.InStock == 0

                                 select new
                                 {
                                     Id = pv.Id,
                                     ProductName = p.Name,
                                     ProductCategory = pc.Name,
                                     PriceAfterDiscount = pvd.CostPrice - (pvd.Discount / 100) * pvd.CostPrice,
                                     CostPrice = pvd.CostPrice,
                                     SellingPrice = pvd.Price,
                                     Discount = pvd.Discount,
                                     InStock = pv.InStock
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }
                else
                {
                    var result = from pv in db.ProductVariantDetails
                                 join p in db.Products on pv.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                                 where p.ProductCategoryId == search.CategoryId
                                 where p.VendorId == search.VendorId && pv.InStock == 0

                                 select new
                                 {
                                     Id = pv.Id,
                                     ProductName = p.Name,
                                     ProductCategory = pc.Name,
                                     PriceAfterDiscount = pvd.CostPrice - (pvd.Discount / 100) * pvd.CostPrice,
                                     CostPrice = pvd.CostPrice,
                                     SellingPrice = pvd.Price,
                                     Discount = pvd.Discount,
                                     InStock = pv.InStock
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private object getPaging(IQueryable<object> result, int skipData, int size)
        {
            return new
            {
                data = result.ToList().Skip(skipData).Take(size),
                count = result.ToList().Count(),
            };
        }
    }
}