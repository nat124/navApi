using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TestCore.Helper;
using Localdb;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using Microsoft.Extensions.Configuration;
using AllModels;
using TestCore.JwtHelpers;
using Microsoft.AspNetCore.Hosting;

namespace TestCore.Controllers
{
    [Route("api/user")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class UserController : ControllerBase
    {
        private readonly IHostingEnvironment environment;
        public IConfiguration _configuration { get; }
        private readonly PistisContext db;
        private IOptions<AppSettings> _settings;
        public UserController(PistisContext pistis, IOptions<AppSettings> settings,IConfiguration configuration, IHostingEnvironment host)
        {
            db = pistis;
            _settings = settings;
            _configuration = configuration;
            environment = host;
        }
        
        //ForLogin
        public User GetUser(User model)
        {
            var user = db.Users.Where(x => x.Email == model.Email && x.IsVerified == true && x.IsActive == true && x.RoleId == 1).FirstOrDefault();
            if (user != null)
                if (CommonFunctions.ValidateUser(user.PasswordHash, user.PasswordSalt, model.Password))
                    return user;
            return null;
        }
        [Route("getCustomer")]
     //  [Authorize(Roles="Admin,Customer")]
        public IActionResult Customer(int Id)
        {
            var data = new User();
            try
            {
                if (Id > 0)
                    data = db.Users.Where(x => x.IsVerified == true && x.RoleId == 1 && x.Id == Id).OrderByDescending(x => x.Id).FirstOrDefault();
                if(data==null)
                    data = db.Users.Where(x => x.IsVerified == true && x.RoleId == 2 && x.Id == Id).OrderByDescending(x => x.Id).FirstOrDefault();
               if(data==null)
                    data = db.Users.Where(x => x.IsVerified == true && x.RoleId == 3 && x.Id == Id).OrderByDescending(x => x.Id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Ok(data);
        }
        [HttpPost]
        [Route("registerCustomer")]
        [EnableCors("EnableCORS")]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomer model)
        {
            var user = new User();
            var salt = CommonFunctions.CreateSalt(64);      //Generate a cryptographic random number.  
            var hashAlgorithm = new SHA512HashAlgorithm();
            var data = db.Users.Where(x => x.Email == model.Email && x.IsActive == true && x.RoleId == 1).FirstOrDefault();
            if (data == null)
            {
                user = new User()
                {
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    PasswordHash = hashAlgorithm.GenerateSaltedHash(CommonFunctions.GetBytes(model.Password), salt),
                    PasswordSalt = salt,
                    RoleId = 1,
                    IsVerified = true,
                    IsActive = true,
                    DateTime = DateTime.Now

                };
                db.Users.Add(user);
                db.SaveChanges();
                MailAddress objFrom = new MailAddress(_settings.Value.ADMINEMAIL, "info@eschedule");
                MailMessage mailMsg = new MailMessage();
                mailMsg.From = objFrom;
                var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/UserRegister.html");

                html = html.Replace("{{userName}}", user.FirstName);
                Emailmodel emailmodel = new Emailmodel();
                emailmodel.From = "";
                emailmodel.To = user.Email;
                emailmodel.Subject = " Congratulations, Registered Successfully";
                emailmodel.Body = html;
                emailmodel.key = "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";
                await Example.Execute(emailmodel);

                user.ReturnCode = 0;
                user.ReturnMessage = "You are registered successfully";
            }
            else
            {
                user.ReturnCode = -1;
                user.ReturnMessage = "Email is already registered";
            }
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("registerForCheckout")]
        [EnableCors("EnableCORS")]
        public IActionResult registerForCheckout(RegisterCustomer model)
        {
            var user = new User();
            var salt = CommonFunctions.CreateSalt(64);      //Generate a cryptographic random number.  
            var hashAlgorithm = new SHA512HashAlgorithm();
            var data = db.Users.Where(x => x.Email == model.Email && x.IsActive == true && x.RoleId == 1).FirstOrDefault();
            if (data == null)
            {
                user = new User()
                {
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    PasswordHash = hashAlgorithm.GenerateSaltedHash(CommonFunctions.GetBytes(model.Password), salt),
                    PasswordSalt = salt,
                    RoleId = 1,
                    IsVerified = true,
                    IsActive = true,
                    DateTime = DateTime.Now

                };
                db.Users.Add(user);
                db.SaveChanges();
                MailAddress objFrom = new MailAddress(_settings.Value.ADMINEMAIL, "info@eschedule");
                MailMessage mailMsg = new MailMessage();
                mailMsg.From = objFrom;
                var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/UserRegister.html");

                html = html.Replace("{{userName}}", user.FirstName);
                Emailmodel emailmodel = new Emailmodel();
                emailmodel.From = "";
                emailmodel.To = user.Email;
                emailmodel.Subject = " Congratulations, Registered Successfully";
                emailmodel.Body = html;
                emailmodel.key = "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";
                Example.Execute(emailmodel);

                //login code
                LoginModel model1 = new LoginModel();
                var user1 = db.Users.Where(x => x.Email == user.Email && x.IsVerified == true && x.IsActive == true).Include(x => x.Role).FirstOrDefault();

                if (user1 != null)
                {
                    var newsIsSucbribed = db.Newsletters.Where(x => (x.Email.Trim() == user1.Email.Trim() && x.IsSubscribed == true) || (x.UserId == user1.Id && x.IsSubscribed == true)).FirstOrDefault();

                    if (newsIsSucbribed != null)
                    {
                        model1.IsSubscribed = 1;
                    }
                    else
                    {
                        model1.IsSubscribed = 0;
                    }
                    var result = true;


                    if (result)
                    {
                        // JWT Token
                        var token = new JwtTokenBuilder()
                                  .AddSecurityKey(JwtSecurityKey.Create(_configuration.GetValue<string>("JwtSecretKey")))
                                  .AddIssuer(_configuration.GetValue<string>("JwtIssuer"))
                                  .AddAudience(_configuration.GetValue<string>("JwtAudience"))
                                  .AddExpiry(60)
                                  .AddClaim("Name", user.Email)
                                  .AddRole(user.Role.Name)
                                  .Build();
                        var _refreshTokenObj = new RefreshTokens
                        {
                            Email = user.Email,
                            Refreshtoken = Guid.NewGuid().ToString(),
                            Revoked = false,
                        };
                        db.RefreshTokens.Add(_refreshTokenObj);
                        db.SaveChanges();



                        model1.Token = token.Value;
                        model1.refreshToken = _refreshTokenObj.Refreshtoken;
                        model1.username = user.FirstName;
                        model1.roleId = user1.RoleId;
                        model1.success = true;
                        model1.id = user1.Id;
                        model1.message = "login Successful";
                        //}
                    }

                    else
                    {
                        if (user.Email.ToString().Trim() == user.Email.ToString().Trim())
                        {
                            model1.success = false;
                            model1.message = "Invalid password!";
                        }
                        else
                        {
                            model1.success = false;
                            model1.message = "Invalid email address!";
                        }

                    }


                }
                //login code end




                user.ReturnCode = 0;
                user.ReturnMessage = "You are registered successfully";
            }
            else
            {
                user.ReturnCode = -1;
                user.ReturnMessage = "Email is already registered";
            }
            try
            {
                

            }
            catch (Exception ex)
            {

                throw;
            }
            var res = new ResponseModel();
            res.ReturnCode = user.ReturnCode;
            res.ReturnMessage = user.ReturnMessage;
            res.Id = user.Id;
            res.FirstName = user.FirstName;
            res.RoleId = user.RoleId;
            return Ok(res);
        }

        [HttpGet]
        [Route("generateOtp")]
        [EnableCors("EnableCORS")]
        public async Task<IActionResult> generateOtp(int UserId, string Email)
        {
            var message = 0;
            var email = "";
            try
            {
                var check = db.Users.Where(x => x.Id == UserId && x.RoleId == 1 && x.IsActive == true).FirstOrDefault();
                if (check != null)
                {
                    check.Otp = GenerateNumericOTP();
                    db.SaveChanges();
                    if (check != null)
                    {

                        MailAddress objFrom = new MailAddress(_settings.Value.ADMINEMAIL, "info@eschedule");
                        MailMessage mailMsg = new MailMessage();
                        mailMsg.From = objFrom;
                        if (check.Email != null)
                            email= check.Email;
                        else if (Email != null)
                            email=Email;
                       var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/otp.html");
                        html = html.Replace("{{userName}}", check.FirstName);
                        html = html.Replace("{{otp}}", check.Otp);
                        mailMsg.Body = html;
                        mailMsg.Subject = "One Time Password";
                        mailMsg.IsBodyHtml = true;
                       Emailmodel emailmodel = new Emailmodel();
                        emailmodel.From = "";
                        emailmodel.To = email;
                        emailmodel.Subject = "One Time Password";
                     emailmodel.Body= html;
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
                    message = 1;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);

        }

        [Route("changePassword")]
        [EnableCors("EnableCORS")]
        public IActionResult changePassword([FromBody]changePassword changePassword)
        {
            var message = 0;
            try
            {
                if (changePassword.userId > 0)
                {
                    var user = db.Users.Where(x => x.Id == changePassword.userId
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
        [Route("changeEmail")]
        [EnableCors("EnableCORS")]
        public IActionResult changeEmail([FromBody]changePassword changePassword)
        {
            var message = 0;
            try
            {
                var user = db.Users.Where(x => x.Id == changePassword.userId
                && x.IsActive == true
                && x.RoleId == 1 && x.Otp == changePassword.Otp
               )
                .FirstOrDefault();
                if (user != null)
                {
                    if (changePassword.EmailId != null || changePassword.EmailId != "")
                        user.Email = changePassword.EmailId;
                    if (changePassword.phoneNo != null || changePassword.phoneNo != "")
                        user.Phone = changePassword.phoneNo;
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
        [HttpPost]
        [Route("changeUserName")]
        [EnableCors("EnableCORS")]
        public IActionResult changeUserName([FromBody]userData userData)
        {
            var message = 0;
            try
            {
                if (userData.UserId > 0)
                {
                    var user = db.Users.Where(x => x.Id == userData.UserId && x.IsActive == true ).FirstOrDefault();
                    if (user != null)
                    {
                        if (userData.firstName != null)
                            user.FirstName = userData.firstName;
                        if (userData.middleName != null)
                            user.MiddleName = userData.middleName;
                        if (userData.lastName != null)
                            user.LastName = userData.lastName;
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
        [Route("registerVendor")]
        public async Task<User> RegisterVendor(RegisterVendor model)
        {
            JsonResult response = null;
            var user = new User();
            var salt = CommonFunctions.CreateSalt(64);      //Generate a cryptographic random number.  
            var hashAlgorithm = new SHA512HashAlgorithm();
            var data = db.Users.Where(x => (x.Email == model.Email || x.Company.Name==model.Company) && x.IsActive == true && x.RoleId == 2).Include(x=>x.Company).FirstOrDefault();
            if (data == null)
            {
                try {
                    //state
                    var states = db.States.Where(x => x.IsActive == true).ToList();
                    if (model.State != null)
                    {
                        var state = states.Where(x => x.Name.ToLower().Trim().Equals(model.State.ToLower().Trim())).FirstOrDefault();
                        if (state != null)
                        { model.StateId = state.Id; }
                        else
                        {
                            var enter = new State();
                            enter.IsActive = true;
                            enter.Name = model.State;
                            enter.CountryId = Convert.ToInt32(model.CountryId);
                            db.States.Add(enter);
                            db.SaveChanges();
                            model.StateId = enter.Id;
                        }
                    }
                    user = new User()
                    {
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        DisplayName = model.DisplayName,
                        UserName = model.UserName,
                        Address = model.Address,
                        City = model.City,
                        CountryId = model.CountryId,
                        StateId = model.StateId,
                        Email = model.Email,
                        Phone = model.Phone,
                        PasswordHash = hashAlgorithm.GenerateSaltedHash(CommonFunctions.GetBytes(model.Password), salt),
                        PasswordSalt = salt,
                        RoleId = 2,
                        IsVerified = true,
                        IsActive = true,
                        FacebookId = model.FacebookId,
                        TwitterId = model.TwitterId,
                        GenderId = model.GenderId,
                        LanguageId = model.LanguageId,
                        PostalCode = model.PostalCode,
                        DOB = model.DOB,
                        VendorId = "Pistis_sno_" + model.Company,
                        RFC = model.RFC
                    };
                    if (model.Image != null && model.Image!="")
                    {
                        var imageResponse = await S3Service.UploadObject(model.Image);
                        response = new JsonResult(new object());

                        if (imageResponse.Success)
                            user.Image = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                    }
                    var com = new Company();
                    if (model.Logo != null && model.Logo != "")
                    {
                        var imageResponse = await S3Service.UploadObject(model.Logo);
                        response = new JsonResult(new object());

                        if (imageResponse.Success)
                            com.Logo = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                    }
                    com.IsActive = true;
                    com.Name = model.Company;
                    db.Companies.Add(com);
                    db.SaveChanges();
                    user.CompanyId = com.Id;
                    db.Users.Add(user);
                    db.SaveChanges();

                    var proof = new Models.VendorIDProof();
                    if (model.IdProof != null && model.IdProof != "")
                    {
                        var imageResponse = await S3Service.UploadObject(model.IdProof);
                        response = new JsonResult(new object());
                        if (imageResponse.Success)
                        {
                            proof.Proof = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                            proof.UserId = user.Id;
                            proof.IsActive = true;
                            db.VendorIDProof.Add(proof);
                            db.SaveChanges();
                        }
                    }
                    //email
                    if (user.Id != 0)
                    {
                        MailAddress objFrom = new MailAddress(_settings.Value.ADMINEMAIL, "info@eschedule");
                        MailMessage mailMsg = new MailMessage();
                        mailMsg.From = objFrom;
                        var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/vendorRegister.html");

                        html = html.Replace("{{userName}}", user.FirstName);
                        Emailmodel emailmodel = new Emailmodel();
                        emailmodel.From = "";
                        emailmodel.To = user.Email;
                        emailmodel.Subject = " Congratulations, Registered Successfully";
                        emailmodel.Body = html;
                        emailmodel.key = "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";
                        await Example.Execute(emailmodel);
                       
                    }

                    var result = new User();
                    result.ReturnCode = 0;
                    result.ReturnMessage = "You are registered successfully";
                    return result;

                }
                catch(Exception ex)
                {
                    user.ReturnCode = -1;
                    user.ReturnMessage = ex.Message;
                    return user;
                }
                }
            else
            {

                user.ReturnCode = -1;
                if (data.Email == model.Email)
                    user.ReturnMessage = "Email is already registered";
                else if (data.Company.Name == model.Company)
                    user.ReturnMessage = "Company is already registered";
                else
                    user.ReturnMessage = "Something went Wrong";
                return user;
            }

        }




        [HttpPost]
        [Route("verifyUser")]
        [EnableCors("EnableCORS")]
        public LoginModel Login([FromBody]UserLogin data)
        {
            try
            {
                LoginModel model = new LoginModel();
                var user = db.Users.Where(x => x.Email == data.username && x.IsVerified == true && x.IsActive == true).Include(x => x.Role).FirstOrDefault();

                if (user != null)
                {
                    var newsIsSucbribed = db.Newsletters.Where(x => (x.Email.Trim() == user.Email.Trim() && x.IsSubscribed == true) || (x.UserId == user.Id && x.IsSubscribed == true)).FirstOrDefault();

                    if (newsIsSucbribed != null)
                    {
                        model.IsSubscribed = 1;
                    }
                    else
                    {
                        model.IsSubscribed = 0;
                    }
                    var result = CommonFunctions.ValidateUser(user.PasswordHash, user.PasswordSalt, data.password);


                    if (result)
                    {
                        // JWT Token
                        var token = new JwtTokenBuilder()
                                  .AddSecurityKey(JwtSecurityKey.Create(_configuration.GetValue<string>("JwtSecretKey")))
                                  .AddIssuer(_configuration.GetValue<string>("JwtIssuer"))
                                  .AddAudience(_configuration.GetValue<string>("JwtAudience"))
                                  .AddExpiry(60)
                                  .AddClaim("Name", data.username)
                                  .AddRole(user.Role.Name)
                                  .Build();
                        var _refreshTokenObj = new RefreshTokens
                        {
                            Email = user.Email,
                            Refreshtoken = Guid.NewGuid().ToString(),
                            Revoked = false,
                        };
                        db.RefreshTokens.Add(_refreshTokenObj);
                        db.SaveChanges();



                        model.Token = token.Value;
                        model.refreshToken = _refreshTokenObj.Refreshtoken;
                        model.username = user.FirstName;
                        model.roleId = user.RoleId;
                        model.success = true;
                        model.id = user.Id;
                        model.message = "login Successful";
                        //}
                    }

                    else
                    {
                        if (user.Email.ToString().Trim() == data.username.ToString().Trim())
                        {
                            model.success = false;
                            model.message = "Invalid password!";
                        }
                        else
                        {
                            model.success = false;
                            model.message = "Invalid email address!";
                        }
                      
                    }


                }
                return model;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody]UserLogin data)
        {
            var _refreshToken = db.RefreshTokens.SingleOrDefault(m => m.Refreshtoken == data.refreshToken);
            var role = db.Roles.Where(x => x.Id == data.RoleId).FirstOrDefault().Name;
            if (_refreshToken == null)
            {
                return Ok(null);
            }
            var token = new JwtTokenBuilder()
                              .AddSecurityKey(JwtSecurityKey.Create(_configuration.GetValue<string>("JwtSecretKey")))
                              .AddIssuer(_configuration.GetValue<string>("JwtIssuer"))
                              .AddAudience(_configuration.GetValue<string>("JwtAudience"))
                              .AddExpiry(60)
                              .AddClaim("Email", data.username)
                              .AddRole(role)
                              .Build();

            _refreshToken.Refreshtoken = Guid.NewGuid().ToString();
            db.RefreshTokens.Update(_refreshToken);
            db.SaveChanges();
            data.Token = token.Value;
            data.refreshToken = _refreshToken.Refreshtoken;

            return Ok(data);
        }
        [HttpGet]
        [Route("getVendors")]
        [EnableCors("EnableCORS")]
        public IActionResult getVendors()
        {
            
                return Ok(db.Users.Where(x => x.IsVerified == true && x.IsActive == true && x.RoleId == (int)RoleType.Vendor).ToList());
        }
        [HttpGet]
        [Route("changeSalt")]
        public IActionResult changePass(int UserId, string password)
        {
            var message = 0;
            try
            {
                var checkTheif = db.Users.Where(x => x.Id == UserId).FirstOrDefault();
                if (checkTheif != null)
                {
                    if (password != null)
                    {
                        var result = CommonFunctions.ValidateUser(checkTheif.PasswordHash, checkTheif.PasswordSalt, password);
                        if (result)
                        {
                            message = 1;
                        }
                        else
                        {
                            message = 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);

        }

        [HttpGet]
        [Route("checkOtp")]
        public IActionResult checkOtp(int UserId, string password)
        {
            var message = 0;
            try
            {
                var checkTheif = db.Users.Where(x => x.Id == UserId).FirstOrDefault();
                if (checkTheif != null)
                {
                    if (password != null)
                    {
                        var result = checkTheif.Otp == password ? true : false;
                        if (result)
                        {
                            message = 1;
                        }
                        else
                        {
                            message = 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);

        }
        [HttpGet]
        [Route("changeSalt1")]
        public IActionResult changePass1(int UserId, string password)
        {
            var message = 0;
            try
            {
                var checkTheif = db.Users.Where(x => x.Id == UserId).FirstOrDefault();
                if (checkTheif != null)
                {
                    if (password != null)
                    {
                        var result = CommonFunctions.ValidateUser(checkTheif.PasswordHash, checkTheif.PasswordSalt, password);
                        if (result)
                        {
                            message = 1;
                            checkTheif.IsActive = false;
                            db.SaveChanges();
                        }
                        else
                        {
                            message = 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);

        }
        [HttpGet]
        [Route("saveSalt")]
        public IActionResult savePass(int UserId, string password)
        {
            var message = 0;
            try
            {
                var checkTheif = db.Users.Where(x => x.Id == UserId).FirstOrDefault();
                if (checkTheif != null)
                {
                    if (password != null)
                    {
                        var salt = CommonFunctions.CreateSalt(64);      //Generate a cryptographic random number.  
                        var hashAlgorithm = new SHA512HashAlgorithm();
                        checkTheif.PasswordHash = hashAlgorithm.GenerateSaltedHash(CommonFunctions.GetBytes(password), salt);
                        checkTheif.PasswordSalt = salt;
                        message = 1;
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }
        
        [HttpGet]
        [Route("getCustomers")]
        public IActionResult getCustomer(string firstDate, string seconedDate)
        {
            var data = new List<User>();
            var user = new UserInfo();
            user.message = 0;
            try
            {
                var dateeee = DateTime.Now.Day;
                if (firstDate != null && seconedDate != null)
                {
                    var first = Convert.ToDateTime(firstDate);
                    var Seconed = Convert.ToDateTime(seconedDate);
                    data = db.Users.Where(x => x.DateTime >= first && x.DateTime <= Seconed).ToList();
                    user.lenght = data.Count();
                    user.message = 1;
                    var first1 = Convert.ToDateTime(firstDate);
                    var Seconed2 = Convert.ToDateTime(seconedDate);
                    user.date = first1.Date.ToShortDateString() + " to " + Seconed2.Date.ToShortDateString();

                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(user);
        }
        [HttpPost]
        [Route("getCustomers1")]
        public IActionResult getCustomer1([FromBody]date model)
        {
            var data = new List<User>();
            var user = new UserInfo();
            user.message = 0;
            try
            {
                var dateeee = DateTime.Now.Day;
                if (model.firstDate != null && model.seconedDate != null)
                {
                    var first = Convert.ToDateTime(model.firstDate);
                    var Seconed = Convert.ToDateTime(model.seconedDate);
                    data = db.Users.Where(x => x.DateTime.Value.Date >= first.Date && x.DateTime.Value.Date <= Seconed.Date).ToList();
                    user.lenght = data.Count();
                    user.message = 1;
                    var first1 = Convert.ToDateTime(model.firstDate);
                    var Seconed2 = Convert.ToDateTime(model.seconedDate);
                    user.date = first1.Date.ToShortDateString() + " to " + Seconed2.Date.ToShortDateString();

                }

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
            return Ok(user);
        }
        public class date
        {
            public string firstDate { get; set; }
            public string seconedDate { get; set; }
        }
        [HttpGet]
        [Route("getCustomersday")]
        public IActionResult getCustomer123(string DMY)
        {

            var data = new List<User>();
            var user = new UserInfo();
            user.message = 0;
            try
            {
                if (DMY.ToLower() == "day")
                {
                    data = db.Users.Where(x => x.DateTime.Value.Date == DateTime.Now.Date).ToList();
                    if (data.Count > 0)
                    {
                        user.lenght = data.Count();
                        user.message = 1;
                        if (data[0].DateTime != null)
                            user.date = data[0].DateTime.Value.Day + "/" + data[0].DateTime.Value.Year;
                    }
                }
                else if (DMY.ToLower() == "month")
                {
                    data = db.Users.Where(x => x.DateTime.Value.Month == DateTime.Now.Month).ToList();
                    if (data.Count > 0)
                    {
                        user.message = 1;
                        user.lenght = data.Count();
                        if (data[0].DateTime != null)
                            user.date = data[0].DateTime.Value.Month + "/" + data[0].DateTime.Value.Year;
                    }
                }
                else if (DMY.ToLower() == "year")
                {

                    data = db.Users.Where(x => x.DateTime.Value.Year == DateTime.Now.Year).ToList();
                    if (data.Count > 0)
                    {
                        user.message = 1;
                        user.lenght = data.Count();
                        if (data[0].DateTime != null)
                            user.date = data[0].DateTime.Value.Year.ToString();
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(user);
        }
        [HttpGet]
        [Route("getUserOrders")]
        public IActionResult getOrders(string firstDate, string seconedDate)
        {
            var UserOrder = new UserOrders();

            var UserOrdersList = new List<UserOrders>();
            try
            {
                if (firstDate != null && seconedDate != null)
                {
                    var first = Convert.ToDateTime(firstDate);
                    var Seconed = Convert.ToDateTime(seconedDate);
                    var Checkouts = db.Checkouts
                       .Where(x => x.CheckoutDate >= first && x.CheckoutDate <= Seconed)
                        .Include(x => x.User).ToList();
                    var data = from chcekout in Checkouts
                               group chcekout by chcekout.UserId into Amounts
                               select new
                               {
                                   useId = Amounts.Key,
                                   AverageScore = Amounts.Average(x => x.TotalAmount),
                                   totalOrders = Amounts.Count(),
                                   totlAmount = Amounts.Sum(v => v.TotalAmount),
                               };
                    var dat1 = data.ToList();

                    foreach (var item in dat1)
                    {
                        UserOrder = new UserOrders();
                        UserOrder.AvgOrderAmt = item.AverageScore;
                        UserOrder.NoOfOrders = item.totalOrders;
                        UserOrder.TotalOrderAmount = item.totlAmount;
                        if (item.useId > 0)
                        {

                            var username = db.Users.Where(x => x.Id == item.useId).FirstOrDefault().FirstName;
                            if (username != null)
                            {
                                UserOrder.UserName = username;
                            }
                            UserOrder.period = first.Date.ToShortDateString() + " to " + Seconed.Date.ToShortDateString();
                        }
                        UserOrdersList.Add(UserOrder);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(UserOrdersList);
        }
        [Route("getUserOrdersDOY")]
        public IActionResult getOrdersDOY(string DOY)
        {
            var UserOrder = new UserOrders();
            var Checkouts = new List<Checkout>();
            var UserOrdersList = new List<UserOrders>();
            try
            {
                if (DOY != null)
                {
                    if (DOY.ToLower() == "day")
                    {
                        Checkouts = db.Checkouts
                             .Where(x => x.CheckoutDate.Day == DateTime.Now.Date.Day)
                              .Include(x => x.User).ToList();
                    }
                    else if (DOY.ToLower() == "month")
                    {
                        Checkouts = db.Checkouts
                           .Where(x => x.CheckoutDate.Month == DateTime.Now.Date.Month)
                            .Include(x => x.User).ToList();
                    }
                    else if (DOY.ToLower() == "year")
                    {
                        Checkouts = db.Checkouts
                            .Where(x => x.CheckoutDate.Year == DateTime.Now.Date.Year)
                             .Include(x => x.User).ToList();
                    }

                    var data = from chcekout in Checkouts
                               group chcekout by chcekout.UserId into Amounts
                               select new
                               {
                                   useId = Amounts.Key,
                                   AverageScore = Amounts.Average(x => x.TotalAmount),
                                   totalOrders = Amounts.Count(),
                                   totlAmount = Amounts.Sum(v => v.TotalAmount),
                               };
                    var dat1 = data.ToList();

                    foreach (var item in dat1)
                    {
                        UserOrder = new UserOrders();
                        UserOrder.AvgOrderAmt = item.AverageScore;
                        UserOrder.NoOfOrders = item.totalOrders;
                        UserOrder.TotalOrderAmount = item.totlAmount;
                        if (item.useId > 0)
                        {

                            var username = db.Users.Where(x => x.Id == item.useId).FirstOrDefault().FirstName;
                            if (username != null)
                            {
                                UserOrder.UserName = username;
                            }
                            if (DOY.ToLower() == "day")
                                UserOrder.period = DateTime.Now.Day + "/" + DateTime.Now.Year;
                            else if (DOY.ToLower() == "month")
                                UserOrder.period = DateTime.Now.Month + "/" + DateTime.Now.Year;
                            else if (DOY.ToLower() == "year")
                                UserOrder.period = DateTime.Now.Year.ToString();

                        }
                        UserOrdersList.Add(UserOrder);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(UserOrdersList);
        }
    }
    public class UserInfo
    {
        public int userId { get; set; }
        public string EmailId { get; set; }
        public string firstName { get; set; }
        public int lenght { get; set; }
        public string date { get; set; }
        public int message { get; set; }

    }
    public class changePassword
    {
        public int userId { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string Otp { get; set; }
        public string phoneNo { get; set; }
    }
    public class userData
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public int otp { get; set; }
        public string lastName { get; set; }
        public int UserId { get; set; }



    }
    public class UserOrders
    {
        public string period { get; set; }
        public string UserName { get; set; }
        public int NoOfOrders { get; set; }
        public decimal AvgOrderAmt { get; set; }
        public decimal TotalOrderAmount { get; set; }
    }
}
