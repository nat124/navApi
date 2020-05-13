using Localdb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.Extension_Method;

namespace TestCore.Controllers
{
    [Route("api/Spinner")]
    [ApiController]
    public class SpinnerController : ControllerBase
    {
        private readonly PistisContext db;
        private readonly IHostingEnvironment environment;
        int response = 0;

        public SpinnerController(PistisContext pistis, IHostingEnvironment host)
        {
            db = pistis;
            environment = host;

        }
        [HttpGet]
        [Route("SpinnerOptions")]
        public IActionResult getList()
        {
            try
            {
                var spinnerPromotion = db.SpinnerPromotion.Where(v => v.IsActive == true).Include(c=>c.Mood).AsNoTracking().ToList();
                //var data = db.SpinnerPromotion.Where(v => v.IsActive == true && v.ActiveFrom>DateTime.Now.Date && v.ActiveTo<DateTime.Now.Date ).Include(c => c.Mood).AsNoTracking().ToList();
                //var spinnerPromotion = new List<SpinnerPromotion>();
                //foreach ( var s in data)
                //{
                //    var from = Convert.ToDateTime(s.ActiveFromTime).TimeOfDay;
                //    var to = Convert.ToDateTime(s.ActiveToTime).TimeOfDay;
                //    if (from > DateTime.Now.TimeOfDay && to<=DateTime.Now.TimeOfDay)
                //    {
                //        spinnerPromotion.Add(s);
                //    }
                //}
                var optionPeriods = db.SpinnerOptionsPeriod.Where(v => v.IsActive == true).ToList();

                foreach (var promotion in spinnerPromotion)
                {
                    foreach (var periods in optionPeriods)
                    {
                        if (periods.SpinnerPromotionId == promotion.Id)
                            promotion.SpinnerOptionsPeriod = periods;
                        if (promotion.Mood != null && promotion.Mood.SpinnerPromotion != null)
                            promotion.Mood.SpinnerPromotion = null;
                    }
                }

                //var data = db.SpinnerPromotion.Where(x => x.IsActive == true).Include(c=>c.Mood)
                //    .Include(x => x.SpinnerOptionsPeriod).ToList().Select(v => new SpinnerPromotion
                //    {
                //        Id = v.Id,
                //        ActiveFrom = v.ActiveFrom,
                //        ProductId = v.ProductId,
                //        Description = v.Description,
                //        ProductCategoryId = v.ProductCategoryId,
                //        MoodId = v.MoodId,
                //        MaxQty = v.MaxQty,
                //        IsActive = v.IsActive,
                //        Image = v.Image,
                //        Filterurl = v.Filterurl,
                //        DisplayMessage = v.DisplayMessage,
                //        DiscountPrice = v.DiscountPrice,
                //        ActiveFromTime = v.ActiveFromTime,
                //        ActiveTo = v.ActiveTo,
                //        ActiveToTime = v.ActiveToTime,
                //        CategoryId = v.CategoryId,
                //        DiscountPercentage = v.DiscountPercentage,
                //        Mood = new Mood
                //        {
                //            Id = v.Mood.Id,
                //            IsActive = v.Mood.IsActive,
                //            Name = v.Mood.Name,
                //        },
                //        SpinnerOptionsPeriod = new SpinnerOptionsPeriod
                //        {
                //            Id = v.SpinnerOptionsPeriod.Id,
                //            Chances = v.SpinnerOptionsPeriod.Chances,
                //            IsActive = v.SpinnerOptionsPeriod.IsActive,
                //            SpinnerPromotionId=v.SpinnerOptionsPeriod.SpinnerPromotionId,
                //            Period=v.SpinnerOptionsPeriod.Period,
                //        }
                //    }).ToList();

                return Ok(spinnerPromotion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("SpinnerOptionsFront")]
        public IActionResult getListFront()
        {
            try
            {
                //var spinnerPromotion = db.SpinnerPromotion.Where(v => v.IsActive == true).Include(c => c.Mood).AsNoTracking().ToList();
                var data = db.SpinnerPromotion.Where(v => v.IsActive == true && v.ActiveFrom.Value.Date <= DateTime.Now.Date && v.ActiveTo.Value.Date >= DateTime.Now.Date).Include(c => c.Mood).AsNoTracking().ToList();

                //var data = db.SpinnerPromotion.Where(v => v.IsActive == true && v.ActiveFrom.Value.Year <= DateTime.Now.Year && v.ActiveFrom.Value.Month <= DateTime.Now.Month && v.ActiveFrom.Value.Day <= DateTime.Now.Day && v.ActiveTo.Value.Year >= DateTime.Now.Year && v.ActiveTo.Value.Month >= DateTime.Now.Month && v.ActiveTo.Value.Day >= DateTime.Now.Day).Include(c => c.Mood).AsNoTracking().ToList();
                var spinnerPromotion = new List<SpinnerPromotion>();
                foreach (var s in data)
                {
                    if(s.ActiveFrom.Value.Date==DateTime.Now.Date || s.ActiveTo.Value.Date==DateTime.Now.Date)
                    {
                        var from = Convert.ToDateTime(s.ActiveFromTime).TimeOfDay;
                        var to = Convert.ToDateTime(s.ActiveToTime).TimeOfDay;
                        if (s.ActiveFrom.Value.Date == DateTime.Now.Date && s.ActiveTo.Value.Date == DateTime.Now.Date)
                        {
                            if (from < DateTime.Now.TimeOfDay && to >= DateTime.Now.TimeOfDay)
                            {
                                spinnerPromotion.Add(s);
                            }
                        }
                        else if(s.ActiveFrom.Value.Date == DateTime.Now.Date)
                        {
                            if (from < DateTime.Now.TimeOfDay)
                            {
                                spinnerPromotion.Add(s);
                            }
                        }
                        else if(s.ActiveTo.Value.Date == DateTime.Now.Date)
                        {
                            if (to >= DateTime.Now.TimeOfDay)
                            {
                                spinnerPromotion.Add(s);
                            }
                        }
                    }
                   
                    else
                    {
                        spinnerPromotion.Add(s);
                    }
                }
                var optionPeriods = db.SpinnerOptionsPeriod.Where(v => v.IsActive == true).ToList();

                foreach (var promotion in spinnerPromotion)
                {
                    foreach (var periods in optionPeriods)
                    {
                        if (periods.SpinnerPromotionId == promotion.Id)
                            promotion.SpinnerOptionsPeriod = periods;
                        if (promotion.Mood != null && promotion.Mood.SpinnerPromotion != null)
                            promotion.Mood.SpinnerPromotion = null;
                    }
                }

                return Ok(spinnerPromotion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("FilterSpinnerOptions")]
        public IActionResult filteroptions()
        {
            try
            {
                var chances = db.SpinnerOptionsPeriod.Where(x => x.Chances > 0 && x.IsActive == true).ToList();
                var data = from sp in chances
                           join s in db.SpinnerPromotion on sp.SpinnerPromotionId equals s.Id
                           select new
                           {
                               Description = s.Description,
                               Id = s.Id,
                               MoodId = s.MoodId,
                               SpinnerOptionsPeriodId = sp.Id,
                               SpinnerOptionsChances = sp.Chances
                           };
                var list = data.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("UpdatePromotionChances")]
        public IActionResult updatePromotions(int Id)
        {
            try
            {
                var spinnerOptionChances = db.SpinnerOptionsPeriod.Where(x => x.SpinnerPromotionId == Id).FirstOrDefault();
                if (spinnerOptionChances != null)
                {
                    spinnerOptionChances.Chances = --spinnerOptionChances.Chances;
                    if (spinnerOptionChances.Chances == 0)
                    {
                        spinnerOptionChances.IsActive = false;

                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }

        [HttpGet]
        [Route("DeleteSpinnerOption")]
        public IActionResult deleteSpinnerOption(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    var data = db.SpinnerPromotion.Where(x => x.Id == Id).FirstOrDefault();
                    if (data != null)
                        data.IsActive = false;
                    db.SaveChanges();
                    response = 1;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveSpinnerOptions")]
        public IActionResult savespinnerOptions([FromBody] SpinnerPromotion model)
        {
            var response = 0;
            try
            {
                model.ActiveTo=DateTime.ParseExact(model.ActiveTos, "MM-dd-yyyy", null);
                model.ActiveFrom = DateTime.ParseExact(model.ActiveFroms, "MM-dd-yyyy", null);
                if (model.Id > 0)
                {

                    var data = db.SpinnerPromotion.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (data != null)
                    {

                        data.ActiveFrom = model.ActiveFrom;
                        data.ActiveToTime = model.ActiveToTime;
                        data.ActiveFromTime = model.ActiveFromTime;
                        data.ActiveTo = model.ActiveTo;
                        data.MaxQty = model.MaxQty;
                        data.CategoryId = model.CategoryId;
                        data.Description = model.Description;
                        data.DiscountPercentage = model.DiscountPercentage;
                        data.DiscountPrice = model.DiscountPrice;
                        data.DisplayMessage = model.DisplayMessage;
                        data.Filterurl = model.Filterurl;
                        data.Image = model.Image;
                        data.IsActive = model.IsActive;
                        data.MoodId = model.MoodId;
                        data.ProductCategoryId = model.ProductCategoryId;
                        data.ProductId = model.ProductId;

                        db.SaveChanges();
                        response = 4;
                        return Ok(response);

                    }
                }
                if (model != null)
                {
                    var IsDescriptionExists = db.SpinnerPromotion.Any(x => x.IsActive == true && x.Description == model.Description);
                    if (IsDescriptionExists)
                    {
                        response = -1;
                        return Ok(response);
                    }
                    else
                    {
                       var  data = new SpinnerPromotion();
                        data.ActiveFrom = model.ActiveFrom;
                        data.ActiveToTime = model.ActiveToTime;
                        data.ActiveFromTime = model.ActiveFromTime;
                        data.ActiveTo = model.ActiveTo;
                        data.MaxQty = model.MaxQty;
                        data.CategoryId = model.CategoryId;
                        data.Description = model.Description;
                        data.DiscountPercentage = model.DiscountPercentage;
                        data.DiscountPrice = model.DiscountPrice;
                        data.DisplayMessage = model.DisplayMessage;
                        data.Filterurl = model.Filterurl;
                        data.Image = model.Image;
                        data.IsActive = model.IsActive;
                        data.MoodId = model.MoodId;
                        data.ProductCategoryId = model.ProductCategoryId;
                        data.ProductId = model.ProductId;
                        data.IsActive = true;
                        db.SpinnerPromotion.Add(data);
                        db.SaveChanges();
                        response = 0;
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                response = -2;
                return Ok(response);
                throw;
            }
            return Ok(response = - 3);
        }
        [HttpGet]
        [Route("EditSpinnerOption")]
        public IActionResult editSpinnerOption(int Id)
        {
            var data = new SpinnerPromotion();

            try
            {
                if (Id > 0)
                {
                    data = db.SpinnerPromotion.Where(x => x.Id == Id).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(data);
        }
        [HttpPost]
        [Route("updateSpinnerOptions")]
        public IActionResult updatepinnerOptions([FromBody] SpinnerPromotion model)
        {
            try
            {
                if (model != null)
                {
                    var data = db.SpinnerPromotion.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (data != null)
                    {
                        data.ActiveFrom = model.ActiveFrom;
                        data.ActiveFromTime = model.ActiveFromTime;
                        data.ActiveTo = model.ActiveTo;
                        data.MaxQty = model.MaxQty;
                        data.CategoryId = model.CategoryId;
                        data.Description = model.Description;
                        data.DiscountPercentage = model.DiscountPercentage;
                        data.DiscountPrice = model.DiscountPrice;
                        data.DisplayMessage = model.DisplayMessage;
                        data.Filterurl = model.Filterurl;
                        data.Image = data.Image;
                        data.IsActive = data.IsActive;
                        data.MoodId = data.MoodId;
                        data.ProductCategoryId = model.ProductCategoryId;
                        data.ProductId = model.ProductId;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok();
        }
        [HttpGet]
        [Route("getMoods")]
        public IActionResult getMoods()
        {
            try
            {
                var data = db.Mood.Where(x => x.IsActive == true).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("deleteSpinnerDurationOption")]
        public IActionResult deleteoption(int Id)
        {
            try
            {
                var data = db.SpinnerOptionsPeriod.Where(x => x.Id == Id).FirstOrDefault();
                if (data != null)
                {
                    data.IsActive = false;
                    db.SaveChanges();
                    response = 1;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("SaveSpinnerOptionsDuration")]

        public IActionResult saveSpinnerOption([FromBody]SpinnerOptionsPeriod model)
        {
            try
            {
                if (model != null)
                {
                    var data = new SpinnerOptionsPeriod();
                    data.Chances = model.Chances;
                    data.IsActive = true;
                    data.SpinnerPromotionId = model.SpinnerPromotionId;
                    data.Period = model.Period;
                    data.IsActive = true;
                    db.SpinnerOptionsPeriod.Add(data);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }
        [HttpGet]
        [Route("getSpinnerDurations")]
        public IActionResult getspinneroptionsdurations()
        {
            try
            {
                var spinneroptiondurations = db.SpinnerOptionsPeriod.Where(x => x.IsActive == true).ToList();
                var data = from s in spinneroptiondurations
                           join sp in db.SpinnerPromotion on s.SpinnerPromotionId equals sp.Id
                           select new
                           {
                               Id = s.Id,
                               Description = sp.Description,
                               Period = s.Period,
                               Chances = s.Chances
                           };
                var list = data.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("checkSpinnerOption")]
        public IActionResult checkSpinnerOptios(int Id)
        {
            try
            {
                var data = db.SpinnerOptionsPeriod.Where(x => x.SpinnerPromotionId == Id && x.IsActive == true).FirstOrDefault();
                if (data != null)
                    response = 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(response);
        }
        [HttpGet]
        [Route("getSpinnerOptionDetails")]
        public IActionResult getSpinnerDet(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    var data = db.SpinnerPromotion.Where(x => x.Id == Id).FirstOrDefault();
                    return Ok(data);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }
        [HttpPost]
        [Route("SaveSpinUserData")]
        public async Task<IActionResult> saveDataAsync([FromBody]spin model)
        {
            try
            {
                if (model != null)
                {
                    if (model.UserId > 0)
                    {
                        var UserSpindata = db.SpinUserData.Where(x => x.UserId == model.UserId).FirstOrDefault();
                        if (UserSpindata == null)
                        {

                            var data = new SpinUserData();
                            data.IsActive = true;
                            data.SpinCount = model.SpinCount;
                            data.SpinnerPromotionId = model.SpinnerPromotionId;
                            data.UserId = model.UserId;
                            data.MoodId = model.MoodId;
                            data.IsUsed = false;
                            var date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                            data.SpinDate = date;
                            data.CancelCounter = 0;
                            db.SpinUserData.Add(data);
                            db.SaveChanges();

                        }
                        else
                        {
                            UserSpindata.IsActive = true;
                            UserSpindata.SpinCount = model.SpinCount;
                            UserSpindata.SpinnerPromotionId = model.SpinnerPromotionId;
                            UserSpindata.UserId = model.UserId;
                            UserSpindata.MoodId = model.MoodId;
                            UserSpindata.IsUsed = false;
                            UserSpindata.CancelCounter = 0;
                            var date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                            UserSpindata.SpinDate = date;
                            db.SaveChanges();
                        }
                        if(model.MoodId==3)
                        if (model.SpinnerPromotionId > 0)
                        {
                            var userEmail = db.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
                            var spinnerdata = db.SpinnerPromotion.Where(x => x.Id == model.SpinnerPromotionId).FirstOrDefault();
                            var url = "";
                            if (spinnerdata.CategoryId > 0)
                            {
                                url = "https://www.pistis.com.mx/productcatalogue?Id=" + spinnerdata.CategoryId;
                                if (spinnerdata.ProductCategoryId > 0)
                                {
                                    url = "https://www.pistis.com.mx/productcatalogue?Id=" + spinnerdata.ProductCategoryId;
                                    if (spinnerdata.ProductId > 0)//productvarientId
                                    {
                                        var productId = db.ProductVariantDetails.Where(x => x.Id == spinnerdata.ProductId).FirstOrDefault();
                                        url = "https://www.pistis.com.mx/product-details?Id=" + productId.ProductId + "&variantId=" + productId.Id;

                                    }
                                }
                            }

                            var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/Spinner.html");
                            html = html.Replace("{{url}}", url);
                            html = html.Replace("{{Prizename}}", spinnerdata.Description);

                            Emailmodel emailmodel = new Emailmodel();
                            emailmodel.From = "";
                            emailmodel.To = userEmail?.Email;
                            emailmodel.Subject = "!Felicidades! Has recibido un cupon PISTIS Mexico.";
                            //  emailmodel.Body = "Mail Subject : Your order from PISTIS.com.mx of" + productName + "Hi  " + sendingdetails.UserName + "Thanks for your order. Your request will be reviewed against availability of inventory, if confirmed you will receive an email with more details. Additional information .The details of your order are indicated below. Your esitmated delivary date is:"+ deliveryDate + "Shipping Type:"+ sendingdetails.shippingType;
                            emailmodel.Body = html;
                            emailmodel.key = "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";
                            await Example.Execute(emailmodel);
                        }
                    }
                }
                else
                {


                    var data = new SpinUserData();
                    data.IsActive = true;
                    data.SpinCount = model.SpinCount;
                    data.SpinnerPromotionId = model.SpinnerPromotionId;
                    data.UserId = model.UserId;
                    data.CancelCounter = 0;
                    db.SpinUserData.Add(data);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                return Ok(ex);

            }
            return Ok();
        }
        [HttpGet]
        [Route("CheckUserData")]
        public IActionResult checUser(int UserId)
        {
            var respone = 0;
            try
            {
                var spinnerInformation = db.SpinUserData.Where(x => x.UserId == UserId).FirstOrDefault();
                if (spinnerInformation != null)
                {
                    respone = 1;
                }
                return Ok(respone);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("CheckUserDatacheckout")]
        public IActionResult checUser1(int UserId,int CartId)
        {
            var response = false;
            try
            {
                var spinnerInformation = db.SpinUserData.Where(x => x.UserId == UserId).Include(x=>x.SpinnerPromotion).FirstOrDefault();
                if (spinnerInformation != null)
                {
                    if(spinnerInformation.SpinnerPromotion.CategoryId!=0)
                    {
                        if(spinnerInformation.SpinnerPromotion.ProductCategoryId!=0)
                        {
                            if(spinnerInformation.SpinnerPromotion.ProductId!=0)
                            {
                               response= IsCartContain(spinnerInformation.SpinnerPromotion.CategoryId, spinnerInformation.SpinnerPromotion.ProductCategoryId, spinnerInformation.SpinnerPromotion.ProductId, CartId);
                            }
                            else
                            {
                                response = IsCartContain(spinnerInformation.SpinnerPromotion.CategoryId, spinnerInformation.SpinnerPromotion.ProductCategoryId, 0, CartId);
                            }
                        }
                        else
                        {
                            response = IsCartContain(spinnerInformation.SpinnerPromotion.CategoryId, 0, 0, CartId);
                        }
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       public  bool IsCartContain(int? CategoryId,int? SubCategoryId,int? ProductId,int CartId)
        {
            var cart = db.CartItems.Where(x => x.CartId == CartId && x.IsActive == true).Include(x=>x.ProductVariantDetail.Product.ProductCategory).ToList();
            var cat = db.ProductCategories.Where(x => x.IsActive == true && x.IsShow == true).ToList();

            if (CategoryId!=0)
            {
                if(SubCategoryId!=0)
                {
                    if(ProductId!=0)
                    {
                        if (cart.Any(x => x.ProductVariantDetailId == ProductId))
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        var catIds =new List<Int32>();
                        foreach(var c in cart)
                        {
                            var sub = cat.Where(x => x.Id == c.ProductVariantDetail.Product.ProductCategoryId).FirstOrDefault().Id;
                          
                            catIds.Add(sub);
                        }
                        foreach (var id in catIds)
                        {
                            if (SubCategoryId == id)
                                return true;
                        }
                        return false;
                    }
                }
                else
                {
                    var catIds = new List<Int32>();
                    foreach (var c in cart)
                    {
                        var sub=getparentCat(c.ProductVariantDetail.Product.ProductCategoryId);
                        catIds.Add(Convert.ToInt32(sub));
                    }
                    foreach (var id in catIds)
                    {
                        if (CategoryId == id)
                            return true;
                    }
                    return false;
                }

            }
            return false;
        }
        int getparentCat(int Id)
        {
            var data = db.ProductCategories.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault().RemoveReferences();
            if (data.ParentId == null)
                return data.Id;
            else
                return getparentCat(Convert.ToInt32(data.ParentId));
        }
        [HttpGet]
        [Route("getproductId")]
        public IActionResult getvarientId(int Id)
        {
            try
            {
                var productVairentId = db.ProductVariantDetails.Where(x => x.Id == Id).FirstOrDefault();
                if (productVairentId != null)
                {
                    return Ok(productVairentId.ProductId);
                }
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("CheckSpinChance")]
        public IActionResult checkSpinnerChances(int UserId)
        {
            var spin = 0;
            try
            {
                var spinnerData = db.SpinUserData.ToList();
                var Userdata = spinnerData.Where(x => x.UserId == UserId).FirstOrDefault();
                if (Userdata != null)
                {
                    if (Userdata.MoodId == 3 && Userdata.IsUsed == false)
                    {
                        spin = 0;//nospin
                    }
                    else
                    {
                        spin = 1;//spin
                    };
                    if (Userdata.MoodId == 2)
                    {
                        var datetimeNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        var check24Hours = Convert.ToDateTime(Userdata.SpinDate).AddHours(24);
                        if (datetimeNow >= check24Hours)
                        {
                            spin = 1;//spin
                        }
                        else
                        {
                            spin = 0;//nospin
                        }
                    }
                }
                else
                {
                    spin = 1;//spin

                }
                return Ok(spin);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("cancelCounter")]
        public IActionResult cancelCounterCheck(int userId,int cancelClick)
        {
            var counter = 1;
            var checkUser = new SpinUserData();

            try
            {
                if (userId > 0)
                {
                    checkUser = db.SpinUserData.Where(x => x.UserId == userId).FirstOrDefault();


                    if (checkUser != null)
                    {
                        if (cancelClick > 0)
                        {
                            var count = ++checkUser.CancelCounter ?? 1;
                            checkUser.CancelCounter = count;
                            checkUser.CancelDate = DateTime.Now;
                            checkUser.SpinCount = 0;
                            db.SaveChanges();
                        }
                        counter = checkUser.CancelCounter ?? 1;

                    }
                    else
                    {
                        var model = new SpinUserData();
                        model.CancelCounter = 1;
                        model.IsActive = true;
                        model.IsUsed = false;
                        model.CancelDate = DateTime.Now;
                        model.UserId = userId;
                        db.SpinUserData.Add(model);
                        db.SaveChanges();
                    }
                }
               

                return Ok(counter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
            [HttpGet]
        [Route("CheckSpinnerToDisplay")]
        public IActionResult CheckSpinnerToDisplay(int? userId)
        {
            var checkUser = new SpinUserData();
            var Spinnoptions = db.SpinnerPromotion.Where(x => x.IsActive == true && x.ActiveTo.Value.Date >= DateTime.Now.Date && x.ActiveFrom.Value.Date <= DateTime.Now.Date).ToList();

            if (userId > 0 )
                {
                    checkUser = db.SpinUserData.Where(x => x.UserId == userId).FirstOrDefault();
                }
                else 
                {
                if (Spinnoptions.Count > 0)
                    return Ok(true);
                else
                    return Ok(false);
                }

                if (checkUser != null)
                {
                var date = DateTime.Now.AddHours(-24);
                if (checkUser.CancelCounter >= 3 && checkUser.CancelDate>date)
                    return Ok(false);
                else if(checkUser.CancelCounter >= 3 && checkUser.CancelDate.Value <= date && Spinnoptions.Count>0)
                {
                    checkUser.CancelCounter = 0;
                    checkUser.CancelDate = DateTime.Now;
                    db.SaveChanges();
                    return Ok(true);
                }
                else
                {
                    if(Spinnoptions.Count>0)
                    return Ok(true);
                    else
                        return Ok(false);
                }
                }
                else
            {
                if (Spinnoptions.Count > 0)
                    return Ok(true);
                else
                    return Ok(false);
            }
           
        }
        [HttpGet]
        [Route("getSpinnerReport")]
        public IActionResult getSpinnerReoirt()
        {
            try
            {
                var data1 = db.SpinUserData.ToList();
                var data = from spd in data1
                           join sid in db.SpinnerPromotion on spd.SpinnerPromotionId equals sid.Id
                           join uid in db.Users on spd.UserId equals uid.Id
                           select new
                           {
                               SpinUserDataId = spd.Id,
                               UserId = uid.Id,
                               CancelCounter = spd.CancelCounter??0,
                               MoodId = spd.MoodId,
                               IsUsed = spd.IsUsed,
                               Spindate = spd.SpinDate.Value.Date.ToShortDateString(),
                               UserEmail = uid.Email,
                               Prize= sid.Description,
                               SpinCount= data1.Count()
                           };
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("getSpinnerReportFilter")]
        public IActionResult filteredSpinner(string filter)
        {
            try
            {
                var data = db.SpinUserData.ToList();
                if (filter.ToLower()=="spin") {
                    var spinData = from spd in data
                                   
                               join sid in db.SpinnerPromotion on spd.SpinnerPromotionId equals sid.Id
                               join uid in db.Users on spd.UserId equals uid.Id
                               select new
                               {
                                   SpinUserDataId = spd.Id,
                                   UserId = uid.Id,
                                   CancelCounter = spd.CancelCounter??0,
                                   MoodId = spd.MoodId,
                                   IsUsed = spd.IsUsed,
                                   Spindate = spd.SpinDate.Value.Date.ToShortDateString(),
                                   UserEmail = uid.Email,
                                   Prize = sid.Description,
                                   SpinCount = data.Count()
                               };
                    return Ok(spinData);
                }else if (filter.ToLower() == "won")
                {
                    var spinData = from spd in data
                                   where spd.MoodId==3
                                   join sid in db.SpinnerPromotion on spd.SpinnerPromotionId equals sid.Id
                                   join uid in db.Users on spd.UserId equals uid.Id
                                   select new
                                   {
                                       SpinUserDataId = spd.Id,
                                       UserId = uid.Id,
                                       CancelCounter = spd.CancelCounter ?? 0,
                                       MoodId = spd.MoodId,
                                       IsUsed = spd.IsUsed,
                                       Spindate = spd.SpinDate.Value.Date.ToShortDateString(),
                                       UserEmail = uid.Email,
                                       Prize = sid.Description,
                                       SpinCount = data.Count()
                                   };
                    return Ok(spinData);
                }
                else if (filter.ToLower() == "used")
                {
                    var spinData = from spd in data
                                   where spd.MoodId == 3 && spd.IsUsed==true
                                   join sid in db.SpinnerPromotion on spd.SpinnerPromotionId equals sid.Id
                                   join uid in db.Users on spd.UserId equals uid.Id
                                   select new
                                   {
                                       SpinUserDataId = spd.Id,
                                       UserId = uid.Id,
                                       CancelCounter = spd.CancelCounter ?? 0,
                                       MoodId = spd.MoodId,
                                       IsUsed = spd.IsUsed,
                                       Spindate = spd.SpinDate.Value.Date.ToShortDateString(),
                                       UserEmail = uid.Email,
                                       Prize = sid.Description,
                                       SpinCount = data.Count()
                                   };
                    return Ok(spinData);
                }
                else if (filter.ToLower() == "lost")
                {
                    var spinData = from spd in data
                                   where spd.MoodId == 2
                                   join sid in db.SpinnerPromotion on spd.SpinnerPromotionId equals sid.Id
                                   join uid in db.Users on spd.UserId equals uid.Id
                                   select new
                                   {
                                       SpinUserDataId = spd.Id,
                                       UserId = uid.Id,
                                       CancelCounter = spd.CancelCounter ?? 0,
                                       MoodId = spd.MoodId,
                                       IsUsed = spd.IsUsed,
                                       Spindate = spd.SpinDate.Value.Date.ToShortDateString(),
                                       UserEmail = uid.Email,
                                       Prize = sid.Description,
                                       SpinCount = data.Count()
                                   };
                    return Ok(spinData);
                }
                else if (filter.ToLower() == "used")
                {
                    var spinData = from spd in data
                                   where spd.IsUsed == true
                                   join sid in db.SpinnerPromotion on spd.SpinnerPromotionId equals sid.Id
                                   join uid in db.Users on spd.UserId equals uid.Id
                                   select new
                                   {
                                       SpinUserDataId = spd.Id,
                                       UserId = uid.Id,
                                       CancelCounter = spd.CancelCounter ?? 0,
                                       MoodId = spd.MoodId,
                                       IsUsed = spd.IsUsed,
                                       Spindate = spd.SpinDate.Value.Date.ToShortDateString(),
                                       UserEmail = uid.Email,
                                       Prize = sid.Description,
                                       SpinCount = data.Count()
                                   };
                    return Ok(spinData);
                }


                return Ok();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("Countresults")]
        public IActionResult countSpinner()
        {
            try
            {
                var data = db.SpinUserData.ToList();
                //var counts = from spd in data
                //             group spd by spd.Id into spdata
                //             select new
                //             {
                //                 Title = spdata.Key,
                //                 SpinnedWheel = spdata.Count(x => x.SpinCount > 0),
                //                 UserWonPrize = spdata.Count(x => x.MoodId == 3),
                //                 UserLostprize = spdata.Count(x => x.MoodId == 2),
                //                 UserAvailprize = spdata.Count(x => x.IsUsed == true)
                //             };
                var result = new counts();
                result.SpinnedWheel = data.Count();
                result.UserWonPrize = data.Count(x => x.MoodId == 3);
                result.UserLostprize = data.Count(x => x.MoodId == 2);
                result.UserAvailprize = data.Count(x => x.IsUsed == true);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("getUserCupon")]
        public IActionResult userCupons(int Id)
        {
            try
            {
                var data = db.SpinUserData.Where(x => x.UserId == Id).FirstOrDefault();
                if (data != null)
                {
                    data.IsUsed = true;
                    db.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("removeUserCupon")]
        public IActionResult userCuponss(int Id)
        {
            try
            {
                var data = db.SpinUserData.Where(x => x.UserId == Id).FirstOrDefault();
                if (data != null)
                {
                    data.IsUsed = false;
                    db.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IActionResult> sendemailAsync(string prizename,int Id)
        {
            try
            {
              
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok();
        }
    }
    public class counts
    {
        public int  SpinnedWheel {get;set;}
        public int UserWonPrize { get; set; }
        public int UserLostprize { get; set; }
        public int UserAvailprize { get; set; }
    }
    public class spin
    {
        public int SpinnerPromotionId { get; set; }
        public int? SpinCount { get; set; }
        public int UserId { get; set; }
        public int? MoodId { get; set; }
    }

}