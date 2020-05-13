using Localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestCore.Helper;
using Models;
using TestCore.Extension_Method;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Configuration;

namespace TestCore.Controllers
{
    [Route("api/CustomerHistory")]
    [ApiController]
    public class CustomerHistoryController : ControllerBase
    {
        private readonly PistisContext db ;
        public CustomerHistoryController(PistisContext pistis)
        {
            db = pistis;
        }
        
        [HttpPost]
        [Route("saveCustomerHistory")]
        public CustomerHistory CustomerHistory([FromBody]custom data)
        {
            Models.CustomerHistory customerHistory = new CustomerHistory();
            if (data != null)
            {
                customerHistory.CustomerId = data.customerId;
                customerHistory.ProductId = data.ProductId;
                customerHistory.IpAddress = data.IpAddress;
                customerHistory.Date = DateTime.Now;
                customerHistory.IsActive = true;
            }
            try
            {
                db.CustomerHistories.Add(customerHistory);
                db.SaveChanges();
                return customerHistory;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return customerHistory;

            }
        }
       
    }
  
    public class custom
    {
        public int customerId { get; set; }
        public string IpAddress { get; set; }
        public int ProductId { get; set; }
    }

}
