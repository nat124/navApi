using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using Localdb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCore;
using TestCore.Controllers;
using System.Threading.Tasks;
using Models;
using Microsoft.Extensions.Options;
using TestCore.Helper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;

namespace Api.Controllers
{
    [Route("api/login")]
    [EnableCors("EnableCORS")]
    public class LoginController : ControllerBase
    {
        private IOptions<AppSettings> _settings;
        private readonly PistisContext db;
        private readonly IHostingEnvironment environment;

        public LoginController(PistisContext pistis, IHostingEnvironment host)
        {
            db = pistis;
            environment = host;
        }
        [HttpGet]
        [Route("checkEmail")]
        public  IActionResult checkEmail(string email)
        {
            var message= 0;
            try
            {
                var checkemail = db.Users.Where(x => x.IsActive == true && x.RoleId == 1 && x.Email.ToLower() == email.ToLower())
                    .FirstOrDefault();
            
                if (checkemail != null)
                {
                    var result =generateOtp(checkemail.Id,checkemail.Email) ;
                    message = 1;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }
        public async Task<IActionResult> generateOtp(int UserId, string Email)
        {
            var email = "";
            var check = new User();
            try
            {
             check = db.Users.Where(x => x.Id == UserId && x.RoleId == 1 && x.IsActive == true).FirstOrDefault();
                if (check != null)
                {
                    check.Otp = GenerateNumericOTP();
                    db.SaveChanges();
                    if (check != null)
                    {
                        var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/otp.html");

                        html = html.Replace("{{userName}}", check.FirstName);
                        html = html.Replace("{{otp}}", check.Otp);

                        Emailmodel emailmodel = new Emailmodel();
                        emailmodel.From = "";
                        emailmodel.To = Email;
                        emailmodel.Subject = "One Time Password";
                        //   emailmodel.Body = "As you requested, your One Time Password for your account has now been reset." + check.Otp + " .If it was not at your request, then please contact support immediately.";
                        emailmodel.Body = html;
                      emailmodel.key = "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";
                     //   SmtpClient ObjSmtpClient = new SmtpClient(_settings.Value.SMTPADDRESS, 587);
                       // ObjSmtpClient.Credentials = new System.Net.NetworkCredential(_settings.Value.SMTPUSERNAME.ToString(), _settings.Value.SMTPPASSWORD.ToString());
                       // ObjSmtpClient.EnableSsl = true;
                       // var key = _settings.Value.SENDGRID_API_KEY;

                        await Example.Execute(emailmodel);
                        //  ObjSmtpClient.UseDefaultCredentials = false;
                      //  ObjSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        try
                        {
                          
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    return Ok(check);
                }
              
            }
            catch (Exception ex)
            {

                throw ex;
              
            }
            return Ok(0);
            
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
        [HttpPost]
        [Route("changePassword")]
        public IActionResult changePassword([FromBody]changePassword changePassword)
        {
            var message = 0;
            try
            {
                if (changePassword.EmailId !=null)
                {
                    var user = db.Users.Where(x =>x.Email.ToLower() ==changePassword.EmailId.ToLower()
                    && x.IsActive == true
                    && x.RoleId == 1
                    && x.Otp == changePassword.Otp)
                    .FirstOrDefault();
                    var salt = CommonFunctions.CreateSalt(64);      //Generate a cryptographic random number.  
                    var hashAlgorithm = new SHA512HashAlgorithm();
                    if (user != null)
                    {
                        user.PasswordHash = hashAlgorithm.GenerateSaltedHash(CommonFunctions.GetBytes(changePassword.Password), salt);
                        user.PasswordSalt = salt;
                        db.SaveChanges();
                        message = 1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }

        [Route("checkotp")]
        [HttpPost]
        public IActionResult changePassword1([FromBody]changePassword changePassword)
        {
            var message = 0;
            try
            {
                if (changePassword.EmailId != null)
                {
                    var user = db.Users.Where(x => x.Email.ToLower() == changePassword.EmailId.ToLower()
                    && x.IsActive == true
                    && x.RoleId == 1
                    && x.Otp == changePassword.Otp)
                    .FirstOrDefault();
                    if (user != null)
                    {
                       
                        message = 1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }



    }
}
