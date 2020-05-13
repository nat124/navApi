using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TestCore.Helper;


namespace TestCore.Controllers
{
    [Route("api/homeCategory")]
    [ApiController]
   
    public class HomeCategoryController : ControllerBase
    {
        private readonly PistisContext db;
        public HomeCategoryController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("orderNumberCount")]
        public IActionResult orderNumber()
        {
            try
            {
                var data = db.HomeCategory.Where(x => x.IsActive == true).Count();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [Route("SaveListName")]
        public IActionResult savelistdetails([FromBody]listDetails listDetails)
        {
            var message = 0;
            try
            {
                if (listDetails.OrderNumber > 0)
                {
                    var data = db.HomeCategory.Where(x => x.OrderNumber == listDetails.OrderNumber && x.IsActive==true).FirstOrDefault();
                    var dataCount = db.HomeCategory.Where(x => x.IsActive == true).ToList();
                    if (data == null)
                    {
                      

                        var Data = new Models.HomeCategory();
                        Data.OrderNumber = listDetails.OrderNumber;
                        Data.Name = listDetails.listName;
                        Data.IsActive = listDetails.IsActive;
                        Data.IsDesktop = listDetails.IsDesktop;
                        Data.Shape = listDetails.Shape == "Circle" ? "Circle" : listDetails.Shape == "Square" ? "Square" : "Rectangular";
                        db.HomeCategory.Add(Data);
                        db.SaveChanges();
                        message = 1;
                    }
                    else
                    {
                        // var oldOrderNumber = data.OrderNumber;
                        // var oldName = data.Name;
                        //var oldIsactive = data.IsActive;
                        //  data.IsActive = false;
                        //    db.SaveChanges();
                        //first false and store infor
                        var Data = new Models.HomeCategory();

                        Data.IsDesktop = listDetails.IsDesktop;
                        Data.Shape = listDetails.Shape == "Circle" ? "Circle" : listDetails.Shape == "Square" ? "Square" : "Rectangular";
                        Data.OrderNumber = listDetails.OrderNumber;
                        Data.Name = listDetails.listName;
                        Data.IsActive = listDetails.IsActive;
                        db.HomeCategory.Add(Data);
                        db.SaveChanges();
                        //seconed add the new one

                        var newOrderNumberforOld = dataCount.Count() + 1;
                        //var Data2 = new Models.HomeCategory();
                        //Data2.IsActive = true;
                        //Data2.Name = oldName;
                        //Data2.OrderNumber = newOrderNumberforOld;
                        //db.HomeCategory.Add(Data2);
                        data.OrderNumber = newOrderNumberforOld;
                        db.SaveChanges();
                        message = 1;
                        //add the old one with count +1 ordernumber
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            var newList = db.HomeCategory.ToList();
            return Ok(newList);
        }
        [HttpGet]
        [Route("OrderList")]
        public IActionResult orderList()
        {
            try
            {
                var list = db.HomeCategory.Where(x => x.IsActive == true).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("deactivate")]
        public IActionResult deativatelist(int Id)
        {
            var message = 0;
            try
            {
                if (Id > 0)
                {
                    var data = db.HomeCategory.Where(x => x.Id == Id).FirstOrDefault();

                    var checkOrderNumber = db.HomeCategory.Where(x => x.Id == Id && x.IsActive == true).Select(x=>x.OrderNumber).FirstOrDefault();


                    if (data.IsActive == false)
                    {
                        if (checkOrderNumber==0)
                        {
                            var count = db.HomeCategory.Max(x=>x.OrderNumber);
                            var newOrderNumber = count + 1;
                            data.OrderNumber = newOrderNumber;

                        }

                        data.IsActive = true;
                        db.SaveChanges();

                    }
                    else
                    {
                        checkOrderNumber = db.HomeCategory.Where(x => x.Id == Id && x.IsActive == true).Select(x => x.OrderNumber).FirstOrDefault();

                        data.IsActive = false;
                        data.OrderNumber = 0;
                        db.SaveChanges();
                        if (!db.HomeCategory.Any(x => x.OrderNumber == checkOrderNumber))
                        {
                            var biggerdata = db.HomeCategory.Where(x => x.OrderNumber > checkOrderNumber).ToList();
                            if (biggerdata != null)
                            {
                                foreach (var item in biggerdata)
                                {
                                    item.OrderNumber = item.OrderNumber - 1;
                                    db.SaveChanges();
                                }
                            }
                        }
                        }

                        message = 1;
                }
                return Ok(message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("getDealsProducts")]
        public IActionResult getDealsProducts()
          {
           
            var finalDeal = DealHelper.getdeals(db);
            finalDeal = finalDeal.Where(x => x.IsShow == true).ToList();//to show on home page

            var DealList = new List<Deal>();
            var dealLists = new DealLists();
            var DealListsss = new List<DealLists>();
            try
            {

                foreach (var item in finalDeal)
                {
                    var productList = new List<product7>();

                    dealLists = new DealLists();
                    dealLists.Id = item.Id;
                    dealLists.ListName = item.Name;
                    dealLists.ActiveFrom = item.ActiveFrom.Date.ToShortDateString() + " " + item.ActiveFromTime;
                    dealLists.ActiveTo = item.ActiveTo.Date.ToShortDateString() + " " + item.ActiveToTime;
                   var list= allProducts(item.DealProduct.Select(x => x.ProductVariantId).ToList(),item.Id);
                    dealLists.products = list.Take(6).OrderByDescending(x=>x.Id).ToList();

                    DealListsss.Add(dealLists);

                }
               
                return Ok(DealListsss);
            }
           catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("getMobileDealsProducts")]
        public IActionResult getDealsProductsMobile()
        {

            var finalDeal = DealHelper.getdeals(db);
            finalDeal = finalDeal.Where(x => x.IsShow == true).ToList();//to show on home page
            var DealList = new List<Deal>();
            var dealLists = new DealLists();
            var DealListsss = new List<DealLists>();
            try
            {
                foreach (var item in finalDeal)
                {
                    var productList = new List<product7>();

                    dealLists = new DealLists();
                    dealLists.Id = item.Id;
                    dealLists.ListName = item.Name;
                    dealLists.ActiveFrom = item.ActiveFrom.Date.ToShortDateString() + " " + item.ActiveFromTime;
                    dealLists.ActiveTo = item.ActiveTo.Date.ToShortDateString() + " " + item.ActiveToTime;
                    var list = allProducts(item.DealProduct.Select(x => x.ProductVariantId).ToList(), item.Id);
                    dealLists.products = list.Take(4).OrderByDescending(x => x.Id).ToList();

                    DealListsss.Add(dealLists);

                }

                return Ok(DealListsss);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("getParticularDealProducts")]
        public IActionResult DealProducts(int Id)
        {
            var finalDeal = DealHelper.getdeals(db);
            if(Id>0)
            finalDeal = finalDeal.Where(x => x.Id == Id).ToList();
            var DealList = new List<Deal>();
            var dealLists = new DealLists();
            var DealListsss = new List<DealLists>();
            try
            {

                foreach (var item in finalDeal)
                {
                    var productList = new List<product7>();

                    dealLists = new DealLists();
                    dealLists.Id = item.Id;
                    dealLists.ListName = item.Name;
                    dealLists.ActiveFrom = item.ActiveFrom.Date.ToShortDateString() + " " + item.ActiveFromTime;
                    dealLists.ActiveTo = item.ActiveTo.Date.ToShortDateString() + " " + item.ActiveToTime;
                    //foreach (var item2 in item.DealProduct)
                    //{
                    //    var ProductData = allProducts(item2.ProductId, item.Name, 1);
                    //    productList.Add(ProductData);
                    //}
                    //dealLists.products.AddRange(productList);
                    dealLists.products = allProducts(item.DealProduct.Select(x => x.ProductVariantId).ToList(),item.Id);
                    DealListsss.Add(dealLists);

                }
                return Ok(DealListsss);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("getAllDealProducts")]
        public IActionResult DealAllProducts()
        {
            var finalDeal = DealHelper.getdeals(db);
            var DealList = new List<Deal>();
            var dealLists = new DealLists();
            var DealListsss = new List<DealLists>();
            try
            {

                foreach (var item in finalDeal)
                {
                    var productList = new List<product7>();

                    dealLists = new DealLists();
                    dealLists.Id = item.Id;
                    dealLists.ListName = item.Name;
                    dealLists.ActiveFrom = item.ActiveFrom.Date.ToShortDateString() + "" + item.ActiveFromTime;
                    dealLists.ActiveTo = item.ActiveFrom.Date.ToShortDateString() + "" + item.ActiveFromTime;
                    //foreach (var item2 in item.DealProduct)
                    //{
                    //    var ProductData = allProducts(item2.ProductId, item.Name, 1);
                    //    productList.Add(ProductData);
                    //}
                    //dealLists.products.AddRange(productList);
                    dealLists.products = allProducts(item.DealProduct.Select(x => x.ProductVariantId).ToList(),item.Id);
                    DealListsss.Add(dealLists);

                }
                return Ok(DealListsss);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("getAllLists")]
        public IActionResult getlists()
        {
            try
            {
                //var products = db.HomeCategory.GroupBy(x=>x.OrderNumber).Select(x=> new
                //{
                //    Name=x.
                //})

                // var data = from hcat in db.HomeCategory
                //            group hcat by new { hcat.OrderNumber, hcat.Name, hcat.IsActive, hcat.Id } into ul
                //            select new
                //            {
                //                Name = ul.Key.Name,
                //                OrderNumber = ul.Key.OrderNumber,
                //                IsActive = ul.Key.IsActive,
                //                 Id = ul.Key.Id
                //            };
                //var data2 = data.ToList();

                var data2 = db.HomeCategory.OrderByDescending(x=>x.Id).ToList();


                return Ok(data2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
        [HttpGet]
        [Route("getProducts")]
        public IActionResult getproducts(int Id)
        {
            try
            {
                var product = db.Products.Where(x => x.IsActive == true && x.ProductCategoryId == Id && x.IsEnable == true)
                   .Select(x => new
                   {
                       name = x.Name,
                       Id = x.Id
                   });
                var data = product.ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [Route("saveproducts")]
        public IActionResult addProducts([FromBody]homeCategoryList model)
        {   
            var message = 0;
            try
            {
                var homeCategory = new List<Models.HomeCategoryProduct>();
                var homeCatProd = db.HomeCategoryProduct.Where(x => x.IsActive == true && x.HomeCategoryId==model.homeCategoryId).ToList();
                if (model.homeCategoryId > 0)
                {
                    foreach (var item in model.productID)
                    {
                       
                       if(homeCatProd.Any(x => x.ProductId == item))
                        {

                        }
                        else {
                            var hcat = new HomeCategoryProduct();
                            hcat.HomeCategoryId = model.homeCategoryId;

                            hcat.ProductId = item;
                            hcat.IsActive = true;
                            homeCategory.Add(hcat);
                        }

                          
                    }
                    db.HomeCategoryProduct.AddRange(homeCategory);
                    db.SaveChanges();
                    message = 1;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }
        [HttpGet]
        [Route("Deleteproducts")]
        public IActionResult deleteProduct(int homeCategoryId,int ProductId)
        {
            var message = 0;
            try
            {
                var data = db.HomeCategoryProduct.Where(x => x.HomeCategoryId == homeCategoryId && x.ProductId == ProductId && x.IsActive==true).FirstOrDefault();
                data.IsActive = false;
                db.SaveChanges();
                message = 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }
        [HttpGet]
        [Route("getHomelist")]
        public IActionResult getHomeList(int Id)
        {
            try
            {
                var data = db.HomeCategoryProduct.Where(x => x.IsActive == true && x.HomeCategoryId == Id).ToList();
                var productsData = db.Products.Where(x => x.IsActive == true && x.IsEnable == true).ToList();
                var productList = new List<Product>();
                foreach (var item in data)
                {
                    var products = productsData.Where(x => x.Id == item.ProductId).FirstOrDefault();
                    if(products!=null)
                    productList.Add(products);

                }
             var productslii=   productList.Select(x => new
                {
                    name = x.Name,
                    Id = x.Id
                });
                productslii = productslii.ToList();
                return Ok(productslii);
                    
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("checkListName")]
        public IActionResult checkListname(string name)
        {
            var message = 0;
            try
            {
                var data = db.HomeCategory.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
                if (data != null)
                {
                    message = 1;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(message);
        }
        ///////on home page////
       
        [HttpGet]
        [Route("getLists")]
        public  IActionResult lists()
        {
            try
            {
                var data = db.HomeCategory.Where(x => x.IsActive == true).OrderBy(x=>x.OrderNumber).ToList();
                var homelist = new List<HomeCategoryProduct>();
                var homeList = new homeLists();
                var homeListsss = new List<homeLists>();

                foreach (var item in data)
                {
                    var productList = new List<product7>();

                    homeList = new homeLists();
                    homeList.Id = item.Id;
                    homeList.ListName = item.Name;
                    homeList.OrderNumber = item.OrderNumber;
                    homeList.Shape = item.Shape==null?"Circle":item.Shape;
                    homeList.IsDesktop = item.IsDesktop==null?true:item.IsDesktop;
                    var homeCatProd = db.HomeCategoryProduct.Where(x => x.HomeCategoryId == item.Id && x.IsActive==true).ToList();
                    foreach (var item2 in homeCatProd)
                    {
                        var ProductData = allProducts(item2.ProductId, item.Name, item.OrderNumber);
                        if(ProductData.Id>0)
                        productList.Add(ProductData);


                    }
                    if (productList.Count > 0)
                    {
                        homeList.products.AddRange(productList.Take(6));

                        homeListsss.Add(homeList);
                    }
                }
              

                return Ok(homeListsss);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        [Route("getMobileLists")]
        public IActionResult listsMobile()
        {
            try
            {
                var data = db.HomeCategory.Where(x => x.IsActive == true).OrderBy(x => x.OrderNumber).ToList();
                var homelist = new List<HomeCategoryProduct>();
                var homeList = new homeLists();
                var homeListsss = new List<homeLists>();

                foreach (var item in data)
                {
                    var productList = new List<product7>();

                    homeList = new homeLists();
                    homeList.Id = item.Id;
                    homeList.ListName = item.Name;
                    homeList.OrderNumber = item.OrderNumber;
                    var homeCatProd = db.HomeCategoryProduct.Where(x => x.HomeCategoryId == item.Id && x.IsActive == true).ToList();
                    foreach (var item2 in homeCatProd)
                    {
                        var ProductData = allProducts(item2.ProductId, item.Name, item.OrderNumber);
                        if (ProductData.Id > 0)
                            productList.Add(ProductData);


                    }
                    if (productList.Count > 0)
                    {
                        homeList.products.AddRange(productList.Take(4));

                        homeListsss.Add(homeList);
                    }
                }


                return Ok(homeListsss);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //for deals
        public List<product7> allProducts(List<int> variantId,int DealId)
        {
          
            var all = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Product.IsActive==true && x.Product.IsEnable==true)
                .Include(x=>x.Product.ProductImages)
                .Include(x=>x.Product.RatingReviews)
                .ToList();
            var result = new List<ProductVariantDetail>();
            foreach(var id in variantId)
            {
                var data = all.Where(x => x.Id == id).Select(X => new ProductVariantDetail()
                { Id = X.Id,
                    IsActive=X.IsActive,
                    IsDefault=X.IsDefault,
                    Product=X.Product,
                    Weight=X.Weight,
                    Discount=X.Discount,
                    CostPrice=X.CostPrice,
                    InStock=X.InStock,
                    Price=X.Price,
                    PriceAfterdiscount= X.PriceAfterdiscount,
                    ProductId=X.ProductId,
                    ProductImages=X.ProductImages.Where(x=>x.IsActive==true).ToList(),
                    ProductSKU=X.ProductSKU,
                
                }).FirstOrDefault();
                if(data!=null)
                result.Add(data);
            }


            result = DealHelper.calculateDealonetime(result, DealId,db);
            result = PriceIncrementHelper.calculatePrice(result, db);

            var list = new List<product7>();
            foreach(var r in result)
            {
                var model = new product7();
                model.InStock = r.InStock;
                model.CostPrice = r.CostPrice;
                model.Discount = r.Discount;
                model.Id = r.ProductId;
                model.SellingPrice = r.Price;
                model.VariantDetailId = r.Id;
                model.LandingVariant.InStock = r.InStock;
                model.LandingVariant.Id = r.Id;
                model.LandingVariant.Price = r.Price;
                model.LandingVariant.PriceAfterdiscount = r.PriceAfterdiscount;
                model.Name = r.Product?.Name;
                model.ShipmentVendor = r.Product?.ShipmentVendor??false;
                model.ShipmentTime = r.Product?.ShipmentTime??0;
                model.ShipmentCost = r.Product?.ShipmentCost??0;
                model.PriceAfterdiscount = r.PriceAfterdiscount;
                model.ProductCategoryId =Convert.ToInt32( r.Product?.ProductCategoryId);
                if (r.Product?.ProductImages.Where(x=>x.IsActive==true).Count() != 0)
                {
                    model.Url = r.Product?.ProductImages?.Where(x => x.IsActive == true).FirstOrDefault().ImagePath;
                    model.Url150 = r.Product?.ProductImages?.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150;
                    model.Url450 = r.Product?.ProductImages?.Where(x => x.IsActive == true).FirstOrDefault().ImagePath450x450;

                }
               var rating= all.Where(x => x.Id == r.Id).Select(x => x.Product.RatingReviews).FirstOrDefault();
                model.Rating = rating.Select(x => x.Rating).FirstOrDefault();
                model.Description = r.Product?.Description;
                list.Add(model);
            }
            return list;
        }


     

        public  product7 allProducts(int Id,string name,int orderNumber)
        {
            var eachPrd = new product7();
            product7 _prd = new product7();
            try
            {
                var data = db.Products.Where(x => x.IsActive == true && x.IsEnable == true && x.Id==Id )
                    .Include(c => c.ProductVariantDetails)
                    .Include(x=>x.RatingReviews)
                    .Include(x => x.ProductImages).OrderByDescending(x => x.Id).FirstOrDefault();
                if (data != null)
                {
                    eachPrd.listName = name;
                    eachPrd.listOrder = orderNumber;
                    eachPrd.Id = data.Id;
                    if (data.ProductImages.Count() != 0)
                    {
                        eachPrd.Url = data.ProductImages?.FirstOrDefault().ImagePath;
                        eachPrd.Url150 = data.ProductImages?.FirstOrDefault().ImagePath150x150;
                        eachPrd.Url450 = data.ProductImages?.FirstOrDefault().ImagePath450x450;

                    }
                    eachPrd.Rating = data.RatingReviews.Select(x => x.Rating).FirstOrDefault();
                    eachPrd.Description = data.Description;
                    eachPrd.Name = data.Name;
                    eachPrd.ShipmentVendor = data.ShipmentVendor ?? false;
                    eachPrd.ShipmentTime = data.ShipmentTime ?? 0;
                    eachPrd.ShipmentCost = data.ShipmentCost ?? 0;
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

                    eachPrd = DealHelper.calculateDealForProducts(eachPrd, db);
                    eachPrd = PriceIncrementHelper.calculatePriceForProducts(eachPrd, db);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return eachPrd;
        }
        [HttpGet]
        [Route("searchedlist")]
        public IActionResult getlists2(string name)
        {
            try
            {
               
                var data2 = db.HomeCategory.Where(x => x.Name.ToLower()==name.ToLower()).OrderByDescending(x=>x.Id).ToList();


                return Ok(data2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private static List<Deal> deals(PistisContext db)
        {
            var todayTime = DateTime.Now.TimeOfDay;
            var finaldDeal = new List<Deal>();
            var deals = db.Deal.Where(x => x.IsActive == true && x.Status == "open" && x.ActiveFrom.Date <= DateTime.Now.Date && x.ActiveTo.Date >= DateTime.Now.Date
            ).Include(x => x.DealProduct).ToList();
            foreach (var d in deals)
            {

                var start = (Convert.ToDateTime(d.ActiveFromTime)).TimeOfDay;
                var end = (Convert.ToDateTime(d.ActiveToTime)).TimeOfDay;
                if (start < todayTime)
                {
                    if (d.ActiveTo.Date > DateTime.Now.Date)
                        finaldDeal.Add(d);
                    else if (d.ActiveTo.Date == DateTime.Now.Date && end > todayTime)
                    {
                        finaldDeal.Add(d);
                    }
                }

            }

            return finaldDeal;
        }



    }
    public class DealLists
    {
        public DealLists()
        {
            products = new List<product7>();
        }
        public int Id { get; set; }
        public string ActiveFrom { get; set; }
        public string ActiveTo { get; set; }

        public string ListName { get; set; }
        public bool ShipmentVendor { get; set; }
        public int ShipmentTime { get; set; }
        public decimal ShipmentCost { get; set; }
        public List<product7> products { get; set; }
        //public List<ProductVariantDetail> products { get; set; }
    }
    public class listDetails
    {
        public bool? IsDesktop { get; set; }
        public string listName { get; set; }
        public int OrderNumber { get; set; }
        public bool IsActive { get; set; }
        public string Shape { get; set; }

    }
    public class homeCategoryList
    {
        public homeCategoryList()
        {
            productID = new List<int>();
        }
        public List<int> productID { get; set; }
        public int homeCategoryId { get; set; }

    }
    public class product7
    {
        public product7()
        {
            Images = new List<string>();
            Images150 = new List<string>();
            Images450 = new List<string>();
            Variant = new List<VariantsModel>();
            VariantOption = new HashSet<VariantOption>();
            ProductionSpecification = new List<ProductionSpecification>();
            ControlType = new HashSet<string>();
            ProductVariantOption = new List<ProductVariantOption>();
            LandingVariant = new ProductVariantDetailModel();
        }
        public  string listName { get; set; }
        public int listOrder { get; set; }
        public int WishListId { get; set; }
        public string ProductSpecificationHeading { get; set; }
        public string ProductSpecificationDescription { get; set; }
        public int CompareProductId { get; set; }
        public string LandingImage { get; set; }
        public int Id { get; set; }
        public int InStock { get; set; }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool ShipmentVendor { get; set; }
        public int ShipmentTime { get; set; }
        public decimal ShipmentCost { get; set; }
        public int ProductCategoryId { get; set; }
        public int UnitId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Discount { get; set; }
        public int Commission { get; set; }
        public decimal PriceAfterdiscount { get; set; }
        public string Description { get; set; }
        public string ProductSKU { get; set; }
        public string Url { get; set; }
        public string Url150 { get; set; }
        public string Url450 { get; set; }
        public float RatingAvg { get; set; }
        public int? Rating { get; set; }
        public int ReviewCount { get; set; }
        public int RatingCount { get; set; }
        public int Onestar { get; set; }
        public int Twostar { get; set; }
        public int Threestar { get; set; }
        public int Fourstar { get; set; }
        public int Fivestar { get; set; }
        public int Onestarper { get; set; }
        public int Twostarper { get; set; }
        public int Threestarper { get; set; }
        public int Fourstarper { get; set; }
        public int Fivestarper { get; set; }
        public string ParentName { get; set; }
        public int? VariantDetailId { get; set; }
        public ProductVariantDetailModel LandingVariant { get; set; }
        public HashSet<string> ControlType { get; set; }
        public List<ProductionSpecification> ProductionSpecification { get; set; }
        public List<string> Images { get; set; }
        public List<string> Images150 { get; set; }
        public List<string> Images450 { get; set; }
        public List<VariantsModel> Variant { get; set; }
        public HashSet<VariantOption> VariantOption { get; set; }
        public List<ProductVariantOption> ProductVariantOption { get; set; }
    }
    public class homeLists
    {
        public homeLists()
        {
            products = new List<product7>();
        }
        public int Id { get; set; }
        public bool? IsDesktop { get; set; }
        public string Shape { get; set; }
        public int OrderNumber { get; set; }
        public string ListName { get; set; }
        public List<product7> products{get; set; }
    }
}