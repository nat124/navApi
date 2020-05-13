using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestCore.Extension_Method;
using Localdb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace TestCore.Controllers
{
    [Route("api/FooterUrl")]
    [ApiController]
    public class FooterUrlController : ControllerBase
    {
        private readonly PistisContext db;
        public FooterUrlController(PistisContext pistis)
        {
            db = pistis;
        }
        [Route("getFooter")]
        public List<FooterHeader> getFooter()
        {
            var data = db.FooterHeaders.Where(x => x.IsActive == true).ToList();
            try
            {
                return data;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return data;

            }
        }
        [Route("getFooterUrl")]
        public List<Models.FooterUrl> GetFooter()
        {
            var data = db.FooterUrls.Where(x => x.IsActive == true).Include(x=>x.FooterHeader).ToList().RemoveReferences();
            try
            {
                return data;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return data;

            }
        }
        [Route("getOneFooterUrl")]
        public IActionResult GetFooter1(int Id)
        {
            var data = db.FooterUrls.Where(x => x.Id == Id).FirstOrDefault();
            FooterUrl footerUrl = new FooterUrl();
            footerUrl.Id = data.Id;
            footerUrl.Name = data.Name;
            footerUrl.Url = data.Url;
            footerUrl.FooterHeaderId = data.FooterHeaderId;
            try
            {

            }
            catch (Exception ex)
            {
                Console.Write(ex);


            }
            return Ok(footerUrl);
        }
        [Route("getFooterUrlData")]
        public IActionResult GetFooterUrls()
        {
            var data = db.FooterUrls.Where(x => x.IsActive == true).ToList();
            try
            {
                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Ok(data);

            }
        }
        [Route("deleteFooterUrl")]
        public Models.FooterUrl deleteFooter(int Id)
        {
            var obj = db.FooterUrls.Where(x => x.Id == Id).FirstOrDefault();
            if (obj != null)
            {
                obj.IsActive = false;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            { }
            return obj;
        }

        [HttpPost]
        [Route("saveFooterUrl")]
        public Models.FooterUrl saveFooter([FromBody]FooterUrl footerUrl)
        {
            Models.FooterUrl footer = new Models.FooterUrl();
           if(footerUrl!=null)
            {
                footer.Name = footerUrl.Name;
                footer.Url = footerUrl.Url;
                footer.FooterHeaderId = footerUrl.FooterHeaderId;
                footer.IsActive = true;
            }
            try
            {
                db.FooterUrls.Add(footer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return footer;
        }
        [Route("updateFooterUrl")]
        public Models.FooterUrl updateFooter(FooterUrl footerUrl)
        {
            var obj = db.FooterUrls.Where(x => x.Id == footerUrl.Id).FirstOrDefault();
            if (obj != null)
            {
                obj.Name = footerUrl.Name;
                obj.Url = footerUrl.Url;
                obj.FooterHeaderId = footerUrl.FooterHeaderId;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return obj;
        }
    }
    public class FooterUrl
    { public int FooterHeaderId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
   
}
