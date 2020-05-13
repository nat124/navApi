using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TestCore.Extension_Method;
using TestCore.Helper;

namespace TestCore.Controllers
{
    [EnableCors("EnableCORS")]
    [Route("api/commission")]
    [ApiController]
    public class CategoryCommissionController : ControllerBase
    {
        private readonly PistisContext db;
        CategoryCommission obj;
        public CategoryCommissionController(PistisContext pistis)
        {
            db = pistis;
            obj = new CategoryCommission(db);

        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            //var data = db.ProductCategoryCommission.Where(x => x.IsActive == true).Include(x => x.ProductCategory).ToList();
            var data = obj.CategoryCommissions;
            var all = db.ProductCategories.Where(x => x.IsActive == true && x.ParentId == null).ToList().RemoveReferences();
            foreach(var d in data)
            {
                d.CategoryName = all.Where(x => x.Id == d.ProductCategoryId).FirstOrDefault().Name;
                d.ProductCategory = null;
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("getById")]
        public ProductCategoryCommission GetById(int Id)
        {
            var data = db.ProductCategoryCommission.Where(x => x.IsActive == true && x.Id==Id).Include(x => x.ProductCategory).FirstOrDefault();
            return data;
        }
        [HttpGet]
        [Route("getByCategoryId")]
        public int GetByCategoryId(int id)
        {
            var data = db.ProductCategoryCommission.Where(x => x.IsActive == true && x.ProductCategoryId == id).FirstOrDefault();
            if (data!=null)
                return data.Commission;
            else
                return 0;
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(ProductCategoryCommission model)
        {
            model.IsActive = true;
            try
            {
                db.ProductCategoryCommission.Add(model);
                db.SaveChanges();
                obj.CategoryCommissions=null;
                return Ok(true);
            }
            catch(Exception ex)
            {
                return Ok(false);
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit([FromQuery] int Id, [FromBody]int Commission)
        {
           var commission= GetById(Id);
            if (commission != null)
            {
                commission.Commission = Commission;
                db.SaveChanges();
                obj.CategoryCommissions=null;
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpGet]
        [Route("delete")]
        public IActionResult delete(int Id)
        {
            var data = GetById(Id);
            if(data!=null)
            {
                data.IsActive = false;
                db.SaveChanges();
                obj.CategoryCommissions=null;
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpGet]
        [Route("getCategory")]
        public List<Models.ProductCategory> Ge()
        {
            var ids = db.ProductCategoryCommission.Where(x => x.IsActive == true).Select(x=>x.ProductCategoryId).ToList();
            var prod = db.ProductCategories.Where(x => x.IsActive == true && x.ParentId == null && x.IsShow==true)
            .OrderByDescending(s => s.Id).ToList().RemoveReferences();
            

var results = prod.Where(i => !ids.Any(e => i.Id==e)).ToList();
            return results;
        }

        [HttpGet]
        [Route("getIncresedPrice")]
        public IActionResult getIncresedPrice(Decimal price)
        {
            if(price>=500)
            {
                var data = db.PaymentConfiguration.Where(x => x.IsApplied == true).FirstOrDefault();
                if(data!=null)
                {
                    price = price + (price * data.Percentage / 100);
                }
            }
            return Ok(price);
        }
    }
}