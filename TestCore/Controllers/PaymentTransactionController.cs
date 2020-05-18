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
namespace TestCore.Controllers
{
    [Route("api/PaymentTransaction")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly PistisContext db;
        public PaymentTransactionController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpPost]
        [Route("saveTransaction")]
        public IActionResult transaction([FromBody]Models.PaymentTransaction paymentTransaction)
         {
            PaymentTransaction model = new PaymentTransaction();
            try
            {
                if (paymentTransaction != null)
                {
                   
                    model.intent = paymentTransaction.intent;
                    model.FeesAmount = paymentTransaction.FeesAmount;
                    model.orderID = paymentTransaction.orderID;
                    model.payerID = paymentTransaction.payerID;
                    model.paymentID = paymentTransaction.paymentID;
                    //model.PaymentMethod = paymentTransaction.PaymentMethod;
                    model.PaymentMethod = "MercadoPago";

                    model.paymentToken = paymentTransaction.paymentToken;
                    model.StatusDetail = paymentTransaction.StatusDetail;
                    model.TransactionAmount = paymentTransaction.TransactionAmount;
                    if(paymentTransaction.UserId>0)
                    model.UserId = paymentTransaction.UserId;
                    db.PaymentTransaction.Add(model);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }
        [HttpPost]
        [Route("saveTransaction2")]
        public IActionResult transaction2([FromBody]Models.PaymentTransaction paymentTransaction)
        {
            PaymentTransaction model = new PaymentTransaction();
            try
            {
                if (paymentTransaction != null)
                {

                    model.intent = paymentTransaction.intent;
                    model.FeesAmount = paymentTransaction.FeesAmount;
                    model.orderID = paymentTransaction.orderID;
                    model.payerID = paymentTransaction.payerID;
                    model.paymentID = paymentTransaction.paymentID;
                    //model.PaymentMethod = paymentTransaction.PaymentMethod;
                    model.PaymentMethod = "Paypal";

                    model.paymentToken = paymentTransaction.paymentToken;
                    model.StatusDetail = paymentTransaction.StatusDetail;
                    model.TransactionAmount = paymentTransaction.TransactionAmount;
                    if (paymentTransaction.UserId > 0)
                        model.UserId = paymentTransaction.UserId;
                    db.PaymentTransaction.Add(model);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }
        [HttpPost]
        [Route("paymentConfig")]
        public async Task<IActionResult> savePaymentConfig(Models.PaymentConfiguration model)
        {
            try
            {
                var obj = await db.PaymentConfiguration.FirstOrDefaultAsync();
                if (obj == null)
                {
                    obj = new PaymentConfiguration();
                    obj.Amount = model.Amount;
                    obj.IsApplied = model.IsApplied;
                    obj.Percentage = model.Percentage;
                    db.PaymentConfiguration.Add(obj);
                    db.SaveChanges();
                }
                else
                {
                    obj.Amount = model.Amount;
                    obj.IsApplied = model.IsApplied;
                    obj.Percentage = model.Percentage;
                    db.SaveChanges();
                }
                return Ok();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

      [HttpGet]
        [Route("getPayConfig")]
        public async Task<IActionResult> getPayConfig()
        {
            try
            {
                var obj = await db.PaymentConfiguration.FirstOrDefaultAsync();
                return Ok(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}