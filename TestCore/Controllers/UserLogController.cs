using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCore.Helper;
using Localdb;
using Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using TestCore.Extension_Method;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq.Extensions;

using System.Xml.Linq;
using System.Net;
using System.Text;
using System.IO;

namespace TestCore.Controllers
{
    [EnableCors("EnableCORS")]
    [Route("api/UserLog")]
    [ApiController]
    public class UserLogController : ControllerBase
    {
        private readonly PistisContext db;

        public UserLogController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("Enable")]
        public IActionResult enable()
        {
            try
            {
                var enable = db.LogControl.FirstOrDefault();
                if (enable.IsEnable == true)
                {
                    enable.IsEnable = false;
                }
                else
                {
                    enable.IsEnable = true;
                }
                db.SaveChanges();
                return Ok(enable.IsEnable);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetEnable")]
        public IActionResult enable1()
        {
            try
            {
                var enable = db.LogControl.FirstOrDefault();
                return Ok(enable.IsEnable);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("SaveUserLog")]
        public IActionResult userLog([FromBody]userlogg data)
        {
            UserLog userLog = new UserLog();
            if (data != null)
            {
                if (data.IPAddress != "undefined" && data.IPAddress != null)
                    userLog.IPAddress = data.IPAddress.Replace('"', ' ').Trim();
                userLog.UserId = data.UserId;
                userLog.LogInDate = data.LogInDate;
                userLog.LogOutDate = data.LogOutDate;
                userLog.PageId = data.PageId;
                userLog.Url = data.Url;
                userLog.ActionId = data.ActionId;
                userLog.ProductId = data.ProductId == 0 ? null : data.ProductId;
                userLog.IsActive = true;
            }
            try
            {
                db.UserLogs.Add(userLog);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Ok();
        }
        [HttpPost]
        [Route("TrackUser")]
        public IActionResult TrackUser([FromBody]Log data)
        {
            string country="";
            Log userLog = new Log();
            //string ApiKey = "5d3d0cdbc95df34b9db4a7b4fb754e738bce4ac914ca8909ace8d3ece39cee3b";
            //string Url = "http://api.ipinfodb.com/v3/ip-country/?key=" + ApiKey + "&ip=" + data.IpAddress;
            //WebRequest request = WebRequest.Create(Url);
            //try
            //{

            //    using (var response = (HttpWebResponse)request.GetResponse())
            //    {
            //        // We try to use the "correct" charset
            //        Encoding encoding = response.CharacterSet != null ? Encoding.GetEncoding(response.CharacterSet) : null;

            //        using (var sr = encoding != null ? new StreamReader(response.GetResponseStream(), encoding) :
            //                                           new StreamReader(response.GetResponseStream(), true))
            //        {
            //            var response2 = sr.ReadToEnd();
            //            var parts = response2.Split(';');

            //            if (parts.Length != 5)
            //            {
            //                throw new Exception();
            //            }
            //             country = parts[4];
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    var a= ex;
            //}
            //if (data.Country == null || data.Country == "")
            //    if(country!=null ||country!="")
                    userLog.Country = "Mexico";

                if (data != null)
            {
                if (data.AppVersion != "undefined" || data.AppVersion != null)
                    userLog.AppVersion = data.AppVersion;
                userLog.IsDesktop = data.IsDesktop;
                if (data.IpAddress != "undefined")
                    userLog.IpAddress = data.IpAddress.Replace('"', ' ').Trim();
                userLog.UserId = data.UserId ?? 0;
                userLog.IsActive = true;
                if (data.Result.Contains("Error"))
                    userLog.LogtypeId = 2;
                else if (data.Result.Contains("Request"))
                    userLog.LogtypeId = 3;
                else
                    userLog.LogtypeId = 1;

                userLog.ActionDateTime = DateTime.Now;
                userLog.Action = data.Action;
                userLog.Description = data.Description;
                if (data.RequestUrl == "/")
                    data.RequestUrl = "home page";

                userLog.RequestUrl = data.RequestUrl;
                if (data.PageUrl == "/")
                    data.PageUrl = "home page";
                userLog.PageUrl = data.PageUrl;
                userLog.Result = data.Result.Replace(@"\", "");
                userLog.Guid = data.Guid;

            }
            try
            {
                db.Log.Add(userLog);
                db.SaveChanges();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Ok();
        }
        
        [HttpGet]
        [Route("last20MinUsers")]
        public IActionResult users(int page, int pageSize, string search)
        {
            var data = new List<UserLog>();
            var filterTime = db.UserLogs.Where(x => x.IsActive == true).LastOrDefault();
            var logoutDate = Convert.ToDateTime(filterTime.LogOutDate);
            var skipData = pageSize * (page - 1);
            try
            {
                if (filterTime != null)
                {
                    var checkTime = logoutDate.AddMinutes(-20);
                    if (search == null)
                        data = db.UserLogs.Where(x => x.LogOutDate >= checkTime).Include(x => x.User).OrderByDescending(x => x.Id).ToList().RemoveReferences();
                    else
                        data = db.UserLogs.Where(x => x.LogOutDate >= checkTime && x.User.Email.Contains(search)).Include(x => x.User).OrderByDescending(x => x.Id).ToList().RemoveReferences();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            var response = new
            {

                data = data.Skip(skipData).Take(pageSize).ToList(),
                count = data.Count
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("getuser")]
        public IActionResult getUser(int Id, int page, int pageSize, string search)
        {
            var list = new List<UserLog>();
            var skipData = pageSize * (page - 1);
            try
            {
                var data = db.UserLogs.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefault();

                if (search != null)
                    list = db.UserLogs.Where(x => x.Product.Name.Contains(search) && x.UserId == data.UserId || x.IPAddress == data.IPAddress).OrderByDescending(x => x.Id).Include(x => x.User).ToList().RemoveReferences();
                else
                    list = db.UserLogs.Where(x => x.UserId == data.UserId || x.IPAddress == data.IPAddress).Include(x => x.User).OrderByDescending(x => x.Id).ToList().RemoveReferences();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            var response = new
            {
                data = list.Skip(skipData).Take(pageSize).ToList(),
                count = list.Count
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult Allusers(int page, int pageSize, string search)
        {
            var data = new List<UserLog>();
            var skipData = pageSize * (page - 1);
            try
            {
                if (search == null)
                    data = db.UserLogs.Include(x => x.User).OrderByDescending(x => x.Id).ToList().RemoveReferences();
                else
                    data = db.UserLogs.Where(x => x.User.Email.Contains(search)).Include(x => x.User).OrderByDescending(x => x.Id).ToList().RemoveReferences();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            var response = new
            {
                data = data.Skip(skipData).Take(pageSize).ToList(),
                count = data.Count
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("getRecentlyViewed")]
        public IActionResult getRecentlyViewed(int UserId, string IpAddress)
        {
            var data = new List<UserLog>();

            if (UserId > 0)
            {
                data = db.UserLogs.Where(x => x.UserId == UserId && x.IsActive == true && x.Url.Contains("product-details") && x.Product.IsActive == true).Include(x => x.Product).OrderByDescending(x => x.Id).ToList();
            }
            else
                data = db.UserLogs.Where(x => x.IPAddress == IpAddress && x.IsActive == true && x.Url.Contains("product-details") && x.Product.IsActive == true).Include(x => x.Product).OrderByDescending(x => x.Id).ToList();
            var data1 = new List<UserLog>();
            var productIds = data.Select(x => x.ProductId).Distinct().Take(7).ToList();
            foreach (var i in productIds)
            {
                var model = new UserLog();
                model = data.Where(x => x.ProductId == i).FirstOrDefault();
                data1.Add(model);
            }

            var final = new List<ProductVariantDetail>();
            var all = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Product.IsActive == true).Include(x => x.Product).Include(x => x.Product.ProductImages).ToList();
            //var list1 = new List<ProductVariantDetail>();
            //foreach (var item in all)
            //{
            //    if( item.Product.IsActive == true)
            //    {
            //        var product = item;
            //        list1.Add(product);
            //    }

            //}
            //all = new List<ProductVariantDetail>();
            //all.AddRange(list1);

            foreach (var d in data1)
            {
                var variant = d.Url.Split('&')[1];
                var VariantId = Convert.ToInt32(variant.Split('=')[1]);
                var pro = new ProductVariantDetail();
                var vari = new ProductVariantDetail();
                if (VariantId > 0)
                {
                    vari = all.Where(x => x.Id == VariantId).FirstOrDefault();
                }
                if (vari != null)
                    final.Add(vari);
            }
            final = DealHelper.calculateDeal(final, db);
            final = PriceIncrementHelper.calculatePrice(final, db);
            var list = new List<ProductCatalogue>();
            foreach (var vari in final)
            {
                var pro = new ProductCatalogue();
                pro.CostPrice = vari.CostPrice;
                pro.Description = vari.Product.Description;
                pro.Discount = vari.Discount;
                pro.Id = vari.Product.Id;
                if (vari.Product?.ProductImages != null)
                    if (vari.Product?.ProductImages.Where(x => x.IsActive == true).ToList().Count > 0)
                    {
                        pro.Image = vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? vari.Product?.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath : vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath;
                        pro.Image150 = vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? vari.Product?.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
                        pro.Image450 = vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? vari.Product?.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath450x450 : vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath450x450;

                    }
                pro.InStock = vari.InStock;
                pro.Name = vari.Product.Name;
                pro.PriceAfterDiscount = vari.PriceAfterdiscount;
                pro.ProductCategoryId = vari.Product.ProductCategoryId;
                pro.SellingPrice = vari.Price;
                pro.VariantDetailId = vari.Id;
                pro.VendorId = vari.Product.VendorId;
                pro.ShipmentVendor = vari.Product.ShipmentVendor??false;
                list.Add(pro);

            }
            if (list.Count() >= 2)
                if (list[0].Id == list[1].Id)
                    list.RemoveAt(0);
            if (list.Count() >= 7)
                list = list.Take(6).ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("getRecentlyViewedMobile")]
        public IActionResult getRecentlyViewedMobile(int UserId, string IpAddress)
        {
            var data = new List<UserLog>();

            if (UserId > 0)
            {
                data = db.UserLogs.Where(x => x.UserId == UserId && x.IsActive == true && x.Url.Contains("product-details") && x.Product.IsActive == true).Include(x => x.Product).OrderByDescending(x => x.Id).ToList();
            }
            else
                data = db.UserLogs.Where(x => x.IPAddress == IpAddress && x.IsActive == true && x.Url.Contains("product-details") && x.Product.IsActive == true).Include(x => x.Product).OrderByDescending(x => x.Id).ToList();
            var data1 = new List<UserLog>();
            var productIds = data.Select(x => x.ProductId).Distinct().Take(5).ToList();
            foreach (var i in productIds)
            {
                var model = new UserLog();
                model = data.Where(x => x.ProductId == i).FirstOrDefault();
                data1.Add(model);
            }

            var final = new List<ProductVariantDetail>();
            var all = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Product.IsActive == true).Include(x => x.Product).Include(x => x.Product.ProductImages).ToList();
            //var list1 = new List<ProductVariantDetail>();
            //foreach (var item in all)
            //{
            //    if( item.Product.IsActive == true)
            //    {
            //        var product = item;
            //        list1.Add(product);
            //    }

            //}
            //all = new List<ProductVariantDetail>();
            //all.AddRange(list1);

            foreach (var d in data1)
            {
                var variant = d.Url.Split('&')[1];
                var VariantId = Convert.ToInt32(variant.Split('=')[1]);
                var pro = new ProductVariantDetail();
                var vari = new ProductVariantDetail();
                if (VariantId > 0)
                {
                    vari = all.Where(x => x.Id == VariantId).FirstOrDefault();
                }
                if (vari != null)
                    final.Add(vari);
            }
            final = DealHelper.calculateDeal(final, db);
            final = PriceIncrementHelper.calculatePrice(final, db);

            var list = new List<ProductCatalogue>();
            foreach (var vari in final)
            {
                var pro = new ProductCatalogue();
                pro.CostPrice = vari.CostPrice;
                pro.Description = vari.Product.Description;
                pro.Discount = vari.Discount;
                pro.Id = vari.Product.Id;
                if (vari.Product?.ProductImages != null)
                    if (vari.Product?.ProductImages.Where(x => x.IsActive == true).ToList().Count > 0)
                    {
                        pro.Image = vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? vari.Product?.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath : vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath;
                        pro.Image150 = vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? vari.Product?.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
                        pro.Image450 = vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? vari.Product?.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath450x450 : vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath450x450;

                    }
                pro.InStock = vari.InStock;
                pro.Name = vari.Product.Name;
                pro.PriceAfterDiscount = vari.PriceAfterdiscount;
                pro.ProductCategoryId = vari.Product.ProductCategoryId;
                pro.SellingPrice = vari.Price;
                pro.VariantDetailId = vari.Id;
                pro.VendorId = vari.Product.VendorId;
                list.Add(pro);

            }
            if (list.Count() >= 2)
                if (list[0].Id == list[1].Id)
                    list.RemoveAt(0);
            if (list.Count() >= 5)
                list = list.Take(4).ToList();
            return Ok(list);
        }

        [HttpPost]
        [Route("getAllRecentlyViewed")]
        public IActionResult getAllRecentlyViewed([FromBody] PageData model)
        {
            var data = new List<UserLog>();

            if (model.UserId > 0)
            {
                data = db.UserLogs.Where(x => x.UserId == model.UserId && x.IsActive == true && x.Url.Contains("product-details") && x.Product.IsActive == true).Include(x => x.Product).OrderByDescending(x => x.Id).ToList();
            }
            else
                data = db.UserLogs.Where(x => x.IPAddress == model.IpAddress && x.IsActive == true && x.Url.Contains("product-details") && x.Product.IsActive == true).Include(x => x.Product).OrderByDescending(x => x.Id).ToList();
            var data1 = new List<UserLog>();
            var page = model.Page + 1;
            var skipData = model.PageSize * (page - 1);
            var alldistinct = data.Select(x => x.ProductId).Distinct().ToList();
            var productIds = data.Select(x => x.ProductId).Distinct().Skip(skipData).Take(model.PageSize).ToList();
            var showed = skipData + model.PageSize;
            var ShowMore = false;
            if (alldistinct.Count > showed)
                ShowMore = true;
            foreach (var i in productIds)
            {
                var model1 = new UserLog();
                model1 = data.Where(x => x.ProductId == i).FirstOrDefault();
                data1.Add(model1);
            }

            var final = new List<ProductVariantDetail>();
            var all = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Product.IsActive == true).Include(x => x.Product.ProductImages).ToList();
            foreach (var d in data1)
            {
                var variant = d.Url.Split('&')[1];
                var VariantId = Convert.ToInt32(variant.Split('=')[1]);
                var pro = new ProductVariantDetail();
                var vari = new ProductVariantDetail();
                if (VariantId > 0)
                {
                    vari = all.Where(x => x.Id == VariantId).FirstOrDefault();
                }
                if (vari != null)
                    final.Add(vari);
            }
            final = DealHelper.calculateDeal(final, db);
            final = PriceIncrementHelper.calculatePrice(final, db);

            var list = new List<ProductCatalogue>();
            foreach (var vari in final)
            {
                var pro = new ProductCatalogue();
                pro.CostPrice = vari.CostPrice;
                pro.Description = vari.Product.Description;
                pro.Discount = vari.Discount;
                pro.Id = vari.Product.Id;
                if (vari.Product?.ProductImages != null)
                    if (vari.Product?.ProductImages.Where(x => x.IsActive == true).ToList().Count > 0)
                    {
                        pro.Image = vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? vari.Product?.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath : vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath;
                        pro.Image150 = vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? vari.Product?.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
                        pro.Image450 = vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? vari.Product?.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath450x450 : vari.Product?.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath450x450;

                    }
                pro.InStock = vari.InStock;
                pro.Name = vari.Product.Name;
                pro.PriceAfterDiscount = vari.PriceAfterdiscount;
                pro.ProductCategoryId = vari.Product.ProductCategoryId;
                pro.SellingPrice = vari.Price;
                pro.VariantDetailId = vari.Id;
                pro.VendorId = vari.Product.VendorId;
                pro.ShowMore = ShowMore;
                pro.Page = page;
                list.Add(pro);

            }
            if (list.Count() >= 2)
                if (list[0].Id == list[1].Id)
                    list.RemoveAt(0);
            return Ok(list);
        }
        [HttpGet]
        [Route("getTrack")]
        public IActionResult getTrack()
        {
            try
            {
                var list = new List<Log>();
                var data = db.Log.Where(x => x.PageUrl == "/checkout-process/checkout").Include(x => x.User).GroupBy(x => x.IpAddress != null);
                foreach (var items in data)
                {
                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }

                //var filter = list.GroupBy(x => x.Guid != null).ToList();
                //list = new List<Log>();
                //foreach (var items in filter)
                //{
                //    if (items.Key == true)
                //        foreach (var item in items)
                //        {
                //            list.Add(item);
                //        }
                //}

                //var action = list.GroupBy(x => x.Action).ToList();
                //list = new List<Log>();
                //foreach (var items in action)
                //{
                //    foreach (var item in items)
                //    {
                //        list.Add(item);
                //    }
                //}
                var filteredList = list.Where(x => x.Guid != null).ToList();
                var action = from c in filteredList
                             group c by new
                             {
                                 c.Action,
                                 c.Guid
                             } into gcs
                             select gcs;
                list = new List<Log>();
                foreach (var items in action)
                {

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }


                foreach (var item in list)
                {
                    if (item.Result.Contains("Request"))
                        item.Result = "1";
                    if (item.Result.Contains("Error"))
                        item.Result = "Error";

                }

                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [Route("getHomePageTrack")]
        public IActionResult getHomePageTrack()
        {
            try
            {
                var list = new List<Log>();
                var data = db.Log.Where(x => x.PageUrl.Trim().ToLower() == "home page".Trim().ToLower()).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                foreach (var items in data)
                {
                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }
                //var action = list.GroupBy(x => x.Guid != null).ToList();
                var action = from c in list
                             group c by new
                             {
                                 c.Action,
                                 c.Guid
                             } into gcs
                             select gcs;
                list = new List<Log>();
                foreach (var items in action)
                {

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }



                foreach (var item in list)
                {
                    if (item.Result.Contains("Request"))
                        item.Result = "1";
                    if (item.Result.Contains("Error"))
                        item.Result = "Error";

                }

                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //[Route("productDetailPageTrack")]
        //[Route("getHomePageTrack")]
        //public IActionResult getHomePageTrack()
        //{
        //    try
        //    {
        //        var list = new List<Log>();
        //        var data = db.Log.Where(x => x.PageUrl.Trim().ToLower() == "home page".Trim().ToLower()).Include(x => x.User).GroupBy(x => x.IpAddress != null);
        //        foreach (var items in data)
        //        {
        //            foreach (var item in items)
        //            {
        //                list.Add(item);
        //            }
        //        }
        //        //var filter = list.GroupBy(x => x.Action).ToList();
        //        //list = new List<Log>();
        //        //foreach (var items in filter)
        //        //{

        //        //        foreach (var item in items)
        //        //        {
        //        //            list.Add(item);
        //        //        }
        //        //}

        //        //var action = list.GroupBy(x => x.Guid != null).ToList();
        //        //list = new List<Log>();
        //        //foreach (var items in action)
        //        //{
        //        //    if (items.Key == true)
        //        //        foreach (var item in items)
        //        //    {
        //        //        list.Add(item);
        //        //    }
        //        list = list.Where(x => x.Guid != null).ToList();
        //        var abc = from c in list
        //                  group c by new
        //                  {
        //                      c.Action,
        //                      c.Guid
        //                  } into gcs
        //                  select gcs;
        //        var action = abc.ToList();
        //        foreach (var items in action)
        //        {

        //         foreach (var item in items)
        //         {
        //         list.add(item);
        //         }
        //        foreach (var item in list)
        //        {
        //            if (item.Result.Contains("Request"))
        //                item.Result = "1";
        //            if (item.Result.Contains("Error"))
        //                item.Result = "Error";

        //        }

        //        return Ok(list);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        [HttpGet]
        [Route("productDetailPageTrack")]
        public IActionResult productDetailPageTrack()
        {
            return Ok(tracking("product-details"));

        }
        [HttpGet]
        [Route("getById")]
        public IActionResult getById(int Id)
        {
            try
            {
                var data = new Result();
                var val = db.Log.Where(x => x.Id == Id).FirstOrDefault();

                data.Res = val.Result;
                data.ActionName = val.Action;
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("getUsers")]
        public IActionResult getUserIds()
        {
            try
            {
                var userId = db.Users.Where(x => x.RoleId == 1).Select(x => new
                {
                    Id = x.Id,
                    Name = x.Email
                }).ToList();



                return Ok(userId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("trackMycart")]
        public IActionResult trackMycart()
        {
            return Ok(tracking("mycart"));

        }
        [HttpGet]
        [Route("trackProductCat")]
        public IActionResult trackProductCat()
        {
            return Ok(tracking("productcatalogue"));
        }
        [HttpGet]
        [Route("trackwishProd")]
        public IActionResult trackwishProd()
        {
            return Ok(tracking("wishlist"));
        }
        [HttpGet]
        [Route("trackCompareProducts")]
        public IActionResult trackCompareProducts()
        {
            return Ok(tracking("CompareProducts"));
        }
        [HttpGet]
        [Route("trackHeader")]
        public IActionResult trackHeader()
        {
            return Ok(tracking("header"));
        }
        [HttpGet]
        [Route("trackfooter")]
        public IActionResult trackfooter()
        {
            return Ok(tracking("footer"));
        }
        [HttpGet]
        [Route("trackMyProfile")]
        public IActionResult trackMyProfile()
        {
            return Ok(tracking("profile"));
        }
        //all notification
        [HttpGet]
        [Route("trackAllnotification")]
        public IActionResult trackAllnotification()
        {
            return Ok(tracking("AllNotifications"));
        }
        //confirmation track
        [HttpGet]
        [Route("trackconfirmation")]
        public IActionResult trackconfirmation()
        {
            return Ok(tracking("confirmation"));
        }
        //trackorder 
        [HttpGet]
        [Route("trackorder")]
        public IActionResult trackorder()
        {
            return Ok(tracking("trackorder"));
        }
        //dealscatalogue track
        [HttpGet]
        [Route("trackdealscatalogue")]
        public IActionResult trackdealscatalogue()
        {
            return Ok(tracking("dealscatalogue"));
        }
        //RatingReview track
        [HttpGet]
        [Route("trackdealscatalogue")]
        public IActionResult trackRatingReview()
        {
            return Ok(tracking("dealscatalogue"));
        }
        //customer/UserLogin track
        [HttpGet]
        [Route("trackUserLogin")]
        public IActionResult trackUserLogin()
        {
            return Ok(tracking("UserLogin"));
        }
        //MyOrders
        [HttpGet]
        [Route("trackMyOrders")]
        public IActionResult trackMyOrders()
        {
            return Ok(tracking("MyOrders"));
        }
        //customer/UserRegistration
        [HttpGet]
        [Route("trackUserRegistration")]
        public IActionResult trackUserRegistration()
        {
            return Ok(tracking("UserRegistration"));
        }
        // allreviews
        [Route("trackallreviews")]
        public IActionResult trackallreviews()
        {
            return Ok(tracking("allreviews"));
        }


        //getFilter acccording to date 
        [HttpGet]
        [Route("getFilterDate")]
        public IActionResult getFilterData(string Date,string pageName)
        {
            try
            {
                var changeIntoDate = Convert.ToDateTime(Date);
                var list = new List<Log>();

                if(pageName=="home page")
                { 
                var data = db.Log.Where(x => x.PageUrl!=null && x.PageUrl.Trim().ToLower() == "home page".Trim().ToLower() && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.Guid != null).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                    foreach (var items in data)
                    {
                        foreach (var item in items)
                        {
                            list.Add(item);
                        }
                    }
                }else if(pageName== "/checkout-process/checkout")
                {
                    var data = db.Log.Where(x => x.PageUrl != null && x.PageUrl.Trim().ToLower() == "/checkout-process/checkout".Trim().ToLower() && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.Guid != null).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                    foreach (var items in data)
                    {
                        foreach (var item in items)
                        {
                            list.Add(item);
                        }
                    }
                }else if (pageName == "" || pageName==null)
                {
                    var data = db.Log.Where(x => x.PageUrl!=null && x.LogtypeId==2 && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.Guid != null).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                    foreach (var items in data)
                    {
                        foreach (var item in items)
                        {
                            list.Add(item);
                        }
                    }
                }
                else
                {
                    var data = db.Log.Where(x => x.PageUrl != null && x.PageUrl.Contains(pageName) && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.Guid != null).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                    foreach (var items in data)
                    {
                        foreach (var item in items)
                        {
                            list.Add(item);
                        }
                    }
                }
                  
                var action = list.DistinctBy(x => x.Guid).ToList();
                var filterDataList = new List<filterData>();
                var i = 0;
                foreach (var item in action)
                {
                   
                    var model = new filterData();
                    model.Guid = item.Guid;
                    model.UserId = item.UserId;
                    var first = list.Where(x => x.Guid == item.Guid).FirstOrDefault();
                    var last = list.Where(x => x.Guid == item.Guid).LastOrDefault();
                    model.SessionId = "Session-" + ++i + "(" + first.ActionDateTime.Value.ToShortTimeString() + "-" + last.ActionDateTime.Value.ToShortTimeString()+")";
                    filterDataList.Add(model);
                }
              var finalres=  filterDataList.ToList();
                return Ok(finalres);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //getFilter user accoridng to date
        [HttpGet]
        [Route("filterUser")]
        public IActionResult getFilterUser(string Date)
        {
            try
             {
                var changeIntoDate = Convert.ToDateTime(Date);
                var list = new List<Log>();
                var data = db.Log.Where(x => x.Guid != null && x.UserId != null && x.AppVersion!="Web").DistinctBy(x=>x.UserId).ToList();
                var users = db.Users.Where(x => x.RoleId == 1).ToList();
                var filter = new List<filteredUser>();
                foreach (var item in data)
                {
                    var filterUser = users.Where(x => x.Id == item.UserId).Select(x=> new filteredUser
                    {
                        Id=x.Id,
                        Name=x.Email
                    }).FirstOrDefault();
                    if (filterUser!=null)
                    filter.Add(filterUser);
                }
                return Ok(filter);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getFilterData")]
        public IActionResult getFilterData()
        {
            var list = new List<liveTraffic>();
            try
            {

                var data = db.Log.Where(x => x.UserId != null && x.AppVersion != null).DistinctBy(x=>x.Country).Select(x=>x.Country).ToList();
                var logs = db.Log.Where(x=>x.PageUrl!=null && x.AppVersion!=null).ToList();
                
                foreach (var item in data)
                {
                    
                    var live= new liveTraffic();
                    var windowsCout = logs.Where(x => x.AppVersion == "Web").DistinctBy(x=>x.UserId).Count();
                    live.Country = item;
                    live.Device = "Web";
                    live.Users = windowsCout;
                        list.Add(live);
                    live = new liveTraffic();
                    var iphoneCount = logs.Where(x => x.AppVersion == "iphone").DistinctBy(x => x.UserId).Count();
                    live.Country = item;
                    live.Device = "iphone";
                    live.Users = iphoneCount;
                    list.Add(live);
                    live = new liveTraffic();
                    var AndroidCout = logs.Where(x => x.AppVersion == "Android").DistinctBy(x => x.UserId).Count();
                    live.Country = item;
                    live.Device = "Android";
                    live.Users = AndroidCout;
                    list.Add(live);
                     }
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("liveDateFilter")]
        public IActionResult liveDateFil(string Date,string date2)
        {
            var list = new List<liveTraffic>();
            try
            {
                var changeIntoDate = Convert.ToDateTime(Date);
                var changeIntoDate2 = Convert.ToDateTime(date2);
                // x.ActiveFrom.Date <= DateTime.Now.Date && x.ActiveTo.Date >= DateTime.Now.Date
                //    x.ActionDateTime.Value.Date >= changeIntoDate2.Date
                // &&  
             //   &&
               // x.ActionDateTime.Value.Date >= changeIntoDate2.Date
                var logs = db.Log.Where(x=> x.ActionDateTime.Value.Date >= changeIntoDate.Date ).ToList();
                logs = logs.Where(x => x.ActionDateTime.Value.Date <= changeIntoDate2.Date).ToList();

                var data = db.Log.DistinctBy(x => x.Country).Select(x => x.Country).ToList();
                foreach (var item in data)
                {
                    var live = new liveTraffic();
                    var windowsCout = logs.Where(x => x.AppVersion == "Web").DistinctBy(x => x.UserId).Count();
                    live.Country = item;
                    live.Device = "Web";
                    live.Users = windowsCout;
                    list.Add(live);
                    live = new liveTraffic();
                    var iphoneCount = logs.Where(x => x.AppVersion == "iphone").DistinctBy(x => x.UserId).Count();
                    live.Country = item;
                    live.Device = "iphone";
                    live.Users = iphoneCount;
                    list.Add(live);
                    live = new liveTraffic();
                    var AndroidCout = logs.Where(x => x.AppVersion == "Android").DistinctBy(x => x.UserId).Count();
                    live.Country = item;
                    live.Device = "Android";
                    live.Users = AndroidCout;
                    list.Add(live);
                }
                
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getCountriesss")]
        public IActionResult getCountry()
        {
            try
            {

               var country = db.Log.DistinctBy(x => x.Country).Select(x=>x.Country).ToList();
                return Ok(country);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getmostVisitedProduct")]
        public IActionResult getUserAct(string val)
        {
            var date = DateTime.Now;
            DateTime hour;
            DateTime week;
            int month;
            int year;
            var data = db.Log.ToList();
            if (val == "H")
            {
                hour = date.AddHours(-1);
                data = data.Where(x => x.ActionDateTime <= hour).ToList();
            }
            else if (val == "W")
            {
                week = date.AddDays(-7);
                data = data.Where(x => x.ActionDateTime <= week).ToList();


            }
            else if (val == "M")
            {
                month = date.Month;
                data = data.Where(x => x.ActionDateTime.Value.Month <=month).ToList();

            }
            else if (val == "Y")
            {
                year = date.Year;
                data = data.Where(x => x.ActionDateTime.Value.Year <= year).ToList();

            }
            var list = new List<productName>();
            try
            {
               
              //  var productDet = data.Where(x => x.Description == "Getting product details on product detail page").GroupBy(x => x.RequestUrl).Count();
            var mostVisitedProduct= data.Where(x => x.Description == "Getting product details on product detail page").GroupBy(x => x.RequestUrl)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() }).Take(5).OrderByDescending(x => x.Counter)
              .ToList();
                string[] split;
                foreach (var item in mostVisitedProduct)
                {
                    var product = new productName();
                    if (item.Element != null) { 
                split = item.Element.Split("=");
                        var id = Convert.ToInt32(split[1].Replace("&variantId", ""));
                        var prod = db.Products.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();
                        product.ProductCount = item.Counter;
                        product.ProductName = prod;
                        list.Add(product);

                    }

                    // id = item.Element.Replace("&variantId=2851", "");
                }
               
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("getPageDateFilter1")]
        public IActionResult getPageDateFilter()
        {
            //  string date, int UserId, string PageName
            // var changeIntoDate = Convert.ToDateTime(date);
            var list = new List<productName>();

            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            // chart.colors = new List<string>() { "#77cc15" };
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Most visited product" } });

            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Product" } });
            var result = new List<Log>();
        //    && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.UserId == UserId
            var data = db.Log.Where(x => x.PageUrl != null  && x.RequestUrl!=null).ToList();

            //  var productDet = data.Where(x => x.Description == "Getting product details on product detail page").GroupBy(x => x.RequestUrl).Count();
            //.GroupBy(x => x.RequestUrl)
            var mostVisitedProduct = data.Where(x => x.RequestUrl.Contains("variantId")).GroupBy(x => x.PageUrl)
            .Take(5)
              .ToList();
            string[] split;
            //  .Select(y => new { Element = y.Key, Counter = y.Count() }).OrderByDescending(x => x.Counter)
           
           // int first = 0;
            //int seconed = 1;
            //int J = 1;
            foreach (var items in mostVisitedProduct)
            {
                foreach (var item in items)
                {
                    var product = new productName();

                    split = item.RequestUrl.Split("=");
                    var id = Convert.ToInt32(split[1].Replace("&variantId", ""));
                    var prod = db.Products.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();
                    //foreach (var items in mostVisitedProduct)
                    //{
                    //    if (mostVisitedProduct[first].PageUrl == mostVisitedProduct[seconed].PageUrl)
                    //       ++I;
                    //    else
                    //    {
                    //        I = 1;
                    //        
                    //    }
                    //}
                    product.ProductCount = items.Count();
                    product.ProductName = item.PageUrl + "(" + prod + ")";
                    list.Add(product);
                   

                }

            }
            //}
            //    // id = item.Element.Replace("&variantId=2851", "");
            //}


            //var actions = result.DistinctBy(x => x.Action).Select(x => x.Action).ToList();

            var tempList = new List<Data>();

            foreach (var item in list)
            {
                var temp = new Data();
                temp.name = item.ProductName;
                temp.y = Convert.ToDecimal(item.ProductCount);
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Page logs",
                data = tempList
            });

            return Ok(chart);
        }



        [HttpGet]
        [Route("getPageDateFilter2")]
        public IActionResult getPageDateFilter1(string date)
        {
            //  string date, int UserId, string PageName
         var changeIntoDate = Convert.ToDateTime(date);
            var list = new List<productName>();

            HighChartModel chart = new HighChartModel();
            chart.chart.type = "column";
            chart.title.text = "";
            chart.chart.height = 600;
            chart.chart.width = 500;
            // chart.colors = new List<string>() { "#77cc15" };
            chart.colors = new List<string>() { "#77cc15", "#FB1926", "#FFE033", "#FF7A05", "#fe5722", "#ff9902", "#bb8800", "#3eb943", "#4cb050", "#cddc39", "#3f51b5", "#2095f", "#9c28b1", "#cc29ea" };
            chart.yAxis.Add(new YAxis() { title = new Title() { text = "Most visited product" } });

            chart.xAxis.Add(new XAxis() { title = new Title() { text = "Product" } });
            var result = new List<Log>();
            //    && x.ActionDateTime.Value.Date == changeIntoDate.Date && x.UserId == UserId
            var data = db.Log.Where(x => x.PageUrl != null && x.RequestUrl != null && x.ActionDateTime.Value.Date == changeIntoDate.Date).ToList();

            //  var productDet = data.Where(x => x.Description == "Getting product details on product detail page").GroupBy(x => x.RequestUrl).Count();
            var mostVisitedProduct = data.Where(x => x.RequestUrl.Contains("variantId")).GroupBy(x => x.RequestUrl)
              .Where(g => g.Count() > 1)
            .Take(5)
              .ToList();
            string[] split;
            //  .Select(y => new { Element = y.Key, Counter = y.Count() }).OrderByDescending(x => x.Counter)
            foreach (var items in mostVisitedProduct)
            {

                foreach (var item in items)
                {
                    var product = new productName();

                    split = item.RequestUrl.Split("=");
                    var id = Convert.ToInt32(split[1].Replace("&variantId", ""));
                    var prod = db.Products.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();
                    product.ProductCount = items.Count();
                    product.ProductName = item.PageUrl + "(" + prod + ")";
                    list.Add(product);


                }


            }
            //    // id = item.Element.Replace("&variantId=2851", "");
            //}


            var actions = result.DistinctBy(x => x.Action).Select(x => x.Action).ToList();

            var tempList = new List<Data>();

            foreach (var item in list)
            {
                var temp = new Data();
                temp.name = item.ProductName;
                temp.y = Convert.ToDecimal(item.ProductCount);
                tempList.Add(temp);
            }

            chart.series.Add(new Series()
            {
                name = "Page logs",
                data = tempList
            });

            return Ok(chart);
        }
        public class productName
        {
            public int ProductCount { get; set; }
            public string ProductName { get; set; }
        }
        [HttpGet]
        [Route("getmostVistedpage")]
        public IActionResult getProducts(string val)
        {
            try
            {
                var date = DateTime.Now;
                DateTime hour;
                DateTime week;
                int month;
                int year;
                var data = db.Log.Where(x=>x.PageUrl!=null).ToList();
                if (val == "H")
                {
                    hour = date.AddHours(-1);
                    data = data.Where(x => x.ActionDateTime == hour).ToList();
                }
                else if (val == "W")
                {
                    week = date.AddDays(-7);
                    data = data.Where(x => x.ActionDateTime <= week).ToList();
                }
                else if (val == "M")
                {
                    month = date.Month;
                    data = data.Where(x => x.ActionDateTime.Value.Month <= month).ToList();
                }
                else if (val == "Y")
                {
                    year = date.Year;
                    data = data.Where(x => x.ActionDateTime.Value.Year <= year).ToList();
                }

             //   var data = db.Log.ToList();
                //  var productDet = data.Where(x => x.Description == "Getting product details on product detail page").GroupBy(x => x.RequestUrl).Count();
               
                var mostVistedpage = data.GroupBy(x => x.PageUrl)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() }).OrderByDescending(x => x.Counter)
              .ToList();
                return Ok(mostVistedpage);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("getSubNregval")]
        public IActionResult getSUbRegval(string val)
        {
            var date = DateTime.Now;
            DateTime hour;
            DateTime week;
            int month;
            int year;
            int registeredUSer=0;
                var newsletterData = new List<Newsletter>();

            if (val == "H")
            {
                hour = date.AddHours(-1);
                registeredUSer = db.Users.Where(x => x.RoleId == 1 && x.DateTime <= hour).Count();
                newsletterData = db.Newsletters.Where(x => x.Email != null && x.Date <= hour).ToList();
            }
            else if (val == "W")
            {
                week = date.AddDays(-7);
                registeredUSer = db.Users.Where(x => x.RoleId == 1 && x.DateTime <= week).Count();
                newsletterData = db.Newsletters.Where(x => x.Email != null && x.Date == week).ToList();
            }
            else if (val == "M")
            {
                month = date.Month;
                 var getYears = db.Users.Where(x => x.RoleId == 1).OrderBy(x => x.Id).FirstOrDefault();
                  var countYears =  getYears.DateTime.Value.Month -date.Month ;
                
                registeredUSer = db.Users.Where(x => x.RoleId == 1 && x.DateTime.Value.Month <= date.Month).Count();
                newsletterData = db.Newsletters.Where(x => x.Email != null && x.Date.Value.Month <= date.Month).ToList();
            }
            else if (val == "Y")
            {
                year = date.Year;
              //  var getYears = db.Users.Where(x => x.RoleId == 1).OrderBy(x => x.Id).FirstOrDefault();
              //  var countYears = getYears.DateTime.Value.Year;
                registeredUSer = db.Users.Where(x => x.RoleId == 1 && x.DateTime.Value.Year <= year).Count();
                newsletterData = db.Newsletters.Where(x => x.Email != null && x.Date.Value.Year <= year).ToList();
            }
                 var SubscribedNewsletter = newsletterData.Where(x => x.IsSubscribed == true).Count();
                var unSubscribedNewsletter = newsletterData.Where(x => x.IsSubscribed == false).Count();
                var data = new SubNUnsub();
                data.SubscribedNewsletter = SubscribedNewsletter;
                data.unSubscribedNewsletter = unSubscribedNewsletter;
                data.registeredUSer = registeredUSer;
                return Ok(data);
        }

        [HttpGet]
        [Route("getSales")]
        public IActionResult GetSales(string val)
        {
           var  Year = DateTime.Now.Year;
          var   Period = 0;
            var sale = db.PaymentTransaction.Where(x => x.intent == "sale" && x.paymentID != null && x.CheckoutId != null && x.CheckoutId != 0 && x.Date.Value.Year == Year);
            var refund = db.PaymentTransaction.Where(x => x.intent == "refund" && x.ReturnId != null && x.Date.Value.Year == Year);
            //Sale
            var JanSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 01).Sum(x => x.TransactionAmount));
            var FebSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 02).Sum(x => x.TransactionAmount));
            var MarSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 03).Sum(x => x.TransactionAmount));
            var AprSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 04).Sum(x => x.TransactionAmount));
            var MaySale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 05).Sum(x => x.TransactionAmount));
            var JunSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 06).Sum(x => x.TransactionAmount));
            var JulSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 07).Sum(x => x.TransactionAmount));
            var AugSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 08).Sum(x => x.TransactionAmount));
            var SepSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 09).Sum(x => x.TransactionAmount));
            var OctSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 10).Sum(x => x.TransactionAmount));
            var NovSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 11).Sum(x => x.TransactionAmount));
            var DecSale = Convert.ToDecimal(sale.Where(x => x.Date.Value.Month == 12).Sum(x => x.TransactionAmount));
            //Refund
            var JanRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 01).Sum(x => x.TransactionAmount));
            var FebRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 02).Sum(x => x.TransactionAmount));
            var MarRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 03).Sum(x => x.TransactionAmount));
            var AprRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 04).Sum(x => x.TransactionAmount));
            var MayRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 05).Sum(x => x.TransactionAmount));
            var JunRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 06).Sum(x => x.TransactionAmount));
            var JulRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 07).Sum(x => x.TransactionAmount));
            var AugRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 08).Sum(x => x.TransactionAmount));
            var SepRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 09).Sum(x => x.TransactionAmount));
            var OctRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 10).Sum(x => x.TransactionAmount));
            var NovRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 11).Sum(x => x.TransactionAmount));
            var DecRefund = Convert.ToDecimal(refund.Where(x => x.Date.Value.Month == 12).Sum(x => x.TransactionAmount));
            if (val == "Y")//yearly
            {

              var saleTotal=  JanSale - JanRefund;
                saleTotal+= FebSale - FebRefund;
                saleTotal += MarSale - MarRefund;
                saleTotal += AprSale - AprRefund;
                saleTotal += MaySale - MayRefund;
                saleTotal += JunSale - JunRefund;
                saleTotal += JulSale - JulRefund;
                saleTotal += AugSale - AugRefund;
                saleTotal += SepSale - SepRefund;
                saleTotal += OctSale - OctRefund;
                saleTotal += NovSale - NovRefund;
                saleTotal += DecSale - DecRefund;
                return Ok((double)saleTotal /12);
            }
            else
            {
                if (val == "W")
                {
                    var sale1 = db.PaymentTransaction.Where(x => x.intent == "sale" && x.paymentID != null && x.CheckoutId != null && x.CheckoutId != 0 && x.Date.Value.AddDays(-7) <= DateTime.Now.Date && x.Date.Value.AddDays(-7) >= DateTime.Now.Date).Sum(x => x.TransactionAmount);
                    var refund1 = db.PaymentTransaction.Where(x => x.intent == "refund" && x.ReturnId != null && x.Date.Value.AddDays(-7) <= DateTime.Now.Date && x.Date.Value.AddDays(-7) >= DateTime.Now.Date).Sum(x => x.TransactionAmount);
                    var today = Convert.ToDecimal(sale1 - refund1);
                    // var data1 = db.Checkouts.Where(x => x.IsActive == true && x.IsPaid == true && x.Date.Date == DateTime.Now.Date).ToList();
                    // var today = data1.Sum(x => x.TransactionAmount);
                    return Ok((double)today /7);
                }
                else if (val == "H")

                {
                    var sale1 = db.PaymentTransaction.Where(x => x.intent == "sale" && x.paymentID != null && x.CheckoutId != null && x.CheckoutId != 0 && x.Date == DateTime.Now.Date).ToList();
                    var refund1 = db.PaymentTransaction.Where(x => x.intent == "refund" && x.ReturnId != null && x.Date == DateTime.Now.Date).ToList();
                    var saleamount = sale1.Where(x => x.Date >= DateTime.Now.AddHours(-1)).Sum(x => x.TransactionAmount);
                    var refundamount = refund1.Where(x => x.Date >= DateTime.Now.AddHours(-1)).Sum(x => x.TransactionAmount);

                    var LastoneHour = (double)(saleamount - refundamount);
                    return Ok(LastoneHour);

                    //var data1 = db.Checkouts.Where(x => x.IsActive == true && x.IsPaid == true && x.Date.Date == DateTime.Now.Date).ToList();
                    //var today = data1.Where(x => x.Date >= DateTime.Now.AddHours(-1)).Sum(x => x.TransactionAmount);
                }
                else if (val == "M")//yearly
                {

                    var saleTotal = JanSale - JanRefund;
                    saleTotal += FebSale - FebRefund;
                    saleTotal += MarSale - MarRefund;
                    saleTotal += AprSale - AprRefund;
                    saleTotal += MaySale - MayRefund;
                    saleTotal += JunSale - JunRefund;
                    saleTotal += JulSale - JulRefund;
                    saleTotal += AugSale - AugRefund;
                    saleTotal += SepSale - SepRefund;
                    saleTotal += OctSale - OctRefund;
                    saleTotal += NovSale - NovRefund;
                    saleTotal += DecSale - DecRefund;
                    return Ok((double)saleTotal / 30);
                }
                return Ok();
            }
        }
        [HttpGet]
        [Route("getOrders")]
        public IActionResult getOrders(string val)
        {
            try
            {
                var data = db.PaymentTransaction.Where(x => x.Date != null).ToList();
                if (val == "Y")
                {
                    var year = DateTime.Now.Year;
                    data = data.Where(x => x.Date.Value.Year == year).ToList();
                    double res = (double)data.Count() / 12;
                return Ok(res);
                   
                }
                else if (val == "M")
                {
                    var month = DateTime.Now.Month;
                    data = data.Where(x => x.Date.Value.Month == month).ToList();
                    double res = (double)data.Count() / 30;
                    return Ok(res);

                }else if (val == "H")
                {
                    var hour = DateTime.Now.AddHours(-1);
                    data = data.Where(x => x.Date.Value.AddHours(-1) == hour).ToList();
                    double res = (double)data.Count() / 60;
                    return Ok(res);
                }
                else if (val == "W")
                {
                    
                    data = data.Where(x => x.Date.Value.AddDays(-7) <= DateTime.Now.Date && x.Date.Value.AddDays(-7) >= DateTime.Now.Date).ToList();
                    double res = (double)data.Count() / 7;
                    return Ok(res);
                }
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public class SubNUnsub
        {
            public int registeredUSer { get; set; }
            public int SubscribedNewsletter { get; set; }
            public int unSubscribedNewsletter { get; set; }
        }
        [HttpGet]
        [Route("getSubNregM")]
        public IActionResult getSUbReg(string val)
        {
            try
            {
                var registeredUSer = db.Users.Where(x => x.RoleId == 1).Count();
                var users = db.Users.Where(x => x.RoleId == 1 && x.DateTime!=null).ToList();
                var StartMonth = db.Users.Where(x=>x.DateTime!=null).OrderBy(x => x.Id).FirstOrDefault();
                var months = DateTime.Now.Month - StartMonth.DateTime.Value.Month;
                    
                var date = DateTime.Now;
               // if (val == "Month")
            //    {
                    var month = date.Month;
                var jan= users.Where(x => x.DateTime.Value.Month == 1).Count();
                    var janAvg = jan!=0?12 / jan:0;
                

                var feb = users.Where(x => x.DateTime.Value.Month == 2).Count();
               var febAvg = feb!=0?12 / feb:0;

                var mar = users.Where(x => x.DateTime.Value.Month == 3).Count();
               var marAvg = mar!=0?12 / mar : 0;

                var apr = users.Where(x => x.DateTime.Value.Month == 4).Count();

                var   aprAvg= apr!= 0 ? 12 / apr : 0;

                var may = users.Where(x => x.DateTime.Value.Month == 5).Count();
                var mayAvg = 0;
                    mayAvg =may != 0 ? 12 / may : 0;

                var june = users.Where(x => x.DateTime.Value.Month == 6).Count();
                var juneAvg = 0;
                   juneAvg= june!= 0 ? 12 / june : 0;

                var jul = users.Where(x => x.DateTime.Value.Month == 7).Count();
                var julAvg = jul!= 0 ? 12 / jul : 0;

                var aug = users.Where(x => x.DateTime.Value.Month == 8).Count();
                var augAvg = aug != 0 ? 12 / aug : 0;

                var sep = users.Where(x => x.DateTime.Value.Month == 9).Count();
                var sepAvg = sep!= 0 ? 12 / sep : 0;

                var oct = users.Where(x => x.DateTime.Value.Month == 10).Count();
                var octAvg =oct != 0 ? registeredUSer / oct : 0;

                var nov = users.Where(x => x.DateTime.Value.Month == 11).Count();
                var novAvg = nov!= 0 ? registeredUSer / nov : 0;

                var dec = users.Where(x => x.DateTime.Value.Month == 12).Count();
                var decAvg =dec != 0 ? registeredUSer / dec : 0;

                var monthlyAvg = janAvg + febAvg + marAvg + aprAvg + mayAvg + juneAvg + julAvg + augAvg + sepAvg + octAvg + novAvg + decAvg;

               var avg = users.Count() / monthlyAvg;
            //    }
           if (val == "Year")
                {
                    var year = date.Year;
                    registeredUSer = db.Users.Where(x => x.RoleId == 1 && x.DateTime.Value.Year == year).Count();

                }
                else if (val == "day")
                {
                    //var year = date.Year;
                   // registeredUSer = db.Users.Where(x => x.RoleId == 1 && x.DateTime.Value. == year).Count();

                }
                else if (val == "Hour")
                {
                    var hour = date.Hour;
                }


                var newsletterData = db.Newsletters.Where(x => x.Email != null).ToList();
                var SubscribedNewsletter = newsletterData.Where(x => x.IsSubscribed == true).Count();
                var unSubscribedNewsletter = newsletterData.Where(x => x.IsSubscribed == false).Count();
                return Ok();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("ErrorsTacking")]
        public IActionResult ErrorsTacking()
        {
            try
            {
                var data = ErrorsTacking1();
                return Ok(data);
            }
            catch (Exception  ex)
            {

                throw;
            }
        }
        public class liveTraffic
        {
          
            public string Country { get; set; }
            public string Device { get; set; }
            public int Users { get; set; }
        }
        public class filteredUser
        {
            public int Id {get; set; }
        public string Name { get; set; }
    }
        public class filterData
        {
            public int? UserId { get; set; }
            public string Guid  { get; set; }
            public string SessionId { get; set; }
}
        //CompareProducts MyProfile
        public List<Log> tracking(string val)
        {
            try
            {
                var contains = val;
                var list = new List<Log>();
                var data= new List<Log>();
                //   var data = db.Log.Where(x => x.PageUrl.Contains("mycart")).Include(x => x.User).GroupBy(x => x.IpAddress != null);
                if (contains == "profile")
                {
                   data = db.Log.Where(x => x.PageUrl == "profile").Include(x => x.User).ToList();
                }
                else
                {
                    data = db.Log.Where(x => x.PageUrl.Contains(contains)).Include(x => x.User).ToList();
                }
                //  data = data.GroupBy(x => x.IpAddress );
                var groupedData = data.GroupBy(x => x.IpAddress != null);
                foreach (var items in groupedData)
                {
                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }
              
                var filterList = list.Where(x => x.Guid != null).ToList();
                var action = from c in filterList
                             group c by new
                             {
                                 c.Action,
                                 c.Guid
                             } into gcs
                             select gcs;
                list = new List<Log>();
                foreach (var items in action)
                {

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }
                foreach (var item in list)
                {
                    if (item.Result.Contains("Request"))
                        item.Result = "1";
                    if (item.Result.Contains("Error"))
                        item.Result = "Error";

                }

                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<Log> ErrorsTacking1()
        {
            try
            {
                var list = new List<Log>();
                var data = new List<Log>();
                //   var data = db.Log.Where(x => x.PageUrl.Contains("mycart")).Include(x => x.User).GroupBy(x => x.IpAddress != null);
            
                    data = db.Log.Where(x => x.PageUrl !=null && x.LogtypeId==2).Include(x => x.User).ToList();
              
                //  data = data.GroupBy(x => x.IpAddress );
                var groupedData = data.GroupBy(x => x.IpAddress != null);
                foreach (var items in groupedData)
                {
                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }

                var filterList = list.Where(x => x.Guid != null).ToList();
                var action = from c in filterList
                             group c by new
                             {
                                 c.Action,
                                 c.Guid
                             } into gcs
                             select gcs;
                list = new List<Log>();
                foreach (var items in action)
                {

                    foreach (var item in items)
                    {
                        list.Add(item);
                    }
                }
                foreach (var item in list)
                {
                    if (item.Result.Contains("Request"))
                        item.Result = "1";
                    if (item.Result.Contains("Error"))
                        item.Result = "Error";

                }

                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public class userlogg
        {
            public int Id { get; set; }
            public int? UserId { get; set; }
            public int? ActionId { get; set; }
            public int? ProductId { get; set; }
            public int? PageId { get; set; }
            public string IPAddress { get; set; }
            public string Url { get; set; }
            public DateTime? LogInDate { get; set; }
            public DateTime? LogOutDate { get; set; }
        }
        public class Result
        {
            public string ActionName { get; set; }
            public string Res { get; set; }
        }
    }
}