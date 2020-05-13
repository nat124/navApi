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

namespace TestCore.Controllers
{
    [Route("api/vendorBalance")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class VendorBalanceController : ControllerBase
    {
        private readonly PistisContext db;
        public VendorBalanceController(PistisContext pistis)
        {
            db = pistis;
        }  
        [HttpGet,Route("getAll")]
        public IActionResult GetAll(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);
            var data1 = from vts in db.VendorTransactionSummary
                        join v in db.Users on vts.VendorId equals v.Id
                       where  vts.IsActive == true && v.IsActive == true
                       select new VendorBalance()
                       {
                           VendorId = vts.VendorId,
                           VendorName = v.FirstName + " " + v.LastName,
                           Email = v.Email,
                           Phone = v.Phone,
                           LastPaymentDate = vts.ModifyOn,
                           AmountDue = vts.DueAmount
                       };
           var data = data1.ToList();
            int Count = 0;
            if (search == null)
                Count = data.Count;
            else
            {
                try
                {
                    data = data.Where(v => v.Email.Contains(search) || v.VendorName.Contains(search)
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

        [HttpPost,Route("getDetail")]
        public IActionResult getDetail([FromBody] VendorBalanceDetail model)
        {
            var skipData = model.pageSize * (model.page - 1);
            var data1 = from vt in db.VendorTransaction where vt.VendorId==model.vendorId 
                        join v in db.Users on vt.VendorId equals v.Id
                        join vts in db.VendorTransactionSummary on v.Id equals vts.VendorId
                        where vt.IsActive == true && v.IsActive == true && vts.IsActive==true
                        select new 
                        {
                            VendorId = vt.VendorId,
                            VendorName = v.FirstName + " " + v.LastName,
                            Email = v.Email,
                            Phone = v.Phone,
                            TransactionDate = vt.TransactionDate,
                            Amount = vt.AmountPaid,
                            DueAmount=vts.DueAmount,
                            TransactionNumber=vt.TransactionNumber,
                            IsPaidByPistis=vt.IsPaidByPistis==true?"Paid":"Pending"
                        };
            var data = data1.ToList();
            int Count = 0;
            if (model.isPaid == "all")
                Count = data.Count();
            else if (model.isPaid == "0")
            {
                data = data.Where(x => x.IsPaidByPistis == "Pending").ToList();
                Count = data.Count(); }
            else
            {
                data = data.Where(x => x.IsPaidByPistis == "Paid").ToList();
                Count = data.Count();
            }
            var response = new
            {
                data = data.Skip(skipData).Take(model.pageSize).ToList(),
                count = Count,
            };

            return Ok(response);
        }

        [HttpGet,Route("getById")]
        public IActionResult getById(int VendorId)
        {
            var data1 = from vts in db.VendorTransactionSummary where vts.VendorId==VendorId
                        join v in db.Users on vts.VendorId equals v.Id
                        where vts.IsActive == true && v.IsActive == true
                        select new VendorBalance()
                        {
                            VendorId = vts.VendorId,
                            VendorName = v.FirstName + " " + v.LastName,
                            Email = v.Email,
                            Phone = v.Phone,
                            LastPaymentDate = vts.ModifyOn,
                            AmountDue = vts.DueAmount
                        };
            return Ok(data1.FirstOrDefault());
        }

        [HttpPost, Route("addWithTrans")]
        public IActionResult addWithTrans([FromBody]VendorBalance model)
        {
            var result = new VendorTransaction();
            result.IsActive = true;
            result.IsPaidByPistis = true;
            result.TransactionDate = DateTime.Now;
            result.TransactionNumber = model.Transno;
            result.VendorId = model.VendorId;
            result.AmountPaid =Convert.ToDecimal(model.Payment);
            db.VendorTransaction.Add(result);
            try
            {
                db.SaveChanges();
                var sum = db.VendorTransactionSummary.Where(x => x.IsActive == true && x.VendorId == model.VendorId).FirstOrDefault();
                if(sum!=null)
                {
                    var pending= Convert.ToDecimal(sum.DueAmount - model.Payment);
                    sum.DueAmount = pending;
                    sum.ModifyOn = DateTime.Now;
                    db.Entry(sum).State = EntityState.Modified;
                    db.SaveChanges();
                }
           return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }

        }
    }
}