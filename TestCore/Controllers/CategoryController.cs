using TestCore.Extension_Method;
using Localdb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TestCore.Helper;
using Microsoft.AspNetCore.Authorization;
using TestCore.Helper;

namespace TestCore.Controllers
{
    [Route("api/category")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class CategoryController : ControllerBase
    {
        private readonly PistisContext db;
        private readonly IHostingEnvironment environment;

        public CategoryController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getSubcategories")]
        public List<Models.ProductCategory> GetSubcategories()
        {
            var prod = db.ProductCategories.Where(x => x.IsActive == true && x.ParentId != null)
            .OrderByDescending(s => s.Id).ToList().RemoveReferences();
            return prod;
        }
        [HttpGet]
        [Route("getSubMenu")]
        public List<ProductCategory> getSubMenu(int Id)
        {
            var data = new List<ProductCategory>();
            try
            {
                if (Id > 0)
                {
                    data = db.ProductCategories.Where(x => x.ParentId == Id).ToList();

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return data;
        }
        [HttpGet]
        [Route("getCategoryName")]
        public string GetCategoryName([FromQuery]int id)
        {
            if (id == 0)
            {
                return "All Categories";
            }
            var data = db.ProductCategories.Where(x => x.IsActive == true && x.Id == id).Include(x => x.ProductCategory1).FirstOrDefault().RemoveReferences();
            if (data.ParentId == null)
                return data.Name;
            else
            {
                var res = db.ProductCategories.Where(x => x.Id == data.ParentId).FirstOrDefault().RemoveReferences();
                return res.Name;
            }

        }
        [HttpGet]
        [Route("getSubCat")]
        public string GetSubCat(int id)
        {
            var data = db.ProductCategories.Where(x => x.IsActive == true && x.Id == id).FirstOrDefault().RemoveReferences();
            return data.Name;
        }
        [HttpGet]
        [Route("getChildCategory")]
        public List<Models.ProductCategory> getChildCategory(int? Id)
        {
            var data = db.ProductCategories.Where(x => x.IsActive == true && x.ParentId == Id).ToList().RemoveReferences();
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
        //[Route("getCategory5")]
        //public IHttpActionResult getCategory5()
        //{
        // var data = db.ProductCategories.Where(x => x.IsActive == true).ToList().RemoveReferences().Take(5;
        // try
        // {
        // return Ok(data);
        // }
        // catch (Exception ex)
        // {
        // Console.Write(ex);
        // return Ok(data);

        // }
        //}
        [Route("getProducts")]
        public List<Models.ProductCategory> getProducts()
        {
            var data = db.ProductCategories.Where(x => x.IsActive == true && x.ParentId == null).ToList().RemoveReferences();
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


        [HttpPost]
        [Route("SaveMainMenu")]
        public async Task<IActionResult> SaveMainMenu([FromBody] ProductCategoryModel model)
        {
            JsonResult response = null;
            var Message = 0;
            var MenuName = new ProductCategory();
            var checkData = new ProductCategory();
            try
            {
                if (model.Name != null)
                {

                    checkData = db.ProductCategories.Where(x => x.Name.ToLower() == model.Name.ToLower() && x.IsActive == true).FirstOrDefault();
                    if (checkData == null)
                    {
                        MenuName.Name = model.Name;
                        MenuName.SpanishName = model.SpanishName;
                        MenuName.IsActive = true;
                        MenuName.IsShow = true;
                        MenuName.IsAdult = model.IsAdult;
                        if (model.ActualIcon != null && model.ActualIcon !="" )
                        {
                            var imageResponse = await S3Service.UploadObject(model.ActualIcon);
                            response = new JsonResult(new Object());
                            if (imageResponse.Success)
                            {
                                MenuName.ActualIcon = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                            }
                            else
                            {
                                return Ok(imageResponse.code);
                            }
                        }
                        if (model.Icon != null && model.Icon !="")
                        {
                            var imageResponse = await S3Service.UploadObject(model.Icon);
                            response = new JsonResult(new Object());
                            if (imageResponse.Success)
                            {
                                MenuName.Icon = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                            }
                            else
                            {
                                return Ok(imageResponse.code);
                            }
                        }
                        db.ProductCategories.Add(MenuName);
                        db.SaveChanges();
                        Message = 1;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(Message);

        }
        [HttpPost]
        [Route("uploadFile")]
        public async Task<IActionResult> UploadFile([FromBody] ProductCategoryModel model)
        {
            Models.ProductCategory category = new Models.ProductCategory();
            var Id = model.Id;
            int productId = Convert.ToInt32(Id);
            if (productId > 0)
            {
                category = db.ProductCategories.AsNoTracking().Where(x => x.Id == productId).FirstOrDefault();
            }

            category.Name = model.Name;
            category.ParentId = model.ParentId;
            category.IsActive = true;
            var response = new JsonResult(new Object());
            if (model.Icon != null)
            {
                var imageResponse = await S3Service.UploadObject(model.Icon);
                response = new JsonResult(new Object());
                if (imageResponse.Success)
                {
                    category.Icon = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                    //category.Icon150 = $"https://pistis150x150.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                   // category.Icon450 = $"https://pistis450x450.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";

                }
            }
            try
            {
                if (productId > 0)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.ProductCategories.Add(category);
                    db.SaveChanges();
                }
                //return Request.CreateResponse(HttpStatusCode.Created);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                //return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                return Ok();
            }

        }
        [HttpPost]
        [Route("updateCategory1")]
        public async Task<IActionResult> UpdateCategory1(ProductCategoryModel item)
        {
            JsonResult response = null;
            ProductCategoryModel category = new ProductCategoryModel();
            var obj = db.ProductCategories.Find(item.Id);

            if (obj != null)
            {
                obj.Name = item.Name;
                obj.SpanishName = item.SpanishName;
              //  obj.ParentId = item.ParentId;
                obj.Icon = item.Icon;
                obj.IsActive = true;
                obj.IsAdult = item.IsAdult;
                if (item.ActualIcon != null && item.ActualIcon != "")
                {
                    if (item.ActualIcon.Contains("https://pistis.s3.us-east-2.amazonaws.com"))
                    {
                    }
                    else
                    {
                        var imageResponse = await S3Service.UploadObject(item.ActualIcon);

                        response = new JsonResult(new Object());
                        if (imageResponse.Success)
                            obj.ActualIcon = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                        else
                            return Ok(imageResponse.code);
                    }
                }
                if (item.Icon != null && item.ActualIcon != "")
                {
                    if (item.Icon.Contains("https://pistis.s3.us-east-2.amazonaws.com"))
                    {
                    }
                    else
                    {
                        var imageResponse = await S3Service.UploadObject(item.Icon);

                        response = new JsonResult(new Object());
                        if (imageResponse.Success)
                            obj.Icon = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                        else
                            return Ok(imageResponse.code);
                    }
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }

            return Ok(obj);

        }
        [HttpPost]
        [Route("updateCategory")]
        public async Task<IActionResult> UpdateCategory(ProductCategoryModel item)
        {
            JsonResult response = null;
            ProductCategoryModel category = new ProductCategoryModel();
            var obj = db.ProductCategories.Find(item.Id);

            if (obj != null)
            {
                obj.Name = item.Name;
                obj.SpanishName = item.SpanishName;
                obj.ParentId = item.ParentId;
                obj.Icon = item.Icon;
                obj.IsActive = true;
                obj.IsAdult = item.IsAdult;
                if (item.ActualIcon != null && item.ActualIcon != "")
                {
                    if (item.ActualIcon.Contains("https://pistis.s3.us-east-2.amazonaws.com"))
                    {
                    }
                    else
                    {
                        var imageResponse = await S3Service.UploadObject(item.ActualIcon);

                        response = new JsonResult(new Object());
                        if (imageResponse.Success)
                            obj.ActualIcon = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                        else
                            return Ok(imageResponse.code);
                    }
                }
                if (item.Icon != null && item.ActualIcon != "")
                {
                    if (item.Icon.Contains("https://pistis.s3.us-east-2.amazonaws.com"))
                    {
                    }
                    else
                    {
                        var imageResponse = await S3Service.UploadObject(item.Icon);

                        response = new JsonResult(new Object());
                        if (imageResponse.Success)
                            obj.Icon = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                        else
                            return Ok(imageResponse.code);
                    }
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }

            return Ok(obj);

        }
        [HttpGet]
        [Route("CategoryDelete")]
        public Models.ProductCategory DeleteCategory(int id)
        {
            var obj = db.ProductCategories.Find(id);
            if (obj != null)
            {
                obj.IsActive = false;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            { }
            return obj;

        }
        [HttpGet]
        [Route("deleteProduct")]
        public IActionResult removeproduct(int Id)
        {
            var message = 0;
            try
            {
                if (Id > 0)
                {
                    var data = db.WishLists.Where(x => x.Id == Id).FirstOrDefault();
                    data.IsActive = false;
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    message = 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return Ok(message);
        }

        [Route("getAllCategory")]
        public async Task<IActionResult> getAllCategory()
        {
            var data = await db.ProductCategories.Where(x => x.IsActive == true && x.IsShow == true).ToListAsync();
            data.Select(m => new ProductCategoryModel
            {
                Icon = m.Icon,
                Id = m.Id,
                Name = m.Name,
                SpanishName=m.SpanishName,
                IsActive = m.IsActive,
                ParentId = m.ParentId
            }).ToList();
            foreach (var d in data)
            {
                if (data.Any(x => x.ParentId == d.Id))
                {
                    foreach (var child in data.Where(x => x.ParentId == d.Id))
                    {
                        if (child.ProductCategory1 != null)
                        {
                            if (child.ProductCategory1.Count > 0)
                            {
                                foreach (var item in child.ProductCategory1)
                                {
                                    item.Parent = null;
                                }
                            }
                            d.ProductCategory1.Add(child);
                        }

                    }
                }
            }
            data = data.Where(x => x.ParentId == null).ToList().RemoveReferences();
            return Ok(data);
        }
        //get menu  and menu>>submenu
        [HttpGet]
        [Route("getCategory")]
        public IActionResult getCategory()
        {
            var mainMenu = new HashSet<customModel>();
            var data = db.ProductCategories.Where(x => x.IsActive && x.ParentId == null).ToList();
            foreach (var item in data)
            {
                var model = new customModel();
                model.Name = item.Name;
                var MainMenu = item.Name;
                var spanishMenu = item.SpanishName;
                var MainId = item.Id;
                model.Id = item.Id;
                model.subId = 0;
                model.Icon = item.Icon;
                mainMenu.Add(model);
                var MenuName = db.ProductCategories.Where(x => x.ParentId == item.Id).ToList();
                if (MenuName.Count > 0)
                {
                    foreach (var seconed in MenuName)
                    {
                        var model1 = new customModel();
                        model1.Name = MainMenu + ">>" + seconed.Name;
                        
                        model1.subId = seconed.Id;
                        model1.Id = item.Id;
                        model1.Icon = seconed.Icon;
                        mainMenu.Add(model1);
                    }
                }
            }

            // var groupby = mainMenu.GroupBy(x => x.Id).ToList();
            var mainMenu5 = mainMenu.GroupBy(x => x.Id).ToHashSet();
            foreach (var item1 in mainMenu5)
            {
                foreach (var filter in item1)
                {
                    var filterdata = new customModel();
                    filterdata.Name = filter.Name;
                    filterdata.Id = filter.Id;
                    filterdata.subId = filter.subId;
                    mainMenu.Add(filterdata);


                }
            }
            try
            {
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return Ok(mainMenu);

        }

        [HttpGet]
        [Route("getCategoryinSpanish")]
        public IActionResult getCategory2()
        {
            var mainMenu = new HashSet<customModel>();
            var data = db.ProductCategories.Where(x => x.IsActive && x.ParentId == null).ToList();
            foreach (var item in data)
            {
                var model = new customModel();
                model.Name = item.SpanishName;
               // var MainMenu = item.Name;
                var spanishMenu = item.SpanishName;
                var MainId = item.Id;
                model.Id = item.Id;
                model.subId = 0;
                model.Icon = item.Icon;
                mainMenu.Add(model);
                var MenuName = db.ProductCategories.Where(x => x.ParentId == item.Id).ToList();
                if (MenuName.Count > 0)
                {
                    foreach (var seconed in MenuName)
                    {
                        var model1 = new customModel();
                        model1.Name = spanishMenu + ">>" + seconed.SpanishName;

                        model1.subId = seconed.Id;
                        model1.Id = item.Id;
                        model1.Icon = seconed.Icon;
                        mainMenu.Add(model1);
                    }
                }
            }

            // var groupby = mainMenu.GroupBy(x => x.Id).ToList();
            var mainMenu5 = mainMenu.GroupBy(x => x.Id).ToHashSet();
            foreach (var item1 in mainMenu5)
            {
                foreach (var filter in item1)
                {
                    var filterdata = new customModel();
                    filterdata.SpanishName = filter.SpanishName;
                    filterdata.Id = filter.Id;
                    filterdata.subId = filter.subId;
                    mainMenu.Add(filterdata);


                }
            }
            try
            {
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return Ok(mainMenu);

        }


        [HttpGet]
        [Route("getCategory1")]
        public IActionResult getCategory1()
        {
            var mainMenu = new HashSet<customModel>();
            var data = db.ProductCategories.Where(x => x.IsActive && x.ParentId == null).ToList();
            foreach (var item in data)
            {
                var model = new customModel();
                // model.Name = item.Name;
                var MainMenu = item.Name;
                var MainId = item.Id;
                //  model.Id = item.Id;
                //  model.subId = 0;
                // model.Icon = item.Icon;
                // mainMenu.Add(model);
                var MenuName = db.ProductCategories.Where(x => x.ParentId == item.Id).ToList();
                if (MenuName.Count > 0)
                {
                    foreach (var seconed in MenuName)
                    {
                        var model1 = new customModel();
                        model1.Name = MainMenu + ">>" + seconed.Name;
                        model1.subId = seconed.Id;
                        model1.Id = item.Id;
                        model1.Icon = seconed.Icon;
                        mainMenu.Add(model1);
                    }
                }
            }

            // var groupby = mainMenu.GroupBy(x => x.Id).ToList();
            var mainMenu5 = mainMenu.GroupBy(x => x.Id).ToHashSet();
            foreach (var item1 in mainMenu5)
            {
                foreach (var filter in item1)
                {
                    var filterdata = new customModel();
                    filterdata.Name = filter.Name;
                    filterdata.Id = filter.Id;
                    filterdata.subId = filter.subId;
                    mainMenu.Add(filterdata);


                }
            }
            try
            {
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return Ok(mainMenu);

        }




        //get menu  and menu>>submenu third level
        [HttpGet]
        [Route("getSubCategory")]
        public IActionResult getsubCategory(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);
            var mainMenu = new List<customModel>();
            var data = db.ProductCategories.Where(x => x.IsActive && x.ParentId == null).ToList();
            foreach (var item in data)
            {
                var model = new customModel();
                model.Name = item.Name;
                model.MenuName = item.Name;
                var MainMenu = item.Name;
                var MainId = item.Id;
                model.Id = item.Id;
                model.subId = 0;
                mainMenu.Add(model);
                var MenuName = db.ProductCategories.Where(x => x.ParentId == item.Id).ToList();
                if (MenuName.Count > 0)
                {
                    foreach (var seconed in MenuName)
                    {
                        var model1 = new customModel();
                        model1.Name = MainMenu + ">>" + seconed.Name;
                        model1.MenuName = item.Name;
                        model1.subId = seconed.Id;
                        model1.seconedlevel = 2;
                        model1.Id = item.Id;
                        mainMenu.Add(model1);
                        var MenuName2 = db.ProductCategories.Where(x => x.ParentId == seconed.Id).ToList();
                        if (MenuName2.Count > 0)
                        {
                            foreach (var third in seconed.ProductCategory1)
                            {
                                var model2 = new customModel();
                                model2.MenuName = item.Name;
                                model2.Id = item.Id;
                                model2.Name = MainMenu + ">>" + seconed.Name + ">>" + third.Name;
                                model2.subId = third.Id;
                                model2.thridLevel = 3;
                                mainMenu.Add(model2);
                            }
                        }
                    }
                }
            }

            int Count = 0;
            if (search != null)
            {
                mainMenu = mainMenu.Where(c => c.Name.ToLower().Contains(search.ToLower())).ToList();
                Count = mainMenu.Count;
            }
            else
                Count = mainMenu.Count;

            mainMenu = mainMenu.Skip(skipData).Take(pageSize).ToList();
            var response = new
            {
                data = mainMenu.GroupBy(x => x.Id).ToList(),
                count = Count,
            };
            return Ok(response);
        }
        //save Menu
        //[HttpGet]
        //[Route("SaveMainMenu")]
        //public int SaveMainMenu(string Name)
        //{
        //    var Message = 0;
        //    var MenuName = new ProductCategory();
        //    var checkData = new ProductCategory();
        //    try
        //    {
        //        if (Name != null)
        //        {

        //            checkData = db.ProductCategories.Where(x => x.Name.ToLower() == Name.ToLower()).FirstOrDefault();
        //            if (checkData == null)
        //            {
        //                MenuName.Name = Name;
        //                MenuName.IsActive = true;
        //                db.ProductCategories.Add(MenuName);
        //                db.SaveChanges();
        //                Message = 1;
        //            }
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return Message;

        //}
        
        
        //save sub menu
        [HttpPost]
        [Route("SaveSubMenu")]
        public int SaveSubMenu([FromBody]customModel model)
        {
            var message = 0;
            var data = new ProductCategory();

            if (model != null)
            {
                data.Name = model.Name;
                data.SpanishName = model.SpanishName;
                data.ParentId = model.Id;
                data.IsActive = true;
                data.IsShow = true;
                data.IsAdult = model.IsAdult;
                message = 1;
            }

            try
            {
                db.ProductCategories.Add(data);
                db.SaveChanges();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return message;
        }
        //delete Menu
        [HttpGet]
        [Route("DeleteMenu")]
        public int DeleteMenu(int Id)
        {
            var message = 0;
            if (Id > 0)
            {
                var obj = new ProductCategory();
                if (db.ProductCategories.Where(x => x.IsActive == true && x.Id == Id).Any())
                {
                    obj = db.ProductCategories.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault();
                    obj.IsActive = false;
                    message = 1;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                }

                if (db.ProductCategories.Where(x => x.ParentId == obj.Id).Any())
                {
                    var DeleteSubMenu = db.ProductCategories.Where(x => x.ParentId == obj.Id).ToList();
                    foreach (var item in DeleteSubMenu)
                    {
                        item.IsActive = false;

                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                        message = 1;
                    }



                }
            }
            try
            {



            }
            catch (Exception)
            {

                throw;
            }
            return message;
        }
        //edit Menu
        [HttpGet]
        [Route("editmenu")]
        public IActionResult editmenu(int Id)
        {
            var data = new ProductCategory();
            try
            {
                if (Id > 0)
                {
                    data = db.ProductCategories.Where(x => x.Id == Id).FirstOrDefault();
                    if (data.ParentId == null)
                    {

                        var custom = new customModel();
                        custom.MenuName = data.Name;
                        custom.Name = "";
                        custom.Id = data.Id;
                        custom.subId = 0;
                        return Ok(custom);
                    }
                    else
                    {
                        var parent = db.ProductCategories.Where(x => x.Id == data.ParentId).FirstOrDefault();
                        if (parent.ParentId == null)
                        {
                            var custom = new customModel();
                            custom.MenuName = parent.Name;
                            custom.Name = data.Name;
                            custom.Id = parent.Id;
                            custom.subId = data.Id;
                            return Ok(custom);
                        }
                        else
                        {
                            var parent2 = db.ProductCategories.Where(x => x.Id == parent.ParentId).FirstOrDefault();
                            if (parent2.ParentId == null)
                            {
                                var custom = new customModel();
                                custom.MenuName = parent.Name + ">>" + data.Name;

                                custom.Name = data.Name;
                                custom.Id = parent2.Id;
                                custom.subId = data.Id;
                                return Ok(custom);
                            }

                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();

        }
        [HttpGet]
        [Route("getProductDetails")]
        public async Task<IActionResult> productDetails(int Id, int variantId)
        {
            var result = new product1();
            try
            {
                result = await GetProductDetail(Id, variantId);
            }
            catch (Exception ex)
            {
                result = null;
                Console.WriteLine(ex.Message);
            }
            for (int i = 0; i < result.Variant.Count() - 1; i++)
            {
                if (result.Variant[i].Name.ToLower() == "color" || result.Variant[i].Name.ToLower() == "colour")
                {
                    var data1 = result.Variant[i];
                    result.Variant[i] = result.Variant[result.Variant.Count() - 1];
                    result.Variant[result.Variant.Count() - 1] = data1;

                }
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("getProductReview")]
        public IActionResult productDetailReview([FromBody]ProductreviewDetails model)
        {
           // int Id, int variantId, int page, int pageSize, string search
            var result = new product1();
            var skipData = model.pageSize * (model.page - 1);
            try
            {
                result = GetProductDetailreview(model.productId, model.varientId, model.pageSize, skipData,model.searchName);
               
            }
            catch (Exception ex)
            {
                result = null;
                Console.WriteLine(ex.Message);
            }
            return Ok(result);
        }
        public product1 GetProductDetailreview(int Id, int variantId, int pageSize, int skipData,int SearchName)
        {
            var RatingCount = 0;
            var ReviewCount = 0;
            var ratingTotal = 0;
            var oneStar = 0;
            var TwoStar = 0;
            var ThreeStar = 0;
            var FourStar = 0;
            var FiveStar = 0;
            var productDetail = new product1();
            var prod = new Models.Product();
            var customModel1 = new List<customModel1>();

            try
            {
                if (Id > 0)
                {
                    prod = db.Products.Where(x => x.IsActive == true && x.Id == Id)
                    .OrderByDescending(s => s.Id)
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductImages)
                    .Include(x => x.RatingReviews)
                    .Include(x => x.ProductImages)
                    .Include(x => x.ProductionSpecifications)
                    .AsNoTracking().FirstOrDefault();

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

                    var productVarientDetail = db.ProductVariantDetails.Where(x => x.Id == variantId && x.IsActive == true)
                    .Include(c => c.ProductVariantOptions).FirstOrDefault();

                    productDetail.CostPrice = Convert.ToDecimal(productVarientDetail.CostPrice);
                    productDetail.Discount = Convert.ToInt32(productVarientDetail.Discount);
                    productDetail.PriceAfterdiscount = Convert.ToDecimal(productVarientDetail.PriceAfterdiscount);
                    productDetail.SellingPrice = productVarientDetail.Price;
                    productDetail.InStock = productVarientDetail.InStock;
                    productDetail.ProductionSpecification = prod.ProductionSpecifications.Where(x => x.IsActive == true && x.VariantDetailId == variantId)
                    .Select(m => new ProductionSpecification
                    {
                        HeadingName = m.HeadingName,
                        Description = m.Description,
                    }).ToList();

                    productDetail.ProductVariantOption = productVarientDetail.ProductVariantOptions.Where(c => c.IsActive == true && c.ProductVariantDetailId == variantId)
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
                    }).ToList();

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

                    var variantData = optionQry.Distinct().ToList();
                    foreach (var item in variantData)
                    {
                        if (!allOptions.Any(c => c.Id == item.VariantModel.Id))
                            allOptions.Add(item.VariantModel);
                    }

                    //foreach (var vari in productDetail.ProductVariantOption)
                    //{
                    // var variant = db.Variants.Where(c => c.IsActive == true && c.Id == vari.VariantOption.VariantId).Include(c => c.VariantOptions).FirstOrDefault();
                    // allOptions.Add(variant);
                    //}

                    var Images = prod.ProductImages.Where(x => x.ProductVariantDetailId == productVarientDetail.Id && x.IsActive == true).ToList();
                    var img = Images.Where(c => c.IsDefault == true).FirstOrDefault();
                    if (img != null)
                    {
                        productDetail.LandingImage = img.ImagePath;
                        productDetail.Images.Add(img.ImagePath);
                        productDetail.LandingImage150 = img.ImagePath150x150;
                        productDetail.Images150.Add(img.ImagePath150x150);
                        productDetail.LandingImage450 = img.ImagePath450x450;
                        productDetail.Images450.Add(img.ImagePath450x450);
                    }
                    else
                    {
                        if (Images.Count > 0 && Images.Any(b => b.ImagePath != null))
                        {
                            productDetail.LandingImage = Images.FirstOrDefault().ImagePath;
                            productDetail.LandingImage150 = Images.FirstOrDefault().ImagePath150x150;
                            productDetail.LandingImage450 = Images.FirstOrDefault().ImagePath450x450;

                        }
                    }

                    foreach (var image in Images)
                    {
                        if (image.IsDefault == true)
                            continue;
                        productDetail.Images.Add(image.ImagePath);
                        productDetail.Images150.Add(image.ImagePath150x150);
                        productDetail.Images450.Add(image.ImagePath450x450);

                    }

                    //--------------set images limit to (6)
                    productDetail.Images = productDetail.Images.Skip(0).Take(6).ToList();

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
                }
                if (prod != null)
                {
                    var Users = db.Users.Where(x => x.RoleId == 1 && x.IsActive == true).ToList();

                    if (prod.RatingReviews != null)
                    {
                        foreach (var item in prod.RatingReviews)
                        {
                            var userRating = new UserRating();
                            userRating.Id = item.Id;
                            userRating.Username = Users.Where(x => x.Id == item.UserId).FirstOrDefault()?.FirstName??"";
                            userRating.UserReviews = item.Review;
                            userRating.UserRatings = item.Rating;
                            productDetail.UserRatings.Add(userRating);

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
                        productDetail.UserRatings = productDetail.UserRatings.Skip(skipData).Take(pageSize).ToList();
                        if(SearchName==1 ||SearchName==0)
                        productDetail.UserRatings = productDetail.UserRatings.OrderByDescending(x => x.Id).ToList();
                        if(SearchName==2)
                            productDetail.UserRatings = productDetail.UserRatings.OrderBy(x => x.UserRatings).ToList();
                        if(SearchName==3)
                            productDetail.UserRatings = productDetail.UserRatings.OrderByDescending(x => x.UserRatings).ToList();

                        productDetail.Count = productDetail.UserRatings.Count;
                    }
                    if (RatingCount > 0)
                    {
                        if (FiveStar > 0)
                            productDetail.Fivestarper = (FiveStar * 100) / RatingCount;
                        if (FourStar > 0)
                            productDetail.Fourstarper = (FourStar * 100) / RatingCount;
                        if (ThreeStar > 0)
                            productDetail.Threestarper = (ThreeStar * 100) / RatingCount;
                        if (TwoStar > 0)
                            productDetail.Twostarper = (TwoStar * 100) / RatingCount;
                        if (oneStar > 0)
                            productDetail.Onestarper = (oneStar * 100) / RatingCount;
                    }
                    if (ratingTotal > 0 && RatingCount > 0)
                        productDetail.RatingAvg = ratingTotal / RatingCount;
                    if (prod.ProductCategory.ParentId > 0)
                    {
                        var parentproduct = db.ProductCategories.Where(x => x.Id == prod.ProductCategory.ParentId).AsNoTracking().FirstOrDefault();
                        productDetail.ParentName = parentproduct.Name;
                    }
                    productDetail.ReviewCount = ReviewCount;
                    productDetail.RatingCount = RatingCount;
                    productDetail.Description = prod.Description;
                    productDetail.Id = prod.Id;
                    productDetail.Name = prod.Name;
                    productDetail.ProductCategoryId = prod.ProductCategoryId;
                    productDetail.UnitId = prod.UnitId;
                    productDetail.Onestar = oneStar;
                    productDetail.Twostar = TwoStar;
                    productDetail.Threestar = ThreeStar;
                    productDetail.Fourstar = FourStar;
                    productDetail.Fivestar = FiveStar;
                }
                productDetail.VariantDetailId = variantId;
                productDetail = DealHelper.calculateDealForProducts(productDetail, db);
                productDetail = PriceIncrementHelper.calculatePriceForProducts(productDetail, db);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productDetail;
        }
        public async Task<product1> GetProductDetail(int Id, int variantId)
        {
            var RatingCount = 0;
            var ReviewCount = 0;
            var ratingTotal = 0;
            var oneStar = 0;
            var TwoStar = 0;
            var ThreeStar = 0;
            var FourStar = 0;
            var FiveStar = 0;
            var productDetail = new product1();
            var prod = new Models.Product();
            var customModel1 = new List<customModel1>();

            try
            {
                if (Id > 0)
                {
                    prod = await db.Products.Where(x => x.IsActive == true && x.Id == Id)
                    .OrderByDescending(s => s.Id)
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductImages)
                    .Include(x => x.RatingReviews)
                    .Include(x => x.ProductImages)
                    .Include(x => x.ProductionSpecifications)
                    .AsNoTracking().FirstOrDefaultAsync();

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

                    var productVarientDetail=await db.ProductVariantDetails.Where(x => x.Id == variantId && x.IsActive == true)
                    .Include(c => c.ProductVariantOptions).FirstOrDefaultAsync();

                    productDetail.CostPrice = Convert.ToDecimal(productVarientDetail.CostPrice);
                    productDetail.Discount = Convert.ToInt32(productVarientDetail.Discount);
                    productDetail.PriceAfterdiscount = Convert.ToDecimal(productVarientDetail.PriceAfterdiscount.ToString("#.00"));
                    productDetail.SellingPrice = productVarientDetail.Price;
                    productDetail.InStock = productVarientDetail.InStock;
                    productDetail.ProductionSpecification = prod.ProductionSpecifications.Where(x => x.IsActive == true && x.VariantDetailId == variantId)
                    .Select(m => new ProductionSpecification
                    {
                        HeadingName = m.HeadingName,
                        Description = m.Description,
                    }).ToList();
                    if(productDetail.ProductionSpecification.Count()>0)
                    if (productDetail.ProductionSpecification?[0].HeadingName == "")
                        productDetail.ProductionSpecification = new List<ProductionSpecification>();
                        productDetail.ProductVariantOption = productVarientDetail.ProductVariantOptions
                                                            .Where(c => c.IsActive == true && c.ProductVariantDetailId == variantId)
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
                                                            }).ToList();

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


                    var Images = prod.ProductImages.Where(x => x.ProductVariantDetailId == productVarientDetail.Id && x.IsActive == true).ToList();
                    var img = Images.Where(c => c.IsDefault == true).FirstOrDefault();
                    if (img != null)
                    {
                        productDetail.LandingImage = img.ImagePath;
                        productDetail.LandingImage150 = img.ImagePath150x150;
                        productDetail.LandingImage450 = img.ImagePath450x450;

                        productDetail.Images.Add(img.ImagePath);
                        productDetail.Images150.Add(img.ImagePath150x150);
                        productDetail.Images450.Add(img.ImagePath450x450);
                    }
                    else
                    {
                        if (Images.Count > 0 && Images.Any(b => b.ImagePath != null))
                        {
                            productDetail.LandingImage = Images.FirstOrDefault().ImagePath;
                            productDetail.LandingImage150 = Images.FirstOrDefault().ImagePath150x150;
                            productDetail.LandingImage450 = Images.FirstOrDefault().ImagePath450x450;

                        }
                    }

                    foreach (var image in Images)
                    {
                        if (image.IsDefault == true)
                            continue;
                        productDetail.Images.Add(image.ImagePath);
                        productDetail.Images150.Add(image.ImagePath150x150);
                        productDetail.Images450.Add(image.ImagePath450x450);

                    }

                    //--------------set images limit to (6)
                    productDetail.Images150 = productDetail.Images150.Skip(0).Take(6).ToList();
                    productDetail.Images450 = productDetail.Images450.Skip(0).Take(6).ToList();
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
                }
                if (prod != null)
                {
                    var Users = await db.Users.Where(x =>  x.IsActive == true).ToListAsync();

                    if (prod.RatingReviews != null)
                    {
                        foreach (var item in prod.RatingReviews)
                        {
                            var userRating = new UserRating();
                            var name= Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                            userRating.Username = name.FirstName;
                            userRating.UserReviews = item.Review;
                            userRating.UserRatings = item.Rating;
                            productDetail.UserRatings.Add(userRating);
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
                    }
                    if (RatingCount > 0)
                    {
                        if (FiveStar > 0)
                            productDetail.Fivestarper = (FiveStar * 100) / RatingCount;
                        if (FourStar > 0)
                            productDetail.Fourstarper = (FourStar * 100) / RatingCount;
                        if (ThreeStar > 0)
                            productDetail.Threestarper = (ThreeStar * 100) / RatingCount;
                        if (TwoStar > 0)
                            productDetail.Twostarper = (TwoStar * 100) / RatingCount;
                        if (oneStar > 0)
                            productDetail.Onestarper = (oneStar * 100) / RatingCount;
                    }
                    if (ratingTotal > 0 && RatingCount > 0)
                        productDetail.RatingAvg = ratingTotal / RatingCount;
                    if (prod.ProductCategory.ParentId > 0)
                    {
                        var parentproduct = await db.ProductCategories.Where(x => x.Id == prod.ProductCategory.ParentId).AsNoTracking().FirstOrDefaultAsync();
                        productDetail.ParentName = parentproduct.Name;
                    }
                    productDetail.ReviewCount = ReviewCount;
                    productDetail.RatingCount = RatingCount;
                    productDetail.Description = prod.Description;
                    productDetail.Id = prod.Id;
                    productDetail.Name = prod.Name;
                    productDetail.ShipmentVendor = prod.ShipmentVendor ?? false;
                    productDetail.ShipmentTime = prod.ShipmentTime ?? 0;
                    productDetail.ShipmentCost = prod.ShipmentCost ?? 0;
                    productDetail.ProductCategoryId = prod.ProductCategoryId;
                    productDetail.UnitId = prod.UnitId;
                    productDetail.Onestar = oneStar;
                    productDetail.Twostar = TwoStar;
                    productDetail.Threestar = ThreeStar;
                    productDetail.Fourstar = FourStar;
                    productDetail.Fivestar = FiveStar;
                }
                productDetail.VariantDetailId = variantId;
                productDetail = DealHelper.calculateDealForProducts(productDetail,db);
                productDetail = PriceIncrementHelper.calculatePriceForProducts(productDetail, db);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                return productDetail;
        }

        
        [Route("getImages")]
        [HttpGet]
        public async Task<product1> getVariantImage(int Id, int productId)
        {
            var productDeatil = db.ProductVariantDetails.Where(c => c.IsActive == true && c.ProductId == productId).Include(c => c.ProductVariantOptions).AsNoTracking().ToList();
            var detail = new Models.ProductVariantDetail();
            foreach (var item in productDeatil)
            {
                if (item.ProductVariantOptions.Any(c => c.VariantOptionId == Id && c.IsActive == true))
                {
                    detail = item;
                    break;
                }
            }
            var options = new List<Models.ProductVariantOption>();
            if (detail != null)
            {
                options = db.ProductVariantOptions.Where(c => c.IsActive == true && c.ProductVariantDetailId == detail.Id).ToList();
            }

            var data = await GetProductDetail(detail.ProductId, detail.Id);
            data.VariantDetailId = detail.Id;
            return data;
        }
        [Route("filterVariantDetails")]
        [HttpGet]
        public async Task<IActionResult> filterVariantDetails(int productID, string optionId)
        {
            var optionsFilter = optionId.Split(',');

            var productDeatil = db.ProductVariantDetails.Where(c => c.IsActive == true && c.ProductId == productID).Include(c => c.ProductVariantOptions).AsNoTracking().ToList();
            var detail = new Models.ProductVariantDetail();

            int count = 0;
            foreach (var item in productDeatil)
            {
                count = 0;
                foreach (var item1 in optionsFilter)
                {
                    if (item.ProductVariantOptions.Any(c => c.VariantOptionId == Convert.ToInt32(item1) && c.IsActive == true))
                    {
                        detail = item;
                        count++;
                        if (count == optionsFilter.Length)
                            break;
                    }
                    if (count == optionsFilter.Length)
                        break;
                }
                if (count == optionsFilter.Length)
                    break;
            }
            var data = new product1();
            if (count == optionsFilter.Length)
            {
                var options = new List<Models.ProductVariantOption>();
                if (detail != null)
                    options = db.ProductVariantOptions.Where(c => c.IsActive == true && c.ProductVariantDetailId == detail.Id).ToList();
                data = await GetProductDetail(detail.ProductId, detail.Id);
                data.VariantDetailId = detail.Id;
            }
            else
                data = null;
                for (int i = 0; i < data.Variant.Count()-1; i++)
                {
                    if(data.Variant[i].Name.ToLower()=="color"||data.Variant[i].Name.ToLower()=="colour")
                    {
                        var data1 = data.Variant[i];
                        data.Variant[i] = data.Variant[data.Variant.Count() - 1];
                        data.Variant[data.Variant.Count() - 1] = data1;

                    }
                }
            
                       return Ok(data);
        }
        //new
        [HttpGet]
        [Route("getParentCategory")]
        public ProductCategory GetParentCategory([FromQuery]int id)
        {

            var data = db.ProductCategories.Where(x => x.IsActive == true && x.Id == id).Include(x => x.ProductCategory1).FirstOrDefault().RemoveReferences();
            if (data.ParentId == null)
                return data;
            else
            {
                var res = db.ProductCategories.Where(x => x.Id == data.ParentId && x.IsActive == true).FirstOrDefault().RemoveReferences();
                if (res.ParentId == null)
                    return res;
                else
                {
                    var res1 = db.ProductCategories.Where(x => x.Id == res.ParentId && x.IsActive == true).FirstOrDefault().RemoveReferences();
                    return res1;
                }

            }
        }
        [HttpGet]
        [Route("getHierarchy")]
        public string GetHierarchy(int id)
        {
            var result = "";
            var lastchild = db.ProductCategories.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
            if (lastchild != null)
            {
                var child = db.ProductCategories.Where(x => x.Id == lastchild.ParentId && x.IsActive == true).FirstOrDefault();
                if (child?.ParentId != null)
                {
                    var parent = db.ProductCategories.Where(x => x.Id == child.ParentId && x.IsActive == true).FirstOrDefault();
                    result = parent?.Name + ">>" + child?.Name + ">>" + lastchild?.Name;
                }
                else
                    result = child?.Name + ">>" + lastchild?.Name;
            }
            else
                result = lastchild?.Name;
            return result;
        }
        [HttpGet]
        [Route("AddWishListProducts")]
   //   [Authorize(Roles = "Admin,Customer")]
        public IActionResult WishListproductDetails(int variantId, int? UserId, string IpAddress)
        {


            var varientsData = new List<Models.ProductVariantDetail>();
            var ProductVariantOption = new List<Models.ProductVariantOption>();
            var varientsOptions = new List<Models.VariantOption>();
            var varients = new List<Models.Variant>();
            var productDetail = new product1();
            var productDetaillist = new List<product1>();
            var prod = new Models.Product();
            var customModel1 = new List<customModel1>();
            var customModel2 = new customModel1();
            var wishlistDataCheck = new WishList();
            var ProductVariantDetails = new List<ProductVariantDetail>();
            try
            {
                //if (variantId > 0)
                //{
                // if (!string.IsNullOrEmpty(IpAddress))
                // wishlistDataCheck.IpAddress = IpAddress;
                // wishlistDataCheck.ProductVariantDetailId = variantId;
                // if (UserId > 0)
                // wishlistDataCheck.UserId = UserId;
                // wishlistDataCheck.IsActive = true;
                // db.WishLists.Add(wishlistDataCheck);
                // db.SaveChanges();

                //}
                var wishlistData = new List<WishList>();
                if (UserId > 0)
                {
                     wishlistData = db.WishLists.Where(x => (x.IsActive == true) && (x.UserId == UserId))
                    .Include(X => X.ProductVariantDetail)
                    .Include(X => X.ProductVariantDetail.Product)
                    .Include(X => X.ProductVariantDetail.Product.ProductImages)
                    .Include(x => x.ProductVariantDetail.Product.RatingReviews)
                    .ToList();
                }
                else
                {
                    wishlistData = db.WishLists.Where(x => (x.IsActive == true) && (x.IpAddress==IpAddress))
                    .Include(X => X.ProductVariantDetail.Product)
                    .Include(X => X.ProductVariantDetail.Product.ProductImages)
                    .Include(x => x.ProductVariantDetail.Product.RatingReviews)
                    .ToList();
                }
                    foreach (var item in wishlistData)
                    {
                        productDetail = new product1();
                        productDetail.WishListId = item.Id;
                        productDetail.ProductId = item.ProductVariantDetail.ProductId;
                        productDetail.VariantDetailId = item.ProductVariantDetailId;
                        productDetail.Name = item.ProductVariantDetail.Product.Name;
                    productDetail.ShipmentCost = item.ProductVariantDetail.Product.ShipmentCost??0;
                    productDetail.ShipmentTime = item.ProductVariantDetail.Product.ShipmentTime??0;
                    productDetail.ShipmentVendor = item.ProductVariantDetail.Product.ShipmentVendor??false;
                    productDetail.SellingPrice = item.ProductVariantDetail.Price;
                        productDetail.InStock = item.ProductVariantDetail.InStock;

                        productDetail.Discount = item.ProductVariantDetail.Discount;

                        productDetail.PriceAfterdiscount = item.ProductVariantDetail.PriceAfterdiscount;
                        if (item.ProductVariantDetail.Product.ProductImages.Any(c => c.IsActive==true && c.IsDefault == true))
                        {
                            productDetail.LandingImage = item.ProductVariantDetail.Product.ProductImages.Where(c => c.IsDefault == true && c.IsActive == true).FirstOrDefault().ImagePath;
                            productDetail.LandingImage150 = item.ProductVariantDetail.Product.ProductImages.Where(c => c.IsDefault == true && c.IsActive == true).FirstOrDefault().ImagePath150x150;
                            productDetail.LandingImage450 = item.ProductVariantDetail.Product.ProductImages.Where(c => c.IsDefault == true && c.IsActive == true).FirstOrDefault().ImagePath450x450;
                        }
                        else if(item.ProductVariantDetail.Product.ProductImages.Any(c => c.IsActive == true))
                        {
                            productDetail.LandingImage = item.ProductVariantDetail.Product.ProductImages.Where(c => c.IsActive == true).FirstOrDefault().ImagePath;
                            productDetail.LandingImage150 = item.ProductVariantDetail.Product.ProductImages.Where(c => c.IsActive == true).FirstOrDefault().ImagePath150x150;
                            productDetail.LandingImage450 = item.ProductVariantDetail.Product.ProductImages.Where(c => c.IsActive == true).FirstOrDefault().ImagePath450x450;
                        }
                        var reviewCount = item.ProductVariantDetail.Product.RatingReviews.Where(X => X.Review != null).Count();
                        var ratingCount = item.ProductVariantDetail.Product.RatingReviews.Where(x => x.Rating > 0).Count();
                        var ratingSum = item.ProductVariantDetail.Product.RatingReviews.Where(x => x.Rating > 0).Sum(x => x.Rating);
                        productDetail.ReviewCount = reviewCount;
                        if (ratingCount > 0 && ratingSum > 0)
                        {
                            var ratingAvg = Convert.ToSingle(ratingSum) / Convert.ToSingle(ratingCount);
                            productDetail.RatingAvg = ratingAvg;
                        }

                        if (productDetail.ReviewCount > 0 && productDetail.RatingAvg > 0)
                        {
                            productDetail.ReviewCount = 0;
                            productDetail.RatingAvg = 0;
                        }
                        if (productDetail.LandingVariant==null)
                    productDetail.LandingVariant = new ProductVariantDetailModel();

                    productDetail.LandingVariant.Id =Convert.ToInt32(productDetail.VariantDetailId);
                        productDetaillist.Add(productDetail);

                    }
                productDetaillist= DealHelper.calculateDealForProductsList(productDetaillist,db);
                productDetaillist = PriceIncrementHelper.calculatePriceForProductsList(productDetaillist, db);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok(productDetaillist);
        }

        [HttpGet]
        [Route("checkWishList")]
        public int ProductCheck(int VarientId, int? UserId, string IpAddress)
        {
            var message = 0;
            try
            {
                var WishList = new WishList();
                if (VarientId > 0 || UserId > 0)
                {
                    WishList = db.WishLists.Where(x => x.ProductVariantDetailId == VarientId && x.IsActive == true &&  x.UserId == UserId).FirstOrDefault();
                   
                    if (WishList != null)
                        message = 1;
                }
                else
                {
                    WishList = db.WishLists.Where(x => x.ProductVariantDetailId == VarientId && x.IsActive == true && x.IpAddress==IpAddress).FirstOrDefault();

                    if (WishList != null)
                        message = 1;
                }
            }
            catch (Exception ex)
            {

            }
            return message;
        }

        [HttpGet]
        [Route("getByParentId")]
        public IActionResult getByParentId(int id)
        {
            var responseData = db.ProductCategories.Where(y => y.ParentId == id)
            .SelectMany(x => x.ProductCategory1).AsNoTracking().OrderBy(v => v.Name).ToList();
            return Ok(responseData);
        }

        [HttpGet]
        [Route("deleteWishProduct")]
        public int WishProductDelete(int VarientId, int? UserId, string IpAddress)
        {
            var message = 0;
            try
            {
                if (VarientId > 0 || UserId > 0)
                {
                    var data = db.WishLists.Where(x => x.ProductVariantDetailId == VarientId && x.IsActive == true && (x.IpAddress == IpAddress || x.UserId == UserId)).ToList();
                    if (data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            item.IsActive = false;
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        message = 1;
                    }
                    //if (data != null)
                    //{
                    //    data.IsActive = false;
                    //    db.Entry(data).State = EntityState.Modified;
                    //    db.SaveChanges();
                    //    message = 1;
                    //}
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return message;
        }

        [HttpGet]
        [Route("get-category/all")]
        public List<Models.ProductCategory> Ge()
        {
            var prod = db.ProductCategories.Where(x => x.IsActive == true && x.ParentId == null)
            .OrderByDescending(s => s.Id).ToList().RemoveReferences();
            return prod;
        }
        [HttpGet]
        [Route("AddWishListProduct")]
        public int WishListproductDetail(int variantId, int? UserId, string IpAddress)
        {
            var message = 0;
            var check = new WishList();
            try
            {
                if (variantId > 0)
                {

                    var wishlistDataCheck = new WishList();
                    if (UserId > 0 )
                    {
                        check = db.WishLists.Where(x => x.ProductVariantDetailId == variantId && x.IsActive == true && x.UserId == UserId ).FirstOrDefault();
                    }
                    else
                    {
                        check = db.WishLists.Where(x => x.ProductVariantDetailId == variantId && x.IsActive == true &&  x.IpAddress == IpAddress).FirstOrDefault();

                    }

                    if (check == null)
                    {
                        if (!string.IsNullOrEmpty(IpAddress))
                            wishlistDataCheck.IpAddress = IpAddress;
                        wishlistDataCheck.ProductVariantDetailId = variantId;
                        if (UserId > 0)
                            wishlistDataCheck.UserId = UserId;
                        wishlistDataCheck.IsActive = true;
                        db.WishLists.Add(wishlistDataCheck);
                        db.SaveChanges();
                        message = 1;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return message;
        }
        [HttpGet]
        [Route("getParentValue")]
        public IActionResult getParent1(int SubId)
        {
            var menuDetailList = new MenuListDetails();

            try
            {
                if (SubId > 0)
                {
                    //var FirstLevlList = db.ProductCategories.Where(x => x.ParentId == null).ToList();
                    var firstLevel = new ProductCategory();
                    var ThirdLevel = db.ProductCategories.Where(x => x.Id == SubId).FirstOrDefault();
                    var seconedLevel = db.ProductCategories.Where(x => x.Id == ThirdLevel.ParentId).FirstOrDefault();
                    if (seconedLevel.ParentId > 0)
                    {
                        firstLevel = db.ProductCategories.Where(x => x.Id == seconedLevel.ParentId).FirstOrDefault();
                    }
                    menuDetailList.ThridLevelName = ThirdLevel.Name;
                    menuDetailList.ThridLevelSpanishName = ThirdLevel.SpanishName;

                    menuDetailList.SeconedLevelName = seconedLevel.Name;
                    menuDetailList.SeconedLevelSpanishName = seconedLevel.SpanishName;

                    menuDetailList.FirstLevelName = firstLevel.Name;
                    menuDetailList.FirstLevelSpanishName = firstLevel.SpanishName;

                    menuDetailList.FirstLevelId = firstLevel.Id;
                    menuDetailList.SeconedLevelId = seconedLevel.Id;
                    menuDetailList.ThridLevelId = ThirdLevel.Id;
                    menuDetailList.Image = firstLevel.Icon;
                    menuDetailList.SeconedLevelparentId = menuDetailList.FirstLevelId;
                    menuDetailList.ThridLevelparentId = menuDetailList.SeconedLevelId;
                    //foreach (var item in FirstLevlList)
                    //{
                    //    menuDetailList.FirstLevlList.Add(item);
                    //}
                }
                return Ok(menuDetailList);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("parentList")]
        public IActionResult parentList()
        {
            try
            {
                var FirstLevlList = db.ProductCategories.Where(x => x.ParentId == null).ToList();
                return Ok(FirstLevlList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [Route("saveMenuListdetails")]
        public IActionResult saveDeails(MenuListDetails formData)
        {
            var message = 0;
            try
            {
                if (formData.FirstLevelId > 0)
                {
                    var firstlevel = db.ProductCategories.Where(x => x.Id == formData.FirstLevelId).FirstOrDefault();
                    if(formData.FirstLevelName!="" || formData.FirstLevelName!=null)
                    firstlevel.Name = formData.FirstLevelName;
                    if (formData.FirstLevelSpanishName != "" || formData.FirstLevelSpanishName != null)
                        firstlevel.SpanishName = formData.FirstLevelSpanishName;
                    if (formData.Image!="" || formData.Image!=null)
                    firstlevel.Icon = formData.Image;
                    db.SaveChanges();
                    message = 1;
                }
                if (formData.SeconedLevelId > 0)
                {
                    var seconedlevel = db.ProductCategories.Where(x => x.Id == formData.SeconedLevelId).FirstOrDefault();
                    if (formData.SeconedLevelName != "" || formData.SeconedLevelName != null)
                        seconedlevel.Name = formData.SeconedLevelName;
                    if (formData.SeconedLevelSpanishName != "" || formData.SeconedLevelSpanishName != null)
                        seconedlevel.SpanishName = formData.SeconedLevelSpanishName;
                    if (formData.SeconedLevelparentId > 0)
                        seconedlevel.ParentId = formData.SeconedLevelparentId;
                    db.SaveChanges();
                    message = 1;
                }
                if (formData.ThridLevelId > 0)
                {
                    var thirdLevel = db.ProductCategories.Where(x => x.Id == formData.ThridLevelId).FirstOrDefault();
                    if (formData.ThridLevelName != "" || formData.ThridLevelName != null)
                        thirdLevel.Name = formData.ThridLevelName;
                    if (formData.ThridLevelSpanishName != "" || formData.ThridLevelSpanishName != null)
                        thirdLevel.SpanishName = formData.ThridLevelSpanishName;
                    if (formData.ThridLevelparentId > 0)
                        thirdLevel.ParentId = formData.ThridLevelparentId;
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
        [Route("getProductCategory")]
        public IActionResult getProductCategory(int Id)
        {
            try
            {
                var data = db.ProductCategories.Where(x => x.Id == Id).FirstOrDefault();
                if (data.IsAdult == null)
                    data.IsAdult = false;
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getIconCategory")]
        public async Task<IActionResult> getIconCategory()
        {
            var data = await db.ProductCategories.Where(x => x.IsActive == true && x.IsShow == true && x.IsShow && x.ParentId==null).ToListAsync();
            var catList = new List<ProductCategoryModel>();
            foreach(var d in data)
            {
                var cat = new ProductCategoryModel();
                cat.Id = d.Id;
                cat.Name = d.Name;
                cat.SpanishName = d.SpanishName;
                cat.ActualIcon = d.ActualIcon;
                catList.Add(cat);
            }
            return Ok(catList);
        }
        [HttpGet]
        [Route("DeleteMenu1")]
        public IActionResult deleteMenu(int Id)
        {
            try
            {
                var cat = db.ProductCategories.Where(x => x.Id == Id).FirstOrDefault();
                if (cat != null)
                {
                    if (cat.ParentId == null)
                    {
                        cat.IsActive = false;
                        cat.IsShow = false;
                        var child = db.ProductCategories.Where(x => x.ParentId == Id).ToList();
                        foreach (var item in child)
                        {
                            var child1 = db.ProductCategories.Where(x => x.ParentId == item.Id).ToList();
                            if (child1 != null) { 
                            foreach (var item1 in child1)
                            {
                                    item1.ParentId = 0;
                                    item1.IsActive = false;
                                    item1.IsShow = false;
                                    db.SaveChanges();
                                }


                            }


                            item.ParentId = 0;
                            item.IsActive = false;
                            item.IsShow = false;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        cat.ParentId =0;
                        cat.IsActive = false;
                        cat.IsShow = false;
                    }
                }
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
    public class menuName
    {
        public int Id { get; set; }
        public string engName{ get; set; }
        public string spanishName{ get; set; }
    }
    public class MenuListDetails
    {
        //public MenuListDetails()
        //{
        //    FirstLevlList = new List<ProductCategory>();
        //  //  SeconedLevlList = new List<ProductCategory>();
       // }

        public string FirstLevelName { get; set; }
        public string FirstLevelSpanishName { get; set; }
        public int FirstLevelId { get; set; }
        public string SeconedLevelName { get; set; }
        public string SeconedLevelSpanishName { get; set; }
        public int SeconedLevelId { get; set; }
        public int SeconedLevelparentId { get; set; }
        public int ThridLevelparentId { get; set; }

        public string ThridLevelName { get; set; }
        public string ThridLevelSpanishName { get; set; }
        public int ThridLevelId { get; set; }

     //   public List<ProductCategory> FirstLevlList { get; set; }
      //  public List<ProductCategory> SeconedLevlList { get; set; }
        public string   Image { get; set; }
       
    }
    public class customModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SpanishName { get; set; }
        public bool? IsAdult { get; set; }
        public int subId { get; set; }
        public string MenuName { get; set; }
        public string Icon { get; internal set; }
        public int thridLevel { get; set; }
        public int seconedlevel { get; set; }
    }
    public class ProductreviewDetails{
        public int productId { get; set; }
        public int varientId { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int searchName { get; set; }

    }
public class UserRating
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserReviews { get; set; }
        public int UserRatings { get; set; }
    }
    public class product1
    {
        public product1()
        {
            Images = new List<string>();
            Images150 = new List<string>();
            Images450 = new List<string>();

            Variant = new List<VariantsModel>();
            VariantOption = new HashSet<VariantOption>();
            ProductionSpecification = new List<ProductionSpecification>();
            ControlType = new HashSet<string>();
            ProductVariantOption = new List<ProductVariantOption>();
            UserRatings= new List<UserRating>();
        }
        public int Count { get; set; }
        public int WishListId { get; set; }
        public string ProductSpecificationHeading { get; set; }
        public string ProductSpecificationDescription { get; set; }
        public int CompareProductId { get; set; }
        public string LandingImage { get; set; }
        public string LandingImage150 { get; set; }
        public string LandingImage450 { get; set; }

        public int Id { get; set; }
        public int InStock { get; set; }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool ShipmentVendor { get; set; }
        public int? ShipmentTime { get; set; }
        public decimal? ShipmentCost { get; set; }
        public int ProductCategoryId { get; set; }
        public int UnitId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Discount { get; set; }
        public int Commission { get; set; }
        public decimal PriceAfterdiscount { get; set; }
        public DateTime?  ActiveTo { get; set; }
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
      public List<UserRating> UserRatings { get; set; }
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
    public class customModel1
    {
        public customModel1()
        {
            CategoryVarientId = new HashSet<int>();
            controlTypes = new List<string>();
        }
        public Variant VariantModel { get; set; }
        public VariantOption VariantOption { get; set; }
        public int ControlTypeId { get; set; }
        //public string VarientName { get; set; }
        public HashSet<int> CategoryVarientId { get; set; }
        public List<string> controlTypes { get; set; }
    }
}