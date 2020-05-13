
using MercadoPago;
using MercadoPago.Common;
using MercadoPago.DataStructures.Payment;
using MercadoPago.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Cors;
using Models;
using Localdb;
using Refund = MercadoPago.Resources.Refund;
using TestCore;

[Route("api/payment")]
[ApiController]
[EnableCors("EnableCORS")]
public class PaymentController : ControllerBase
{
    private readonly PistisContext db;
    public PaymentController(PistisContext pistis)
    {
        db = pistis;
    }
    [HttpGet]
    [Route("mercadoPayment")]
    public PaymentTransaction MercadoPayment(string token, float amount, string email, string paymentmethodid, int userid,string description)
    {
        var sdk = sdkdata();
        var url = Url.Action("getall", "country", Request.Scheme, HttpContext.Request.Host.Host);
        var notificationUrl="";
        //if (url.Contains("psapsolutions"))
          //  notificationUrl = "https://psapsolutions.com/api/payment/mercadoPayment";
        //else
            notificationUrl = "https://psapsolutions.com/api/payment/mercadoPayment";

        //notificationUrl = "https://api.sathfere.com/api/payment/mercadoPayment";

        Payment payment = new Payment(sdk)
        {

            TransactionAmount = amount,
            NotificationUrl = notificationUrl,
            Token = token,
            Description = description,
            Installments = 1,
            PaymentMethodId = paymentmethodid ?? "visa",
            Payer = new Payer()
            {
                Email = email
            }
        };

        // Save and posting the payment
        try
        {
            payment.Save();
            if (payment.Errors != null)
            {
                var data = new PaymentTransaction();
                data.StatusDetail = payment.Errors.Value.Message.ToString();
                return data;
                //return null;
            }
            else
            {
                var data = PaymentAdd(payment, userid,1);
                return data;

            }

        }
        catch (Exception ex)
        {
            var data = new PaymentTransaction();
            
            data.StatusDetail = payment.Status.ToString();
            return data;

        }



    }
    [HttpGet]
    [Route("mercadoInstallment")]
    public PaymentTransaction MercadoInstallment(string token, float amount, string email, string PaymentMethodId, int userid,int installments,float? InstallmentAmount,string IssuerId)
    {
        var sdk = sdkdata();
        var url = Url.Action("getall", "country", Request.Scheme, HttpContext.Request.Host.Host);

        var notificationUrl = "";
        if (url.Contains("psapsolutions"))
            notificationUrl = "https://psapsolutions.com/api/payment/mercadoPayment";
       else
            notificationUrl = "https://api.sathfere.com/api/payment/mercadoPayment";

        Payment payment = new Payment(sdk)
        {

            TransactionAmount = amount,
            // NotificationUrl = "https://psapsolutions.com/api/payment/mercadoPayment",
            NotificationUrl = notificationUrl,
            Token = token,
            Description = "Selected pistis items",
            Installments = installments,
            IssuerId = IssuerId,
            PaymentMethodId = PaymentMethodId ?? "visa",
            Payer = new Payer()
            {
                Email = email
            }
        };

        // Save and posting the payment
        try
        {
            payment.Save();
            if (payment.Errors != null)
            {
                var data = new PaymentTransaction();
                data.StatusDetail = payment.Errors.Value.Message.ToString();
                return data;
            }
            else
            {
                var data = PaymentAdd(payment, userid,2);
                return data;

            }

        }
        catch (Exception)
        {
            var data = new PaymentTransaction();

            data.StatusDetail = payment.Status.ToString();
            return data;

        }



    }
   

    [HttpPost]
    [Route("status")]

    public IActionResult Status([FromQuery] Models.Data data, string type)
    {
        var sdk = sdkdata();

        if (type == "payment")
        {
            var pay = Payment.FindById(Convert.ToInt64(data.id), sdk);

            //PaymentAdd(pay,data.UserId);
        }
        return Ok();
    }

    private MercadoPago.SDK sdkdata()
    {
        MercadoPago.SDK sdk = new MercadoPago.SDK();
        sdk.ClientId = "4311750694604144";
        sdk.ClientSecret = "4wI1WNjmrpvA5VbOt3UZSW7bWdXXOjx6";
        var url = Url.Action("getall", "country", Request.Scheme, HttpContext.Request.Host.Host);

        if (url.Contains("psapsolutions"))
           sdk.SetAccessToken("APP_USR-4311750694604144-110416-2f54ee29187961161377fb778fd14a89-465370187");
        else
          sdk.SetAccessToken("TEST-4311750694604144-110416-b80c68faa3dbc7a75f0e3a6211ddb4df-465370187");

       
        return sdk;

    }

    public PaymentTransaction PaymentAdd(Payment pay, int userid,int paymenttype)
    {
        var model = new PaymentTransaction();
        model.FeesAmount = Convert.ToInt32(pay.FeeDetails[0].Amount);
        model.intent = "sale";
        model.orderID = pay.Id.ToString();
        model.payerID = pay.Payer.Id;
        model.paymentID = pay.Id.ToString();
        model.PaymentMethod = pay.PaymentMethodId;
        model.StatusDetail = pay.StatusDetail;
        model.TransactionAmount =Convert.ToDecimal(pay.TransactionAmount);
        model.UserId = userid;
        model.PaymentTypeId = paymenttype;
        try
        {
            db.PaymentTransaction.Add(model);
            db.SaveChanges();


            return model;
        }
        catch (Exception ex)
        {
            return model;
        }
    }
}