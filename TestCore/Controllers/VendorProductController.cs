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
    [Route("api/[controller]")]
    [ApiController]
    public class VendorProductController : ControllerBase
    {
        private readonly PistisContext db;
        public VendorProductController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getProducts")]
        public IActionResult GetProducts(int page, int pageSize, string search,int UserId,int RoleId)
        {
            if (UserId != 0 && RoleId == 2)
            {
                var skipData = pageSize * (page - 1);

                var prod = new List<Product>();

                if (search == null)
                {
                    prod = db.Products.Where(x => x.IsActive == true && x.VendorId==UserId)
                    .OrderByDescending(s => s.Id)
                    .Include(v => v.ProductVariantDetails)
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                    .AsNoTracking().ToList();
                }
                else
                {
                    prod = db.Products.Where(x => x.IsActive == true && x.Name.Contains(search) && x.VendorId == UserId)
                    .OrderByDescending(s => s.Id)
                    .Include(v => v.ProductVariantDetails)
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                    .AsNoTracking().ToList();
                }

                var count = prod.Count;

                var model = new List<ProductModel>();
                foreach (var p in prod)
                {
                    var pro = new ProductModel();
                    var list = p.ProductVariantDetails.Where(x => x.ProductId == p.Id && x.IsActive == true).ToList();
                    pro.Id = p.Id;
                    pro.Name = p.Name;
                    pro.IsEnable = p.IsEnable;
                    if (p.ProductImages?.Count > 0)
                        pro.Image = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath;
                    pro.ProductCategoryName = p.ProductCategory.Name;
                    var variants = list.Count();
                    var total = list.Sum(x => x.InStock);
                    pro.Inventory = total + " in stock for " + variants + " variants.";
                    model.Add(pro);
                }
                var response = new
                {
                    data = model.Skip(skipData).Take(pageSize).ToList(),
                    count = count
                };
                return Ok(response);
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost]
        [Route("getProductsFilter")]
        public IActionResult GetProducts1([FromBody]filtration filtration)
            
        {
            if (filtration.UserId != 0 && filtration.RoleId==2)
            {
                var skipData = filtration.pageSize * (filtration.page - 1);
                var finalList = new List<ProductCategory>();

                var prod = new List<Product>();

                if (filtration.SearchName == null || filtration.SearchName == "")
                {
                    if (filtration.variant_isDefault == true)
                    {
                        prod = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.VendorId == filtration.UserId)
                        .OrderByDescending(s => s.Id)
                        .Include(v => v.ProductVariantDetails)
                        .Include(x => x.ProductCategory)
                        .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                        .AsNoTracking().ToList();
                    }
                    else
                    {
                        prod = db.Products.Where(x => x.IsActive == true && x.IsEnable == false && x.VendorId == filtration.UserId)
                          .OrderByDescending(s => s.Id)
                          .Include(v => v.ProductVariantDetails)
                          .Include(x => x.ProductCategory)
                          .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                          .AsNoTracking().ToList();

                    }
                }
                else
                {
                    if (filtration.variant_isDefault == true)
                    {
                        prod = db.Products.Where(x => x.IsActive == true && x.Name.Contains(filtration.SearchName) && x.IsEnable == true && x.VendorId == filtration.UserId)
                         .OrderByDescending(s => s.Id)
                         .Include(v => v.ProductVariantDetails)
                         .Include(x => x.ProductCategory)
                         .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                         .AsNoTracking().ToList();
                    }
                    else
                    {
                        prod = db.Products.Where(x => x.IsActive == true && x.Name.Contains(filtration.SearchName) && x.IsEnable == false && x.VendorId == filtration.UserId)
                         .OrderByDescending(s => s.Id)
                         .Include(v => v.ProductVariantDetails)
                         .Include(x => x.ProductCategory)
                         .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                         .AsNoTracking().ToList();
                    }
                }
                if (filtration.CategoryId > 0)
                {
                    if (filtration.SubCategoryId > 0)
                    {
                        prod = prod.Where(x => x.ProductCategoryId == filtration.SubCategoryId).ToList();
                    }
                    else
                    {
                        var data = db.ProductCategories.Where(x => x.ParentId == filtration.CategoryId).ToList();
                        if (data != null)
                        {
                            var list = new List<ProductCategory>();

                            foreach (var item in data)
                            {
                                var product = db.ProductCategories.Where(x => x.ParentId == item.Id).ToList();
                                foreach (var item1 in product)
                                {
                                    list.Add(item1);
                                }
                            }
                            if (list != null)
                            {
                                foreach (var item in list)
                                {
                                    var product = list.Where(x => x.ParentId == item.Id).ToList();
                                    if (product.Count == 0)
                                    {

                                        finalList.Add(item);
                                    }
                                    else
                                    {
                                        foreach (var item1 in product)
                                        {
                                            finalList.Add(item1);
                                        }
                                    }
                                }
                            }
                            if (finalList != null)
                            {
                                foreach (var item in finalList)
                                {
                                    //  product = new Models.Product();
                                    var product = db.Products.Where(x => x.ProductCategoryId == item.Id).ToList();
                                    foreach (var item3 in product)
                                    {
                                        prod.Add(item3);

                                    }
                                }
                            }
                        }
                    }

                }


                var count = prod.Count;

                var model = new List<ProductModel>();
                foreach (var p in prod)
                {
                    var pro = new ProductModel();
                    var list = p.ProductVariantDetails?.Where(x => x.ProductId == p.Id && x.IsActive == true).ToList();
                    pro.Id = p.Id;
                    pro.Name = p.Name;
                    pro.IsEnable = p.IsEnable;
                    if (p.ProductImages?.Count > 0)
                        pro.Image = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath;
                    pro.ProductCategoryName = p.ProductCategory.Name;
                    if (list != null)
                    {
                        var variants = list.Count();
                        var total = list.Sum(x => x.InStock);

                        pro.Inventory = total + " in stock for " + variants + " variants.";
                    }
                    model.Add(pro);
                }
                var response = new
                {
                    data = model.Skip(skipData).Take(filtration.pageSize).ToList(),
                    count = count
                };
                try
                {
                    return Ok(response);

                }
                catch (Exception ex)
                {
                    return Ok(ex);

                    throw ex;
                }
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Route("addProduct")]
        [EnableCors("EnableCORS")]
        public async Task<IActionResult> PostUser([FromBody]ProductModel product)
        {
            JsonResult response = null;
            product.UnitId = 2;
            //product table
            var pro = new Product();
            if (product.ShipmentVendor==true) { 
            pro.ShipmentCost = product.ShipmentCost;
            pro.ShipmentTime = product.ShipmentTime;
            pro.ShipmentVendor = product.ShipmentVendor;
            }
            else
            {
                pro.ShipmentVendor = product.ShipmentVendor;

            }
            pro.CostPrice = product.CostPrice;
            pro.VendorId = product.VendorId;
            pro.Description = product.Description;
            pro.Discount = product.Discount;
            pro.IsActive = true;
            pro.Name = product.Name;
            pro.PriceAfterdiscount = product.PriceAfterDiscount;
            pro.ProductCategoryId = product.ProductCategoryId;
            pro.ProductTags = product.ProductTags;
            pro.SellingPrice = product.SellingPrice;
            pro.UnitId = product.UnitId;
            pro.VendorId = product.VendorId;
            pro.IsEnable = true;
            db.Products.Add(pro);
            try
            {
                db.SaveChanges();
                if (product.ProductTags != null)
                {
                    var tagList = new List<Tag>();
                    var ProductTag = new List<ProductTag>();
                    var checktag = db.Tag.Where(x => x.IsActive == true).ToList();
                    var tagFound = new Tag();
                    string[] words = product.ProductTags.Split(',');
                    foreach (var item in words)
                    {
                        tagFound = checktag.Where(x => x.Name.ToLower().Trim() == item.ToLower().Trim()).FirstOrDefault();
                        if (tagFound != null)
                        {
                            Tag tag = new Tag();
                            tag.Name = item;
                            tag.IsActive = true;

                            db.Tag.Add(tag);
                            db.SaveChanges();
                            ProductTag productTag = new ProductTag();
                            productTag.TagId = tag.Id;
                            productTag.IsActive = true;
                            productTag.ProductId = pro.Id;
                            productTag.IsApproved = true;
                            ProductTag.Add(productTag);
                        }

                    }
                    if (tagFound != null)
                    {
                        db.ProductTag.AddRange(ProductTag);
                        db.SaveChanges();
                    }
                }
               

                //---inserting variant details
                foreach (var item in product.ProductVariantDetails)
                {
                    var productDetailsData = new Models.ProductVariantDetail();
                    productDetailsData.ProductId = pro.Id;
                    productDetailsData.InStock = item.InStock;
                    productDetailsData.Price = item.Price;
                    productDetailsData.IsActive = true;
                    productDetailsData.IsDefault = item.IsDefault;
                    productDetailsData.CostPrice = item.CostPrice;
                    productDetailsData.Discount = item.Discount;
                    productDetailsData.ProductSKU = item.ProductSKU;
                    productDetailsData.Lenght = item.Lenght;
                    productDetailsData.Width = item.Width;
                    productDetailsData.Height = item.Height;

                    productDetailsData.Weight = item.Weight;
                    productDetailsData.PriceAfterdiscount = item.PriceAfterdiscount;
                    db.ProductVariantDetails.Add(productDetailsData);
                    db.SaveChanges();

                    var specifications = new List<Models.ProductionSpecification>();
                    foreach (var specs in item.ProductSpecifications)
                    {
                        var specification = new Models.ProductionSpecification();
                        specification.HeadingName = specs.HeadingName;
                        specification.Description = specs.Description;
                        specification.IsActive = true;
                        specification.VariantDetailId = productDetailsData.Id;
                        specification.ProductId = pro.Id;
                        db.ProductionSpecifications.Add(specification);
                    }
                    db.SaveChanges();

                    //---inserting variant details imnage
                    if (item.ProductImages != null && item.ProductImages.Count > 0)
                    {
                        foreach (var pi in item.ProductImages)
                        {
                            var image = new ProductImage();
                            if (pi.ImagePath != null)
                            {
                                var imageResponse = await S3Service.UploadObject(pi.ImagePath);
                                response = new JsonResult(new Object());
                                if (imageResponse.Success)
                                    image.ImagePath = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                            }

                            image.IsDefault = pi.IsDefault;
                            image.IsActive = true;

                            image.ProductId = pro.Id;
                            image.ProductVariantDetailId = productDetailsData.Id;
                            db.ProductImages.Add(image);
                        }
                        db.SaveChanges();
                    }

                    if (item.ProductVariantOptions.Count > 0)
                    {
                        foreach (var option in item.ProductVariantOptions)
                        {
                            var varoption = new Models.ProductVariantOption();
                            varoption.CategoryVariantId = option.CategoryVariantId;
                            varoption.ProductVariantDetailId = productDetailsData.Id;
                            varoption.VariantOptionId = option.VariantOptionId;
                            varoption.IsActive = true;
                            db.ProductVariantOptions.Add(varoption);
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("getVendors")]
        [EnableCors("EnableCORS")]
        public IActionResult getVendors(int Id)
        {
            return Ok(db.Users.Where(x => x.IsVerified == true && x.IsActive == true && x.RoleId == (int)RoleType.Vendor && x.Id==Id).FirstOrDefault());
        }
    }
    
}