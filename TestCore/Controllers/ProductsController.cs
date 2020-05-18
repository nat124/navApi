using TestCore.Extension_Method;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Text;
using System.Threading.Tasks;
using TestCore.Helper;
using Microsoft.EntityFrameworkCore.Internal;



namespace TestCore.Controllers
{
    [Route("api/products")]
    [EnableCors("EnableCORS")]
    public class ProductsController : ControllerBase
    {
        private readonly PistisContext db;
        private readonly IHostingEnvironment environment;
        public ProductsController(PistisContext pistis)
        {
            db = pistis;
        }

        // GET: api/Users
        [HttpGet]
        [Route("getProductVarients")]
        public IActionResult getProductVari(int page, int pageSize, string search,int vendorId)
        {
            var skipData = pageSize * (page - 1);
            try
            {
                var productVari = new Productvarients();

                var list = new List<Productvarients>();
                 if (vendorId > 0 && search != "" && search != null)
                {
                    var data = from pro in db.Products
                               where pro.IsActive == true && pro.IsEnable == true && pro.Name.Contains(search) && pro.VendorId == vendorId
                               join prov in db.ProductVariantDetails on pro.Id equals prov.ProductId
                               join provOp in db.ProductVariantOptions on prov.Id equals provOp.ProductVariantDetailId
                               join variOp in db.VariantOptions on provOp.VariantOptionId equals variOp.Id
                               group pro by new { pro.Id, productName = pro.Name, price = prov.Price, varientOptnId = prov.Id, OptionName = variOp.Name, productId = pro.Id, Discount = prov.Discount } into bs
                               select new
                               {
                                   Productname = bs.Key.productName,
                                   Price = bs.Key.price,
                                   ProductVarientdetailId = bs.Key.varientOptnId,
                                   VarientOptnName = bs.Key.OptionName,
                                   productId = bs.Key.productId,
                                   Discount = bs.Key.Discount

                               };
                    var groupedData = data.ToList();
                    var abc = groupedData.GroupBy(x => x.ProductVarientdetailId).ToList();
                    foreach (var items in abc)
                    {
                        productVari = new Productvarients();
                        foreach (var item in items)
                        {
                            //  var item

                            productVari.productId = item.productId;
                            productVari.Productname = item.Productname;
                            productVari.VarientOptnName += item.VarientOptnName + ".";
                            productVari.ProductVarientdetailId = item.ProductVarientdetailId;
                            productVari.Discount = item.Discount;
                            productVari.Price = item.Price;

                            //for (int i = 0; i < items.Count(); i++)
                            //{
                            //    if(items.Key[i])
                            //}
                        }
                        list.Add(productVari);
                    }
                    var finaldata = list.OrderByDescending(x => x.productId).GroupBy(x => x.productId).ToList();
                    int Count = 0;
                    Count = finaldata.Count();
                    var response = new
                    {
                        data = finaldata.Skip(skipData).Take(pageSize).ToList(),
                        count = Count,
                    };
                    return Ok(response);
                }
                else if (search != "" && search!=null)
                {
                    var data = from pro in db.Products
                               where pro.IsActive == true && pro.IsEnable == true && pro.Name.Contains(search)
                               join prov in db.ProductVariantDetails on pro.Id equals prov.ProductId
                               join provOp in db.ProductVariantOptions on prov.Id equals provOp.ProductVariantDetailId
                               join variOp in db.VariantOptions on provOp.VariantOptionId equals variOp.Id
                               group pro by new { pro.Id, productName = pro.Name, price = prov.Price, varientOptnId = prov.Id, OptionName = variOp.Name, productId = pro.Id, Discount = prov.Discount } into bs
                               select new
                               {
                                   Productname = bs.Key.productName,
                                   Price = bs.Key.price,
                                   ProductVarientdetailId = bs.Key.varientOptnId,
                                   VarientOptnName = bs.Key.OptionName,
                                   productId = bs.Key.productId,
                                   Discount = bs.Key.Discount

                               };
                    var groupedData = data.ToList();
                    var abc = groupedData.GroupBy(x => x.ProductVarientdetailId).ToList();
                    foreach (var items in abc)
                    {
                        productVari = new Productvarients();
                        foreach (var item in items)
                        {
                            //  var item

                            productVari.productId = item.productId;
                            productVari.Productname = item.Productname;
                            productVari.VarientOptnName += item.VarientOptnName + ".";
                            productVari.ProductVarientdetailId = item.ProductVarientdetailId;
                            productVari.Discount = item.Discount;
                            productVari.Price = item.Price;

                            //for (int i = 0; i < items.Count(); i++)
                            //{
                            //    if(items.Key[i])
                            //}
                        }
                        list.Add(productVari);
                    }
                    var finaldata = list.OrderByDescending(x => x.productId).GroupBy(x => x.productId).ToList();
                    int Count = 0;
                    Count = finaldata.Count();
                    var response = new
                    {
                        data = finaldata.Skip(skipData).Take(pageSize).ToList(),
                        count = Count,
                    };
                    return Ok(response);

                }
                else if (vendorId > 0)
                {
                    var data = from pro in db.Products
                               where pro.IsActive == true && pro.IsEnable == true && pro.VendorId==vendorId
                               join prov in db.ProductVariantDetails on pro.Id equals prov.ProductId
                               join provOp in db.ProductVariantOptions on prov.Id equals provOp.ProductVariantDetailId
                               join variOp in db.VariantOptions on provOp.VariantOptionId equals variOp.Id
                               group pro by new { pro.Id, productName = pro.Name, price = prov.Price, varientOptnId = prov.Id, OptionName = variOp.Name, productId = pro.Id, Discount = prov.Discount } into bs
                               select new
                               {
                                   Productname = bs.Key.productName,
                                   Price = bs.Key.price,
                                   ProductVarientdetailId = bs.Key.varientOptnId,
                                   VarientOptnName = bs.Key.OptionName,
                                   productId = bs.Key.productId,
                                   Discount = bs.Key.Discount

                               };
                    var groupedData = data.ToList();
                    var abc = groupedData.GroupBy(x => x.ProductVarientdetailId).ToList();
                    foreach (var items in abc)
                    {
                        productVari = new Productvarients();
                        foreach (var item in items)
                        {
                            //  var item

                            productVari.productId = item.productId;
                            productVari.Productname = item.Productname;
                            productVari.VarientOptnName += item.VarientOptnName + ".";
                            productVari.ProductVarientdetailId = item.ProductVarientdetailId;
                            productVari.Discount = item.Discount;
                            productVari.Price = item.Price;

                            //for (int i = 0; i < items.Count(); i++)
                            //{
                            //    if(items.Key[i])
                            //}
                        }
                        list.Add(productVari);
                    }
                    var finaldata = list.OrderByDescending(x => x.productId).GroupBy(x => x.productId).ToList();
                    int Count = 0;
                    Count = finaldata.Count();
                    var response = new
                    {
                        data = finaldata.Skip(skipData).Take(pageSize).ToList(),
                        count = Count,
                    };
                    return Ok(response);

                }
              
                else
                {
                    var data = from pro in db.Products
                               where pro.IsActive == true && pro.IsEnable == true 
                               join prov in db.ProductVariantDetails on pro.Id equals prov.ProductId
                               join provOp in db.ProductVariantOptions on prov.Id equals provOp.ProductVariantDetailId
                               join variOp in db.VariantOptions on provOp.VariantOptionId equals variOp.Id
                               group pro by new { pro.Id, productName = pro.Name, price = prov.Price, varientOptnId = prov.Id, OptionName = variOp.Name, productId = pro.Id, Discount = prov.Discount } into bs
                               select new
                               {
                                   Productname = bs.Key.productName,
                                   Price = bs.Key.price,
                                   ProductVarientdetailId = bs.Key.varientOptnId,
                                   VarientOptnName = bs.Key.OptionName,
                                   productId = bs.Key.productId,
                                   Discount = bs.Key.Discount

                               };
                    var groupedData = data.ToList();
                    var abc = groupedData.GroupBy(x => x.ProductVarientdetailId).ToList();
                    foreach (var items in abc)
                    {
                        productVari = new Productvarients();
                        foreach (var item in items)
                        {
                            //  var item

                            productVari.productId = item.productId;
                            productVari.Productname = item.Productname;
                            productVari.VarientOptnName += item.VarientOptnName + ".";
                            productVari.ProductVarientdetailId = item.ProductVarientdetailId;
                            productVari.Discount = item.Discount;
                            productVari.Price = item.Price;

                            //for (int i = 0; i < items.Count(); i++)
                            //{
                            //    if(items.Key[i])
                            //}
                        }
                        list.Add(productVari);
                    }
                    var finaldata = list.OrderByDescending(x => x.productId).GroupBy(x => x.productId).ToList();
                    int Count = 0;
                    Count = finaldata.Count();
                    var response = new
                    {
                        data = finaldata.Skip(skipData).Take(pageSize).ToList(),
                        count = Count,
                    };
                    return Ok(response);

                }


                //  return Ok(finaldata);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
        [HttpGet]
        [Route("getVendors")]
        public IActionResult getvendors()
        {
            try
            {
                var anon = new List<User>();
                //var vendorIds = db.Products.Where(x => x.IsActive == true && x.VendorId > 0 && x.IsEnable == true)
                //    .ToList();
                var users = db.Users.Where(x => x.IsActive == true && x.RoleId == 2).ToList();
                //foreach (var item in vendorIds)
                //{
                //    var data = users.Where(x => x.Id == item.VendorId).FirstOrDefault();
                //    if (data != null)
                //    {
                //        anon.Add(data);
                //    }
                //}
                var finaldata = users.Select(x => new
                {
                    vendorName=x.FirstName,
                    VendorId=x.Id
                });
                return Ok(finaldata.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("ProductVarientsPrice")]
        public IActionResult getVarientsrate([FromBody] Varientsprice obj)
        {
            try
            {
                int count = 0;
                foreach (var item in obj.productVarientDetailId)
                {
                    
                    var data = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Id == item).FirstOrDefault();
                    if (data != null)
                    {
                        data.Price = obj.price[count];
                        db.SaveChanges();
                    }
                    count++;
                }
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public class Productvarients
        {
            public Productvarients()
            {
                VarientOptnName = "";
            }
            public string Productname { get; set; }
            public decimal Price { get; set; }
            public int ProductVarientdetailId { get; set; }
            public string VarientOptnName { get; set; }
            public int productId { get; set; }
            public int Discount { get; set; }
        }

        public class Varientsprice
        {
            public Varientsprice()
            {
                productVarientDetailId = new List<int>();
                price = new List<int>();
            }
            public  List<int> productVarientDetailId { get; set; }
            public  List<int> price { get; set; }
        }


        [HttpGet]
        [Route("getProducts")]
        public IActionResult GetProducts(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);

            var prod = new List<Product>();

            if (search == null)
            {
                prod = db.Products.Where(x => x.IsActive == true)
                .OrderByDescending(s => s.Id)
                .Include(v => v.ProductVariantDetails)
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                .AsNoTracking().ToList();
            }
            else
            {
                prod = db.Products.Where(x => x.IsActive == true && x.Name.Contains(search))
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
                var catid = getparentCat(p.ProductCategoryId);
                var pro = new ProductModel();
                var list = p.ProductVariantDetails.Where(x => x.ProductId == p.Id && x.IsActive == true).ToList();
                pro.Id = p.Id;
                pro.Commission = DealHelper.GetCommissionByCategoryId(catid, db);
                pro.Name = p.Name;
                pro.ShipmentVendor = p.ShipmentVendor ?? false;
                pro.ShipmentTime = p.ShipmentTime ?? 0;
                pro.ShipmentCost = p.ShipmentCost ?? 0;
                pro.IsEnable = p.IsEnable;
                if (p.ProductImages != null)
                    if(p.ProductImages.Count>0)
                    { 
                    pro.Image = p.ProductImages?.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath;
                    pro.Image150 = p.ProductImages?.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
                    pro.Image450 = p.ProductImages?.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath450x450 : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath450x450;

                }
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
        [HttpPost]
        [Route("getProductsFilter")]
        public IActionResult GetProducts1([FromBody]filtration filtration)
        {

            var skipData = filtration.pageSize * (filtration.page - 1);
            var finalList = new List<ProductCategory>();

            var prod = new List<Product>();

            if (filtration.SearchName == null || filtration.SearchName != "")
            {
                if (filtration.variant_isDefault == true)
                {
                    prod = db.Products.Where(x => x.IsActive == true && x.IsEnable == true &&x.Name.ToLower().Trim().Contains(filtration.SearchName.ToLower().Trim()))
                    .OrderByDescending(s => s.Id)
                    .Include(v => v.ProductVariantDetails)
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                    .AsNoTracking().ToList();
                }
                else
                {
                    prod = db.Products.Where(x => x.IsActive == true && x.IsEnable == false)
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
                   
                        prod = db.Products.Where(x => x.IsActive == true && x.Name.Contains(filtration.SearchName) && x.IsEnable == true)
                    .OrderByDescending(s => s.Id)
                    .Include(v => v.ProductVariantDetails)
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductImages).OrderByDescending(b => b.Id)
                    .AsNoTracking().ToList();
                    

                }
                else
                {
                    prod = db.Products.Where(x => x.IsActive == true && x.Name.Contains(filtration.SearchName) && x.IsEnable == false)
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
                pro.ShipmentCost = p.ShipmentCost??0;
                pro.ShipmentTime = p.ShipmentTime ?? 0;
                pro.ShipmentVendor = p.ShipmentVendor ?? false;
                pro.IsEnable = p.IsEnable;
                if (p.ProductImages != null)
                    if (p.ProductImages.Where(x => x.IsActive == true).ToList().Count > 0)
                    
                        pro.Image150 = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
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

        [HttpGet]
        [Route("filterProducts")]
        public IActionResult getProduct(int ProductCategoryId)
        {
            var list = new List<ProductCategory>();
            var finalList = new List<ProductCategory>();
            var datalist = new List<Models.Product>();
            var data1 = db.ProductCategories.Where(x => x.ParentId == ProductCategoryId && x.IsActive == true)
                .OrderByDescending(x => x.Id).ToList();
            if (data1 != null)
            {
                foreach (var item in data1)
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

            }

            List<product1> _prd = new List<product1>();
            try
            {

                foreach (var product in finalList)
                {
                    var data = db.Products.Where(x => x.IsActive == true && x.ProductCategoryId == product.Id && x.IsEnable == true)
                          .Include(c => c.ProductVariantDetails)
                          .Include(x => x.ProductImages)
                          .OrderByDescending(x => x.Id)
                          .ToList();

                    //  datalist.Add(data);
                    if (data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            var eachPrd = new product1();
                            eachPrd.Id = item.Id;
                            if (item.ProductImages.Count() != 0)
                            {
                                eachPrd.Url = item.ProductImages?.FirstOrDefault().ImagePath;
                                eachPrd.Url150 = item.ProductImages?.FirstOrDefault().ImagePath150x150;
                                eachPrd.Url450 = item.ProductImages?.FirstOrDefault().ImagePath450x450;

                            }
                            eachPrd.Description = item.Description;
                            eachPrd.Name = item.Name;
                            eachPrd.ShipmentVendor = item.ShipmentVendor ?? false;
                            eachPrd.ShipmentTime = item.ShipmentTime ?? 0;
                            eachPrd.ShipmentCost = item.ShipmentCost ?? 0;
                            eachPrd.ProductCategoryId = item.ProductCategoryId;
                            if (item.ProductVariantDetails.Where(c => c.IsActive == true).Any(c => c.IsDefault == true))
                            {
                                eachPrd.LandingVariant = item.ProductVariantDetails.Where(c => c.IsActive == true).Where(c => c.IsDefault == true && c.IsActive == true).Select(d => new ProductVariantDetailModel
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
                                eachPrd.LandingVariant = item.ProductVariantDetails.Where(c => c.IsActive == true).Select(d => new ProductVariantDetailModel
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

                                var img = item.ProductImages.Where(c => c.ProductId == eachPrd.Id && c.ProductVariantDetailId == eachPrd.LandingVariant.Id && c.IsActive == true).ToList();
                                if (img.Any(c => c.IsDefault == true))
                                {
                                    eachPrd.Url = img.Where(c => c.IsDefault == true).FirstOrDefault().ImagePath;
                                    eachPrd.Url150 = img.Where(c => c.IsDefault == true).FirstOrDefault().ImagePath150x150;
                                    eachPrd.Url450 = img.Where(c => c.IsDefault == true).FirstOrDefault().ImagePath450x450;
                                }
                                else
                                {
                                    eachPrd.Url = img.FirstOrDefault().ImagePath;
                                    eachPrd.Url150 = img.FirstOrDefault().ImagePath150x150;
                                    eachPrd.Url450 = img.FirstOrDefault().ImagePath450x450;
                                }
                            }
                            eachPrd.VariantDetailId = eachPrd.LandingVariant.Id;
                            _prd.Add(eachPrd);

                        };
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var count = _prd.Count;
            if (count > 6 && count < 12)
            {
                var data = _prd.Take(6);
                data = DealHelper.calculateDealForProductsList(data.ToList(), db);
                data = PriceIncrementHelper.calculatePriceForProductsList(data.ToList(), db);
                return Ok(data);
            }
            else if (count >= 12)
            {
                var data = _prd.Take(12);
                data = DealHelper.calculateDealForProductsList(data.ToList(), db);
                data = PriceIncrementHelper.calculatePriceForProductsList(data.ToList(), db);

                return Ok(data);
            }
            else
            {
                _prd = DealHelper.calculateDealForProductsList(_prd, db);
                _prd = PriceIncrementHelper.calculatePriceForProductsList(_prd, db);

                return Ok(_prd);
            }


        }
        [HttpGet]
        [Route("allProducts")]
        public async Task<IActionResult> allProducts()
        {
            List<product1> _prd = new List<product1>();
            try
            {
                var data = await db.Products.Where(x => x.IsActive == true && x.IsEnable == true)
                    .Include(c => c.ProductVariantDetails)
                    .Include(x=>x.RatingReviews)
                    .Include(x => x.ProductImages).OrderByDescending(x => x.Id).ToListAsync();
                foreach (var item in data)
                {
                    var eachPrd = new product1();
                    eachPrd.Id = item.Id;
                    if (item.ProductImages.Count() != 0)
                    {
                        eachPrd.Url = item.ProductImages?.FirstOrDefault().ImagePath;
                        eachPrd.Url150 = item.ProductImages?.FirstOrDefault().ImagePath150x150;
                        eachPrd.Url450 = item.ProductImages?.FirstOrDefault().ImagePath450x450;
                    }
                    eachPrd.Description = item.Description;
                    eachPrd.Rating = item.RatingReviews.Select(x => x.Rating).FirstOrDefault();
                    eachPrd.Name = item.Name;
                    eachPrd.ShipmentCost = item.ShipmentCost ?? 0;
                    eachPrd.ShipmentTime = item.ShipmentTime ?? 0;
                    eachPrd.ShipmentVendor = item.ShipmentVendor ?? false;
                    eachPrd.ProductCategoryId = item.ProductCategoryId;
                    if (item.ProductVariantDetails.Where(c => c.IsActive == true).Any(c => c.IsDefault == true))
                    {
                        eachPrd.LandingVariant = item.ProductVariantDetails.Where(c => c.IsActive == true).Where(c => c.IsDefault == true && c.IsActive == true).Select(d => new ProductVariantDetailModel
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
                        eachPrd.LandingVariant = item.ProductVariantDetails.Where(c => c.IsActive == true).Select(d => new ProductVariantDetailModel
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

                        var img = item.ProductImages.Where(c => c.ProductId == eachPrd.Id && c.ProductVariantDetailId == eachPrd.LandingVariant.Id && c.IsActive == true).ToList();
                        if (img.Any(c => c.IsDefault == true))
                        {
                            eachPrd.Url = img.Where(c => c.IsDefault == true).FirstOrDefault().ImagePath;
                            eachPrd.Url150 = img.Where(c => c.IsDefault == true).FirstOrDefault().ImagePath150x150;
                            eachPrd.Url450 = img.Where(c => c.IsDefault == true).FirstOrDefault().ImagePath450x450;
                        }
                        else
                        {
                            eachPrd.Url = img.FirstOrDefault()?.ImagePath;
                            eachPrd.Url = img.FirstOrDefault()?.ImagePath150x150;
                            eachPrd.Url = img.FirstOrDefault()?.ImagePath450x450;
                        }
                    }
                    if (_prd.Count < 12)
                        _prd.Add(eachPrd);
                    else
                        break;
                };
                _prd = DealHelper.calculateDealForProductsList(_prd, db);
                _prd = PriceIncrementHelper.calculatePriceForProductsList(_prd, db);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Ok(_prd);
        }

        //GET: api/Users
        //GET: api/Users

        [HttpGet]
        [Route("getFilterProducts")]
        public List<ProductModel> GetFilterProducts(int CategoryId, string Availability)
        {
            var prod = db.Products.Where(x => x.IsActive == true);
            if (CategoryId > 0)
            {
                prod = prod.Where(x => x.ProductCategoryId == CategoryId);
            }
            if (Availability == "InStock")
            {
                prod = prod.Include(x => x.ProductVariantDetails).Where(y => y.ProductVariantDetails.Any(a => a.InStock > 0));
            }
            else if (Availability == "OutStock")
            {
                prod = prod.Include(x => x.ProductVariantDetails).Where(y => y.ProductVariantDetails.Any(a => a.InStock == 0)); ;
            }
            var prod1 = prod.OrderByDescending(s => s.Id)
            .Include(x => x.ProductCategory).Include(x => x.ProductImages)
            .Include(x=>x.RatingReviews)
            .ToList().RemoveReferences();
            var model = new List<ProductModel>();
            //model = DealHelper.calculateDealForProducts(prod1, db);

            foreach (var p in prod1)
            {
                var pro = new ProductModel();
                pro.Id = p.Id;
                pro.Name = p.Name;
                pro.ShipmentCost = p.ShipmentCost??0;
                pro.ShipmentTime = p.ShipmentTime ?? 0;
                pro.ShipmentVendor = p.ShipmentVendor ?? false;
                pro.Rating = p.RatingReviews.Select(x => x.Rating).FirstOrDefault();
                if (p.ProductImages != null)
                    if (p.ProductImages.Where(x => x.IsActive == true).ToList().Count > 0)
                    {
                        pro.Image = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath;
                        pro.Image150 = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
                        pro.Image450 = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath450x450 : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath450x450;

                    }
                pro.ProductCategoryName = p.ProductCategory.Name;
                var list = db.ProductVariantDetails.Where(x => x.ProductId == p.Id && x.IsActive == true).ToList();
                var variants = list.Count();
                var total = list.Sum(x => x.InStock);
                pro.Inventory = total + " in stock for " + variants + " variants.";
                model.Add(pro);
            }
            return model;
        }

        [HttpGet]
        [Route("getProductsByName")]
        public List<ProductModel> GetProductsByName(string Name)
        {
            var prod = db.Products.Where(x => x.IsActive == true);
            if (Name != null)
            {
                prod = prod.Where(x => x.Name.ToLower().Equals(Name));
            }

            var prod1 = prod.OrderByDescending(s => s.Id)
            .Include(x => x.ProductCategory).Include(x => x.ProductImages).ToList().RemoveReferences();
            var model = new List<ProductModel>();
            foreach (var p in prod1)
            {
                var pro = new ProductModel();
                pro.Id = p.Id;
                pro.Name = p.Name;
                pro.ShipmentCost = p.ShipmentCost ?? 0;
                pro.ShipmentTime = p.ShipmentTime ?? 0;
                pro.ShipmentVendor = p.ShipmentVendor ?? false;
                if (p.ProductImages != null)
                    if (p.ProductImages.Where(x => x.IsActive == true).ToList().Count > 0)
                        {
                        pro.Image = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath;
                        pro.Image150 = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
                        pro.Image450 = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? p.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath450x450 : p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath450x450;

                    }
                pro.ProductCategoryName = p.ProductCategory.Name;
                var list = db.ProductVariantDetails.Where(x => x.ProductId == p.Id && x.IsActive == true).ToList();
                var variants = list.Count();
                var total = list.Sum(x => x.InStock);
                pro.Inventory = total + " in stock for " + variants + " variants.";
                model.Add(pro);
            }
            return model;
        }

        //[HttpGet]
        //[Route("getProductById")]
        //public Product GetProductById(int id)
        //{
        //    Product item = db.Products.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault().RemoveReferences();
        //    if (item == null)
        //    {
        //        return null;
        //    }
        //    var product = new TestCore.ProductModel();
        //    product.Id = item.Id;
        //    product.Barcode = item.Barcode;
        //    product.CostPrice = item.CostPrice;
        //    product.Description = item.Description;
        //    product.Discount = item.Discount;
        //    product.Name = item.Name;
        //    product.PriceAfterDiscount = item.PriceAfterdiscount;
        //    product.ProductCategoryId = item.ProductCategoryId;
        //    product.ProductTags = item.ProductTags;
        //    product.SellingPrice = item.SellingPrice;
        //    product.UnitId = item.UnitId;

        //    return item;
        //}


        [HttpGet]
        [Route("deleteProduct")]
        public Product DeleteProduct(int Id)
        {
            Product user = db.Products.Find(Id);
            if (user == null)
            {
                return null;
            }
            user.IsActive = false;
            try
            {
                db.SaveChanges();
                return user;
            }
            catch (Exception ex) { return null; }
        }
        // DELETE: api/Users/5
        [HttpPost]
        [Route("delete")]
        public Product DeleteUser(int id)
        {
            Product user = db.Products.Find(id);
            if (user == null)
            {
                return null;
            }
            user.IsActive = false;
            try
            {
                db.SaveChanges();
                return user;
            }
            catch (Exception ex) { return null; }
        }
        [HttpPost]
        [Route("uploadFile")]
        public ProductImage UploadFile()
        {
            Models.ProductImage Images = new Models.ProductImage();
            var postedFile = HttpContext.Request.Form.Files["Icon"];
            if (postedFile.Length > 0)
            {
                var fileName = Path.GetFileName(postedFile.FileName);
                var path = Path.Combine(environment.WebRootPath, "~/uploads", fileName);
                postedFile.CopyTo(new FileStream(path, FileMode.Create));
            }
            Images.IsActive = true;
            if (!string.IsNullOrEmpty(postedFile.FileName))
                Images.ImagePath = "~/uploads/" + postedFile.FileName;
            try
            {

                db.ProductImages.Add(Images);
                db.SaveChanges();
                return Images;
                //return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                //return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                return null;
            }

        }

        //For Productlist frontend page
        [EnableCors("EnableCORS")]
        [HttpGet]
        [Route("getproductsByCategory")]
        public List<ProductCatalogue> GetproductsByCategory(int Id, int page, int pagesize)
        {
            var prod = db.Products.Where(x => x.IsActive == true && x.IsEnable == true);
            var vendors = db.Users.Where(b => b.IsActive == true).ToList();
            if (Id > 0)
            {
                prod = prod.Where(x => x.ProductCategoryId == Id);
                //if Id is Main Category Id
                if (prod.Count() == 0)
                {
                    var cats = db.ProductCategories.Where(x => x.ParentId == Id && x.IsActive == true).ToList();
                    var subcats = new List<ProductCategory>();
                    var prods = new List<Product>();
                    foreach (var c in cats)
                    {
                        subcats.AddRange(db.ProductCategories.Where(x => x.ParentId == c.Id && x.IsActive == true).ToList());
                    }
                    subcats.AddRange(cats);
                    //to get products from 3-level categoryId
                    foreach (var s in subcats)
                    {
                        prods.AddRange(db.Products.Where(x => x.ProductCategoryId == s.Id && x.IsActive == true && x.IsEnable == true).Include(x => x.ProductCategory).Include(x => x.ProductImages).ToList().RemoveReferences());
                    }
                    prod = prods.AsQueryable();
                }
            }

            var count = prod.Count();
            var prod1 = prod.OrderByDescending(s => s.Id)
            .Include(x => x.ProductCategory).Include(x => x.ProductImages).Skip((page - 1) * pagesize).Take(pagesize).ToList().RemoveReferences();
            ////pricefilter
            //var minprice = prod.Count() == 0 ? 0 : prod.Min(x => x.SellingPrice);
            //var maxprice = prod.Count() == 0 ? 0 : prod.Max(x => x.SellingPrice);
            //var pricelist = PriceRange(maxprice, minprice);
            var model = new List<ProductCatalogue>();

            var optionsList = db.VariantOptions.Where(b => b.IsActive == true).Include(x => x.Variant).ToList();
            var ProductVariantOptions = db.ProductVariantOptions.Where(b => b.IsActive == true).ToList();

            try
            {
                var variantsList = new List<ProductVariantDetail>();
                foreach (var p in prod1)
                {
                    var pro = new ProductCatalogue();

                    pro.Id = p.Id;
                    pro.Name = p.Name;
                    pro.ShipmentCost = p.ShipmentCost ?? 0;
                    pro.ShipmentTime = p.ShipmentTime ?? 0;
                    pro.ShipmentVendor = p.ShipmentVendor ?? false;
                    pro.ProductCategoryName = p.ProductCategory.Name;
                    pro.ProductCategoryId = p.ProductCategoryId;
                    var list = db.ProductVariantDetails.Where(x => x.ProductId == p.Id && x.IsActive == true).ToList();
                    variantsList.AddRange(list);
                    var variants = list.Count();
                    var total = list.Sum(x => x.InStock);
                    //pro.Inventory = total + " in stock for " + variants + " variants.";

                    var prodDetail = new ProductVariantDetail();
                    prodDetail = list.Where(c => c.IsDefault == true && c.IsActive == true).FirstOrDefault();
                    if (prodDetail == null)
                        prodDetail = list.Where(c => c.IsActive == true).FirstOrDefault();

                    if (prodDetail != null)
                    {
                        pro.VariantDetailId = prodDetail.Id;
                        pro.Discount = prodDetail.Discount;
                        pro.SellingPrice = prodDetail.Price;
                        pro.PriceAfterDiscount = prodDetail.PriceAfterdiscount;
                        pro.InStock = prodDetail.InStock;
                        if (prodDetail.Product.VendorId > 0)
                        {
                            var vendor = vendors.Where(v => v.Id == prodDetail.Product.VendorId && v.RoleId == (int)RoleType.Vendor).FirstOrDefault();
                            if (vendor != null)
                                pro.VendorName = vendor.FirstName + " " + vendor.LastName;
                        }
                    }


                    if (p.ProductImages?.Count > 0)
                    {
                        var selectedImage = new ProductImage();
                        selectedImage = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true && x.ProductVariantDetailId == pro.VariantDetailId).FirstOrDefault();
                        if (selectedImage == null)
                            selectedImage = p.ProductImages.Where(x => x.IsActive == true && x.ProductVariantDetailId == pro.VariantDetailId).FirstOrDefault();
                        if (selectedImage.ImagePath != null)
                        {
                            pro.Image = selectedImage.ImagePath;
                            pro.Image150 = selectedImage.ImagePath150x150;
                            pro.Image450 = selectedImage.ImagePath450x450;

                        }
                        var VariantOptionIds = ProductVariantOptions.Where(x => x.ProductVariantDetailId == pro.VariantDetailId && x.IsActive == true).ToList().Select(x => x.VariantOptionId);
                        foreach (var v in VariantOptionIds)
                        {
                            var data = optionsList.Where(x => x.Id == v && x.IsActive == true).FirstOrDefault().RemoveReferences();
                            pro.VariantOptions.Add(data);
                        }
                    }

                    pro.Count = count;
                    //pro.PriceList = pricelist;
                    pro.AvgRate = AvgRating(p.Id);
                    pro.Description = p.Description;
                    model.Add(pro);
                }
                //pricefilter
                var minprice = variantsList.Count() == 0 ? 0 : variantsList.Min(x => x.Price);
                var maxprice = variantsList.Count() == 0 ? 0 : variantsList.Max(x => x.Price);
                var pricelist = PriceRange(maxprice, minprice);
                foreach (var item in model)
                    item.PriceList = pricelist;

            }
            catch (Exception ex)
            {
                throw;
            }
            return model.OrderByDescending(x => x.Id).ToList();
        }


        [HttpGet]
        [Route("getproductsByCategoryData")]
        public List<ProductCatalogue> GetproductsByCategoryData(int Id, string search, int page, int pagesize)
        {
            var prod = db.Products.Where(x => x.IsActive == true && x.IsEnable == true);
            if (search == "undefined")
            {
                search = "";
            }
            if (Id > 0)
            {
                prod = prod.Where(x => x.ProductCategoryId == Id);
                //if Id is Main Category Id

                if (prod.Count() == 0)
                {
                    var cats = db.ProductCategories.Where(x => x.ParentId == Id && x.IsActive == true).ToList();
                    var subcats = new List<ProductCategory>();
                    var prods = new List<Product>();
                    var all = db.ProductCategories.Where(x => x.IsActive == true);
                    foreach (var c in cats)
                    {
                        subcats.AddRange(all.Where(x => x.ParentId == c.Id).ToList());
                    }
                    subcats.AddRange(cats);
                    //to get products from 3-level categoryId
                    var allprod = db.Products.Where(x => x.IsActive == true && x.IsEnable == true);
                    foreach (var s in subcats)
                    {
                        prods.AddRange(allprod.Where(x => x.ProductCategoryId == s.Id).Include(x => x.ProductVariantDetails).Include(x => x.ProductCategory).Include(x => x.ProductImages).ToList().RemoveReferences());
                    }
                    prod = prods.AsQueryable();
                }
            }
            var priceListdata = new List<ProductVariantDetail>();
            var plist = prod.ToList();
            var variantlist = db.ProductVariantDetails.Where(x => x.IsActive == true).ToList();
            foreach (var p in plist)
            {
                var list = variantlist.Where(x => x.ProductId == p.Id).ToList();
                priceListdata.AddRange(list);
            }

            prod = prod.Where(x => x.Name.ToLower().Contains(search.ToLower()) || x.ProductTags.ToLower().Contains(search.ToLower()));
            var prod1 = prod.OrderByDescending(s => s.Id)
            .Include(x => x.ProductCategory).Include(b => b.ProductVariantDetails).Include(x => x.ProductImages).Skip((page - 1) * pagesize).Take(pagesize).ToList().RemoveReferences();
            var count = prod.Count();

            var model = new List<ProductCatalogue>();

            var ProductVariantOptions = db.ProductVariantOptions.Where(b => b.IsActive == true).ToList();
            var optionsList = db.VariantOptions.Where(b => b.IsActive == true).Include(x => x.Variant).ToList();

            var variantsList = new List<ProductVariantDetail>();
            //foreach (var p in prod1)
            //{
            //    var pro = new ProductCatalogue();
            //    pro.Id = p.Id;
            //    pro.Name = p.Name;

            //    pro.ProductCategoryName = p.ProductCategory.Name;
            //    pro.ProductCategoryId = p.ProductCategoryId;
            //    var list = db.ProductVariantDetails.Where(x => x.ProductId == p.Id && x.IsActive == true).ToList();
            //    variantsList.AddRange(list);
            //    var variants = list.Count();
            //    var total = list.Sum(x => x.InStock);
            //    //pro.Inventory = total + " in stock for " + variants + " variants.";

            //    var prodDetail = new ProductVariantDetail();
            //    prodDetail = list.Where(c => c.IsDefault == true && c.IsActive == true).FirstOrDefault();
            //    if (prodDetail == null)
            //        prodDetail = list.Where(c => c.IsActive == true).FirstOrDefault();

            //    if (prodDetail != null)
            //    {
            //        pro.VariantDetailId = prodDetail.Id;
            //        pro.Discount = prodDetail.Discount;
            //        pro.SellingPrice = prodDetail.Price;
            //        pro.PriceAfterDiscount = prodDetail.PriceAfterdiscount;
            //        pro.InStock = prodDetail.InStock;
            //    }

            //    if (p.ProductImages?.Count > 0)
            //    {
            //        var selectedImage = new ProductImage();
            //        selectedImage = p.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true && x.ProductVariantDetailId == pro.VariantDetailId).FirstOrDefault();
            //        if (selectedImage == null)
            //            selectedImage = p.ProductImages.Where(x => x.IsActive == true && x.ProductVariantDetailId == pro.VariantDetailId).FirstOrDefault();
            //        pro.Image = selectedImage.ImagePath;
            //        var VariantOptionIds = ProductVariantOptions.Where(x => x.ProductVariantDetailId == pro.VariantDetailId && x.IsActive == true).ToList().Select(x => x.VariantOptionId);
            //        foreach (var v in VariantOptionIds)
            //        {
            //            var data = optionsList.Where(x => x.Id == v && x.IsActive == true).FirstOrDefault().RemoveReferences();
            //            pro.VariantOptions.Add(data);
            //        }
            //    }

            //    pro.Count = count;
            //    //pro.PriceList = pricelist;
            //    pro.AvgRate = AvgRating(p.Id);
            //    pro.Description = p.Description;
            //    model.Add(pro);
            //}

            //pricefilter
            var minprice = priceListdata.Count() == 0 ? 0 : priceListdata.Min(x => x.Price);
            var maxprice = priceListdata.Count() == 0 ? 0 : priceListdata.Max(x => x.Price);
            var pricelist = PriceRange(maxprice, minprice);
            foreach (var item in model)
                item.PriceList = pricelist;

            var variantsData = new List<ProductVariantDetail>();
            foreach (var item in plist)
            {
                var data = item.ProductVariantDetails.Where(b => b.IsActive == true).ToList();
                variantsData.AddRange(data);
            }
            var final = variantsData.OrderByDescending(x => x.Id).Skip((page - 1) * pagesize).Take(pagesize).ToList();
            foreach (var p in final)
            {
                var pro = new ProductCatalogue();
                pro.Id = p.ProductId;
                //pro.Name = p.Product.Name;
                var pdata = prod.Where(x => x.Id == p.ProductId).FirstOrDefault();
                pro.Name = pdata?.Name;
                pro.ShipmentCost = pdata.ShipmentCost ?? 0;
                pro.ShipmentTime = pdata.ShipmentTime ?? 0;
                pro.ShipmentVendor = pdata.ShipmentVendor ?? false;
                pro.Description = pdata?.Description;
                pro.ProductCategoryName = pdata?.ProductCategory?.Name;
                pro.ProductCategoryId = pdata?.ProductCategoryId ?? 0;

                pro.InStock = p.InStock;

                pro.VariantDetailId = p.Id;
                pro.Discount = p.Discount;
                pro.SellingPrice = p.Price;
                pro.PriceAfterDiscount = p.Price - ((p.Price * p.Discount) / 100);
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

                pro.Count = count;
                pro.PriceList = pricelist;
                pro.AvgRate = AvgRating(p.Id);

                model.Add(pro);
            }


            return model.OrderByDescending(x => x.Id).ToList();
        }


        [HttpGet]
        [Route("getVariants")]
        public List<VariantsModel> getVariants(int Id)
        {
            var vari = db.CategoryVariants.Where(x => x.ProductCategoryId == Id && x.IsSearchOption == true && x.IsActive == true).Include(x => x.Variant).ToList();
            var variants = new List<VariantsModel>();
            var optionvalues = new List<VariantOption>();
            foreach (var v in vari)
            {
                if (!variants.Any(x => x.Name.ToLower().Equals((v.Variant?.Name).ToLower())))
                {
                    var variant = new VariantsModel();
                    variant.Id = Convert.ToInt32(v.Variant?.Id);
                    variant.Name = v.Variant?.Name;
                    optionvalues = db.VariantOptions.Where(x => x.VariantId == variant.Id && x.IsActive == true).ToList().RemoveReferences();
                    if (optionvalues.Count() > 0)
                        variant.VariantOptions = optionvalues;
                    variants.Add(variant);
                }

            }
            return variants;
        }


        [HttpPost]
        [Route("getFilterProducts")]
        public async Task<List<ProductCatalogue>> getFilterProducts([FromBody] Filter filter, [FromQuery]int page, [FromQuery] int pagesize)
        {
            try
            {
                if (filter.SearchData == "undefined" || filter.SearchData == null)
            {
                filter.SearchData = "";
            }

            var model = new List<ProductCatalogue>();
            var products = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.ProductCategory.IsShow==true).Include(x=>x.ProductVariantDetails);
            var prod = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.ProductCategoryId == filter.CategoryId && x.ProductCategory.IsShow == true).Include(x=>x.ProductVariantDetails);
            var catName = "";

            //to display category Name
            if (filter.CategoryId > 0)
                catName =  db.ProductCategories.Where(x => x.Id == filter.CategoryId && x.IsActive == true &&x.IsShow==true).FirstOrDefault().Name;
            else
                catName = "All Categories";
            decimal minprice = 0;
            decimal maxprice = 0;
            if (prod.Count() == 0) //if 2nd/1st level category
            {
                if (filter.CategoryId != 0) //find all 3rd lecevel categories under clicked category
                {
                    var cats =await db.ProductCategories.Where(x => x.ParentId == filter.CategoryId && x.IsActive == true && x.IsShow == true).ToListAsync();
                    var subcats = new List<ProductCategory>();
                    var prods = new List<Product>();
                    var all = db.ProductCategories.Where(x => x.IsActive == true);
                    foreach (var c in cats)
                    {
                        subcats.AddRange(all.Where(x => x.ParentId == c.Id).ToList());
                    }
                    subcats.AddRange(cats);
                    //to get products from 3-level categoryId
                    var allprod = db.Products.Where(x => x.IsActive == true && x.IsEnable == true);
                    foreach (var s in subcats)
                    {
                        prods.AddRange(allprod.Where(x => x.ProductCategoryId == s.Id).Include(x => x.ProductVariantDetails).Include(x => x.ProductCategory).Include(x => x.ProductImages).ToList().RemoveReferences());
                    }
                    prod = prods.AsQueryable().Include(x=>x.ProductVariantDetails);
                }
                else
                {
                    prod = products;
                    }
            }
            else
            {
                    prod = products;
            }

            var productsDAta =await prod.ToListAsync();
            //for variantfilers
            if (filter.SelectedVariants.Count() > 0)
            {

                foreach (var s in filter.SelectedVariants)
                {
                    var result = new List<Product>();
                    foreach (var o in s.VariantOptionId)
                    {
                        var pro = from cv in db.CategoryVariants
                                  join pvo in db.ProductVariantOptions on cv.Id equals pvo.CategoryVariantId
                                  join pvd in db.ProductVariantDetails on pvo.ProductVariantDetailId equals pvd.Id
                                  join p in products on pvd.ProductId equals p.Id
                                  where s.CategoryId == cv.ProductCategoryId && s.VariantId == cv.VariantId && o == pvo.VariantOptionId
                                  && cv.IsActive == true && pvo.IsActive == true && pvd.IsActive == true && p.IsActive == true
                                  select p;
                        result.AddRange(pro.ToList());
                    }
                    productsDAta = result;
                }
            }

            //to link variantdetails in productData
            var variantsData = new List<ProductVariantDetail>();
            foreach (var item in productsDAta)
            {
                if (item.ProductVariantDetails == null)
                {
                    item.ProductVariantDetails =await db.ProductVariantDetails.Where(x => x.ProductId == item.Id && x.IsActive == true).ToListAsync();
                }
                var data = item.ProductVariantDetails.Where(b => b.IsActive == true).ToList();
                variantsData.AddRange(data);
            }
           // var priceListdata = new List<ProductVariantDetail>();
            //var plist = prod.ToList();
            //var variantlist =await db.ProductVariantDetails.Where(x => x.IsActive == true).Include(x => x.Product).ToListAsync();
            //foreach (var p in plist)
            //{
            //    var list = variantlist.Where(x => x.ProductId == p.Id).ToList();
            //    priceListdata.AddRange(list);
            //}
            //pricefilter
            var minprice1 = prod.Count() == 0 ? 0 : prod.Min(x => x.SellingPrice);
            var maxprice1 = prod.Count() == 0 ? 0 : prod.Max(x => x.SellingPrice);
            var pricelist = PriceRange(maxprice1, minprice1);

            var list1 = new List<ProductVariantDetail>();
            minprice = filter.MinPrice != "" && filter.MinPrice != "0" ? Convert.ToDecimal(filter.MinPrice.Split('$')[1]) : 0;
            maxprice = filter.MaxPrice != "" && filter.MaxPrice != "0" ? Convert.ToDecimal(filter.MaxPrice.Split('$')[1]) : 0;
            if (maxprice == 0)
            {
                list1 = variantsData.Where(x => x.PriceAfterdiscount >= minprice).ToList();
            }
            else
            {
                list1 = variantsData.Where(x => x.PriceAfterdiscount >= minprice && x.PriceAfterdiscount < maxprice).ToList();
            }
            //search by name
            foreach (var l in list1)
            {
                if (l.Product == null)
                {
                    l.Product =  await db.Products.Where(x => x.Id == l.ProductId && x.IsActive == true && x.IsEnable == true).FirstOrDefaultAsync();
                }
            }
            if (filter.SearchData != "")
            {
                var nameList = list1.Where(x => x.Product.Name.ToLower().Trim().Contains(filter.SearchData.ToLower().Trim())).ToList();
                    
                //idhar 
                var Producttags =await db.ProductTag.Where(x => x.IsActive == true && x.IsApproved == true).ToListAsync();
                var tags = await db.Tag.Where(x => x.IsActive == true).ToListAsync();
                var tagsList = new List<ProductVariantDetail>();
                var tagsProductFound = new List<ProductTag>();

                var data2 = tags.Where(x => x.Name.ToLower().Trim() == filter.SearchData.ToLower().Trim()).FirstOrDefault();
                if (data2 != null)
                    tagsProductFound = Producttags.Where(x => x.TagId == data2.Id).ToList();
                foreach (var item1 in tagsProductFound)
                {
                    var tagsProductListFound =await db.ProductVariantDetails.Where(x => x.ProductId == item1.ProductId && x.IsActive == true).ToListAsync();
                    tagsList.AddRange(tagsProductListFound);
                }

                nameList.AddRange(tagsList.Where(x => nameList.All(x1 => x1.ProductId != x.ProductId)));
                list1 = nameList;
            }
                //get data for page
                list1 = list1.OrderByDescending(x => x.Id).ToList();
                var provariant = db.ProductVariantOptions.Where(x => x.IsActive == true).Include(x=>x.VariantOption);
                var variants = db.CategoryVariants.Where(x => x.IsActive == true).Include(x => x.Variant);
                var final = new List<ProductVariantDetail>();
            foreach (var l in list1)
                {
                    var item =  provariant.Where(x=> x.ProductVariantDetailId == l.Id).ToList();
                    foreach(var i in item)
                    {
                        if(variants.Any(x=>x.Id==i.CategoryVariantId && x.Variant.Name.ToLower().Trim()=="color"))
                            {
                            if(!final.Any(x=>x.ProductId==l.ProductId))
                            {
                                final.Add(l);
                                
                            }
                            var data = final.Where(x => x.ProductId == l.ProductId).FirstOrDefault();
                            i.VariantOption.Variant = null;
                            data?.ColorVariantOptions.Add(i.VariantOption);
                        }
                        else
                        {
                            if (!final.Any(x => x.ProductId == l.ProductId))
                            {
                                final.Add(l);

                            }
                        }

                    }
                    if (item.Count() == 0)
                        final.Add(l);
                }
                final = DealHelper.calculateDeal(final, db);
                final = PriceIncrementHelper.calculatePrice(final, db);
                //sorting
                if (filter.SortBy == "HighToLow")
                    final = final.OrderByDescending(x => x.PriceAfterdiscount).ToList();
                else if (filter.SortBy == "LowToHigh")
                    final = final.OrderBy(x => x.PriceAfterdiscount).ToList();
                var final1 = final.Skip((page - 1) * pagesize).Take(pagesize).ToList();
            var count = final.Count();

            var ProductVariantOptions =await db.ProductVariantOptions.Where(b => b.IsActive == true).ToListAsync();
            var optionsList = await db.VariantOptions.Where(b => b.IsActive == true).Include(x => x.Variant).ToListAsync();

            var variantsList = new List<ProductVariantDetail>();
           
            
                
            foreach (var p in final1)
            {
                
                var pro = new ProductCatalogue();
                pro.Id = p.ProductId;
                var pdata = productsDAta.Where(x => x.Id == p.ProductId).FirstOrDefault();
                pro.Name = pdata?.Name;
                pro.ShipmentVendor = pdata?.ShipmentVendor??false;
                pro.ShipmentCost = pdata?.ShipmentCost??0;
                pro.ShipmentTime = pdata?.ShipmentTime??0;
                pro.Description = pdata?.Description;
                pro.ProductCategoryName = pdata?.ProductCategory?.Name;
                pro.ProductCategoryId = Convert.ToInt32(pdata?.ProductCategoryId);
                pro.InStock = p.InStock;
                pro.VariantDetailId = p.Id;
                pro.Discount = p.Discount;
                pro.SellingPrice = p.Price;
                pro.PriceAfterDiscount = p.PriceAfterdiscount;

                var selectedImage = new ProductImage();
                selectedImage =await db.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true && x.ProductVariantDetailId == p.Id).FirstOrDefaultAsync();
                if (selectedImage == null)
                    selectedImage =await db.ProductImages.Where(x => x.IsActive == true && x.ProductVariantDetailId == pro.VariantDetailId).FirstOrDefaultAsync();
                    pro.Image = selectedImage?.ImagePath;
                pro.Image150 = selectedImage?.ImagePath150x150;
                pro.Image450 = selectedImage?.ImagePath450x450;

                var VariantOptionIds = ProductVariantOptions.Where(x => x.ProductVariantDetailId == pro.VariantDetailId && x.IsActive == true).ToList().Select(x => x.VariantOptionId);
                foreach (var v in VariantOptionIds)
                {
                    var data = optionsList.Where(x => x.Id == v && x.IsActive == true).FirstOrDefault().RemoveReferences();
                    pro.VariantOptions.Add(data);
                }
                    pro.ColorVariantOptions = p.ColorVariantOptions;
               pro.Rating = db.RatingReviews.Where(x => x.IsActive == true && x.ReviewStatusId == 1 && x.ProductId == p.ProductId).FirstOrDefault()?.Rating;
                pro.Count = count;
                pro.PriceList = pricelist;
                pro.AvgRate = AvgRating(p.Id);
                pro.ActiveTo = p.ActiveTo;
                if(pdata!=null)
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
           foreach(var r in model)
                {
                    foreach(var v in r.ColorVariantOptions)
                    {
                        v.Variant = null;
                        foreach (var l in v.ProductVariantOptions)
                        {
                            l.ProductVariantDetail = null;
                            l.VariantOption = null;
                        }
                    }
                }

                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //vishal deal

        [HttpPost]
        [Route("getFilterDealProducts")]
        public List<ProductCatalogue> getFilterDealProducts([FromBody] Filter filter, [FromQuery]int page, [FromQuery] int pagesize)
        {
            try
            {
                if (filter.SearchData == "undefined" || filter.SearchData == null)
                {
                    filter.SearchData = "";
                }
                var prod = new List<ProductVariantDetail>();
                var model = new List<ProductCatalogue>();

                var dealCatId = 0;
               // var dealproducts = db.DealProduct.Where(x => x.DealId == filter.CategoryId).Select(x => x.ProductVariantId).ToList();
                var products = db.ProductVariantDetails.Where(x => x.IsActive == true );

                var productsdeal = new List<DealProduct>();
                List<int> varientId = new List<int>();
                var activeDeal = Helper.DealHelper.getdeals(db);
                
                foreach (var item in activeDeal)
                {
                    var id = item.DealProduct.Where(x => x.DealId == filter.CategoryId).Select(x => x.ProductVariantId ).Distinct().ToList();
                    if (id.Count() > 0)
                    {
                        varientId.AddRange(id);
                    }
                }

                if (filter.CategoryId > 0)
                {
                    dealCatId = activeDeal.Where(x =>x.Id == filter.CategoryId).FirstOrDefault().ProductCategoryId;

                    //   products = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.ProductCategory.IsShow == true);
                    //  var dealcatproducts2 = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.ProductCategoryId == dealCatId && x.ProductCategory.IsShow == true).ToList();

                
                    foreach (var item in products)
                    {
                        foreach (var productVarId in varientId)
                        {
                            if (item.Id == productVarId)
                                prod.Add(item);
                        }
                    }
                }


                var catName = "";
                //to display category Name
                if (filter.CategoryId > 0)
                    catName = db.ProductCategories.Where(x => x.Id == dealCatId && x.IsActive == true && x.IsShow == true).FirstOrDefault().Name;
                else
                    catName = "All Deals";
                decimal minprice = 0;
                decimal maxprice = 0;
               
                    if (filter.SelectedVariants.Count() > 0)
                    {

                        foreach (var s in filter.SelectedVariants)
                        {
                            var result = new List<ProductVariantDetail>();
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
                
                var productsDAta = new List<ProductVariantDetail>();
                productsDAta.AddRange(prod);

                if (filter.CategoryId == 0)
                {
                    //   var productsdeal = db.DealProduct.Where(x => x.Deal.IsActive == true).Select(x => x.ProductVariantId).Distinct().ToList();
                   productsdeal = new List<DealProduct>();
                     varientId = new List<int>();
                   activeDeal = Helper.DealHelper.getdeals(db);
                    foreach (var item in activeDeal)
                    {
                        var id=item.DealProduct.Select(x => x.ProductVariantId).Distinct().ToList();
                        if (id.Count() > 0)
                        {
                            varientId.AddRange(id);
                        }
                    }



                 prod = new List<ProductVariantDetail>();
                    var Productz =db.ProductVariantDetails.Where(x => x.IsActive == true);

                    foreach (var item in varientId)
                    {
                        var selectedProdut = Productz.Where(x => x.Id == item).FirstOrDefault();
                        prod.Add(selectedProdut);
                    }
                    productsDAta.AddRange(prod);
                }
                //if (filter.CategoryId > 0)
                //{
                //    dealCatId = db.Deal.Where(x => x.IsActive == true && x.Id == filter.CategoryId).FirstOrDefault().ProductCategoryId;

                //    products = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.ProductCategory.IsShow == true);
                //    var dealcatproducts2 = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.ProductCategoryId == dealCatId && x.ProductCategory.IsShow == true).ToList();

                //    prod = new List<Product>();
                //    foreach (var item in dealcatproducts2)
                //    {
                //        foreach (var productId in dealproducts)
                //        {
                //            if (item.Id == productId)
                //                prod.Add(item);
                //        }
                //    }
                //    productsDAta.AddRange(prod);
                //}

                //var products2 = db.Products.Where(v => v.ProductCategoryId == dealCatId && v.IsActive == true && v.IsEnable == true).Include(v => v.ProductVariantDetails).ToList();
                //foreach (var item in products2)
                //{
                //    foreach (var productId in dealproducts)
                //    {
                //        if (item.Id == productId)
                //            productsDAta.Add(item);
                //    }
                //}

                //if (productsDAta.Count() == 0)
                //{
                //    if (dealCatId != 0)
                //    {
                //        var cats = db.ProductCategories.Where(x => x.ParentId == dealCatId && x.IsActive == true).ToList();
                //        var subcats = new List<ProductCategory>();
                //        var prods = new List<Product>();
                //        var all = db.ProductCategories.Where(x => x.IsActive == true);
                //        foreach (var c in cats)
                //        {
                //            subcats.AddRange(all.Where(x => x.ParentId == c.Id).ToList());
                //        }
                //        subcats.AddRange(cats);
                //        //to get products from 3-level categoryId
                //        var allprod = db.Products.Where(x => x.IsActive == true && x.IsEnable == true);
                //        foreach (var s in subcats)
                //        {
                //            productsDAta.AddRange(allprod.Where(x => x.ProductCategoryId == s.Id && x.IsActive == true).Include(x => x.ProductVariantDetails).Include(x => x.ProductCategory).Include(x => x.ProductImages).ToList().RemoveReferences());
                //        }
                //    }
                //    else
                //    {u23we
                //        productsDAta = db.Products.Where(v => v.IsActive == true && v.IsEnable == true).Include(v => v.ProductVariantDetails).ToList();
                //    }
                //}
                //for variantfilers
                if (filter.SelectedVariants.Count() > 0)
                {

                    foreach (var s in filter.SelectedVariants)
                    {
                        var result = new List<ProductVariantDetail>();
                        foreach (var o in s.VariantOptionId)
                        {
                            var pro = from cv in db.CategoryVariants
                                      join pvo in db.ProductVariantOptions on cv.Id equals pvo.CategoryVariantId
                                      join pvd in db.ProductVariantDetails on pvo.ProductVariantDetailId equals pvd.Id
                                      join p in products on pvd.ProductId equals p.Id
                                      where s.CategoryId == cv.ProductCategoryId && s.VariantId == cv.VariantId && o == pvo.VariantOptionId
                                      && cv.IsActive == true && pvo.IsActive == true && pvd.IsActive == true && p.IsActive == true
                                      select p;
                            result.AddRange(pro.ToList());
                        }
                        productsDAta = result;
                    }
                }




                var variantsData = new List<ProductVariantDetail>();
                variantsData.AddRange(productsDAta);

                //foreach (var item in productsDAta)
                //{
                //    if (item != null)
                //    {
                //        if (item.ProductVariantDetails == null)
                //        {
                //            item.ProductVariantDetails = db.ProductVariantDetails.Where(x => x.ProductId == item.Id && x.IsActive == true).ToList();
                //        }
                //        if (item.ProductVariantDetails.Count() > 0)
                //        {
                //            var data = item.ProductVariantDetails.Where(b => b.IsActive == true).ToList();
                //            variantsData.AddRange(productsDAta);
                //        }
                //    }
                //}
                //variantsData.OrderByDescending(x => x.Id);
                var priceListdata = new List<ProductVariantDetail>();
                var plist = prod.ToList();
                var variantlist = db.ProductVariantDetails.Where(x => x.IsActive == true).Include(x => x.Product).ToList();
                foreach (var p in plist)
                {
                    if (p != null)
                    {
                        var list = variantlist.Where(x => x.ProductId == p.ProductId).ToList();
                        priceListdata.AddRange(list);
                    }
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
                    var vlist=new List<ProductVariantDetail>();
                    foreach (var v in variantsData)
                    {
                        if (v != null)
                            vlist.Add(v);
                    }
                    list1 = vlist?.Where(x => x.Price >= minprice).ToList();
                }
                else
                {
                    var vlist = new List<ProductVariantDetail>();
                    foreach (var v in variantsData)
                    {
                        if (v != null)
                            vlist.Add(v);
                    }
                    list1 = vlist.Where(x => x.Price >= minprice && x.Price < maxprice).ToList();
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
                //  list1 = list1.Where(x => x.Product.Name.ToLower().Trim().Contains(filter.SearchData.ToLower().Trim())).ToList();
                if (filter.SearchData != "")
                {
                    var nameList = list1.Where(x => x.Product.Name.ToLower().Trim().Contains(filter.SearchData.ToLower().Trim())).ToList();

                    //idhar 
                    var Producttags = db.ProductTag.Where(x => x.IsActive == true && x.IsApproved == true).ToList();
                    var tags = db.Tag.Where(x => x.IsActive == true).ToList();
                    var tagsList = new List<ProductVariantDetail>();
                    var tagsProductFound = new List<ProductTag>();

                    var data2 = tags.Where(x => x.Name.ToLower().Trim() == filter.SearchData.ToLower().Trim()).FirstOrDefault();
                    if (data2 != null)
                        tagsProductFound = Producttags.Where(x => x.TagId == data2.Id).ToList();
                    foreach (var item1 in tagsProductFound)
                    {
                        var tagsProductListFound = db.ProductVariantDetails.Where(x => x.ProductId == item1.ProductId && x.IsActive == true).ToList();
                        tagsList.AddRange(tagsProductListFound);
                    }

                    nameList.AddRange(tagsList.Where(x => nameList.All(x1 => x1.ProductId != x.ProductId)));
                    list1 = nameList;
                }
                //get data for page
                var final = list1.OrderByDescending(x => x.Id).Skip((page - 1) * pagesize).Take(pagesize).ToList();
                var count = list1.Count();

                var ProductVariantOptions = db.ProductVariantOptions.Where(b => b.IsActive == true).ToList();
                var optionsList = db.VariantOptions.Where(b => b.IsActive == true).Include(x => x.Variant).ToList();

                var variantsList = new List<ProductVariantDetail>();
                final = DealHelper.calculateDeal(final, db);
                final = PriceIncrementHelper.calculatePrice(final, db);
                var PDlist = new List<ProductVariantDetail>();
                foreach( var p in productsDAta)
                {
                    if (p != null)
                        PDlist.Add(p);
                }
                foreach (var p in final)
                {

                    var pro = new ProductCatalogue();
                    pro.Id = p.ProductId;
                    //pro.Name = p.Product.Name;

                    var pdata = PDlist.Where(x => x.Id == p.Id).FirstOrDefault();
                    pro.Name = pdata?.Product.Name;
                    pro.ShipmentVendor = pdata?.Product.ShipmentVendor ?? false;
                    pro.ShipmentCost = pdata?.Product.ShipmentCost ?? 0;
                    pro.ShipmentTime = pdata?.Product.ShipmentTime ?? 0;
                    pro.Description = pdata?.Product.Description;
                    pro.ProductCategoryName = pdata?.Product.ProductCategory?.Name;
                    pro.ProductCategoryId = Convert.ToInt32(pdata?.Product.ProductCategoryId);

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

        //vishal

        //karan

        [HttpGet]
        [Route("getProductById")]
        public ProductModel GetProductById(int Id)
        {
            try
            {
                Product item = db.Products.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefault().RemoveReferences();
                if (item == null)
                    return null;
                else
                {
                    item.ProductVariantDetails = db.ProductVariantDetails.Where(c => c.ProductId == item.Id && c.IsActive == true).AsNoTracking().ToList();
                }
                var product = new TestCore.ProductModel();
                product.Id = item.Id;
                if (item.ShipmentVendor == true && item.ShipmentVendor != null)
                {
                    product.ShipmentTime = item.ShipmentTime;
                    product.ShipmentCost = item.ShipmentCost;
                }
                else
                {
                    item.ShipmentVendor = false;
                }
                product.VendorId = item.VendorId;
                product.Barcode = item.Barcode;
                product.CostPrice = item.CostPrice;
                product.Description = item.Description;
                product.Discount = item.Discount;
                product.IsEnabled = item.IsEnable;
                product.Name = item.Name;
                product.PriceAfterDiscount = item.PriceAfterdiscount;
                product.ProductCategoryId = item.ProductCategoryId;
                product.ProductTags = db.ProductTag.Where(x => x.IsActive == true && x.ProductId == Id).Select(x => x.Tag.Name).ToList().Join(",").ToString();
                product.ProductRelatedTags = db.ProductRelatedTagMap.Where(x => x.ProductId == Id).Select(x => x.RelatedTag.Name).ToList().Join(",").ToString();

                product.SellingPrice = item.SellingPrice;
                product.UnitId = item.UnitId;
                product.VendorId = item.VendorId;
                if (item.ProductVariantDetails != null && item.ProductVariantDetails.Count > 0)
                {
                    product.ProductVariantDetails = item.ProductVariantDetails.Select(v => new ProductVariantDetailModel
                    {
                        Id = v.Id,
                        ProductId = v.ProductId,
                        InStock = v.InStock,
                        Price = v.Price,
                        IsDefault = v.IsDefault,
                        CostPrice = v.CostPrice,
                        Discount = v.Discount,
                        PriceAfterdiscount = v.PriceAfterdiscount,
                        ProductSKU = v.ProductSKU,
                        Weight = v.Weight,
                        Lenght = v.Lenght,
                        Width = v.Width,
                        Height = v.Height
                    }).ToList();
                }
                if (item.ProductionSpecifications != null && item.ProductionSpecifications.Count > 0)
                {
                    product.ProductSpecifications = item.ProductionSpecifications.Where(c => c.IsActive == true).Select(c => new ProductSpecification
                    {
                        Specification = c.Description,
                        SpecificationHeading = c.HeadingName,
                        HeadingName = c.HeadingName,
                        Description = c.Description,
                    }).ToList();
                }

                product.VariantModels = new List<VariantModel>();
                product.FinalVariants = new List<Variants>();
                product.ProductImages = new List<ProductImageModel>();

                foreach (var variant in product.ProductVariantDetails)
                {
                    var imgs = db.ProductImages.Where(c => c.ProductVariantDetailId == variant.Id && c.ProductId == product.Id && c.IsActive == true).ToList();
                    if (imgs != null && imgs.Count > 0)
                    {
                        foreach (var img in imgs)
                        {
                            var prodImage = new ProductImageModel();
                            prodImage.ProductVariantDetailId = img.ProductVariantDetailId;
                            prodImage.ImagePath = img.ImagePath;
                            prodImage.ImagePath150 = img.ImagePath150x150;
                            prodImage.ImagePath450 = img.ImagePath450x450;
                            prodImage.ProductVariantDetailId = img.ProductVariantDetailId;
                            prodImage.IsDefault = img.IsDefault;
                            product.ProductImages.Add(prodImage);
                        }
                    }

                    var model = new VariantModel();
                    model.Id = variant.Id;
                    model.price = variant.Price;
                    model.CostPrice = variant.CostPrice;
                    model.Discount = variant.Discount;
                    model.PriceAfterdiscount = variant.PriceAfterdiscount;
                    model.quantity = variant.InStock;
                    model.isDefault = variant.IsDefault;
                    model.ProductSKU1 = variant.ProductSKU;
                    model.Weight1 = variant.Weight;
                    model.ProductVariantDetailId = variant.Id;
                    model.Height1 = variant.Height;
                    model.Lenght1 = variant.Lenght;
                    model.Width1 = variant.Width;
                    model.Specification = db.ProductionSpecifications.Where(c => c.IsActive == true && c.VariantDetailId == variant.Id).Select(b => new ProductSpecification
                    {
                        Heading = b.HeadingName,
                        Description = b.Description,
                    }).ToList();

                    variant.ProductVariantOptions = db.ProductVariantOptions.Where(c => c.ProductVariantDetailId == variant.Id && c.IsActive == true).AsNoTracking().Select(v => new ProductVariantOption
                    {
                        Id = v.Id,
                        ProductVariantDetailId = v.ProductVariantDetailId,
                        CategoryVariantId = v.CategoryVariantId,
                        VariantOptionId = v.VariantOptionId,
                    }).ToList();
                    StringBuilder sb = new StringBuilder();
                    int count = 0;
                    foreach (var options in variant.ProductVariantOptions)
                    {
                        count++;
                        options.VariantOption = db.VariantOptions.Where(c => c.Id == options.VariantOptionId).AsNoTracking().Select(v => new VariantOptionModel
                        {
                            Id = v.Id,
                            VariantId = v.VariantId,
                            Name = v.Name,
                        }).FirstOrDefault();

                        options.CategoryVariant = db.CategoryVariants.Where(c => c.Id == options.CategoryVariantId).AsNoTracking().Select(v => new CategoryVariantModel
                        {
                            Id = v.Id,
                            VariantId = v.VariantId,
                            ProductCategoryId = v.ProductCategoryId,
                        }).FirstOrDefault();

                        var final = db.Variants.Where(c => c.Id == options.CategoryVariant.VariantId).AsNoTracking().Select(v => new Variants
                        {
                            option = v.Name,
                            optionvalue = new List<string>(),
                        }).FirstOrDefault();

                        var optionsValues = db.VariantOptions.Where(c => c.VariantId == options.CategoryVariant.VariantId).AsNoTracking().ToList();
                        final.optionvalue = optionsValues.Select(c => c.Name).ToList();

                        if (product.FinalVariants.Count == 0)
                            product.FinalVariants.Add(final);
                        if (!product.FinalVariants.Any(c => c.option == final.option))
                            product.FinalVariants.Add(final);

                        sb.Append(final.option + ":" + options.VariantOption.Name);

                        if (count < variant.ProductVariantOptions.Count)
                            sb.Append("|");
                        else
                        {
                            model.variants = sb.ToString();
                            foreach (var pimg in product.ProductImages)
                            {
                                if (pimg.ProductVariantDetailId == variant.Id)
                                    pimg.Variants = model.variants;
                            }
                        }
                    }

                    product.VariantModels.Add(model);

                }

                if (item.ProductCategoryId > 0)
                {
                    var parent = db.ProductCategories.Where(v => v.Id == item.ProductCategoryId).FirstOrDefault().ParentId;
                    if (parent > 0)
                        product.MainCategoryId = db.ProductCategories.Where(v => v.Id == parent).FirstOrDefault().ParentId;
                }
                return product;
            }
            catch (Exception ex)
            {

                throw;
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
            if (product.ShipmentVendor == true)
            {
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
            //pro.ProductRelatedTags = product.ProductRelatedTags;
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
                        if (tagFound == null)
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
                            db.ProductTag.Add(productTag);
                            db.SaveChanges();
                        }
                        else
                        {
                            ProductTag productTag = new ProductTag();
                            productTag.TagId = tagFound.Id;
                            productTag.IsActive = true;
                            productTag.ProductId = pro.Id;
                            productTag.IsApproved = true;
                            db.ProductTag.Add(productTag);
                            db.SaveChanges();
                        }

                    }
                    
                }
                if (product.ProductRelatedTags != null)
                {
                    var tagList = new List<RelatedTag>();
                    var ProductTag = new List<ProductRelatedTagMap>();
                    var checktag = db.RelatedTag.Where(x => x.IsActive == true).ToList();
                    var tagFound = new RelatedTag();
                    string[] words = product.ProductRelatedTags.Split(',');
                    foreach (var item in words)
                    {
                        tagFound = checktag.Where(x => x.Name.ToLower().Trim() == item.ToLower().Trim()).FirstOrDefault();
                        if (tagFound == null)
                        {
                            RelatedTag tag = new RelatedTag();
                            tag.Name = item;
                            tag.IsActive = true;

                            db.RelatedTag.Add(tag);
                            db.SaveChanges();
                            ProductRelatedTagMap productTag = new ProductRelatedTagMap();
                            productTag.RelatedTagId = tag.Id;
                            productTag.ProductId = pro.Id;
                            db.ProductRelatedTagMap.Add(productTag);
                            db.SaveChanges();
                        }
                        else
                        {
                            ProductRelatedTagMap productTag = new ProductRelatedTagMap();
                            productTag.RelatedTagId = tagFound.Id;
                            productTag.ProductId = pro.Id;
                            db.ProductRelatedTagMap.Add(productTag);
                            db.SaveChanges();
                        }
                    }
                }

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
                    productDetailsData.Weight = item.Weight;
                    productDetailsData.Lenght = item.Lenght;
                    productDetailsData.Width = item.Width;
                    productDetailsData.Height = item.Height;
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
                                {
                                    image.ImagePath = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                                    try
                                    {
                                        var imageResponse1 = await S3Service.UploadObject150(pi.ImagePath, imageResponse.FileName);
                                        if (imageResponse1.Success)
                                        {
                                            image.ImagePath150x150 = $"https://pistis150x150.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";

                                        }
                                        else
                                        {
                                            image.ImagePath150x150 = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";

                                        }
                                    }
                                    catch( Exception ex)
                                    {
                                        image.ImagePath150x150 = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";

                                    }
                                    try
                                    {
                                        var imageResponse2 = await S3Service.UploadObject450(pi.ImagePath, imageResponse.FileName);

                                        if (imageResponse2.Success)
                                            image.ImagePath450x450 = $"https://pistis450x450.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                                        else
                                            image.ImagePath450x450 = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                                    }
                                    catch(Exception ex)
                                    {
                                        image.ImagePath450x450 = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";

                                    }
                                }
                                else
                                {
                                    return Ok(imageResponse.code);
                                }
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
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("updateProduct")]
        [EnableCors("EnableCORS")]
        public async Task<IActionResult> updateProduct([FromBody]ProductModel product)
        {
            JsonResult response = null;
            var pro = db.Products.Where(c => c.Id == product.Id && c.IsActive == true).Include(c => c.ProductVariantDetails).FirstOrDefault();
            if (pro != null)
            {

                product.UnitId = 1;
                //product table
                if (product.ShipmentVendor == true)
                {
                    pro.ShipmentCost = product.ShipmentCost;
                    pro.ShipmentTime = product.ShipmentTime;
                    pro.ShipmentVendor = product.ShipmentVendor;
                }
                else
                {
                    pro.ShipmentVendor = product.ShipmentVendor;

                }
                pro.CostPrice = product.CostPrice;
                pro.Description = product.Description;
                pro.Discount = product.Discount;
               pro.VendorId = product.VendorId;
                pro.IsActive = true;

                pro.Name = product.Name;
                pro.PriceAfterdiscount = product.PriceAfterDiscount;
                pro.ProductCategoryId = product.ProductCategoryId;
                pro.ProductTags = product.ProductTags;

                pro.SellingPrice = product.SellingPrice;
                pro.UnitId = product.UnitId;
                //pro.VendorId = product.VendorId;
                db.SaveChanges();
                var tags = db.Tag.Where(x => x.IsActive == true).ToList();
                string[] words = product.ProductTags.Split(',');
                var tagFound = new Tag();
                var productTag = db.ProductTag.ToList();
                var removeTags = productTag.Where(x => x.ProductId == product.Id).ToList();

                db.ProductTag.RemoveRange(removeTags);
                db.SaveChanges();
                foreach (var item in words)
                {
                    //var filterData = tags.Where(x => x.Name.ToLower().Trim() == item.ToLower().Trim()).FirstOrDefault();
                    //if (filterData != null)
                    //{
                    //    filterData.Name = item;
                    //    db.SaveChanges();
                    //    var gettags = productTag.Where(x => x.TagId == filterData.Id && x.ProductId == product.Id).FirstOrDefault();
                    //    if (item == "")
                    //    {
                    //        var removeTags = productTag.Where(x =>  x.ProductId == product.Id).ToList();

                    //        db.ProductTag.RemoveRange(removeTags);
                    //        db.SaveChanges();
                    //    }
                    //    if (gettags == null)
                    //    {
                    //        gettags = new ProductTag();
                    //        gettags.TagId = filterData.Id;
                    //        gettags.IsActive = true;
                    //        gettags.ProductId = product.Id;
                    //        gettags.IsApproved = true;
                    //        db.ProductTag.Add(gettags);
                    //        db.SaveChanges();
                    //    }
                    //}
                    //else
                    //  {
                    Tag tag = new Tag();
                    if (!tags.Any(x => x.Name.ToLower().Trim() == item.ToLower().Trim()))
                    {
                      
                        tag.Name = item;
                        tag.IsActive = true;
                        db.Tag.Add(tag);
                        db.SaveChanges();
                    }
                    else
                    {
                        tag = tags.Where(x => x.Name.Trim().ToLower() == item.ToLower().Trim()).FirstOrDefault();

                    }
                    if(tag.Id!=0)
                    { 
                        ProductTag gettags = new ProductTag();
                        gettags.TagId = tag.Id;
                        gettags.IsActive = true;
                        gettags.ProductId = product.Id;
                        gettags.IsApproved = true;
                        db.ProductTag.Add(gettags);
                        db.SaveChanges();
                   }
                }
                //RelatedTAgs
                var relatedtags = db.RelatedTag.Where(x => x.IsActive == true).ToList();
                string[] relatedwords = product.ProductRelatedTags.Split(',');
                var relatedtagFound = new Tag();
                var productRelatedTag = db.ProductRelatedTagMap.ToList();
                var removerelatedTags = productRelatedTag.Where(x => x.ProductId == product.Id).ToList();
                db.ProductRelatedTagMap.RemoveRange(removerelatedTags);
                db.SaveChanges();
                foreach (var item in relatedwords)
                {
                    //var filterData = relatedtags.Where(x => x.Name.ToLower().Trim() == item.ToLower().Trim()).FirstOrDefault();
                    //if (filterData != null)
                    //{
                    //    filterData.Name = item;
                    //    db.SaveChanges();
                    //    var gettags = productRelatedTag.Where(x => x.RelatedTagId == filterData.Id && x.ProductId == product.Id).FirstOrDefault();
                    //    if (gettags == null)
                    //    {
                    //        gettags = new ProductRelatedTagMap();
                    //        gettags.RelatedTagId = filterData.Id;
                    //        gettags.ProductId = product.Id;
                    //        db.ProductRelatedTagMap.Add(gettags);
                    //        db.SaveChanges();
                    //    }
                    //}
                    //else
                    //{
                        RelatedTag tag = new RelatedTag();
                    if (!relatedtags.Any(x => x.Name.ToLower().Trim() == item.ToLower().Trim()))
                    {

                        tag.Name = item;
                        tag.IsActive = true;
                        db.RelatedTag.Add(tag);
                        db.SaveChanges();
                    }
                    else
                    {
                        tag = relatedtags.Where(x => x.Name.Trim().ToLower() == item.ToLower().Trim()).FirstOrDefault();

                    }
                   if(tag.Id!=0)
                    { 
                        ProductRelatedTagMap gettags = new ProductRelatedTagMap();
                        gettags.RelatedTagId = tag.Id;
                        gettags.ProductId = product.Id;
                        db.ProductRelatedTagMap.Add(gettags);
                        db.SaveChanges();
                    }
                }
                try
                {

                    //deleted variants
                    var deletedVariants = pro.ProductVariantDetails.Where(v => !product.ProductVariantDetails.Any(c => c.Id == v.Id)).ToList();
                    foreach (var item in deletedVariants)
                        item.IsActive = false;
                    db.SaveChanges();

                    //---inserting variant details
                    var productvarients = db.ProductVariantDetails.Where(x => x.IsActive == true).Include(v => v.ProductVariantOptions).ToList();
                    var productImages = db.ProductImages.Where(x => x.IsActive == true).ToList();
                    foreach (var item in product.ProductVariantDetails)
                    {
                        var productDetailsData = new Models.ProductVariantDetail();
                        if (item.Id > 0)
                        {
                            productDetailsData = productvarients.Where(c => c.Id == item.Id && c.ProductId == pro.Id && c.IsActive == true).FirstOrDefault();
                            if (productDetailsData != null)
                            {
                                productDetailsData.ProductId = pro.Id;
                                productDetailsData.InStock = item.InStock;
                                productDetailsData.Price = item.Price;
                                productDetailsData.IsActive = true;
                                productDetailsData.IsDefault = item.IsDefault;
                                productDetailsData.CostPrice = item.CostPrice;
                                productDetailsData.Discount = item.Discount;
                                productDetailsData.PriceAfterdiscount = item.PriceAfterdiscount;
                                productDetailsData.ProductSKU = item.ProductSKU;
                                productDetailsData.Weight = item.Weight;
                                productDetailsData.Lenght = item.Lenght;
                                productDetailsData.Width = item.Width;
                                productDetailsData.Height = item.Height;

                                foreach (var options in productDetailsData.ProductVariantOptions)
                                    options.IsActive = false;

                                var imgs = productImages.Where(c => c.ProductId == pro.Id && c.ProductVariantDetailId == item.Id ).ToList();
                                foreach (var img in imgs)
                                    img.IsActive = false;
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            if (productDetailsData == null)
                                productDetailsData = new ProductVariantDetail();
                            productDetailsData.ProductId = pro.Id;
                            productDetailsData.InStock = item.InStock;
                            productDetailsData.Price = item.Price;
                            productDetailsData.IsActive = true;
                            productDetailsData.IsDefault = item.IsDefault;
                            productDetailsData.CostPrice = item.CostPrice;
                            productDetailsData.Discount = item.Discount;
                            productDetailsData.PriceAfterdiscount = item.PriceAfterdiscount;
                            db.ProductVariantDetails.Add(productDetailsData);
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


                        var oldSpec = db.ProductionSpecifications.Where(c => c.IsActive == true && c.VariantDetailId == productDetailsData.Id).ToList();
                        foreach (var sp in oldSpec)
                            sp.IsActive = false;

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
                                    if (pi.ImagePath.Contains("https://pistis.s3.us-east-2.amazonaws.com"))
                                    {
                                        image = db.ProductImages.Where(b => b.ImagePath == pi.ImagePath && b.ProductVariantDetailId == productDetailsData.Id && b.ProductId == pro.Id).FirstOrDefault();
                                        image.IsDefault = pi.IsDefault;
                                        image.IsActive = true;
                                        image.ProductId = pro.Id;
                                        image.ProductVariantDetailId = productDetailsData.Id;
                                        db.SaveChanges();
                                        continue;
                                    }
                                    else
                                    {
                                        var imageResponse = await S3Service.UploadObject(pi.ImagePath);
                                
                                        response = new JsonResult(new Object());
                                        if (imageResponse.Success)
                                        {
                                            image.ImagePath = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                                            try
                                            {
                                                var imageResponse1 = await S3Service.UploadObject150(pi.ImagePath, imageResponse.FileName);
                                                if (imageResponse1.Success)
                                                {

                                                    image.ImagePath150x150 = $"https://pistis150x150.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";

                                                }
                                                else
                                                {
                                                    image.ImagePath150x150 = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";

                                                }
                                            }
                                            catch(Exception ex)
                                            {
                                                image.ImagePath150x150 = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";

                                            }
                                            try
                                            {
                                                var imageResponse2 = await S3Service.UploadObject450(pi.ImagePath, imageResponse.FileName);

                                                if (imageResponse2.Success)
                                                    image.ImagePath450x450 = $"https://pistis450x450.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                                                else
                                                    image.ImagePath450x450 = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                                            }
                                            catch(Exception ex)
                                            {
                                                image.ImagePath450x450 = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";

                                            }
                                        }
                                        else
                                        {
                                            return Ok(imageResponse.code);
                                        }
                                        image.IsDefault = pi.IsDefault;
                                        string imageName = "img" + DateTime.Now + ".jpg";
                                        image.ProductId = pro.Id;
                                        image.ProductVariantDetailId = productDetailsData.Id;
                                        image.IsActive = true;
                                        db.ProductImages.Add(image);
                                        db.SaveChanges();

                                    }

                                }
                            }
                        }

                    }
                    return Ok(product);
                }
                catch (Exception ex) {

                    return Ok(ex);
                }
            }
            else
                return Ok("not Found");
        }

        [HttpGet]
        [Route("checkExistingSKU")]
        public IActionResult checkExistingSKU(int? productId = 0, int? variantId = 0, string sku = "")
        {
            var data = db.ProductVariantDetails.Where(c => c.IsActive == true && c.ProductId != productId && c.Id != variantId && c.ProductSKU == sku).FirstOrDefault();
            if (data != null)
                return Ok(true);
            else
                return Ok(false);
        }


        [HttpGet]
        [Route("getAllVariants")]
        public IActionResult getAllVariants()
        {
            var data = db.ProductVariantDetails.Where(c => c.IsActive == true).AsNoTracking().ToList();
            if (data.Count > 0)
                return Ok(data);
            else
                return Ok(false);
        }




        //------------Karan------------------------------||


        //get Main Category
        int getparentCat(int Id)
        {
            var data = db.ProductCategories.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault().RemoveReferences();
            if (data.ParentId == null)
                return data.Id;
            else
                return getparentCat(Convert.ToInt32(data.ParentId));
        }
        //price range
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

        [HttpGet]
        [Route("getbreadcrumb")]
        public SideCategoryModel getbreadcrumb(int id)
        {
            var result = new SideCategoryModel();
            var two = new ProductCategory();
            if (id > 0)
            {
                var data = db.ProductCategories.Where(x => x.Id == id && x.IsActive == true).Include(x => x.Parent).FirstOrDefault().RemoveReferences();
                if (data != null)
                {
                    result.ChildId = data.Id;
                    result.ChildName = data.Name;
                    if (data.ParentId != null)
                    {
                        two = db.ProductCategories.Where(x => x.Id == data.ParentId && x.IsActive == true).FirstOrDefault();

                        result.Idlevel2 = Convert.ToInt32(two?.Id);
                        result.Namelevel2 = two?.Name;
                    }
                    if (two.ParentId != null)
                    {
                        var one = db.ProductCategories.Where(x => x.Id == two.ParentId && x.IsActive == true).FirstOrDefault();
                        if (one != null)
                        {
                            result.Idlevel1 = Convert.ToInt32(one?.Id);
                            result.Namelevel1 = one?.Name;
                        }
                    }
                }


            }
            return result;
        }

        [HttpGet]
        [Route("Product-variant-detail")]
        public IActionResult ProductVariatDetail(int Id, int dealId)
        {
            try
            {
                var result = (from pv in db.ProductVariantDetails
                              join p in db.Products on pv.ProductId equals p.Id
                              join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                              where c.Id == Id && pv.IsActive == true && p.IsActive == true && p.IsEnable == true
                              select new DealProductModel
                              {
                                  Id = pv.Id,
                                  Selected = false,
                                  Name = p.Name,
                                  //ShipmentVendor=p.ShipmentVendor??false,
                                  //ShipmentCost=p.ShipmentCost??0,
                                  //ShipmentTime=p.ShipmentTime??0,
                                  Price = pv.PriceAfterdiscount,
                                  Category = c.Name,
                                  SKU = pv.ProductSKU,
                                  CategoryId = c.Id,
                                  ProductId = p.Id,
                                  AlreadyInDeal = false,
                              }).ToList();
                var deals = DealHelper.getdeals(db);
                var dealpro = new List<DealProduct>();
                var result1 = new List<DealProductModel>();
                if (dealId == 0)
                {
                    dealpro = deals.SelectMany(x => x.DealProduct).ToList();
                }
                else
                {
                    dealpro = deals.Where(x => x.Id != dealId).SelectMany(x => x.DealProduct).ToList();
                }
                foreach (var d in result)
                {
                    if (dealpro.Any(z => z.ProductVariantId == d.Id))
                    {
                        d.AlreadyInDeal = true;
                    }
                    result1.Add(d);

                }
                return Ok(result1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Product-variant-details")]
        public IActionResult ProductVariatDetails(int Id)
        {
            try
            {
                var result = (from pv in db.ProductVariantDetails
                              join p in db.Products on pv.ProductId equals p.Id
                              join c in db.ProductCategories on p.ProductCategoryId equals c.Id
                              where c.Id == Id && pv.IsActive == true && p.IsActive == true && p.IsEnable == true
                              select new DealProductModel
                              {
                                  Id = pv.Id,
                                  Selected = false,
                                  Name = p.Name,
                                  ShipmentVendor =Convert.ToBoolean(p.ShipmentVendor),
                                  ShipmentCost =Convert.ToDecimal( p.ShipmentCost),
                                  ShipmentTime =Convert.ToInt32( p.ShipmentTime),
                                  Price = pv.PriceAfterdiscount,
                                  Category = c.Name,
                                  SKU = pv.ProductSKU,
                                  CategoryId = c.Id,
                                  ProductId = p.Id,
                                  AlreadyInDeal = false,
                              }).ToList();



                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("enableProduct")]
        public IActionResult enableProduct(int id)
        {
            var data = db.Products.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
            if (data != null)
            {
                data.IsEnable = true;
            }
            try
            {
                db.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        [HttpGet]
        [Route("disableProduct")]
        public IActionResult disableProduct(int id)
        {
            var data = db.Products.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
            if (data != null)
            {
                data.IsEnable = false;
            }
            try
            {
                db.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }
        [HttpGet]
        [Route("getproductsAllCat")]
        public SideCategoryModel GetproductsAllCat(int id)
        {
            var result = new SideCategoryModel();
            var list = new List<ProductCategory>();
            if (id > 0)
            {
                var data = db.ProductCategories.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
                if (data?.ParentId == null)
                {
                    result.Idlevel1 = data.Id;
                    result.Namelevel1 = data.Name;
                    result.SpanishNamelevel1 = data.SpanishName;
                    list = db.ProductCategories.Where(x => x.ParentId == data.Id && x.IsActive == true).ToList().RemoveReferences();
                    //foreach (var item in list)
                    //{
                    //    var checkChildern = db.ProductCategories.Where(x => x.ParentId == item.Id && x.IsActive == true).ToList().RemoveReferences();
                    //    if (checkChildern.Count == 0)
                    //    {
                    //        if (item.Id == id)
                    //        {
                    //            result.Children.Add(item);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        result.Idlevel2 = item.Id;
                    //        result.Namelevel2 = item.Name;
                    //        result.SpanishNamelevel2 = item.SpanishName;
                    //    }
                    //}
                }
                else
                {
                    list = db.ProductCategories.Where(x => x.ParentId == data.ParentId && x.IsActive == true).ToList().RemoveReferences();
                   foreach (var l in list)
                    {
                        l.Parent = null;
                        if (l.Id == id)
                            result.Children.Add(l);
                    }

                    //result.Children = list;
                    if (data.ParentId != null)
                    {
                        var parent = db.ProductCategories.Where(x => x.Id == data.ParentId && x.IsActive == true).FirstOrDefault();
                        if (parent.ParentId == null)
                        {
                            result.Idlevel1 = parent.Id;
                            result.Namelevel1 = parent.Name;
                            result.SpanishNamelevel1 = parent.SpanishName;
                        }
                        else
                        {
                            result.Idlevel2 = parent.Id;
                            result.Namelevel2 = parent.Name;
                            result.SpanishNamelevel2 = parent.SpanishName;
                            var superparent = db.ProductCategories.Where(x => x.Id == parent.ParentId && x.IsActive == true).FirstOrDefault();
                            result.Idlevel1 = superparent.Id;
                            result.Namelevel1 = superparent.Name;
                            result.SpanishNamelevel1 = superparent.SpanishName;
                        }
                    }
                }

            }
            else
            {
                result.Namelevel1 = "All Categories";
            }
            return result;
        }
        [HttpGet]
        [Route("getproductsSideCat")]
        public SideCategoryModel GetproductsSideCat(int id)
        {
            var result = new SideCategoryModel();
            var list = new List<ProductCategory>();
            if (id > 0)
            {
                var data = db.ProductCategories.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
                if (data?.ParentId == null)
                {
                    result.Idlevel1 = data.Id;
                    result.Namelevel1 = data.Name;
                    result.SpanishNamelevel1 = data.SpanishName;
                    list = db.ProductCategories.Where(x => x.ParentId == data.Id && x.IsActive == true).ToList().RemoveReferences();
                    result.OtherIdlevel2 = list;
                    //foreach (var item in list)
                    //{
                    //    var checkChildern = db.ProductCategories.Where(x => x.ParentId == item.Id && x.IsActive == true).ToList().RemoveReferences();
                    //    if (checkChildern.Count == 0)
                    //    {
                    //        result.Children = list;
                    //    }
                    //    else
                    //    {
                    //        result.Idlevel2 = item.Id;
                    //        result.Namelevel2 = item.Name;
                    //        result.SpanishNamelevel2 = item.SpanishName;

                    //    }
                    //}
                }
                else
                {
                    var list1 = new List<ProductCategory>();
                    list = db.ProductCategories.Where(x => x.ParentId == data.ParentId && x.IsActive == true).ToList().RemoveReferences();
                    var all = db.ProductCategories.Where(x => x.IsActive == true).ToList().RemoveReferences();
                    foreach (var l in list)
                    {
                        if (l.Id == id && all.Any(x => x.ParentId == id))
                        {
                            result.Idlevel2 = id;
                            result.Namelevel2 = l.Name;
                            result.SpanishNamelevel2 = l.SpanishName;
                            list1 = all.Where(x => x.ParentId == id).ToList();
                        }
                        else
                        {
                            if (all.Any(x => x.ParentId == l.Id))
                            {
                                result.OtherIdlevel2.Add(l);
                                //l.Parent = null;
                                //list1.Add(l);
                            }
                            else
                            {
                                //if (l.Id == id)
                                {
                                    list1.Add(l);
                                }
                            }
                        }


                    }
                    result.Children = list1;
                    if (data.ParentId != null)
                    {
                        var parent = db.ProductCategories.Where(x => x.Id == data.ParentId && x.IsActive == true).FirstOrDefault();
                        if (parent.ParentId == null)
                        {
                            result.Idlevel1 = parent.Id;
                            result.Namelevel1 = parent.Name;
                            result.SpanishNamelevel1 = parent.SpanishName;
                        }
                        else
                        {
                            result.Idlevel2 = parent.Id;
                            result.Namelevel2 = parent.Name;
                            result.SpanishNamelevel2 = parent.SpanishName;
                            var superparent = db.ProductCategories.Where(x => x.Id == parent.ParentId && x.IsActive == true).FirstOrDefault();
                            result.Idlevel1 = superparent.Id;
                            result.Namelevel1 = superparent.Name;
                            result.SpanishNamelevel1 = superparent.SpanishName;
                        }
                    }
                }

            }
            else
            {
                result.Namelevel1 = "All Categories";
                result.OtherIdlevel2 = db.ProductCategories.Where(x => x.ParentId == null && x.IsActive == true && x.IsShow == true).ToList();
            }
            return result;
        }

        [HttpGet]
        [Route("getLikeProducts")]
        public IActionResult GetLikeProducts( int ProductId)
        {
            var list = db.ProductTag.Where(x => x.ProductId == ProductId).ToList();
            var alltags = db.ProductTag.Where(x => x.IsActive == true).ToList();
            var finaltags = new List<ProductTag>();
            foreach (var l in list)
            {
                var get = alltags.Where(x => x.TagId == l.TagId && x.ProductId != l.ProductId).ToList();
                finaltags.AddRange(get);
            }
            finaltags.Take(20).ToList();
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
        [Route("checkAdult")]
        public IActionResult checkAdult(int catid,string searchData)
        {
            try
            {
                bool? chechAdult=false;
                if (catid != 0)
                {
                    chechAdult = db.ProductCategories.Where(x => x.Id == catid).FirstOrDefault()?.IsAdult;
                    if(chechAdult==null)
                        chechAdult = false;

                }
                else if(searchData!="" || searchData!=null ||searchData!= "undefined")
                {
                    var product = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Product.Name.ToLower().Trim().Contains(searchData.ToLower().Trim())).Include(x=>x.Product.ProductCategory).ToList();
                   if(product.Count()!=0)
                    foreach (var item in product)
                    {
                            var check = item.Product.ProductCategory.IsAdult;
                            if(check==true)
                            {
                                chechAdult = true;
                                break;
                            }
                    }
                }else if((searchData != "" || searchData != null || searchData != "undefined") && catid != 0)
                {
                    chechAdult = db.ProductCategories.Where(x => x.Id == catid).FirstOrDefault()?.IsAdult;
                    if(chechAdult==null)
                    {
                        var product = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Product.Name.ToLower().Trim().Contains(searchData.ToLower().Trim())).Include(x => x.Product.ProductCategory).ToList();
                        if (product.Count() != 0)
                            foreach (var item in product)
                            {
                                var check = item.Product.ProductCategory.IsAdult;
                                if (check == true)
                                {
                                    chechAdult = true;
                                    break;
                                }
                            }
                    }
                }

                return Ok(chechAdult);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }

    public class product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public int UnitId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Discount { get; set; }
        public decimal PriceAfterdiscount { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

    }
    public class filtration
    {
        public int page { get; set; }
        public string SearchName { get; set; }
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int pageSize { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool variant_isDefault { get; set; }
    }
}
