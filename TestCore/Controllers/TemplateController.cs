using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
namespace TestCore.Controllers
{
    [Route("api/Template")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly PistisContext db;
        public TemplateController(PistisContext pistis)
        {
            db = pistis;
        }
        
        [HttpPost]
        [Route("addNewsletter")]
        [EnableCors("EnableCORS")]
        public IActionResult addNewsletter([FromBody]Template template)
        {
            var data = new Template();
            var message = 0;
            try
            {
                if (template != null)
                {
                    if (template.Id > 0)
                    {
                        var check = db.Template.Where(x => x.IsActive == true && x.Id == template.Id).FirstOrDefault();
                        check.SenderName = template.SenderName;
                        if (template.TemplateContent != null)
                            check.TemplateContent = template.TemplateContent;
                        if (template.TemplateSubject != null)
                            check.TemplateSubject = template.TemplateSubject;
                        if (template.Templatetype != null)

                            check.Templatetype = template.Templatetype;
                        if (template.SenderEmail != null)

                            check.SenderEmail = template.SenderEmail;
                        data.IsActive = true;
                        db.SaveChanges();
                        message = 1;
                    }
                    else
                    {   if(template.SenderName!=null)
                        data.SenderName = template.SenderName;
                        if (template.TemplateContent != null)
                            data.TemplateContent = template.TemplateContent;
                        if (template.TemplateSubject != null)
                            data.TemplateSubject = template.TemplateSubject;
                        if (template.Templatetype != null)
                            data.Templatetype = template.Templatetype;
                        if (template.SenderEmail != null)

                            data.SenderEmail = template.SenderEmail;
                        data.IsActive = true;
                        db.Template.Add(data);
                        db.SaveChanges();
                        message = 2;
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
        [Route("newsLetters")]
        [HttpGet]
        public IActionResult newsletter()
        {
            var data = new List<Template>();
            try
            {
                data = db.Template.Where(x => x.IsActive == true).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(data);
        }
        [Route("getNewsletter")]
        [EnableCors("EnableCORS")]
        public IActionResult getNewsletter(int Id)
        {
            var check = new Template();
            try
            {
                if (Id > 0)
                {
                    check = db.Template.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(check);
        }
        [HttpGet]
        [Route("delNewsletter")]
        [EnableCors("EnableCORS")]
        public IActionResult delNewsletter(int Id)
        {
            var check = new Template();
            try
            {
                if (Id > 0)
                {
                    check = db.Template.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefault();
                    if (check != null)
                    {
                        check.IsActive = false;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(check);
        }
    }
}