using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TestCore.Controllers
{
    [Route("api/related-tag")]
    [ApiController]
    public class RelatedTagController : ControllerBase
    {
        private readonly PistisContext db;

        public RelatedTagController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("add")]
        public IActionResult addtag(int productId, string tagname)
        {
            var message = 0;
            try
            {
                if (tagname != null)
                {
                    var data = db.ProductRelatedTagMap.Where(x =>x.RelatedTag.Name.ToLower() == tagname.ToLower()).FirstOrDefault();
                    if (data != null)
                    {

                        string[] words = tagname.Split(',');
                        foreach (var item in words)
                        {
                            var filterData = db.RelatedTag.Where(x => x.Name.ToLower() == item.ToLower() && x.Id == data.RelatedTagId).FirstOrDefault();
                            if (filterData != null)
                            {
                                filterData.Name = item;
                                db.SaveChanges();
                                var gettags = db.ProductRelatedTagMap.Where(x => x.RelatedTagId == filterData.Id && x.ProductId == productId).FirstOrDefault();

                                gettags.RelatedTagId = filterData.Id;
                                gettags.ProductId = productId;
                                db.SaveChanges();
                            }

                        }
                    }
                    else
                    {
                        string[] words = tagname.Split(',');
                        foreach (var item in words)
                        {
                            RelatedTag tag = new RelatedTag();
                            tag.Name = item;
                            tag.IsActive = true;
                            db.RelatedTag.Add(tag);
                            db.SaveChanges();
                            ProductRelatedTagMap gettags = new ProductRelatedTagMap();
                            gettags.RelatedTagId = tag.Id;
                            gettags.ProductId = productId;
                            db.ProductRelatedTagMap.Add(gettags);
                            db.SaveChanges();
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


    }
}