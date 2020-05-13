using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Localdb;
using Models;
using TestCore.Extension_Method;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;

namespace TestCore.Controllers
{
    [Route("api/NewsLetter")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class NewsLetterController : ControllerBase
    {
        private readonly PistisContext db;
        private readonly IHostingEnvironment environment;        public NewsLetterController(PistisContext pistis, IHostingEnvironment host)        {            db = pistis;            environment = host;        }
        [Route("getCustomers")]
        public IActionResult Customers()
        {
            var list = new List<User>();
            try
            {
                var data = from User in db.Users
                           join N in db.Newsletters
                           on User.Id equals N.UserId
                           where User.IsActive == true && N.IsSubscribed == true && User.RoleId == 1
                           select User;
                list = data.ToList().RemoveReferences();
                if (list.Count() == 0)
                {
                    list = db.Users.Where(x => x.IsActive && x.RoleId == 1).ToList().RemoveReferences();
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("checkRegisterIsSubscribed")]
        public IActionResult checkUser(int UserId)
        {
            var response = 0;
            try
            {
                var subscribedUser = db.Newsletters.Where(x => x.UserId == UserId && x.IsSubscribed == true).FirstOrDefault();
                if (subscribedUser != null)
                {
                    response = 1;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // [HttpPost]
        // [Route("addNewsLetter")]
        // public int AddNews([FromBody]SaveNewsLetter formData)
        //{
        //     Newsletter model = new Newsletter();
        //     var message = 0;

        //     try { 
        //     if (formData != null)
        //     {
        //         var confirmEmail = db.Newsletters.Where(x => x.Email == formData.Email && x.IsActive == true).FirstOrDefault();
        //         var data = db.Users.Where(x => x.Id == formData.UserId).FirstOrDefault();

        //         if (confirmEmail != null)
        //         {
        //             if (formData.Email != null && formData.IpAddress != null)
        //             {
        //                 model.Email = formData.Email;
        //                 model.IpAddress = formData.IpAddress;
        //                 model.UserId = formData.UserId;
        //             }

        //             model.IsSubscribed = true;
        //             model.IsActive = true;

        //         }
        //         else
        //         {
        //             if (data != null)
        //             {
        //                 var confirmEmail1 = db.Newsletters.Where(x => x.Email == data.Email).FirstOrDefault();
        //                 if (data.Email != null && data.Email != confirmEmail1?.Email)
        //                 {
        //                     model.Email = data.Email;
        //                     model.IsSubscribed = true;
        //                         model.IpAddress = formData.IpAddress;
        //                         model.IsActive = true;
        //                     model.UserId = data.Id;
        //                     db.Newsletters.Add(model);

        //                 }
        //             }
        //         }
        //         if (data == null && confirmEmail == null)
        //         {
        //             message = 1;
        //             if (formData.Email != null && formData.IpAddress != null)
        //             {
        //                 model.Email = formData.Email;
        //                 model.IpAddress = formData.IpAddress;
        //                     if (formData.UserId > 0)
        //                         model.UserId = formData.UserId;
        //                     else
        //                         model.User = null;
        //             }
        //             model.IsSubscribed = true;
        //             model.IsActive = true;
        //             db.Newsletters.Add(model);

        //         }


        //     }


        //         db.SaveChanges();
        //     }
        //     catch (Exception e)
        //     {
        //         Console.Write(e.Message);
        //     }
        //     return message;
        // }
        [HttpPost]
        [Route("addNewsLetter")]
        public int AddNews([FromBody]SaveNewsLetter formData)
        {
            try
            { int response = 0;
                var data = db.Newsletters.Where(x => x.IsActive == true && x.IsSubscribed == true && x.Email.ToLower().Trim() == formData.Email.ToLower().Trim()).FirstOrDefault();
                    if (data == null)
                {
                   
                    data = new Newsletter();
                    data.Email = formData.Email;
                    data.IpAddress = formData.IpAddress;
                    data.IsActive = true;
                    data.IsSubscribed = true;
                    data.Date = DateTime.Now;
                    db.Newsletters.Add(data);
                    db.SaveChanges();
                    response = 1;
                }
                else
                {
                    response = 0;

                }
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [EnableCors("EnableCORS")]
        [HttpGet]
        [Route("addToNewsLetterId")]
        public int AddNewsById(int Id)
        {
            var message = 0;

            try
            {
            var newsLetter = new Newsletter();
            var check1 = db.Newsletters.Where(x => x.IsActive == true).ToList();
           var check = check1.Where(x => x.UserId == Id).FirstOrDefault();
            if (check == null)
            {
                if (Id > 0)
                {
                    var data = db.Users.Where(x => x.Id == Id).FirstOrDefault();
                    if (data != null)
                    {
                        newsLetter.Email = data.Email;
                        newsLetter.UserId = data.Id;
                        newsLetter.IsSubscribed = true;
                        newsLetter.IsActive = true;
                            newsLetter.Date = DateTime.Now;
                        message = 1;
                    }
                }
            }
            
                db.Newsletters.Add(newsLetter);
                db.SaveChanges();
                return message;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }
        [HttpGet]
        [Route("newsLetterList")]
        public IActionResult NewsList(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);
            var data = new List<Newsletter>();
            try
            {
                data = db.Newsletters.Where(x => x.IsActive == true).Include(x => x.User).OrderByDescending(x => x.Id).ToList().RemoveReferences();
                if (search != null)
                {
                    data = data.Where(c => c.Email.ToLower().Contains(search.ToLower())
                 //    || c.SenderEmail.ToLower().Contains(search.ToLower())
                   // || c.SenderName.ToLower().Contains(search.ToLower())
                  //  || c.TemplateName.ToLower().Contains(search.ToLower())
                   // || c.TemplateSubject.ToLower().Contains(search.ToLower())
                    ).ToList();
                }

                var response = new
                {
                    data = data.Skip(skipData).Take(pageSize).OrderByDescending(c => c.Id).ToList(),
                    count = data.Count
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("subscribeUser")]
        public IActionResult Subscription(int Id)
        {
            var data = new Newsletter();
            if (Id > 0)
            {
                data = db.Newsletters.Where(x => x.Id == Id).FirstOrDefault();
                if (data.IsSubscribed == false)
                    data.IsSubscribed = true;
                else
                    data.IsSubscribed = false;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Ok(data);
        }
        [Route("deleteNewsletter")]
        public IActionResult deleteNewsletter(int Id)
        {
            var data = new Newsletter();
            if (Id > 0)
            {
                data = db.Newsletters.Where(x => x.Id == Id).FirstOrDefault();
                if (data != null)
                    data.IsActive = false;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("checkIsSubscribed")]
        public IActionResult checkUser(string IpAddress)
        {
            var response = 0;
            try
            {
                var subscribedUser = db.Newsletters.Where(x => x.IpAddress == IpAddress && x.IsSubscribed == true).FirstOrDefault();
                if (subscribedUser != null)
                {
                    response = 1;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [Route("SaveImageNewsLetter")]
        public async Task<IActionResult> uploadImageN([FromBody]NewsletterImage model)
        {
            try
            {
                var data = db.NewsletterImage.Where(x => x.IsActive == true).FirstOrDefault();
                if (data != null)
                {
                    data.IsActive = true;
                    data.Description = model.Description;
                    data.HeaderName = model.HeaderName;
                         var imageResponse = await S3Service.UploadObject(model.Image);
                    var response = new JsonResult(new Object());
                    if (imageResponse.Success)
                    {
                        data.Image = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}"; 
                    }
                    db.SaveChanges();
                }
                else
                {
                    var newsletter = new NewsletterImage();
                    var imageResponse = await S3Service.UploadObject(model.Image);
                    var response = new JsonResult(new Object());
                    if (imageResponse.Success)
                    {
                        newsletter.Image = $"https://pistis.s3.us-east-2.amazonaws.com/{imageResponse.FileName}";
                    }
                    newsletter.IsActive = true;
                    newsletter.Description = model.Description;
                    newsletter.HeaderName = model.HeaderName;
                    db.NewsletterImage.Add(newsletter);
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
        [Route("getNewsletterImage")]
        public IActionResult getnewsletterInformation()
        {
            try
            {
                var data = db.NewsletterImage.Where(x => x.IsActive == true).FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("cancelCounter")]
        public IActionResult cancelCounterCheck(int? userId, string IpAddress,int cancelClick)
        {
            var counter = 1;
            var checkUser = new Newsletter();

            try
            {
                if (userId > 0)
                {
                    checkUser = db.Newsletters.Where(x => x.UserId == userId).FirstOrDefault();
                }
                else if (IpAddress != null || IpAddress == "")
                {
                    checkUser = db.Newsletters.Where(x => x.IpAddress == IpAddress).FirstOrDefault();
                }
                if (checkUser != null)
                {
                    if (cancelClick > 0)
                    {
                       var count = ++checkUser.CancelCounter ;
                        checkUser.CancelCounter = count;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var newsletterCancel = new Newsletter();
                    newsletterCancel.IpAddress = IpAddress;
                  
                    newsletterCancel.UserId = userId==0?null:userId;
                    newsletterCancel.CancelCounter = 1;
                    newsletterCancel.IsSubscribed = false;
                    newsletterCancel.IsActive = false;
                    db.Newsletters.Add(newsletterCancel);
                    db.SaveChanges();
                }
                if (checkUser == null)
                {
                    counter = 1;
                }
                else
                {
                    counter = checkUser.CancelCounter;
                }

                return Ok(counter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("getMixEmailids")]
        public IActionResult getMixEmails()
        {
            var data = getEmails();
            try
            {
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("SendEmailsToUsers")]
        public async Task<IActionResult> sendEmails(int Id)
        {
            try
            {
                var templateData = db.Template.Where(x => x.Id == Id).FirstOrDefault();
                if (templateData != null)
                {
                    var data = getEmails();
                    var html = System.IO.File.ReadAllText(environment.WebRootPath + "/Template/Newsletter.html");

                    html = html.Replace("{{TemplateContent}}", templateData.TemplateContent);
                    foreach (var item in data)
                    {
                        Emailmodel emailmodel = new Emailmodel();
                        emailmodel.From = templateData.SenderEmail;
                        emailmodel.Subject = templateData.TemplateSubject;

                        emailmodel.To = item;
                        // emailmodel.Body = "Subject : Congratulations !! User " + sendingdetails.UserName + " has bought " + productName + "Hello " + venderName + "Thank you for selling with us, we will like to know you that user " + sendingdetails.UserName + "has bought your products " + productName + "The product need to be shipped today by the end of the day. In case of any delay and emergencies please contact PISTIS Support/Inventory team.Before you shipped the product please don’t forget to read about the terms and conditions for shipping the product so that we can take care of your parcel. (www.pistis,com,mx/termsandconditions)Order details are indicated below: Product Sku :"+ productSku + "Quantity :"+ quatity+ "Payment Status : Approved You will receive another email with the shipment label that the user has opted for, print the label along with the PISTIS sticker and drop it to the nearest DHL/FedexEx center. In case if you want to schedule a pickup please reach out to our support/inventory team on (55 6269 1919 ) WhatsApp : 33 1559 6751 / 55 4058 9672 Thank you for selling with us!!!!!!!!!! Regards,TEAM PISTIS Mexico Comprar Con Confianza";
                        emailmodel.Body = html;
                        emailmodel.key = "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";
                        await Example.Execute(emailmodel);
                    }


                }
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //[HttpGet]
        //[Route("countCancelCounter")]
        //public IActionResult counteCnacelClick(int userId, string IpAddress)
        //{
        //    var checkUser = new Newsletter();

        //    if (userId > 0)
        //    {
        //        checkUser = db.Newsletters.Where(x => x.UserId == userId).FirstOrDefault();
        //    }
        //    if (IpAddress != null || IpAddress == "")
        //    {
        //        checkUser = db.Newsletters.Where(x => x.IpAddress == IpAddress).FirstOrDefault();
        //    }

        //}
        public List<string> getEmails()        {            var newlist = new List<string>();            try            {                var UsersEmails = from u in db.Users                                  where u.RoleId == 1 && u.IsActive == true                                  select new                                  {                                      Emails = u.Email                                  }.ToString();                newlist.AddRange(UsersEmails);                var NewLetterEmails = from x in db.Newsletters                                      where x.IsActive == true && x.IsSubscribed == true                                      select new                                      {                                          Emails = x.Email                                      }.ToString();                var data = newlist.Union(NewLetterEmails).ToList(); ;
                // var data= UsersEmails.Union(UsersEmails).
                return data;            }            catch (Exception ex)            {                throw;            }        }

        public class SaveNewsLetter
        {
            public string Email { get; set; }
            public string IpAddress { get; set; }
            public int UserId { get; set; }
            public int Id { get; set; }
        }
    }
}