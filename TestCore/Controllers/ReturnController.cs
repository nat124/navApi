using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using TestCore.Helper;
using MercadoPago;
using Microsoft.EntityFrameworkCore;
using MercadoPago.Resources;

namespace TestCore.Controllers
{
    [Route("api/return")]
    [EnableCors("EnableCORS")]
    [ApiController]
    public class ReturnController : ControllerBase
    {
        private readonly PistisContext db;
        public ReturnController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getAll")]
        public IActionResult getAll(int page, int pageSize)
        {
            var skipData = pageSize * (page - 1);
            var data = from r in db.Return where r.CheckoutItemId != null
                       join ri in db.ReturnImage on r.Id equals ri.ReturnId
                       join rs in db.ReturnStatus on r.ReturnStatusId equals rs.Id 
                       where r.IsActive == true 
                       select new ReturnListModel()
                       {
                           Id = r.Id,
                           Amount = r.Amount,
                           Description = r.Description,
                           Image = ri.Image,
                           Quantity=r.Quantity,
                           ReturnDate=r.ReturnDate,
                           ReturnNumber=r.ReturnNumber,
                           ReturnStatus=rs.Name,
                           IsPaid=r.IsPaid,
                       };
            var response = new
            {
                data = data.Skip(skipData).Take(pageSize).OrderByDescending(c => c.Id).ToList(),
                count = data.Count()
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("refund")]
        public IActionResult refund(decimal amount,int id)
        {
            var data = db.Return.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
            if(data!=null)
            {
                var check = db.CheckoutItems.Where(x => x.Id == data.CheckoutItemId).FirstOrDefault();
                if(check!=null)
                {
                    var tran = db.PaymentTransaction.Where(x => x.CheckoutId == check.CheckoutId).FirstOrDefault();
                    if(tran!=null)
                    {
                        var sdk = sdkdata();
                        var refund = new Refund(sdk);

                        Payment payment = new Payment(sdk);
                        var pay = Payment.FindById(Convert.ToInt64(tran.paymentID), sdk);
                        // var refund = new Payment(sdk).Refund();
                        refund.Amount = amount;
                        refund.manualSetPaymentId (Convert.ToDecimal(tran.paymentID));
                        refund.Save();

                        //var r = payment.Refund(amount);
                        if(refund!=null)
                        {
                            try
                            {
                                var model = new PaymentTransaction();
                            model.CheckoutId = tran.CheckoutId;
                            model.intent = "refund";
                            model.payerID = tran.payerID;
                            model.ReturnId = refund.Id.ToString();
                            model.PaymentMethod = tran.PaymentMethod;
                            model.StatusDetail = "approved";
                            model.TransactionAmount = amount;
                            model.UserId = tran.UserId;
                                
                            
                                db.PaymentTransaction.Add(model);
                                db.SaveChanges();

                            
                            //return table
                            data.IsPaid = true;
                            db.Entry(data).State = EntityState.Modified;
                            db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                return Ok(ex);
                            }
                        }
                            return Ok(refund);
                    }
                }
            }
            return Ok();
        }
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveData([FromBody] ReturnModel model)
        {
            var data = new Return();
            data.Description = model.description;
            data.Discount = model.product.Discount;
            data.CheckoutItemId = model.CheckoutItemId;
            var price = model.product.SellingPrice - ((model.product.SellingPrice * model.product.Discount) / 100);
            data.Amount = price * model.Quantity;
            data.IsActive = true;
            data.IsPaid = false;
            data.productVariantDetailId =Convert.ToInt32(model.product.VariantDetailId);
            data.Quantity = model.Quantity;
            data.ReturnDate = DateTime.Now;
            data.ReturnStatusId = 1;
            data.SellingPrice = model.product.SellingPrice;
            data.UnitId = model.product.UnitId;
            //data.VendorId=
            db.Return.Add(data);
            try
            {
                db.SaveChanges();
                data.ReturnNumber = "RET-" + CommonFunctions.RandCode(data.Id);
                db.SaveChanges();
                bool x= await SaveImages(model.images,data.Id);
                
            }
            catch(Exception ex)
            {
                return Ok(false);
            }
            return Ok(true);
        }

        async Task<bool> SaveImages(List<string> Images, int ReturnId)
        {
            try
            {
                foreach (var i in Images)
            {
                var img = new ReturnImage();
                var imageResponse = await S3Service.UploadObject(i);
                var response = new JsonResult(new Object());
                if (imageResponse.Success)
                {
                    img.Image = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                }
                    img.IsActive = true;
                img.ReturnId = ReturnId;
                db.ReturnImage.Add(img);
            }
            
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            { return false; }

        }
        private MercadoPago.SDK sdkdata()
        {
            MercadoPago.SDK sdk = new MercadoPago.SDK();
            sdk.ClientId = "4311750694604144";
            sdk.ClientSecret = "4wI1WNjmrpvA5VbOt3UZSW7bWdXXOjx6";
            sdk.SetAccessToken("APP_USR-4311750694604144-110416-2f54ee29187961161377fb778fd14a89-465370187");

            return sdk;

        }
    }
   
}