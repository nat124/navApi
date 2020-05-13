using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TestCore.Extension_Method;
using TestCore.Helper;

namespace TestCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeListsController : ControllerBase
    {
        private readonly PistisContext db;
        public HomeListsController(PistisContext pistis)
        {
            db = pistis;
        }

        [HttpPost]
        [Route("getFilterProducts")]
        public List<ProductCatalogue> getFilterProducts([FromBody] Filter filter, [FromQuery]int page, [FromQuery] int pagesize)
        {
            try
            {
                if (filter.SearchData == "undefined" || filter.SearchData == null)
                {
                    filter.SearchData = "";
                }
                var model = new List<ProductCatalogue>();
                var products = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.ProductCategory.IsShow == true);
                var prodIds = db.HomeCategoryProduct.Where(x => x.IsActive == true && x.HomeCategoryId == filter.CategoryId).Select(x=>x.ProductId).ToList();
                var prod = new List<Product>();
               
                decimal minprice = 0;
                decimal maxprice = 0;
                
                    if (filter.SelectedVariants.Count() > 0)
                    {
                        foreach (var s in filter.SelectedVariants)
                        {
                            var result = new List<Product>();
                            foreach (var o in s.VariantOptionId)
                            {
                                var pro = from cv in db.CategoryVariants
                                          where s.CategoryId == cv.ProductCategoryId && s.VariantId == cv.VariantId
                                          join pvo in db.ProductVariantOptions on cv.Id equals pvo.CategoryVariantId
                                          join pvd in db.ProductVariantDetails on pvo.ProductVariantDetailId equals pvd.Id
                                          join p in products on pvd.ProductId equals p.Id
                                          where o == pvo.VariantOptionId
                                          && cv.IsActive == true && pvo.IsActive == true && pvd.IsActive == true && p.IsActive == true
                                          select p;
                                result.AddRange(pro.ToList());
                            }
                            products = result.AsQueryable();
                        }
                    }
                var variantsData = new List<ProductVariantDetail>();
                foreach (var p in prodIds)
                {
                    if (products.Any(x => x.Id == p))
                        prod.Add(products.Where(x => x.Id == p).Include(x=>x.ProductVariantDetails).FirstOrDefault());
                }
                foreach (var item in prod)
                {
                    if (item.ProductVariantDetails == null)
                    {
                        item.ProductVariantDetails = db.ProductVariantDetails.Where(x => x.ProductId == item.Id && x.IsActive == true).ToList();
                    }
                    var data = item.ProductVariantDetails.Where(b => b.IsActive == true).ToList();
                    variantsData.AddRange(data);
                }
                //variantsData.OrderByDescending(x => x.Id);
                var priceListdata = new List<ProductVariantDetail>();
                var plist = prod.ToList();
                var variantlist = db.ProductVariantDetails.Where(x => x.IsActive == true).Include(x => x.Product).ToList();
                foreach (var p in plist)
                {
                    var list = variantlist.Where(x => x.ProductId == p.Id).ToList();
                    priceListdata.AddRange(list);
                }
                //pricefilter
                var minprice1 = priceListdata.Count() == 0 ? 0 : priceListdata.Min(x => x.Price);
                var maxprice1 = priceListdata.Count() == 0 ? 0 : priceListdata.Max(x => x.Price);
                var pricelist = PriceRange(maxprice1, minprice1);

                var list1 = new List<ProductVariantDetail>();
                minprice = filter.MinPrice != "" && filter.MinPrice != "0" ? Convert.ToDecimal(filter.MinPrice.Split('$')[1]) : 0;
                maxprice = filter.MaxPrice != "" && filter.MaxPrice != "0" ? Convert.ToDecimal(filter.MaxPrice.Split('$')[1]) : 0;
                if (maxprice == 0)
                {
                    list1 = variantsData.Where(x => x.Price >= minprice).ToList();
                }
                else
                {
                    list1 = variantsData.Where(x => x.Price >= minprice && x.Price < maxprice).ToList();
                }
                //search by name
                //var data11 = 
                foreach (var l in list1)
                {
                    if (l.Product == null)
                    {
                        l.Product = db.Products.Where(x => x.Id == l.ProductId && x.IsActive == true && x.IsEnable == true).FirstOrDefault();
                    }
                }
               
                //get data for page
                var final = list1.OrderByDescending(x => x.Id).Skip((page - 1) * pagesize).Take(pagesize).ToList();
                var count = list1.Count();

                var ProductVariantOptions = db.ProductVariantOptions.Where(b => b.IsActive == true).ToList();
                var optionsList = db.VariantOptions.Where(b => b.IsActive == true).Include(x => x.Variant).ToList();

                var variantsList = new List<ProductVariantDetail>();
                final = DealHelper.calculateDeal(final, db);
                final = PriceIncrementHelper.calculatePrice(final, db);

                foreach (var p in final)
                {

                    var pro = new ProductCatalogue();
                    pro.Id = p.ProductId;
                    //pro.Name = p.Product.Name;
                    var pdata = prod.Where(x => x.Id == p.ProductId).FirstOrDefault();
                    pro.Name = pdata?.Name;
                    pro.ShipmentVendor = pdata?.ShipmentVendor ?? false;
                    pro.ShipmentCost = pdata?.ShipmentCost ?? 0;
                    pro.ShipmentTime = pdata?.ShipmentTime ?? 0;
                    pro.Description = pdata?.Description;
                    pro.ProductCategoryName = pdata?.ProductCategory?.Name;
                    pro.ProductCategoryId = Convert.ToInt32(pdata?.ProductCategoryId);

                    pro.InStock = p.InStock;

                    pro.VariantDetailId = p.Id;
                    pro.Discount = p.Discount;
                    pro.SellingPrice = p.Price;
                    pro.PriceAfterDiscount = p.PriceAfterdiscount;
                    //}


                    var selectedImage = new ProductImage();
                    selectedImage = db.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true && x.ProductVariantDetailId == p.Id).FirstOrDefault();
                    if (selectedImage == null)
                        selectedImage = db.ProductImages.Where(x => x.IsActive == true && x.ProductVariantDetailId == pro.VariantDetailId).FirstOrDefault();
                    pro.Image = selectedImage?.ImagePath;
                    pro.Image150 = selectedImage?.ImagePath150x150;
                    pro.Image450 = selectedImage?.ImagePath450x450;

                    var VariantOptionIds = ProductVariantOptions.Where(x => x.ProductVariantDetailId == pro.VariantDetailId && x.IsActive == true).ToList().Select(x => x.VariantOptionId);
                    foreach (var v in VariantOptionIds)
                    {
                        var data = optionsList.Where(x => x.Id == v && x.IsActive == true).FirstOrDefault().RemoveReferences();
                        pro.VariantOptions.Add(data);
                    }
                    pro.Rating = db.RatingReviews.Where(x => x.IsActive == true && x.ReviewStatusId == 1 && x.ProductId == p.ProductId).FirstOrDefault()?.Rating;
                    pro.Count = count;
                    pro.PriceList = pricelist;
                    pro.AvgRate = AvgRating(p.Id);
                    pro.ActiveTo = p.ActiveTo;
                    if (pdata != null)
                        model.Add(pro);
                }


                //rate
                model = model.Where(x => x.AvgRate >= filter.AvgRate).OrderByDescending(x => x.Id).ToList();
                //sorting
                if (filter.SortBy == "HighToLow")
                    model = model.OrderByDescending(x => x.PriceAfterDiscount).ToList();
                else if (filter.SortBy == "LowToHigh")
                    model = model.OrderBy(x => x.PriceAfterDiscount).ToList();
                if (model.Count == 0)
                {
                    var model1 = new ProductCatalogue();
                    model1.PriceList = pricelist;
                    model.Add(model1);
                }

                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        List<string> PriceRange(decimal maxprice, decimal minprice)
        {
            var pricelist = new List<string>();
            Int32 increment = Convert.ToInt32((maxprice - minprice) / 5);
            if (increment.ToString().Length == 2)
            {
                increment = Convert.ToInt32(Math.Round(increment / 5d, 0) * 5);
            }
            else if (increment.ToString().Length == 3)
            {
                increment = Convert.ToInt32(Math.Round(increment / 50d, 0) * 50);
            }
            else if (increment.ToString().Length == 4 || increment.ToString().Length == 5)
            {
                increment = Convert.ToInt32(Math.Round(increment / 500d, 0) * 500);
            }

            else
            {
                increment = Convert.ToInt32(Math.Round(increment / 5000d, 0) * 5000);
            }
            var i = 0;
            while (maxprice > minprice)
            {
                Int32 newminprice = Convert.ToInt32(minprice + increment);
                var data = "";
                if (i == 0)
                {
                    data = "Under $" + newminprice;
                    minprice = newminprice;

                }
                else if (i == 4)
                {
                    data = "Above $" + minprice;
                    minprice = maxprice;
                }

                else
                {
                    data = "$" + Convert.ToInt32(minprice) + "- $" + newminprice;
                    minprice = newminprice;

                }
                pricelist.Add(data);
                i++;
            }
            return pricelist;
        }
        public int AvgRating(int ProductId)
        {
            var data = db.RatingReviews.Where(x => x.IsActive == true && x.ProductId == ProductId).ToList().Select(x => x.Rating);
            var count = data.Count();
            Int32 avg = 0;
            if (count > 0)
                avg = Convert.ToInt32(data.Sum() / count);
            return avg;
        }

    }
}