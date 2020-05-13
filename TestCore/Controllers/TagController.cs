using Localdb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestCore
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly PistisContext db;

        public TagController(PistisContext pistis)
        {
            db = pistis;
        }

        [HttpGet]
        [Route("getTagsFront")]
        public IActionResult Customers134(int productId)
        {
            var data1 = new List<ProductTag>();
            try
            {
                var data = from pt in db.ProductTag
                           where pt.IsApproved == true && pt.ProductId == productId && pt.IsActive == true
                           join t in db.Tag on pt.TagId equals t.Id
                           select new
                           {
                               productTagName = t.Name,
                           };
                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Ok(ex.Message);
            }

        }
        [HttpGet]
        [Route("getTags")]
        public IActionResult Customers(int page, int pageSize, string search, int categoryId)
        {
            var skipData = pageSize * (page - 1);
            var data1 = new List<ProductTag>();
            try
            {
                //  var data = db.ProductTag.Where(x => x.IsActive == true)
                var data = from pt in db.ProductTag
                           where pt.IsActive == true
                           //join approve in db.ProductTag on true equals approve.IsApproved 
                           // join disapprove in db.ProductTag on false equals disapprove.IsApproved 
                           join t in db.Tag on pt.TagId equals t.Id
                           group pt by new { pt.TagId, t.Name } into bs
                           select new
                           {
                               //productTagId = bs.Key.ProductId,
                               TagId = bs.Key.TagId,
                               productTagName = bs.Key.Name,
                               productApproveCount = bs.Count(x => x.IsApproved == true),
                               productDisApproveCount = bs.Count(x => x.IsApproved == false),
                           };

                if (search != null)
                {
                    data = data.Where(c => c.productTagName.ToLower().Contains(search.ToLower()));
                }

                var response = new
                {
                    data = data.Skip(skipData).Take(pageSize).ToList(),
                    count = data.Count()
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Ok(ex.Message);
            }

        }
        [HttpGet]
        [Route("deactivateTag")]
        public IActionResult deactivateCustomer(int Id)
        {
            var obj = new ProductTag();
            try
            {
                if (Id > 0)
                {
                    obj = db.ProductTag.Where(x => x.Id == Id).FirstOrDefault();
                    if (obj.IsApproved == false)
                    {
                        obj.IsApproved = true;
                    }
                    else
                    {

                        obj.IsApproved = false;
                    }
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
        [Route("deleteTag")]
        public IActionResult deleteCusomer(int Id)
        {
            var obj = new ProductTag();

            try
            {
                if (Id > 0)
                {
                    obj = db.ProductTag.Where(x => x.Id == Id).FirstOrDefault();
                    var data = db.Tag.Where(x => x.Id == obj.TagId).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsActive = false;

                    }
                    if (obj != null)
                        //if (obj.IsActive == false)
                        //{
                        //    obj.IsActive = true;
                        //    obj.IsApproved = true;
                        //}
                        //else
                        //{
                        obj.IsActive = false;
                    obj.IsApproved = false;
                    //}
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
        [Route("AddTag")]
        public IActionResult addtag(int productId, string tagname)
        {
            var message = 0;
            try
            {
                if (tagname != null)
                {
                    var data = db.ProductTag.Where(x => x.IsActive == true && x.Tag.Name.ToLower() == tagname.ToLower()).FirstOrDefault();
                    if (data != null)
                    {

                        string[] words = tagname.Split(',');
                        foreach (var item in words)
                        {
                            var filterData = db.Tag.Where(x => x.Name.ToLower() == item.ToLower() && x.Id == data.TagId).FirstOrDefault();
                            if (filterData != null)
                            {
                                filterData.Name = item;
                                db.SaveChanges();
                                var gettags = db.ProductTag.Where(x => x.TagId == filterData.Id && x.ProductId == productId).FirstOrDefault();

                                gettags.TagId = filterData.Id;
                                gettags.IsActive = true;
                                gettags.ProductId = productId;
                                gettags.IsApproved = false;
                                db.SaveChanges();
                            }

                        }
                    }
                    else
                    {
                        string[] words = tagname.Split(',');
                        foreach (var item in words)
                        {
                            Tag tag = new Tag();
                            tag.Name = item;
                            tag.IsActive = true;
                            db.Tag.Add(tag);
                            db.SaveChanges();
                            ProductTag gettags = new ProductTag();
                            gettags.TagId = tag.Id;
                            gettags.IsActive = true;
                            gettags.ProductId = productId;
                            gettags.IsApproved = false;
                            db.ProductTag.Add(gettags);
                            db.SaveChanges();
                        }
                    }

                    //var tagList = new List<Tag>();
                    //var ProductTag = new List<ProductTag>();

                    //string[] words = tagname.Split(',');
                    //foreach (var item in words)
                    //{
                    //    Tag tag = new Tag();
                    //    tag.Name = item;
                    //    tag.IsActive = true;
                    //    tagList.Add(tag);
                    //    db.SaveChanges();
                    //    ProductTag productTag = new ProductTag();
                    //    productTag.TagId = tag.Id;
                    //    productTag.IsActive = true;
                    //    productTag.ProductId = productId;
                    //    productTag.IsApproved = true;
                    //    ProductTag.Add(productTag);

                    //}

                    //db.ProductTag.AddRange(ProductTag);
                    //db.SaveChanges();
                    message = 1;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(message);
        }
        [HttpGet]
        [Route("getProduct")]
        public IActionResult getpro(int Id)
        {
            var productDetailList = new List<TagProduct>();

            try
            {
                var data = db.ProductTag.Where(x => x.TagId == Id && x.IsActive == true)
                    .Include(x => x.Product)
                    .ToList();
                foreach (var item in data)
                {
                    var productDetails = new TagProduct();

                    productDetails.IsActive = item.IsActive;
                    productDetails.IsApproved = item.IsApproved;
                    productDetails.ProductName = item.Product.Name;
                    productDetails.ProductTagId = item.Id;
                    productDetails.tagId = item.TagId;
                    productDetailList.Add(productDetails);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(productDetailList);
        }
    }
    public class produc
    {
        public int ProducttagID { get; set; }
        public string ProductName { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }

    }
    public class TagProduct
    {

        public int tagId { get; set; }
        public int ProductTagId { get; set; }
        public string ProductName { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }

    }
}