using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Models;
using TestCore.Extension_Method;
using TestCore.Helper;
using NotificationType = TestCore.Helper.NotificationType;

namespace TestCore.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class CheckoutController : ControllerBase
    {
        private readonly IHostingEnvironment environment;
        public IConfiguration _configuration { get; }
        private readonly PistisContext db;
        private IOptions<AppSettings> _settings;

        public CheckoutController(PistisContext pistis, IOptions<AppSettings> settings, IConfiguration configuration, IHostingEnvironment host)
        {
            db = pistis;
            _settings = settings;
            _configuration = configuration;
            environment = host;
        }
        [HttpGet]
        [Route("checkemail")]
        public bool checkemail(string email)
        {
            var data = db.Users.Where(x => x.Email == email && x.IsActive == true &&
            (x.RoleId == Convert.ToInt32(RoleType.Customer) || x.RoleId == Convert.ToInt32(RoleType.Vendor) || x.RoleId == Convert.ToInt32(RoleType.Admin))).FirstOrDefault();
            if (data == null)
            {
                return false;
            }
            else
                return true;//already exsists
        }
        public String GenerateNumericOTP()
        {
            string numbers = "0123456789";
            Random objrandom = new Random();
            string strrandom = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                int temp = objrandom.Next(0, numbers.Length);
                strrandom += temp;
            }
            return strrandom;
        }
        [HttpGet]
        [Route("checkemail1")]
        public  async Task<IActionResult> checkemail1(string email)
        {
            var data = db.Users.Where(x => x.Email == email && x.IsActive == true &&
            (x.RoleId == Convert.ToInt32(RoleType.Customer) || x.RoleId == Convert.ToInt32(RoleType.Vendor) || x.RoleId == Convert.ToInt32(RoleType.Admin))).FirstOrDefault();
            if (data == null)
            {
                return Ok(0);
            }
            else
            {
                data.Otp = GenerateNumericOTP();
                db.SaveChanges();
                if (data != null)
                {

                    MailAddress objFrom = new MailAddress(_settings.Value.ADMINEMAIL, "info@eschedule");
                    MailMessage mailMsg = new MailMessage();
                    mailMsg.From = objFrom;
                    if (data.Email != null)
                        email = data.Email;
                    else if (data.Email != null)
                        email = data.Email;
                    var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/otp.html");
                    html = html.Replace("{{userName}}", data.FirstName);
                    html = html.Replace("{{otp}}", data.Otp);
                    mailMsg.Body = html;
                    mailMsg.Subject = "One Time Password";
                    mailMsg.IsBodyHtml = true;
                    Emailmodel emailmodel = new Emailmodel();
                    emailmodel.From = "";
                    emailmodel.To = email;
                    emailmodel.Subject = "One Time Password";
                    emailmodel.Body = html;
                    emailmodel.key = _settings.Value.SENDGRID_API_KEY;
                    SmtpClient ObjSmtpClient = new SmtpClient(_settings.Value.SMTPADDRESS, 587);
                    ObjSmtpClient.Credentials = new System.Net.NetworkCredential(_settings.Value.SMTPUSERNAME.ToString(), _settings.Value.SMTPPASSWORD.ToString());
                    ObjSmtpClient.EnableSsl = true;
                    var key = _settings.Value.SENDGRID_API_KEY;

                    await Example.Execute(emailmodel);
                    //  ObjSmtpClient.UseDefaultCredentials = false;
                    ObjSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    try
                    {
                        ObjSmtpClient.Send(mailMsg);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return Ok(data.Id);
            }
        }
        [HttpPost]
        [Route("loginCheckout")]
        public int AddCheckout([FromBody] LoginCheckout model)
        {
            foreach (var item in model.Cart)
            {
                item.ShippingPrice = model.ShippingPrice;
                item.ShippingType = model.ShippingType;
                item.DeliveryDate = model.DeliveryDate;
            }
            if (model.CustomerId == 0)//notloggedin
            {
                model.CustomerId = null;
            }
            if (model.Shipping != null&& model.SelectedShippingId==0)
            {
                model.SelectedShippingId = SaveShipping(model.Shipping);
            }
            var data = SaveCheckout(model.Cart, model);
            return data;

        }

        [HttpPost]
        [Route("SendDetailsToUsers")]
        public async Task<IActionResult> sendDetailsToUsers([FromBody] LoginCheckout model)
        {
            Sendingdetails sendingdetails = new Sendingdetails();
            DateTime? deliveryDate = null;
            DateTime? purchasedOn = null;
            var orderNo = "";
            var  orderId = "";
            decimal ShippingPrice = 0;
            try
            {
                if (model.Cart[0].OrderDate != null)
                {
                    purchasedOn = model.Cart[0].OrderDate;
                    var checkoutNo = db.Checkouts.Where(x => x.CartId == model.Cart[0].Id && x.IsActive == true).FirstOrDefault();
                    orderNo = checkoutNo.InvoiceNumber;
                    orderId = checkoutNo.Id.ToString();
                }
                if (model.ShippingType != null)
                    sendingdetails.shippingType = model.ShippingType;
                if (model.Cart[0].UserId > 0)
                    sendingdetails.UserName = db.Users.Where(x => x.Id == Convert.ToInt32(model.Cart[0].UserId)).FirstOrDefault()?.FirstName;
                if (model.Cart[0].Image != null)
                    sendingdetails.ImageUrl = model.Cart[0].Image;
                if (model.Cart[0].UserId > 0)
                    sendingdetails.UserEmail = db.Users.Where(x => x.Id == Convert.ToInt32(model.Cart[0].UserId)).FirstOrDefault()?.Email;
                if (model.Cart[0].DeliveryDate != null)
                {
                    deliveryDate = model.DeliveryDate;
                }
                if (model.ShippingPrice > 0)
                {
                    ShippingPrice = model.ShippingPrice;
                }
                var shippingaddress1 = model.Shipping?.Name + model.Shipping?.Colony + model.Shipping?.Street + model.Shipping?.Street1 + model.Shipping?.Street2 + model.Shipping?.City + model.Shipping?.StateName + model.Shipping?.Pincode;

                //    var  shippingaddress1 = model.Shipping?.Name + " " + model.Shipping?.Street != null ? (model.Shipping?.Street1 != null ? model.Shipping?.Street2 : " ") : " "+ model.Shipping?.Colony;
                //  var 
                var ShippingName = model.Shipping?.Name;
                var ShippingStreet = model.Shipping?.Street + " " + model.Shipping?.OutsideNumber + " " + model.Shipping?.InteriorNumber;
                var ShippingStreets = model.Shipping?.Street1 + (model.Shipping?.Street1 == null ? "" : " ") + model.Shipping?.Street2;
                var ShippingColony = model.Shipping?.Colony??"";
                var ShippingCity = model.Shipping?.City + (model.Shipping?.City == null ? "" : " ") + model.Shipping?.State;
                var ShippingPin = model.Shipping?.Pincode;
                var ShippingLandmark = model.Shipping?.LandMark??"";
                var ShippingPhone = model.Shipping?.PhoneNo;
                StringBuilder add = new StringBuilder();
                add.Append(ShippingStreet);
                if (ShippingStreets != "")
                    add.Append("<br>" + ShippingStreet);
                if (ShippingColony != "")
                    add.Append("<br>" + ShippingColony);
                if (ShippingCity != "")
                    add.Append("<br>" + ShippingCity);
                if (ShippingPin != "")
                    add.Append("<br>" + ShippingPin);
                if (ShippingLandmark != "")
                    add.Append("<br>" + ShippingLandmark);
                var ShippingAddress = add.ToString();

                StringBuilder sb = new StringBuilder();
                //Table start.
                sb.Append("<table>");

                //Adding DataRow.
                foreach (var row in model.Cart)
                {
                    var productSku = db.ProductVariantDetails.Where(x => x.Id == row.ProductVariantDetailId).Include(x => x.Product).FirstOrDefault()?.ProductSKU;

                    sb.Append("<tr>");

                    sb.Append(" <td style='width: 70px;'><img style='width:70px;height:70px' src= '" + row.Image150 + "'></td>");
                    sb.Append("<td valign='top' style='font-family: Arial, Helvetica,sans-serif; font-size:15px; line-height:19px;font-weight: 600; color:#269417;text-align:left;white-space:nowrap;padding-top:5px;padding-right:0px;padding-left:10px'>" + 
                        row.Name + "<p style='font-size:13px; color:#303030; margin: 0;'>#" + productSku + "</p> </td>");

                    sb.Append("</tr>");
                }

                //Table end.
                sb.Append("</table>");
                var Products = sb.ToString();


                string productName = "";
                decimal totalAmount = 0;
                foreach (var item in model.Cart)
                {
                    totalAmount += item.Amount;
                    productName = db.ProductVariantDetails.Where(x => x.Id == item.ProductVariantDetailId).Include(x => x.Product).FirstOrDefault()?.Product.Name + ",";
                }
                totalAmount += ShippingPrice;
                // var html = System.IO.File.ReadAllText(@"D:\Vishal\PISTISAPI_core\TestCore\Template\Email.html");
                var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/Email.html");

                html = html.Replace("{{purchasedOn}}", purchasedOn.Value.Date.ToShortDateString());
                html = html.Replace("{{orderNo}}", orderNo);
                html = html.Replace("{{orderID}}", orderId);
                html = html.Replace("{{ShippingName}}", ShippingName);
                html = html.Replace("{{ShippingAddress}}", ShippingAddress);
                html = html.Replace("{{ShippingPhone}}", ShippingPhone);
                html = html.Replace("{{Products}}", Products);



                //html = html.Replace("{{ImageUrl}}", sendingdetails.ImageUrl);
                //html = html.Replace("{{UserName}}", sendingdetails.UserName);
                //html = html.Replace("{{productName}}", productName);
                //html = html.Replace("{{totalAmount}}", totalAmount.ToString());
                //html = html.Replace("{{deliveryDate}}", deliveryDate.Value.Date.ToShortDateString());
                //html = html.Replace("{{shippingType}}", sendingdetails.shippingType);
                //html = html.Replace("{{shippingaddress1}}", shippingaddress1);
                Emailmodel emailmodel = new Emailmodel();
                emailmodel.From = "";
                emailmodel.To = sendingdetails.UserEmail;
                emailmodel.Subject = "Pedido realizado con éxito!";
                //  emailmodel.Body = "Mail Subject : Your order from PISTIS.com.mx of" + productName + "Hi  " + sendingdetails.UserName + "Thanks for your order. Your request will be reviewed against availability of inventory, if confirmed you will receive an email with more details. Additional information .The details of your order are indicated below. Your esitmated delivary date is:"+ deliveryDate + "Shipping Type:"+ sendingdetails.shippingType;
                emailmodel.Body = html;
                emailmodel.key = "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";
                await Example.Execute(emailmodel);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }

        //[HttpPost]
        //[Route("SendDetailsToUsers")]
        //public async Task<IActionResult> sendDetailsToUsers([FromBody] LoginCheckout model)
        //{
        //    Sendingdetails sendingdetails = new Sendingdetails();
        //    DateTime? deliveryDate = null;
        //    decimal ShippingPrice = 0;
        //    try
        //    {
        //        if (model.ShippingType != null)
        //            sendingdetails.shippingType = model.ShippingType;
        //        if (model.Cart[0].UserId > 0)
        //            sendingdetails.UserName = db.Users.Where(x => x.Id == Convert.ToInt32(model.Cart[0].UserId)).FirstOrDefault()?.FirstName;
        //        if (model.Cart[0].Image != null)
        //            sendingdetails.ImageUrl = model.Cart[0].Image;
        //        if (model.Cart[0].UserId > 0)
        //            sendingdetails.UserEmail = db.Users.Where(x => x.Id == Convert.ToInt32(model.Cart[0].UserId)).FirstOrDefault()?.Email;
        //        if (model.Cart[0].DeliveryDate != null)
        //        {
        //            deliveryDate = model.DeliveryDate;
        //        }
        //        if (model.ShippingPrice > 0)
        //        {
        //            ShippingPrice = model.ShippingPrice;
        //        }
        //        var shippingaddress1 = model.Shipping?.Name + model.Shipping?.Colony + model.Shipping?.Street + model.Shipping?.Street1 + model.Shipping?.Street2 + model.Shipping?.City + model.Shipping?.StateName + model.Shipping?.Pincode;

        //        //    var  shippingaddress1 = model.Shipping?.Name + " " + model.Shipping?.Street != null ? (model.Shipping?.Street1 != null ? model.Shipping?.Street2 : " ") : " "+ model.Shipping?.Colony;
        //        //  var sh
        //        string productName = "";
        //        decimal totalAmount = 0;
        //        foreach (var item in model.Cart)
        //        {
        //            totalAmount += item.Amount;
        //            productName = db.ProductVariantDetails.Where(x => x.Id == item.ProductVariantDetailId).Include(x => x.Product).FirstOrDefault()?.Product.Name + ",";
        //        }
        //        totalAmount += ShippingPrice;
        //        // var html = System.IO.File.ReadAllText(@"D:\Vishal\PISTISAPI_core\TestCore\Template\Email.html");
        //        var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/Email.html");

        //        html = html.Replace("{{ImageUrl}}", sendingdetails.ImageUrl);
        //        html = html.Replace("{{UserName}}", sendingdetails.UserName);
        //        html = html.Replace("{{productName}}", productName);
        //        html = html.Replace("{{totalAmount}}", totalAmount.ToString());
        //        html = html.Replace("{{deliveryDate}}", deliveryDate.Value.Date.ToShortDateString());
        //        html = html.Replace("{{shippingType}}", sendingdetails.shippingType);
        //        html = html.Replace("{{shippingaddress1}}", shippingaddress1);
        //        Emailmodel emailmodel = new Emailmodel();
        //        emailmodel.From = "";
        //        emailmodel.To = sendingdetails.UserEmail;
        //        emailmodel.Subject = "Order placed successfully!";
        //        //  emailmodel.Body = "Mail Subject : Your order from PISTIS.com.mx of" + productName + "Hi  " + sendingdetails.UserName + "Thanks for your order. Your request will be reviewed against availability of inventory, if confirmed you will receive an email with more details. Additional information .The details of your order are indicated below. Your esitmated delivary date is:"+ deliveryDate + "Shipping Type:"+ sendingdetails.shippingType;
        //        emailmodel.Body = html;
        //        emailmodel.key = "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";
        //        await Example.Execute(emailmodel);

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    return Ok();
        //}
        [HttpPost]
        [Route("SendDetailsToVendor")]
        public async Task<IActionResult> sendDetailsToVenderos([FromBody] LoginCheckout model)
        {
            Sendingdetails sendingdetails = new Sendingdetails();
            try
            {
                DateTime? deliveryDate = null;
                if (model.ShippingType != null)
                    sendingdetails.shippingType = model.ShippingType;
                if (model.Cart[0].VendorId > 0)
                    sendingdetails.UserName = db.Users.Where(x => x.Id == Convert.ToInt32(model.Cart[0].VendorId)).FirstOrDefault()?.FirstName;
                if (model.Cart[0].Image != null)
                    sendingdetails.ImageUrl = model.Cart[0].Image;
                if (model.Cart[0].VendorId > 0)
                    sendingdetails.UserEmail = db.Users.Where(x => x.Id == Convert.ToInt32(model.Cart[0].VendorId)).FirstOrDefault()?.Email;
                if (model.Cart[0].DeliveryDate != null)
                {
                   deliveryDate = model.Cart[0].DeliveryDate;
                }
                //var shippingaddress1 = model.Shipping?.Name  + model.Shipping?.Street+model.Shipping?.OutsideNumber+model.Shipping?.InteriorNumber + model.Shipping?.Street1 + model.Shipping?.Street2 + model.Shipping?.Colony + model.Shipping?.City + model.Shipping?.StateName + model.Shipping?.Pincode;

                //  var shippingaddress1 = model.Shipping?.Name + " " + model.Shipping?.Street != null ? (model.Shipping?.Street1 != null ? model.Shipping?.Street2 : " ") : " " + model.Shipping?.Colony;

                var shippingName = model.Shipping?.Name;
                var ShippingStreet = model.Shipping?.Street + " " + model.Shipping?.OutsideNumber + " " + model.Shipping?.InteriorNumber;
                var ShippingStreets = model.Shipping?.Street1 + (model.Shipping?.Street1 == null ? "" : " ") + model.Shipping?.Street2;
                var ShippingColony = model.Shipping?.Colony??"";
                var ShippingCity = model.Shipping?.City + (model.Shipping?.City == null ? "" : " ") + model.Shipping?.State;
                var ShippingPin = model.Shipping?.Pincode;
                var ShippingLandmark = model.Shipping?.LandMark??"";
                var ShippingPhone = model.Shipping?.PhoneNo;
                StringBuilder add = new StringBuilder();
                add.Append(ShippingStreet);
                if (ShippingStreets != "")
                    add.Append("<br>" + ShippingStreet);
                if (ShippingColony != "")
                    add.Append("<br>" + ShippingColony);
                if (ShippingCity != "")
                    add.Append("<br>" + ShippingCity);
                if (ShippingPin != "")
                    add.Append("<br>" + ShippingPin);
                if (ShippingLandmark != "")
                    add.Append("<br>" + ShippingLandmark);
                var ShippingAddress = add.ToString();
                string productName = "";
                decimal totalAmount = 0;
                foreach (var item in model.Cart)
                {
                    totalAmount += item.Amount;
                    var quatity = item.Quantity;
                    productName = db.ProductVariantDetails.Where(x => x.Id == item.ProductVariantDetailId).Include(x=>x.Product).FirstOrDefault()?.Product?.Name ;
                    var  productSku = db.ProductVariantDetails.Where(x => x.Id == item.ProductVariantDetailId).Include(x => x.Product).FirstOrDefault()?.ProductSKU;
                    var productImage = db.ProductImages.Where(x => x.ProductVariantDetailId == item.ProductVariantDetailId && x.IsActive == true).FirstOrDefault()?.ImagePath150x150;
                    var venderName = db.Users.Where(x => x.RoleId == 2 && x.Id == item.VendorId).FirstOrDefault()?.FirstName;
                    var checkoutid = db.Checkouts.Where(x => x.CartId == item.Id && x.IsActive == true).FirstOrDefault().Id;
                    var paymentStatus = db.PaymentTransaction.Where(x => x.CheckoutId == checkoutid).FirstOrDefault().StatusDetail == "accredited" ? "Approved" : "Pending";
                    var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/vemdor.html");
                    var UserName = "";
                    if (model.CustomerId > 0)
                    {
                        var name = db.Users.Where(x => x.IsActive == true && x.Id == model.CustomerId).FirstOrDefault().FirstName;
                        UserName = name;
                        html = html.Replace("{{userName}}", name);
                    }
                    else
                    {
                        UserName = model.Shipping?.Name;
                        html = html.Replace("{{userName}}", model.Shipping?.Name);
                    }
                    html = html.Replace("{{venderName}}", venderName);
                    html = html.Replace("{{productName}}", productName);
                    html = html.Replace("{{productSku}}", productSku);
                    html = html.Replace("{{productImage}}", productImage);
                    html = html.Replace("{{quatity}}", quatity.ToString());
                    html = html.Replace("{{paymentStatus}}", paymentStatus);
                    html = html.Replace("{{deliveryDate}}", deliveryDate.Value.ToShortDateString());
                    html = html.Replace("{{ShippingAddress}}", ShippingAddress);
                    html = html.Replace("{{shippingName}}", shippingName);


                    Emailmodel emailmodel = new Emailmodel();
                    emailmodel.From = "";
                    emailmodel.To = sendingdetails.UserEmail;
                    emailmodel.Subject = " Felicidades !! Usuario " + UserName + "ha comprado " + productName;
                    //   emailmodel.Body = "Subject : Congratulations !! User " + sendingdetails.UserName + " has bought " + productName + "Hello   " + venderName + "Thank you for selling with us, we will like to know you that user " + sendingdetails.UserName + "has bought your products " + productName + "The product need to be shipped today by the end of the day. In case of any delay and emergencies please contact PISTIS Support/Inventory team.Before you shipped the product please don’t forget to read about the terms and conditions for shipping the product so that we can take care of your parcel. (www.pistis,com,mx/termsandconditions)Order details are indicated below:  Product Sku :"+ productSku + "Quantity :"+ quatity+ "Payment Status : Approved You will receive another email with the shipment label that the user has opted for, print the label along with the PISTIS sticker and drop it to the nearest DHL/FedexEx center. In case if you want to schedule a pickup please reach out to our support/inventory team on (55 6269 1919 ) WhatsApp : 33 1559 6751 / 55 4058 9672 Thank you for selling with us!!!!!!!!!! Regards,TEAM PISTIS Mexico Comprar Con Confianza";
                    emailmodel.Body = html;
                    emailmodel.key = "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";
                    await Example.Execute(emailmodel);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }
        [HttpPost]
        [Route("sendDetailsTopranjal")]
        public async Task<IActionResult> sendDetailsTopranjal([FromBody] LoginCheckout model)
        {
            Sendingdetails sendingdetails = new Sendingdetails();
            try
            {
                DateTime? deliveryDate = null;
                if (model.ShippingType != null)
                    sendingdetails.shippingType = model.ShippingType;
                if (model.Cart[0].VendorId > 0)
                    sendingdetails.UserName = db.Users.Where(x => x.Id == Convert.ToInt32(model.Cart[0].VendorId)).FirstOrDefault()?.FirstName;
                if (model.Cart[0].Image != null)
                    sendingdetails.ImageUrl = model.Cart[0].Image;
                if (model.Cart[0].VendorId > 0)
                    sendingdetails.UserEmail = db.Users.Where(x => x.Id == Convert.ToInt32(model.Cart[0].VendorId)).FirstOrDefault()?.Email;
                if (model.Cart[0].DeliveryDate != null)
                {
                    deliveryDate = model.Cart[0].DeliveryDate;
                }
                //var shippingaddress1 = model.Shipping?.Name  + model.Shipping?.Street+model.Shipping?.OutsideNumber+model.Shipping?.InteriorNumber + model.Shipping?.Street1 + model.Shipping?.Street2 + model.Shipping?.Colony + model.Shipping?.City + model.Shipping?.StateName + model.Shipping?.Pincode;

                //  var shippingaddress1 = model.Shipping?.Name + " " + model.Shipping?.Street != null ? (model.Shipping?.Street1 != null ? model.Shipping?.Street2 : " ") : " " + model.Shipping?.Colony;

                var shippingName = model.Shipping?.Name;
                var ShippingStreet = model.Shipping?.Street + " " + model.Shipping?.OutsideNumber + " " + model.Shipping?.InteriorNumber;
                var ShippingStreets = model.Shipping?.Street1 + (model.Shipping?.Street1 == null ? "" : " ") + model.Shipping?.Street2;
                var ShippingColony = model.Shipping?.Colony??"";
                var ShippingCity = model.Shipping?.City + (model.Shipping?.City == null ? "" : " ") + model.Shipping?.State;
                var ShippingPin = model.Shipping?.Pincode;
                var ShippingLandmark = model.Shipping?.LandMark??"";
                var ShippingPhone = model.Shipping?.PhoneNo;
                StringBuilder add = new StringBuilder();
                add.Append(ShippingStreet);
                if (ShippingStreets != "")
                    add.Append("<br>" + ShippingStreet);
                if (ShippingColony != "")
                    add.Append("<br>" + ShippingColony);
                if (ShippingCity != "")
                    add.Append("<br>" + ShippingCity);
                if (ShippingPin != "")
                    add.Append("<br>" + ShippingPin);
                if (ShippingLandmark != "")
                    add.Append("<br>" + ShippingLandmark);
                var ShippingAddress = add.ToString();
                string productName = "";
                decimal totalAmount = 0;
                foreach (var item in model.Cart)
                {
                    totalAmount += item.Amount;
                    var quatity = item.Quantity;
                    productName = db.ProductVariantDetails.Where(x => x.Id == item.ProductVariantDetailId).Include(x => x.Product).FirstOrDefault()?.Product?.Name;
                    var productSku = db.ProductVariantDetails.Where(x => x.Id == item.ProductVariantDetailId).Include(x => x.Product).FirstOrDefault()?.ProductSKU;
                    var productImage = db.ProductImages.Where(x => x.ProductVariantDetailId == item.ProductVariantDetailId && x.IsActive == true).FirstOrDefault()?.ImagePath150x150;
                    var venderName = db.Users.Where(x => x.RoleId == 2 && x.Id == item.VendorId).FirstOrDefault()?.FirstName;
                    var checkoutid = db.Checkouts.Where(x => x.CartId == item.Id && x.IsActive == true).FirstOrDefault().Id;
                    var paymentStatus = db.PaymentTransaction.Where(x => x.CheckoutId == checkoutid).FirstOrDefault().StatusDetail == "accredited" ? "Approved" : "Pending";
                    var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/vemdor.html");
                    var UserName = "";
                    if (model.CustomerId > 0)
                    {
                        var name = db.Users.Where(x => x.IsActive == true && x.Id == model.CustomerId).FirstOrDefault().FirstName;
                        UserName = name;
                        html = html.Replace("{{userName}}", name);
                    }
                    else
                    {
                        UserName = model.Shipping?.Name;
                        html = html.Replace("{{userName}}", model.Shipping?.Name);
                    }

                   
                    html = html.Replace("{{venderName}}", venderName);
                    html = html.Replace("{{productName}}", productName);
                    html = html.Replace("{{productSku}}", productSku);
                    html = html.Replace("{{productImage}}", productImage);
                    html = html.Replace("{{quatity}}", quatity.ToString());
                    html = html.Replace("{{paymentStatus}}", paymentStatus);
                    html = html.Replace("{{deliveryDate}}", deliveryDate.Value.ToShortDateString());
                    html = html.Replace("{{ShippingAddress}}", ShippingAddress);
                    html = html.Replace("{{shippingName}}", shippingName);


                    Emailmodel emailmodel = new Emailmodel();
                    emailmodel.From = "";
                    emailmodel.To = sendingdetails.UserEmail;
                    emailmodel.Subject = " Felicidades !! Usuario " + UserName + "ha comprado " + productName;
                    //   emailmodel.Body = "Subject : Congratulations !! User " + sendingdetails.UserName + " has bought " + productName + "Hello   " + venderName + "Thank you for selling with us, we will like to know you that user " + sendingdetails.UserName + "has bought your products " + productName + "The product need to be shipped today by the end of the day. In case of any delay and emergencies please contact PISTIS Support/Inventory team.Before you shipped the product please don’t forget to read about the terms and conditions for shipping the product so that we can take care of your parcel. (www.pistis,com,mx/termsandconditions)Order details are indicated below:  Product Sku :"+ productSku + "Quantity :"+ quatity+ "Payment Status : Approved You will receive another email with the shipment label that the user has opted for, print the label along with the PISTIS sticker and drop it to the nearest DHL/FedexEx center. In case if you want to schedule a pickup please reach out to our support/inventory team on (55 6269 1919 ) WhatsApp : 33 1559 6751 / 55 4058 9672 Thank you for selling with us!!!!!!!!!! Regards,TEAM PISTIS Mexico Comprar Con Confianza";
                    emailmodel.Body = html;
                    emailmodel.key = "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";
                    await Example.Execute(emailmodel);
                }




            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }

        [HttpPost]
        [Route("registerCheckout")]
        public User RegisterCheckout([FromBody] RegisterCheckout model)
        {
            var user = new User();
            var salt = CommonFunctions.CreateSalt(64);      //Generate a cryptographic random number.  
            var hashAlgorithm = new SHA512HashAlgorithm();
            var data = db.Users.Where(x => x.Email == model.User.Email && x.IsActive == true && x.RoleId == 1).FirstOrDefault();
            if (data == null)
            {
                user.FirstName = model.User.FirstName;
                user.MiddleName = model.User.MiddleName;
                user.LastName = model.User.LastName;
                user.Email = model.User.Email;
                user.Phone = model.User.Phone;
                user.PasswordHash = hashAlgorithm.GenerateSaltedHash(CommonFunctions.GetBytes(model.User.Password), salt);
                user.PasswordSalt = salt;
                user.RoleId = 1;
                user.IsVerified = true;
                user.IsActive = true;
                db.Users.Add(user);
                db.SaveChanges();
                user.ReturnCode = 0;
                //add shipping
                if (model.Shipping != null)
                {
                    model.SelectedShippingId = SaveShipping(model.Shipping);
                }
                //var check = SaveCheckout(model.Cart, Convert.ToInt32(model.SelectedShippingId));
                return user;
            }
            else
            {
                user.ReturnCode = -1;
                user.ReturnMessage = "Email is already registered";
                return user;
            }

        }



        [HttpGet]
        [Route("getAdressByUser")]
        public IActionResult ByUser(int id, string IpAddress)
        {
            try
            {
                var query = db.Shipping.Where(m => m.IsActive == true);
                var data = new List<ShippingModel>();
                if (id > 0)
                {
                    query = query.Where(v => v.UserId == id);
                    //if (IpAddress != null && id <= 0)
                    //    query = query.Where(v => v.IpAddress == IpAddress && v.UserId == 0);


                     data = query.Include(v => v.Country).Include(b => b.State).Select(b => new ShippingModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        UserId = b.UserId,
                        IpAddress = b.IpAddress,
                        AlternatePhoneNo = b.AlternatePhoneNo,
                        AddressType = b.AddressType,
                        Address = b.Address,
                        City = b.City,
                        CountryId = b.CountryId,
                        CountryName = db.Countries.Where(x => x.Id == b.CountryId && x.IsActive == true).FirstOrDefault().Name,
                        IsActive = b.IsActive,
                        LandMark = b.LandMark??"",
                        PhoneNo = b.PhoneNo,
                        Pincode = b.Pincode,
                        //StateId = b.StateId,
                       
                         StateName = b.StateName == null ? "" : b.StateName,
                        IsDefault = b.IsDefault,
                        Street = b.Street,
                        Street1 = b.Street1??"",
                        Street2 = b.Street2??"",
                        Colony = b.Colony??"",
                        InteriorNumber = b.InteriorNumber??"",
                        OutsideNumber = b.OutsideNumber??"",
                    }).ToList();
                }
                //if (data.Count() !=0)
                //    return Ok(data);
                //else
                    return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        [Route("changeDefaultAddress")]
        public Boolean changeDefaultAddress(int id)
        {

            var data = db.Shipping.Where(x => x.Id == id && x.IsActive == true).FirstOrDefault();
            var all = db.Shipping.Where(x => x.UserId == data.UserId && x.IsDefault == true).ToList();
            foreach (var a in all)
            {
                a.IsDefault = false;
            }
            try
            {
                db.SaveChanges();
                if (data != null)
                {
                    data.IsDefault = true;
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

//[HttpPost]
//[Route("Checklog")]
//public IActionResult checkoutLog([FromBody]CheckoutLog model )
//        {
//            try
//            {
//                var logs = new CheckoutLog();
//                logs.UserId = model.UserId;
//                logs.IpAddress = model.IpAddress;
//                logs.CartId = model.CartId;
//                logs.Date = model.Date;
//                logs.StartTime = model.StartTime;
//                logs.Action = model.Action;
//                logs.Error = model.Error;
//                logs.Pass = model.Pass;
//                logs.ShippingCharges = model.ShippingCharges;
//                logs.Step = model.Step;
//                db.CheckoutLog.Add(logs);
//                db.SaveChanges();
//                return Ok();
                
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
       //async Task<string>  sendDEtails(List<GetCart> cart, LoginCheckout dataModel)
       // {
       //     //Update Cart
       //     var shippingid = Convert.ToInt32(dataModel.SelectedShippingId);
       //     var shippingaddress = db.Shipping.Where(x => x.IsActive == true && x.Id == shippingid).FirstOrDefault();
       //   var  shippingaddress1 = shippingaddress.Name + " " + shippingaddress.Street != null ? (shippingaddress.Street1 != null ? shippingaddress.Street2 : " ") : " "+shippingaddress.Colony;
          
       //     // var billAddId = Convert.ToInt32(dataModel.BillingAddressId);
       //  //   var model = db.Carts.Where(x => x.Id == cart[0].Id && x.IsActive == true).FirstOrDefault();
       //   //  bool data = false;
       //   //  bool Variant = false;
       //    // model.IsConvertToCheckout = true;
       //  //   model.AdditionalCost = 0;
       //     try
       //     {
       //       //  db.SaveChanges();
       //         //Add to Checkout
       //         var check = new Checkout();
       //         check.DeliveryDate = cart[0].DeliveryDate;
       //         check.ShippingType = cart[0].ShippingType;
       //       //  check.ShippingPrice = (cart[0].ShippingPrice);
       //        // check.AdditionalCost = cart[0].AdditionalCost ?? 0;
       //       //  check.CheckoutDate = DateTime.Now;
       //       //  check.CartId = cart[0].Id;
       //       //  check.IpAddress = cart[0].IpAddress;
       //       //  check.IsActive = true;
       //      //   check.IsPaid = true;
       //        // check.LoyalityPointsUsed = 0;
       //        // check.PaymentModeId = 1;
       //         check.TotalAmount = cart[0].TotalAmount;
       //         check.UserId = Convert.ToInt32(cart[0].UserId);
       //         var UserName = db.Users.Where(x => x.Id == check.UserId).FirstOrDefault().FirstName;
       //         Emailmodel emailmodel = new Emailmodel();
       //         emailmodel.From = "";
       //         emailmodel.To = Email;
       //         emailmodel.Subject = "One Time Password";
       //         //   emailmodel.Body = "As you requested, your One Time Password for your account has now been reset." + check.Otp + " .If it was not at your request, then please contact support immediately.";
       //         emailmodel.Body = "Mail Subject : Your order from PISTIS.com.mx of"+""+"Dear " + check.FirstName + " Your One Time Password(OTP) for resetting the password for your pistis.com.mx profile is " + check.Otp + "Please enter this code in the OTP code box listed on the page.Note: Please note that this will be valid for the next few minutes only.contact: support @pistis.com.mx if you are unable to reset.Regards,PISTIS Mexico Team";

       //         emailmodel.key = "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";
       //         //   SmtpClient ObjSmtpClient = new SmtpClient(_settings.Value.SMTPADDRESS, 587);
       //         // ObjSmtpClient.Credentials = new System.Net.NetworkCredential(_settings.Value.SMTPUSERNAME.ToString(), _settings.Value.SMTPPASSWORD.ToString());
       //         // ObjSmtpClient.EnableSsl = true;
       //         // var key = _settings.Value.SENDGRID_API_KEY;

       //         await Example.Execute(emailmodel);
       //         //  check.ShippingId = shippingid;
       //         // check.BillingAddressId = billAddId;
       //         //   db.Checkouts.Add(check);

       //         //   db.SaveChanges();
       //         //  check.InvoiceNumber = "INV" + CommonFunctions.RandCode(check.Id);
       //         //  db.SaveChanges();


       //         //Add to checkoutitems
       //         //foreach (var c in cart)
       //         //{
       //         //    var item = new CheckoutItem();
       //         //    item.Amount = c.Amount;
       //         //    item.CheckoutId = check.Id;
       //         //    item.IsActive = true;
       //         //    item.OrderStatusId = 1;
       //         //    item.ProductVariantDetailId = c.ProductVariantDetailId;
       //         //    item.Quantity = c.Quantity;
       //         //    item.ReturnedQuantity = 0;
       //         //    item.UnitId = c.UnitId;
       //         //    item.UnitPrice = c.SellingPrice;
       //         //    item.VendorId = c.VendorId ?? 0;
       //         //    db.CheckoutItems.Add(item);
       //         //    db.SaveChanges();
       //         //    data = AddStockTransaction(c.ProductVariantDetailId, c.Quantity);
       //         //    Variant = UpdateProductVariantDetail(c.ProductVariantDetailId, c.Quantity);
       //         //    Variant = true;
       //         //}
       //         //if (Variant)
       //         //{
       //         //    //----------adding to trackorder
       //         //    var order = new TrackOrder();
       //         //    order.TrackId = null;
       //         //    order.IsActive = true;
       //         //    order.OrderId = check.Id;
       //         //    order.Status = "Preparing";
       //         //    order.UpdatedDate = System.DateTime.Now;
       //         //    db.TrackOrders.Add(order);
       //         //    db.SaveChanges();

       //         //    var noti = new Notification();
       //         //    noti.CreatedDate = System.DateTime.Now;
       //         //    noti.DeletedDate = null;
       //         //    noti.ReadDate = null;
       //         //    noti.IsRead = false;
       //         //    noti.IsDeleted = false;
       //         //    noti.IsActive = true;
       //         //    noti.NotificationTypeId = Convert.ToInt32(NotificationType.PurchaseOrder);
       //         //    noti.Title = "Order placed successfully";
       //         //    noti.Description = ("Congratulations your order has been placed,your Order number is-" + check.InvoiceNumber).ToString();
       //         //    var TargetURL = db.NotificationTypes.Where(b => b.Id == Convert.ToInt32(NotificationType.PurchaseOrder) && b.IsActive == true)
       //         //         .FirstOrDefault()?.BaseURL + "?orderId=" + check.Id;
       //         //    noti.TargetURL = TargetURL;
       //         //    noti.UserId = check.UserId ?? 0;
       //         //    //----saving notification of purcahse order
       //         //    var status = NotificationHelper.savePurchaseOrderNotification(noti, db);
       //         //    var tran = UpdatePaymentTransaction(check.Id, dataModel.PaymentId);

       //         //    //if (status && tran)
       //         //    //    return true;
       //         //    //else
       //         //    //    return false;
       //         //}
       //         //else
       //         //    return false;

       //     }
       //     catch (Exception ex)
       //     {

       //     }
       //     return false;
       // }
        int SaveCheckout(List<GetCart> cart, LoginCheckout dataModel)
        {
            var spinnerdata = db.SpinUserData.Where(x => x.UserId == Convert.ToInt32(cart[0].UserId)).FirstOrDefault();            if (spinnerdata != null)            {                spinnerdata.IsUsed = false;                db.SaveChanges();            }
            //Update Cart
            var billid = 0;
            if(dataModel.SelectedShippingId==dataModel.BillingAddressId)
            {
                var bill = db.BillingAddress.Where(x => x.IsActive == true && x.Id == dataModel.BillingAddressId).FirstOrDefault();
                if (bill == null)
                {
                    billid = AddBillAddress(Convert.ToInt32(dataModel.SelectedShippingId));
                }
                else
                    billid =Convert.ToInt32( dataModel.BillingAddressId);
            }
            else
            {
                billid = Convert.ToInt32(dataModel.BillingAddressId);
            }
            var shippingid = Convert.ToInt32(dataModel.SelectedShippingId);
            var billAddId = Convert.ToInt32(billid);
            var model = db.Carts.Where(x => x.Id == cart[0].Id && x.IsActive == true).FirstOrDefault();
            bool data = false;
            bool Variant = false;
            model.IsConvertToCheckout = true;
            model.AdditionalCost = 0;
            try
            {
                db.SaveChanges();
                //Add to Checkout
                var check = new Checkout();
                check.DeliveryDate = cart[0].DeliveryDate;
                check.ShippingType = cart[0].ShippingType;
                check.ShippingPrice = (cart[0].ShippingPrice);
                check.AdditionalCost = cart[0].AdditionalCost ?? 0;
                check.CheckoutDate = DateTime.Now;
                check.CartId = cart[0].Id;
                check.IpAddress = cart[0].IpAddress;
                check.IsActive = true;
                check.IsPaid = true;
                check.LoyalityPointsUsed = 0;
                check.PaymentModeId = 1;
                check.TotalAmount = cart[0].TotalAmount;
                check.UserId = Convert.ToInt32(cart[0].UserId);
                check.ShippingId = shippingid;
                check.BillingAddressId = billAddId;
                //EMI
                if (dataModel.CardData != null)
                {
                    check.PaidAmount = dataModel.CardData.InstallmentAmount;
                    check.LastPaymentDate = DateTime.Now;
                    check.NextPaymentDate = DateTime.Now.AddDays(30);
                    check.TotalInstallments = dataModel.CardData.installments;
                    check.InstalmentsPaid = 1;
                    check.InterstRate = dataModel.CardData.InstallmentRate;
                    check.PaymentId = dataModel.PaymentId;
                }
                db.Checkouts.Add(check);

                db.SaveChanges();
                check.InvoiceNumber = "INV" + CommonFunctions.RandCode(check.Id);
                db.SaveChanges();


                //Add to checkoutitems
                var allproducts = db.Products.Where(x => x.IsActive == true && x.IsEnable == true).ToList();
                foreach (var c in cart)
                {
                    var item = new CheckoutItem();
                    item.Amount = c.Amount;
                    item.CheckoutId = check.Id;
                    item.IsActive = true;
                    item.OrderStatusId = 1;
                    item.ProductVariantDetailId = c.ProductVariantDetailId;
                    var p = Convert.ToInt32(db.ProductVariantDetails.Where(x => x.Id == item.ProductVariantDetailId).Include(x => x.Product.ProductCategory).FirstOrDefault().Product?.ProductCategory?.ParentId);
                    var catid = getparentCat(p);
                    item.Commission = GetCommissionByCategoryId(catid);
                    item.Quantity = c.Quantity;
                    item.ReturnedQuantity = 0;
                    item.UnitId = c.UnitId;
                    item.UnitPrice = c.SellingPrice;
                    item.Discount = Convert.ToInt32(c.Discount - c.DealDiscount);
                    item.DealDiscount = c.DealDiscount;
                    item.DealQty = c.DealQuantityPerUser;
                    item.VendorId = allproducts.Where(x=>x.Id==c.ProductId).FirstOrDefault().VendorId;
                    db.CheckoutItems.Add(item);
                    db.SaveChanges();
                    data = AddStockTransaction(c.ProductVariantDetailId, c.Quantity);
                    Variant = UpdateProductVariantDetail(c.ProductVariantDetailId, c.Quantity);
                    Variant = true;
                    //ADD to VendorTransaction
                    var v = new VendorTransaction();
                    v.VendorId = allproducts.Where(x => x.Id == c.ProductId).FirstOrDefault().VendorId;
                    v.AmountPaid = c.SellingPrice * c.Quantity;
                    v.TransactionDate = DateTime.Now;
                    v.IsPaidByPistis = false;
                    v.IsActive = true;
                    db.VendorTransaction.Add(v);
                    db.SaveChanges();
                    //Update Vendor TransactionSummary
                    var vt = db.VendorTransactionSummary.Where(x => x.IsActive == true && x.VendorId == c.VendorId).FirstOrDefault();
                    if(vt==null)
                    {
                        var vts = new VendorTransactionSummary();
                        vts.IsActive = true;
                        vts.ModifyOn = DateTime.Now;
                        vts.VendorId = allproducts.Where(x => x.Id == c.ProductId).FirstOrDefault().VendorId;
                        vts.DueAmount = v.AmountPaid;
                        db.VendorTransactionSummary.Add(vts);
                        db.SaveChanges();
                    }
                    else
                    {
                        vt.DueAmount += v.AmountPaid;
                        db.SaveChanges();
                    }
                }
                
                if (Variant)
                {
                    //----------adding to trackorder
                    var order = new TrackOrder();
                    order.TrackId = null;
                    order.IsActive = true;
                    order.OrderId = check.Id;
                    order.Status = "Preparing";
                    order.UpdatedDate = System.DateTime.Now;
                    db.TrackOrders.Add(order);
                    db.SaveChanges();

                    var noti = new Notification();
                    noti.CreatedDate = System.DateTime.Now;
                    noti.DeletedDate = null;
                    noti.ReadDate = null;
                    noti.IsRead = false;
                    noti.IsDeleted = false;
                    noti.IsActive = true;
                    noti.NotificationTypeId = Convert.ToInt32(NotificationType.PurchaseOrder);
                    noti.Title = "Order placed successfully";
                    noti.SpanishTitle = "Orden colocada correctamente";
                    noti.Description = ("Congratulations your order has been placed,your Order number is-" + check.InvoiceNumber).ToString();
                    noti.SpanishDescription = ("Felicidades su pedido ha sido realizado, su número de pedido es-" + check.InvoiceNumber).ToString();
                    
                    var TargetURL = db.NotificationTypes.Where(b => b.Id == Convert.ToInt32(NotificationType.PurchaseOrder) && b.IsActive == true)
                         .FirstOrDefault()?.BaseURL + "?orderId=" + check.Id;
                    noti.TargetURL = TargetURL;
                    noti.UserId = check.UserId ?? 0;
                    //----saving notification of purcahse order
                    var users = new List<int>();                    users.Add(check.UserId ?? 0);                    var status = NotificationHelper.saveNotification(noti, db, users);
                    var tran = UpdatePaymentTransaction(check.Id, dataModel.PaymentId);

                    if (status && tran)
                        return check.Id;
                    else
                        return 0;
                }
                else
                    return 0;
               

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        bool UpdatePaymentTransaction(int checkoutid, string paymentid)
        {
            var data = db.PaymentTransaction.Where(x => x.paymentID.Equals(paymentid)).FirstOrDefault();
            if (data != null)
            {
                data.CheckoutId = checkoutid;
                data.Date = DateTime.Now;
            }
            try
            {
                db.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        Int32 SaveShipping(Shipping model)
        {
            try
            {
                if (model != null)
                {
                    Shipping shipData = new Shipping();
                    shipData.Name = model.Name;
                    shipData.UserId = model.UserId;
                    shipData.IpAddress = model.IpAddress;
                    shipData.Address = model.Address;
                    shipData.AlternatePhoneNo = model.AlternatePhoneNo;
                    shipData.AddressType = model.AddressType;
                    shipData.City = model.City;
                    shipData.CountryId = model.CountryId;
                    shipData.PhoneNo = model.PhoneNo;
                    shipData.LandMark = model.LandMark;
                    shipData.Pincode = model.Pincode;
                    shipData.StateId = model.StateId;
                    shipData.IsActive = true;
                    db.Shipping.Add(shipData);
                    db.SaveChanges();
                    return shipData.Id;
                }
                else
                    return 0;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        bool AddStockTransaction(int variantDetailId, int quantity)
        {
            var model = new StockTransaction();
            model.IsActive = true;
            model.ProductVariantDetailId = variantDetailId;
            model.Quantity = quantity;
            model.TransactionDate = DateTime.Now;
            model.TransactionTypeId = 1;
            db.StockTransactions.Add(model);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        bool UpdateProductVariantDetail(int Id, int Quantity)
        {
            var data = db.ProductVariantDetails.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefault();
            if (data != null)
            {
                data.InStock = data.InStock - Quantity;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        int getparentCat(int Id)
        {
            var data = db.ProductCategories.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault().RemoveReferences();
            if (data.ParentId == null)
                return data.Id;
            else
                return getparentCat(Convert.ToInt32(data.ParentId));
        }
        int GetCommissionByCategoryId(int id)
        {
            var data = db.ProductCategoryCommission.Where(x => x.IsActive == true && x.ProductCategoryId == id).FirstOrDefault();
            if (data != null)
                return data.Commission;
            else
                return 0;
        }
       int AddBillAddress(int id)
        {
            try
            {
                var model = db.Shipping.Where(v => v.Id == id).FirstOrDefault();
                var oldBillData = db.BillingAddress.Where(v => v.IsActive == true && v.ShippingId == id).FirstOrDefault();
                if (model != null && oldBillData == null)
                {
                    var billData = new BillingAddress();
                    billData.Name = model.Name;
                    billData.UserId = model.UserId;
                    billData.IpAddress = model.IpAddress;
                    billData.Street = model.Street;
                    billData.Street1 = model.Street1;
                    billData.Street2 = model.Street2;
                    billData.OutsideNumber = model.OutsideNumber;
                    billData.InteriorNumber = model.InteriorNumber;
                    billData.City = model.City;
                    billData.PhoneNo = model.PhoneNo;
                    billData.LandMark = model.LandMark;
                    billData.Pincode = model.Pincode;
                    billData.State = model.StateName;
                    billData.Colony = model.Colony;
                    billData.ShippingId = id;
                    billData.IsActive = true;
                    db.BillingAddress.Add(billData);
                    db.SaveChanges();
                    return billData.Id;
                }
                else
                    return oldBillData.Id;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }
    }
    public class Sendingdetails
    {
        public decimal TotalAmount { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string VendorName { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string shippingType { get; set; }


    }
}