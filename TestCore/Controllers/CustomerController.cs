using Localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using TestCore.Extension_Method;
using System.Net.Mail;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AllModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TestCore.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    
    public class CustomerController : ControllerBase
    {
        private IOptions<AppSettings> _settings;

        private readonly PistisContext db;
        public CustomerController(PistisContext pistis, IOptions<AppSettings> settings)
        {
            db = pistis;
            _settings = settings;
        }

        [Route("getCustomer")]
        public IActionResult Customer(int Id)
        {
            var data = new User();
            try
            {
                data = db.Users.Where(x => x.IsVerified == true && x.RoleId == 1 && x.Id == Id).OrderByDescending(x => x.Id).FirstOrDefault().RemoveRefernces();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("getCustomers")]
        public IActionResult Customers(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);

            var data = new List<User>();
            try
            {
                data = db.Users.Where(x => x.IsVerified == true && x.RoleId == 1).ToList().RemoveReferences();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            int Count = 0;
            if (search == null)
                Count = data.Count;
            else
            {
                try
                {
                    data = data.Where(v => v.Email.Contains(search) || v.FirstName.Contains(search)
                ).ToList();
                    Count = data.Count;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            var response = new
            {
                data = data.Skip(skipData).Take(pageSize).ToList(),
                count = Count,
            };

            return Ok(response);
        }
        [HttpGet]
        [Route("deactivateCustomer")]
        public IActionResult deactivateCustomer(int Id)
        {
            var obj = new User();
            try
            {
                if (Id > 0)
                {
                    obj = db.Users.Where(x => x.Id == Id).FirstOrDefault();
                    if (obj.IsActive == false)
                        obj.IsActive = true;
                    else
                        obj.IsActive = false;

                    db.SaveChanges();
                }
            }
            catch (Exception E)
            {

                Console.Write(E.Message);
            }
            return Ok();
        }
        [Route("AddCustomerToNewsLetter")]
        public IActionResult SubcribeCustomer(int Id)
        {
            var obj = new User();
            try
            {
                if (Id > 0)
                {
                    obj = db.Users.Where(x => x.Id == Id).FirstOrDefault();
                    //if (obj.IsSubscribed == false)
                    //    obj.IsSubscribed = true;
                    //else
                    //    obj.IsSubscribed = false;

                    db.SaveChanges();
                }
            }
            catch (Exception E)
            {

                Console.Write(E.Message);
            }
            return Ok();
        }
        [Route("deleteCustomer")]
        public IActionResult deleteCusomer(int Id)
        {
            var obj = new User();
            try
            {
                if (Id > 0)
                {
                    obj = db.Users.Where(x => x.Id == Id).FirstOrDefault();
                    if (obj.IsVerified == false)
                        obj.IsVerified = true;
                    else
                        obj.IsVerified = false;

                    db.SaveChanges();
                }
            }
            catch (Exception E)
            {

                Console.Write(E.Message);
            }
            return Ok();
        }
        [Route("editCustomer")]
        public User editCustomer(User user)
        {
            var obj = db.Users.Where(x => x.IsVerified == true && x.RoleId == 1 && x.Id == user.Id).FirstOrDefault();

            try
            {
                if (user != null)
                {

                    obj.Address = user.Address;
                    obj.Email = user.Email;
                    obj.Phone = user.Phone;
                    obj.StateId = user.StateId;
                    obj.CountryId = user.CountryId;
                    obj.DOB = user.DOB;
                    obj.City = user.City;
                    obj.LanguageId = user.LanguageId;
                    obj.GenderId = user.GenderId;
                    obj.TwitterId = user.TwitterId;
                    obj.FacebookId = user.FacebookId;
                    obj.PostalCode = user.PostalCode;
                    obj.FirstName = user.FirstName;
                    obj.LastName = user.LastName;
                    obj.DisplayName = user.DisplayName;
                    obj.UserName = user.UserName;
                    obj.MiddleName = user.MiddleName;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);

            }
            return obj;
        }
        [HttpGet]
        [Route("forgotpassword1")]
        public async Task<IActionResult> forgotpassword1(int Id)
        {
            ApiResult result = new ApiResult();
            var user = db.Users.FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                user.Password = CreateRandomPassword(7);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                result.success = true;
                result.message = "your password has been updated successfully.";
                Emailmodel emailmodel = new Emailmodel();
                emailmodel.From = "";
                emailmodel.To = user.Email;
                emailmodel.Subject = "Forgot Password";
                // emailmodel.Body = "As you requested, your password for your account has now been reset.<br/> Your new password is <b>" + user.Password + "</b> .<br/> If it was not at your request, then please contact support immediately.";
                emailmodel.Body = "Dear " + user.FirstName + " Your One Time Password(OTP) for resetting the password for your pistis.com.mx profile is " + user.Otp + "Please enter this code in the OTP code box listed on the page.Note: Please note that this will be valid for the next few minutes only.contact: support @pistis.com.mx if you are unable to reset.Regards,PISTIS Mexico Team";
                emailmodel.key = "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";
                await Example.Execute(emailmodel);
                // MailAddress objFrom = new MailAddress(1.Value.ADMINEMAIL, "info@eschedule");
                MailMessage mailMsg = new MailMessage();
                // mailMsg.From = objFrom;
                mailMsg.To.Add(new MailAddress(user.Email));
                mailMsg.Subject = "Forgot Password";
                mailMsg.IsBodyHtml = true;
                mailMsg.Body = "As you requested, your password for your account has now been reset.<br/> Your new password is <b>" + user.Password + "</b> .<br/> If it was not at your request, then please contact support immediately.";
                //"Dear " + user.FirstName + " " + user.LastName + ",<br/>" + " Your have requested the forgot password, your new password is <b>" + user.Password + "</b>.";

                SmtpClient ObjSmtpClient = new SmtpClient(_settings.Value.SMTPADDRESS, 587);
                ObjSmtpClient.Credentials = new System.Net.NetworkCredential(_settings.Value.SMTPUSERNAME.ToString(), _settings.Value.SMTPPASSWORD.ToString());
                ObjSmtpClient.EnableSsl = true;
                // ObjSmtpClient.UseDefaultCredentials = false;
                ObjSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                try
                {
                    ObjSmtpClient.Send(mailMsg);
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.message = ex.Message;
                }
            }
            else
            {
                result.success = false;
                result.message = "sorry, your account is not valid.";
            }
            return Ok(result);
        }
        [Route("getState")]
        public IActionResult getState()
        {
            var data = new List<State>();
            try
            {
                data = db.States.Where(x => x.IsActive == true).ToList();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Ok(data);
        }
        [Route("getCountry")]
        public IActionResult getCountry()
        {
            var data = new List<Country>();
            try
            {
                data = db.Countries.Where(x => x.IsActive == true).ToList();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Ok(data);
        }
        [Route("getGender")]
        public IActionResult getGender()
        {
            var data = new List<Gender>();
            try
            {
                data = db.Genders.Where(x => x.IsActive == true).ToList();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Ok(data);
        }
        [Route("getLanguage")]
        public IActionResult getLanguage()
        {
            var data = new List<Language>();
            try
            {
                data = db.Languages.Where(x => x.IsActive == true).ToList();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Ok(data);
        }
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
    public class ApiResult
    {
        public bool success { get; set; }
        public string message { get; set; }
    }


}
