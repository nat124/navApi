using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using Localdb;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.EntityFrameworkCore;
using TestCore.Extension_Method;
using Microsoft.AspNetCore.Cors;

namespace TestCore.Controllers
{
    [EnableCors("EnableCORS")]
    [Route("api/FeatureProduct")]
    [ApiController]
    public class FeatureProductController : ControllerBase
    {
        private readonly PistisContext db;
        public FeatureProductController(PistisContext pistis)
        {
            db = pistis;
        }

        [Route("getFeatureProduct")]
        public List<Product> getFeatureProduct()
        {
            var data = db.Products.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
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
        [Route("getFilterProduct")]
        public List<Product> getFilterProduct(int ProductId)
        {
            var data = db.Products.Where(x => x.IsActive == true && x.Id == ProductId).ToList().RemoveReferences();
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
        [Route("getFeatureProduct1")]
        public IActionResult getFeatureProduct1(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);

            var data = new List<FeatureProduct>();
            try
            {
                var query = db.FeatureProducts.Where(x => x.IsActive == true)
          .Include(x => x.Product.ProductImages)
          .OrderByDescending(x => x.Id);
                if (search == "")
                {
                    data = query.ToList().RemoveReferences();
                }
                else
                {
                    data = query.Where(c=>c.Product.Name.Contains(search)).ToList().RemoveReferences();
                }
            }

            catch (Exception ex)
            {
                Console.Write(ex);
            }

            var response = new
            {
                data = data.Skip(skipData).Take(pageSize).ToList(),
                count = data.Count
            };
            return Ok(response);
        }
        [Route("deleteFeatureProduct")]
        public FeatureProduct deleteFeatureProduct(int ProductId)
        {
            var obj = db.FeatureProducts.Where(x => x.ProductId == ProductId).FirstOrDefault();
            if (obj != null)
            {
                obj.IsActive = false;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return obj;
        }
        [Route("saveFeatureProduct")]
        public FeatureProduct saveProduct(int ProductId)
        {

            FeatureProduct featureProduct = new FeatureProduct();
            var obj = db.FeatureProducts.Where(x => x.ProductId == ProductId && x.IsActive == true).FirstOrDefault();
            if (obj == null)
            {
                featureProduct.ProductId = ProductId;
                featureProduct.IsActive = true;
                db.FeatureProducts.Add(featureProduct);
                db.SaveChanges();
                featureProduct.Id = 1;
            }
            else
            {
                featureProduct.Id = -1;
            }

            try
            {

            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return featureProduct;


        }
        [Route("updateFeatureProduct")]
        public FeatureProduct updateProduct(int ProductId)
        {
            var obj = db.FeatureProducts.Where(x => x.ProductId == ProductId).FirstOrDefault();
            if (obj != null)
            {
                obj.ProductId = ProductId;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return obj;

        }
        [HttpPost]
        [Route("getProductsFilter")]
        public IActionResult GetProducts1([FromBody]filtration filtration)
        {
          //  var skipData = filtration.pageSize * (filtration.page - 1);
            var finalList = new List<ProductCategory>();

            var prod = new List<Product>();

           
                prod = db.Products.Where(x => x.IsActive == true && x.IsEnable == true)
                .OrderByDescending(s => s.Id)
                .Include(v => v.ProductVariantDetails)
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                .AsNoTracking().ToList();
           
           
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
            //var response = new
            //{
            //    data = model.Skip(skipData).Take(filtration.pageSize).ToList(),
            //    count = count
            //};
            try
            {
            return Ok(model);
            }
            catch (Exception ex)
            {
                return Ok(ex);

                throw ex;
            }
        }
        [HttpGet]
        [Route("getProductsFilter2")]
        public IActionResult GetProducts2()
        {
            try
            {
                //  var skipData = filtration.pageSize * (filtration.page - 1);
                var finalList = new List<ProductCategory>();

            var prod = new List<Product>();
            var featureProducts = db.FeatureProducts.Where(x => x.IsActive == true).ToList();
            foreach (var item in featureProducts)
            {
                var prod2 = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.Id==item.ProductId)
          .OrderByDescending(s => s.Id)
          .Include(v => v.ProductVariantDetails)
          .Include(x => x.ProductCategory)
          .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
          .AsNoTracking().FirstOrDefault();
                    if(prod2!=null)
                prod.Add(prod2);
            }

            var count = prod.Count;
            var model = new List<ProductModel>();
            foreach (var p in prod)
            {
                var pro = new ProductModel();
                var list = p.ProductVariantDetails.Where(x => x.ProductId == p.Id && x.IsActive == true).ToList();
                pro.Id = p.Id;
                pro.Name = p.Name;
                pro.SellingPrice = p.ProductVariantDetails.Select(x => x.Price).FirstOrDefault();
                pro.CostPrice = p.ProductVariantDetails.Select(x => x.CostPrice).FirstOrDefault();
                pro.PriceAfterDiscount = p.ProductVariantDetails.Select(x => x.PriceAfterdiscount).FirstOrDefault();
                pro.IsEnable = p.IsEnable;
                if (p.ProductImages?.Count > 0)
                    pro.Image = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath;
                pro.ProductCategoryName = p.ProductCategory.Name;
                var variants = list.Count();
                var total = list.Sum(x => x.InStock);
                pro.Inventory = total + " in stock for " + variants + " variants.";
                model.Add(pro);
            }
            //var response = new
            //{
            //    data = model.Skip(skipData).Take(filtration.pageSize).ToList(),
            //    count = count
            //};
           
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Ok(ex);

                throw ex;
            }
        }


    }
}
