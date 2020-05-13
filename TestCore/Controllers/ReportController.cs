using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Localdb;
using Microsoft.EntityFrameworkCore;

namespace TestCore.Controllers
{
    [Route("api/report")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class ReportController : ControllerBase
    {
        private readonly PistisContext db;

        public ReportController(PistisContext pistis)
        {
            db = pistis;
        }

        [HttpPost]
        [Route("most-viewed")]
        public IActionResult MostViewed([FromBody] ReportFilter search)
        {
            var skipData = search.size * (search.page - 1);
            try
            {
                if (search.CategoryId <= 0 && search.FromDate == null && search.ToDate == null)
                {
                    var result = from u in db.UserLogs
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 group u by new { u.ProductId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }
                else if(search.CategoryId>0 && search.ToDate==null && search.FromDate==null)
                    {
                        var result = from u in db.UserLogs
                                     join p in db.Products on u.ProductId equals p.Id
                                     join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                     join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                     where p.ProductCategoryId == search.CategoryId
                                     group u by new { u.ProductId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                     select new
                                     {
                                         ProductName = ul.Key.Name,
                                         ProductCategory = ul.Key.CategoryName,
                                         CostPrice = ul.Key.CostPrice,
                                         SellingPrice = ul.Key.Price,
                                         Discount = ul.Key.Discount,
                                         PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                         Reviews = ul.Count(),
                                     };

                        var response = getPaging(result, skipData, search.size);
                        return Ok(response);
                    }
                else if (search.CategoryId > 0 && search.ToDate == null && search.FromDate != null)
                {
                    var result = from u in db.UserLogs
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 where p.ProductCategoryId == search.CategoryId && u.LogInDate >= search.FromDate
                                 group u by new { u.ProductId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }
                else if (search.CategoryId > 0 && search.ToDate != null && search.FromDate == null)
                {
                    var result = from u in db.UserLogs
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 where p.ProductCategoryId == search.CategoryId && u.LogInDate <= search.ToDate
                                 group u by new { u.ProductId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }
                else if (search.CategoryId > 0 && search.ToDate != null && search.FromDate == null)
                {
                    var result = from u in db.UserLogs
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 where p.ProductCategoryId == search.CategoryId && u.LogInDate <= search.ToDate && u.LogInDate>=search.FromDate
                                 group u by new { u.ProductId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }

                else if (search.CategoryId <= 0 && search.ToDate != null && search.FromDate == null)
                {
                    var result = from u in db.UserLogs
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 where u.LogInDate <= search.ToDate
                                 group u by new { u.ProductId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }
                else if (search.CategoryId <= 0 && search.ToDate == null && search.FromDate != null)
                {
                    var result = from u in db.UserLogs
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 where u.LogInDate >= search.FromDate
                                 group u by new { u.ProductId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var response = getPaging(result, skipData, search.size);
                    return Ok(response);
                }

                else if (search.CategoryId <= 0 && search.ToDate != null && search.FromDate != null)
                {
                    var result = from u in db.UserLogs
                                 join p in db.Products on u.ProductId equals p.Id
                                 join pvd in db.ProductVariantDetails on p.Id equals pvd.ProductId
                                 join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                                 where u.LogInDate <= search.ToDate && u.LogInDate>=search.FromDate
                                 group u by new { u.ProductId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var response = getPaging(result, skipData, search.size);
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
                                 group u by new { u.ProductId, p.Name, pvd.CostPrice, pvd.Price, pvd.Discount, CategoryName = c.Name } into ul
                                 select new
                                 {
                                     ProductName = ul.Key.Name,
                                     ProductCategory = ul.Key.CategoryName,
                                     CostPrice = ul.Key.CostPrice,
                                     SellingPrice = ul.Key.Price,
                                     Discount = ul.Key.Discount,
                                     PriceAfterDiscount = ul.Key.CostPrice - (ul.Key.Discount / 100) * ul.Key.CostPrice,
                                     Reviews = ul.Count(),
                                 };

                    var response = getPaging(result,skipData,search.size);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private object getPaging(IQueryable<object> result,int skipData,int size)
        {
            return new
            {
                data = result.ToList().Skip(skipData).Take(size),
                count = result.ToList().Count(),
            };
        }

        [HttpPost]
        [Route("best-seller")]
        public IActionResult BestSeller([FromBody] ReportFilter search)
        {
            try
            {
                if (search.CategoryId == 0 && search.VendorId == 0)
                {
                    var result = from ci in db.CheckoutItems
                                 join pvd in db.ProductVariantDetails on ci.ProductVariantDetailId equals pvd.Id
                                 join p in db.Products on pvd.ProductId equals p.Id
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                                 join v in db.Users on p.VendorId equals v.Id
                                 group ci by new { ci.ProductVariantDetailId, p.Name, CategoryName = pc.Name, pvd.CostPrice, pvd.Price, pvd.Discount,p.VendorId, v.UserName } into bs
                                 select new
                                 {
                                     ProductName = bs.Key.Name,
                                     ProductCategory = bs.Key.CategoryName,
                                     PriceAfterDiscount = bs.Key.CostPrice - (bs.Key.Discount / 100) * bs.Key.CostPrice,
                                     QuantityOrdered = bs.Count(),
                                     CostPrice = bs.Key.CostPrice,
                                     SellingPrice = bs.Key.Price,
                                     Discount = bs.Key.Discount,
                                     VendorId = bs.Key.VendorId,
                                     VendorName =bs.Key.UserName
                                 };

                    return Ok(result);
                }
                else
                {
                    var result = from ci in db.CheckoutItems
                                 join pvd in db.ProductVariantDetails on ci.ProductVariantDetailId equals pvd.Id
                                 join p in db.Products on pvd.ProductId equals p.Id
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                                 where p.ProductCategoryId == search.CategoryId || p.VendorId == search.VendorId
                                 group ci by new { ci.ProductVariantDetailId, p.Name, CategoryName = pc.Name, pvd.CostPrice, pvd.Price, pvd.Discount } into bs
                                 select new
                                 {
                                     ProductName = bs.Key.Name,
                                     ProductCategory = bs.Key.CategoryName,
                                     PriceAfterDiscount = bs.Key.CostPrice - (bs.Key.Discount / 100) * bs.Key.CostPrice,
                                     QuantityOrdered = bs.Count(),
                                     CostPrice = bs.Key.CostPrice,
                                     SellingPrice = bs.Key.Price,
                                     Discount = bs.Key.Discount
                                 };
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                                 join pvd in db .ProductVariantDetails on p.Id equals pvd.ProductId
                                 join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
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

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeletLowStock()
        {
            return Ok();
        }
    }

    public class ReportFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}