using AllModels;
using Localdb;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.Controllers;
using TestCore.Extension_Method;

namespace TestCore.Helper
{

    public static class ChatHelper
    {
        private static Boolean saveInDb(PistisContext db, Message msg)
        {
            var model = new VendorChat();
            model.CustomerId = msg.CustomerId;
            model.IsActive = true;
            model.ProductVariantDetailId = msg.ProductVariantDetailId;
            model.VendorId = msg.VendorId;
            model.IpAddress = msg.IpAddress;
            model.IsArchieved = false;
            model.ProductVariantDetailId = msg.ProductVariantDetailId;
             db.VendorChat.Add(model);
            try
            {
                 db.SaveChanges();
                var data = new VendorChatMsg();
                data.CustomerMsg = msg.CustomerMsg;
                data.DateTime = DateTime.Now;
                data.IsCustomerRead = false;
                data.IsVendorRead = false;
                data.VendorChatId = model.Id;
                data.VendorMsg = msg.VendorMsg;
                 db.VendorChatMsg.Add(data);
                 db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}