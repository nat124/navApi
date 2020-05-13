using System;
using System.Collections.Generic;
using System.Linq;
using TestCore.Extension_Method;
using Localdb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace TestCore.Controllers
{
    [Route("api/footer")]
    [ApiController]
    public class FooterHeaderController : ControllerBase
    {
        private readonly PistisContext db;
        public FooterHeaderController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getFooter")]
        public List<FooterHeader> Footer()
        {
            var data = db.FooterHeaders
                .Include(x=>x.FooterUrls)
                .Where(x => x.IsActive == true)
                .OrderByDescending(x=>x.Id)
                .ToList()
                .RemoveReferences();
           

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
        [Route("savefooterHeader")]
        public FooterHeader saveProduct(string Name)
        {
            Models.FooterHeader footerHeader = new Models.FooterHeader();
            if(!string.IsNullOrEmpty(Name))
            footerHeader.Name = Name;
            footerHeader.IsActive = true;
            try
            {
                db.FooterHeaders.Add(footerHeader);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return footerHeader;
        }
        [Route("updatefooterHeader")]
        public FooterHeader updateProduct(int Id,string Name)
        {
            var obj = db.FooterHeaders.Where(x => x.Id == Id).FirstOrDefault();
            if (obj != null && Name!="" )
            {
                obj.Name = Name;
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) { }
            return obj;
        }
        [HttpGet]
        [Route("deleteFooterHeader")]
        public FooterHeader FeatureProduct(int Id)
        {
            var obj = db.FooterHeaders.Where(x => x.Id == Id).FirstOrDefault();
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

    }
}
