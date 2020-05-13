using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TestCore.Helper;

namespace TestCore.Controllers
{
    [Route("api/home")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class HomePageController : ControllerBase
    {
        private readonly PistisContext db;
        public HomePageController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getLikeProducts")]
        public async Task<IActionResult> GetLikeProducts(string IpAddress, int UserId)


        {
            var list = new List<ProductTag>();
            if (UserId > 0)
            {
                var result =  from log in db.UserLogs
                             where log.IsActive == true && log.UserId == UserId && log.Url.Contains("product-details")
                             join tags in db.ProductTag on log.ProductId equals tags.ProductId
                             join p in db.Products on tags.ProductId equals p.Id
                             select tags;
                list = await result.ToListAsync();
            }
            else
            {
                var result = from log in db.UserLogs
                             where log.IsActive == true && (log.UserId == null || log.UserId == 0) && log.IPAddress == IpAddress && log.Url.Contains("product-details")
                             join tags in db.ProductTag on log.ProductId equals tags.ProductId
                             join p in db.Products on tags.ProductId equals p.Id
                             select tags;
                list = result.ToList();
            }
            var ids = list.Select(x => x.ProductId).Distinct();
            list = new List<ProductTag>();
            foreach (var l in ids)
            {
                list.AddRange(db.ProductTag.Where(x => x.ProductId == l).ToList());

            }
            var alltags = db.ProductTag.Where(x => x.IsActive == true).ToList();
            var finaltags = new List<ProductTag>();
            foreach (var l in list)
            {
                var get = alltags.Where(x => x.TagId == l.TagId && x.ProductId != l.ProductId).ToList();
                finaltags.AddRange(get);
            }
            var final = new List<ProductCatalogue>();
            foreach (var l in finaltags)
            {
                var one = new ProductCatalogue();
                var variant = db.ProductVariantDetails.Where(x => x.IsActive == true && x.ProductId == l.ProductId).FirstOrDefault();
                var varlist = new List<ProductVariantDetail>();

                if (variant != null)
                {
                    varlist.Add(variant);

                    varlist = DealHelper.calculateDeal(varlist, db);
                    varlist = PriceIncrementHelper.calculatePrice(varlist, db);
                   variant = varlist[0];
                    one.Id = l.ProductId;
                    one.Name = db.Products.Where(x => x.Id == l.ProductId).FirstOrDefault().Name;
                    one.CostPrice = variant.CostPrice;
                    one.Discount = variant.Discount;
                    one.PriceAfterDiscount = variant.PriceAfterdiscount;
                    one.SellingPrice = variant.Price;
                    var image = db.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true && x.ProductVariantDetailId == variant.Id).Select(x => x.ImagePath150x150).FirstOrDefault();
                    one.Image150 = image == null ? db.ProductImages.Where(x => x.IsActive == true && x.ProductVariantDetailId == variant.Id).Select(x => x.ImagePath150x150).FirstOrDefault() : image;
                    one.InStock = variant.InStock;
                    one.VariantDetailId = variant.Id;
                    final.Add(one);
                }
            }
            return Ok(final.Take(20).ToList());
        }


        [HttpGet]
        [Route("getRecentlyBoughtItem")]
        public async Task<IActionResult> getRecentlyBoughtItem(int UserId)
        {
            var data = await db.PaymentTransaction.Where(x => x.UserId == UserId).LastOrDefaultAsync();
            var item = new ProductVariantDetailModel();

            if (data != null)
            {
                var checkout = await db.CheckoutItems.Where(x => x.CheckoutId == data.CheckoutId && x.IsActive == true).LastOrDefaultAsync();
                var variant = await db.ProductVariantDetails.Where(x => x.Id == checkout.ProductVariantDetailId ).Include(x=>x.Product).Include(x => x.ProductImages).FirstOrDefaultAsync();
                if (variant != null)
                {
                    item.Id = variant.Id;
                    item.ProductId = variant.ProductId;
                    item.Name = variant.Product.Name;
                    var images = variant.ProductImages.ToList();
                    item.Image = images.Where(x => x.IsDefault == true).FirstOrDefault() == null ? images.FirstOrDefault().ImagePath : images.Where(x => x.IsDefault == true).FirstOrDefault().ImagePath;
                }
                else
                {
                    item = null;
                }
            }
            return Ok(item);
        }
        [HttpGet]
        [Route("getRelatedItems")]
        public IActionResult getRelatedItems(int ProductId)

        {
            var relatedTags = new List<RelatedTag>();
            var result = from prt in db.ProductRelatedTagMap
                         where prt.ProductId == ProductId
                         join rt in db.RelatedTag on prt.RelatedTagId equals rt.Id
                         where rt.IsActive == true
                         select rt;
            relatedTags = result.ToList();
            var tags = db.Tag.Where(x => x.IsActive == true).Include(x=>x.ProductTags).ToList();
            var finaltags = new List<Tag>();
            foreach(var t in relatedTags)
            {
                finaltags.AddRange(tags.Where(x => x.Name.ToLower().Trim() == t.Name.ToLower().Trim()).ToList());
            }
            var final= new List<ProductTag>();
            var all = db.ProductTag.Where(x => x.IsActive == true && x.IsApproved == true).ToList();
            foreach(var f in finaltags)
            {
                final.AddRange(all.Where(x => x.TagId == f.Id).ToList());
            }
            var finalpro = new List<ProductCatalogue>();
            final=final.Where(x => x.ProductId != ProductId).ToList();
            foreach (var l in final)
            {
                var one = new ProductCatalogue();
                var variant = db.ProductVariantDetails.Where(x => x.IsActive == true && x.ProductId == l.ProductId).FirstOrDefault();
                var varlist = new List<ProductVariantDetail>();

                if (variant != null)
                {
                    varlist.Add(variant);

                    varlist = DealHelper.calculateDeal(varlist, db);
                    varlist = PriceIncrementHelper.calculatePrice(varlist, db);
                    variant = varlist[0];
                    one.Id = l.ProductId;
                    one.Name = db.Products.Where(x => x.Id == l.ProductId).FirstOrDefault().Name;
                    one.CostPrice = variant.CostPrice;
                    one.Discount = variant.Discount;
                    one.PriceAfterDiscount = variant.PriceAfterdiscount;
                    one.SellingPrice = variant.Price;
                    var image = db.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true && x.ProductVariantDetailId == variant.Id).Select(x => x.ImagePath150x150).FirstOrDefault();
                    one.Image150 = image == null ? db.ProductImages.Where(x => x.IsActive == true && x.ProductVariantDetailId == variant.Id).Select(x => x.ImagePath150x150).FirstOrDefault() : image;
                    one.InStock = variant.InStock;
                    one.VariantDetailId = variant.Id;
                    finalpro.Add(one);
                }
            }
            return Ok(finalpro.Take(20).ToList());
        }
    }
    
}