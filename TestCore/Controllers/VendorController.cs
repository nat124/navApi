using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using TestCore.Extension_Method;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using TestCore.Helper;

namespace TestCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly PistisContext db;

        public VendorController(PistisContext pistis)
        {
            db = pistis;
        }

        [HttpGet]
        [Route("getVendors")]
        public IActionResult GetVendors(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);

            var data = new List<User>();
            try
            {
                data = db.Users.Where(x => x.IsActive == true && x.RoleId == 2).OrderByDescending(x=>x.Id).ToList().RemoveReferences();
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
                    data = data.Where(v => v.Email.Contains(search, StringComparison.OrdinalIgnoreCase) || v.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                    Count = data.Count;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
           data= data.OrderByDescending(x => x.Id).ToList();
            var response = new
            {
                data = data.Skip(skipData).Take(pageSize).ToList(),
                count = Count,
            };

            return Ok(response);
        }

        [Route("deleteVendor")]
        public IActionResult deleteVendor(int id)
        {
            var obj = new User();
            try
            {
                if (id > 0)
                {
                    obj = db.Users.Where(x => x.Id == id).FirstOrDefault();
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

        [HttpGet]
        [Route("deactivateVendor")]
        public IActionResult deactivateVendor(int Id)
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


        [Route("getVendor")]
        public IActionResult getVendor(int Id)
        {
            var data = new User();
            var model = new RegisterVendor();
            try
            {
                //data = db.Users.Where(x => x.IsVerified == true && x.RoleId == 2 && x.Id == Id).OrderByDescending(x => x.Id).FirstOrDefault().RemoveRefernces();
                data = db.Users.Where(x => x.IsActive == true && x.RoleId == 2 && x.Id == Id).Include(x => x.Company).OrderByDescending(x => x.Id).FirstOrDefault();

                model.Id = data.Id;
                model.FirstName = data.FirstName;
                model.MiddleName = data.MiddleName;
                model.LastName = data.LastName;
                model.Company = data.Company?.Name;
                model.DisplayName = data.DisplayName;
                model.UserName = data.UserName;
                model.Email = data.Email;
                model.Phone = data.Phone;
                model.DOB = data.DOB;
                model.Address = data.Address;
                model.CountryId = data.CountryId;
                model.CompanyId = data.CompanyId;
                model.StateId = data.StateId;
                model.City = data.City;
                model.PostalCode = data.PostalCode;
                model.LanguageId = data.LanguageId;
                model.GenderId = data.GenderId;
                model.FacebookId = data.FacebookId;
                model.TwitterId = data.TwitterId;
                model.Image = data.Image;
                model.Logo = data.Company?.Logo;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return Ok(model);
        }

        [Route("updateVendor")]
        public async Task<RegisterVendor> updateVendor(RegisterVendor user)
        {
            var getCompany = db.Companies.Where(x => x.Name.ToLower().Trim() == user.Company.ToLower().Trim()).FirstOrDefault();
            var obj = db.Users.Where(x => x.IsActive == true && x.RoleId == 2 && x.Id == user.Id).Include(x => x.Company).FirstOrDefault();
            if (obj.Company != null)
            {
                var com = db.Companies.Where(x => x.Id == obj.CompanyId).FirstOrDefault();
            }
            //else
            //{
            //    getCompany = new Company();
            //    getCompany.Name = user.Company;
            //    JsonResult response1 = null;

            //    if (user.Logo != null)//if new image
            //    {
            //        var imageResponse1 = await S3Service.UploadObject(user.Logo);
            //        response1 = new JsonResult(new object());

            //        if (imageResponse1.Success)
            //            user.Logo = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse1.FileName}";
            //    }
            //    getCompany.Logo = user.Logo;
            //    getCompany.IsActive = true;
            //    db.Companies.Add(getCompany);
            //    db.SaveChanges();
            //}
            if (getCompany == null)
            {
                getCompany = new Company();
                getCompany.Name = user.Company;
                JsonResult response1 = null;

                if (user.Logo != null)//if new image
                {
                    var imageResponse1 = await S3Service.UploadObject(user.Logo);
                    response1 = new JsonResult(new object());

                    if (imageResponse1.Success)
                        user.Logo = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse1.FileName}";
                }
                getCompany.Logo = user.Logo;
                getCompany.IsActive = true;
                db.Companies.Add(getCompany);
                db.SaveChanges();
            }

            JsonResult response = null;

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
                    obj.IsActive = true;
                    obj.IsVerified = true;
                    if (obj.Company == null)
                    {
                        obj.CompanyId = getCompany.Id;
                    }
                    
                    if (user.Image != null && !(user.Image.StartsWith("http")))//if new image
                    {
                        var imageResponse = await S3Service.UploadObject(user.Image);
                        response = new JsonResult(new object());

                        if (imageResponse.Success)
                            obj.Image = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                    }
                    else obj.Image = user.Image ?? null;

                    if (user.Logo != null && !(user.Logo.StartsWith("http")))//if new logo
                    {
                        var imageResponse = await S3Service.UploadObject(user.Logo);
                        response = new JsonResult(new object());

                    if (imageResponse.Success)
                            if(obj.Company!=null)
                       obj.Company.Logo = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                    else
                        getCompany.Logo = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                        db.SaveChanges();
                    }
                  //  else obj.Company.Logo = user.Logo ?? null;

                  //  obj.Company.Name = user.Company ?? null;

                   // db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);

            }
            return user;
        }







        [HttpPost]
        [Route("verifyUser")]
        [EnableCors("EnableCORS")]
        public LoginModel Login([FromBody]UserLogin data)
        {
            try
            {
                LoginModel model = new LoginModel();
                var user = db.Users.Where(x => x.Email == data.username && x.IsVerified == true && x.IsActive == true && x.RoleId==2).Include(x => x.Role).FirstOrDefault();

                if (user != null)
                {
                    var result = CommonFunctions.ValidateUser(user.PasswordHash, user.PasswordSalt, data.password);

                    if (result)
                    {
                        // JWT Token
                        //var token = new JwtTokenBuilder()
                        //          .AddSecurityKey(JwtSecurityKey.Create(_configuration.GetValue<string>("JwtSecretKey")))
                        //          .AddIssuer(_configuration.GetValue<string>("JwtIssuer"))
                        //          .AddAudience(_configuration.GetValue<string>("JwtAudience"))
                        //          .AddExpiry(60)
                        //          .AddClaim("Name", data.username)
                        //          .AddRole(user.Role.Name)
                        //          .Build();
                        //var _refreshTokenObj = new RefreshTokens
                        //{
                        //    Email = user.Email,
                        //    Refreshtoken = Guid.NewGuid().ToString(),
                        //    Revoked = false,
                        //};
                        //db.RefreshTokens.Add(_refreshTokenObj);
                        //db.SaveChanges();



                        //model.Token = token.Value;
                        //model.refreshToken = _refreshTokenObj.Refreshtoken;
                        model.username = user.FirstName;
                        model.roleId = user.RoleId;
                        model.success = true;
                        model.id = user.Id;
                        model.message = "login Successful";
                        //}
                    }

                    else
                    {
                        model.success = false;
                        model.message = "Incorrect Password!";
                    }


                }
                else
                {
                    model.success = false;
                    model.message = "Invalid Vendor!";
                }
                return model;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("getVendordetails")]
        public IActionResult details(int id)
        {
            try
            {
                var users = db.Users.ToList();
                var data = from us in users where us.Id==id
                           join co in db.Companies on us.CompanyId equals co.Id
                           join sa in db.States on us.StateId equals sa.Id
                           join vid in db.VendorIDProof on us.Id equals vid.UserId
                           join ca in db.Countries on us.CountryId equals ca.Id
                           //join doc in db.VendorDocuments on us.Id equals doc.UserId
                           select new
                           {
                               UserId = us.Id,
                               EmailId = us.Email,
                               FirstName = us.FirstName,
                               MiddleName = us.MiddleName,
                               LastName = us.LastName,
                               vendorId = vid.Proof,
                               State = sa.Name,
                               PhoneNo = us.Phone,
                               CountryId = us.CountryId,
                               CompanyName = co.Name,
                               CompanyLogo = co.Logo,
                               City=us.City,
                               DisplayName=us.DisplayName
                               //  venderDoc=doc.Document.ToList()

                           };
                var result = data.ToList();
                var finaldata = result.Where(x => x.UserId == id).FirstOrDefault();

                return Ok(finaldata);


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("getCustomerdetails")]
        public IActionResult getdetails(int Id)
        {
            try
            {
                var users = from us in db.Users
                            where us.Id == Id
                            select new
                            {
                                UserId = us.Id,
                                EmailId = us.Email,
                                FirstName = us.FirstName,
                                MiddleName = us.MiddleName,
                                LastName = us.LastName,
                                PhoneNo = us.Phone,
                                CountryId = us.CountryId,
                                City = us.City,
                                DisplayName = us.DisplayName
                            };
                var data = users.FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("getcountry")]
        public IActionResult getCountry()
        {
            try
            {
                var country = db.Countries.Where(x => x.IsActive == true).ToList();
                return Ok(country);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("saveVendorDetails")]
        public async Task<IActionResult> saveVendor(vendorDetails vendor)
        {
            var message = 0;
            try
            {
                var user = db.Users.Where(x => x.Id == vendor.UserId).FirstOrDefault();
                if (user != null)
                {
                    user.Email = vendor.EmailId;
                    user.FirstName = vendor.FirstName;
                    user.MiddleName = vendor.MiddleName;
                    user.LastName = vendor.LastName;
                    user.Phone = vendor.PhoneNo;
                    if (vendor.State != null)
                    {
                        var state = db.States.Where(x => x.Name.ToLower().Trim() == vendor.State.ToLower().Trim()).FirstOrDefault();
                        if (state == null)
                        {
                            state = new State();
                            state.Name = vendor.State;
                            state.IsActive = true;
                            db.States.Add(state);
                            db.SaveChanges();
                        }
                    }
                    user.IsActive = true;
                    if (vendor.CompanyName != null)
                    {
                        var company = db.Companies.Where(x => x.Name.ToLower().Trim() == vendor.CompanyName.ToLower().Trim()).FirstOrDefault();
                        if (company == null)
                        {
                            company = new Company();
                            var imageResponse1 = await S3Service.UploadObject(vendor.CompanyLogo);
                            var response1 = new JsonResult(new Object());
                            if (imageResponse1.Success)
                            {
                                company.Logo= $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse1.FileName}";
                            }
                                company.Name = vendor.CompanyName;
                            company.IsActive = true;
                            db.Companies.Add(company);
                            db.SaveChanges();
                        }
                        user.CompanyId = company.Id;

                    }
                    user.City = vendor.City;
                    user.DisplayName = vendor.DisplayName;
                    var imageResponse = await S3Service.UploadObject(vendor.VenderId);
                    var response = new JsonResult(new Object());
                    if (imageResponse.Success)
                    {
                        var venderIdproof = new VendorIDProof();
                        venderIdproof.Proof = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                        venderIdproof.UserId = user.Id;
                        db.VendorIDProof.Add(venderIdproof);
                        db.SaveChanges();
                    }
                    if (vendor.vendorDocument != null)
                    {
                        foreach (var item in vendor.vendorDocument)
                        {
                            var imageResponse3 = await S3Service.UploadObject(item);
                            var response3 = new JsonResult(new Object());
                            if (imageResponse3.Success)
                            {
                                var venderdoc = new VendorDocuments();
                                venderdoc.Document = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse3.FileName}";
                                venderdoc.UserId = user.Id;
                                venderdoc.IsActive = true;
                                db.VendorDocuments.Add(venderdoc);
                                db.SaveChanges();
                            }

                        }
                    }
                    message = 1;
                    
                }
               
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(message);
        }
    }
    public class vendorDetails
    {
        public vendorDetails()
        {
            vendorDocument = new List<string>();
        }
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNo { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string DisplayName { get; set; }
        public int CountryId { get; set; }
        public List<string> vendorDocument { get; set; }
        public string VenderId { get; set; }
        public string CompanyLogo { get; set; }
    }
}