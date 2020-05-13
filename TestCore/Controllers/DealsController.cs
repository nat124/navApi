using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TestCore.Extension_Method;
using TestCore.Helper;

namespace TestCore.Controllers
{
    [Route("api/deal")]
    [ApiController]
    public class DealsController : ControllerBase
    {

        private readonly PistisContext db;
        public DealsController(PistisContext pistis)
        {
            db = pistis;
        }


        [HttpGet]
        [Route("getAll1")]
        public IActionResult GetDealss1()
         {

            try
            {
                var data = db.Deal.Where(c => c.IsActive == true).Select(v => new DealsModel
                {
                    Id = v.Id,
                    ActiveFrom = v.ActiveFrom,
                    ActiveTo = v.ActiveTo,
                    Discount = v.Discount,
                    Name = v.Name,
                    DealQty = v.DealQty,
                    QuantityPerUser = v.QuantityPerUser,
                    IsFeatured = v.IsFeatured,
                    CategoryId = v.CategoryId,
                    SoldQty = v.SoldQty,
                    Status = v.Status,
                    ActiveFromTime = v.ActiveFromTime,
                    ActiveToTime = v.ActiveToTime,
                    IsShow=v.IsShow
                    // ProductCategoryId = v.ProductCategoryId,
                }).ToList();


                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }


        [HttpGet]
        [Route("getAll")]
        public IActionResult GetDealss(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);
            try
            {
                var data = db.Deal.Where(c => c.IsActive == true).Select(v => new DealsModel
                {
                    Id = v.Id,
                    ActiveFrom = v.ActiveFrom,
                    ActiveTo = v.ActiveTo,
                    Discount = v.Discount,
                    Name = v.Name,
                    DealQty = v.DealQty,
                    QuantityPerUser = v.QuantityPerUser,
                    IsFeatured = v.IsFeatured,
                    CategoryId = v.CategoryId,
                    SoldQty = v.SoldQty,
                    Status = v.Status,
                    IsShow=v.IsShow,
                    //ProductCategoryId = v.ProductCategoryId,
                }).ToList();

                if (search != null)
                {
                    data = data.Where(c => c.Name.ToLower().Contains(search.ToLower())).ToList();
                }

                var response = new
                {
                    data = data.Skip(skipData).Take(pageSize).OrderByDescending(c => c.Id).ToList(),
                    count = data.Count
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpGet]
        [Route("getById")]
        public IActionResult getById(int id)
        {
            var data = db.Deal.Where(c => c.Id == id).Include(x => x.DealProduct).FirstOrDefault().RemoveReferences();
            return Ok(data);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult deleteById(int id)
        {
            var data = db.Deal.Where(c => c.Id == id).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = false;
                db.SaveChanges();
            }
            return Ok(true);
        }


        [HttpPost]
        [Route("addDeal")]
        public IActionResult Add(DealsModel model)
        {
            try
            {
                if (model != null)
                {
                    var data = new Deal();
                    data.CategoryId = model.CategoryId;
                    data.ActiveFrom = model.ActiveFrom;
                    data.ActiveTo = model.ActiveTo;
                    data.Discount = model.Discount;
                    data.QuantityPerUser = model.QuantityPerUser;
                    data.DealQty = model.DealQty;
                    data.ActiveToTime = model.ActiveToTime;
                    data.ActiveFromTime = model.ActiveFromTime;
                    data.SoldQty = model.SoldQty;
                    data.Status = model.Status;
                    data.IsFeatured = model.IsFeatured;
                    data.Name = model.Name;
                    data.IsActive = true;
                    data.IsShow = model.IsShow;
                    data.ProductCategoryId = model.ProductCategoryId;
                    db.Deal.Add(data);
                    var result = db.SaveChanges();
                    List<DealProduct> dealproduct = new List<DealProduct>();
                    foreach (var item in model.DealProduct)
                    {
                        item.DealId = data.Id;
                    }

                    db.DealProduct.AddRange(model.DealProduct);
                    db.SaveChanges();

                    var product = db.ProductVariantDetails.Where(b => b.Id == data.CategoryId).Include(n => n.Product)?.FirstOrDefault()?.Product;
                    var productName = product?.Name;

                    //------creating notification
                    var noti = new Notification();
                    noti.CreatedDate = System.DateTime.Now;
                    noti.DeletedDate = null;
                    noti.ReadDate = null;
                    noti.IsRead = false;
                    noti.IsDeleted = false;
                    noti.IsActive = true;
                    noti.NotificationTypeId = Convert.ToInt32(Helper.NotificationType.DealOffer);
                    noti.Title = model.Name;
                    noti.SpanishTitle = model.Name;
                    noti.Description = ("Deal started from(" + data.ActiveFrom.ToShortDateString() + " to " + data.ActiveTo.ToShortDateString() + ")").ToString();
                    noti.SpanishDescription= ("El trato comenzó a partir de(" + data.ActiveFrom.ToShortDateString() + " a " + data.ActiveTo.ToShortDateString() + ")").ToString();
                    var TargetURL = db.NotificationTypes.Where(b => b.Id == Convert.ToInt32(Helper.NotificationType.DealOffer) && b.IsActive == true)
                    .FirstOrDefault()?.BaseURL + "?Id=" + data.Id;
                    noti.TargetURL = TargetURL;
                    noti.UserId = 0;
                    //----saving notification of purcahse order
                    var users = db.Users.Where(c => c.IsActive == true && c.RoleId == (int)RoleType.Customer).Select(c => c.Id).ToList();
                    var status = NotificationHelper.saveNotification(noti, db, users);


                    return Ok(true);
                }
                else
                    return Ok("receive null model!");
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        [HttpPost]
        [Route("updateDeal")]
        public IActionResult Update(DealsModel model)
        {
            try
            {
                var data = db.Deal.Where(c => c.Id == model.Id).FirstOrDefault();
                var dealProducts = db.DealProduct.Where(x => x.DealId == model.Id).ToList();
                db.DealProduct.RemoveRange(dealProducts);
                db.SaveChanges();
                if (data != null)
                {
                    data.ActiveToTime = model.ActiveToTime;
                    data.ActiveFromTime = model.ActiveFromTime;
                    data.CategoryId = model.CategoryId;
                    data.ActiveFrom = model.ActiveFrom;
                    data.ActiveTo = model.ActiveTo;
                    data.Discount = model.Discount;
                    data.QuantityPerUser = model.QuantityPerUser;
                    data.DealQty = model.DealQty;
                    data.SoldQty = model.SoldQty;
                    data.Status = model.Status;
                    data.IsFeatured = model.IsFeatured;
                    data.Name = model.Name;
                    data.ProductCategoryId = model.ProductCategoryId;
                    data.IsActive = true;
                    data.IsShow = model.IsShow;
                    db.SaveChanges();
                    List<DealProduct> dealproduct = new List<DealProduct>();
                    foreach (var item in model.DealProduct)
                    {
                        item.DealId = data.Id;
                    }
                    db.DealProduct.UpdateRange(model.DealProduct);
                    db.SaveChanges();
                    return Ok(true);
                }
                else
                    return Ok(false);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


     

    }
}