using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllModels;
using Localdb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Models;
using TestCore.Helper;

namespace TestCore.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class ChatController : ControllerBase
    {
        private readonly PistisContext db;
        private IHubContext<MessageHub> _hub;



        public ChatController(PistisContext pistis, IHubContext<MessageHub> hub)
        {
            db = pistis;
            _hub = hub;

        }
        [HttpGet]
        [Route("getAllChats")]
        public async Task<IActionResult> getAllChats(int UserId)
        {
            var result = new List<AllChats>();
            var data = db.VendorChat.Where(x => x.CustomerId == UserId &&x.IsArchieved==false && x.IsActive==true).OrderByDescending(x=>x.Id).ToList();
            result = await chatData(data);

            return Ok(result);
        }
        [HttpGet]
        [Route("getAllVendorChats")]
        public async Task<IActionResult> getAllVendorChats(int UserId)
        {
            var result = new List<AllChats>();
            var data = db.VendorChat.Where(x => x.VendorId == UserId && x.IsArchieved == false && x.IsActive == true).OrderByDescending(x => x.Id).ToList();
            result = await chatData(data);

            return Ok(result);
        }

        [HttpGet]
        [Route("getAllAdminChats")]
        public async Task<IActionResult> getAllAdminChats()
        {
            var result = new List<AllChats>();
            var data = db.VendorChat.Where(x => x.IsArchieved == false && x.IsActive == true).OrderByDescending(x => x.Id).ToList();
            result = await chatData(data);
            return Ok(result);
        }
        private async Task<List<AllChats>> chatData(List<VendorChat> data)
        {
            var result = new List<AllChats>();
            foreach (var d in data)
            {
                if (!result.Any(x => x.VendorId == d.VendorId && x.CustomerId == d.CustomerId))
                {
                    var r = new AllChats();
                    var msg = await db.VendorChatMsg.Where(x => x.VendorChatId == d.Id).LastOrDefaultAsync();
                    var detail = await db.ProductVariantDetails.Where(x => x.Id == d.ProductVariantDetailId).Include(x => x.Product.ProductImages).FirstOrDefaultAsync();
                    r.CustomerId = d.CustomerId;
                    r.Date = msg.DateTime;
                    r.LastMsg = msg.CustomerMsg == null ? msg.VendorMsg : msg.CustomerMsg;
                    r.ProductVariantDetailId = d.ProductVariantDetailId;
                    r.ProductId = detail.ProductId;
                    r.VendorId = detail.Product.VendorId;
                    r.VendorName = db.Users.Where(x => x.Id == r.VendorId).FirstOrDefault().DisplayName;
                    r.ProductImage = detail.Product.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? detail.Product.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : detail.Product.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
                    result.Add(r);
                }
            }
            return result;
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> search(string text,int UserId)
        {
            var vendor = db.Users.Where(x => x.IsActive == true && x.RoleId == 2 && x.DisplayName.ToLower().Contains(text.ToLower())).ToList();
            var result = new List<AllChats>();
            var data = new List<VendorChat>();
            foreach (var v in vendor)
            {
                var d = db.VendorChat.Where(x => x.CustomerId == UserId && x.VendorId == v.Id).ToList();
                data.AddRange(d);
            }
            result = await searchData(data);
            return Ok(result);

        }
        [HttpGet]
        [Route("searchForvendor")]
        public async Task<IActionResult> searchForVendor(string text, int UserId)
        {
            var customer = db.Users.Where(x => x.IsActive == true && x.RoleId == 1 && x.DisplayName.ToLower().Contains(text.ToLower())).ToList();
            var result = new List<AllChats>();
            var data = new List<VendorChat>();
            foreach (var v in customer)
            {
                var d = db.VendorChat.Where(x => x.VendorId == UserId && x.CustomerId == v.Id).ToList();
                data.AddRange(d);
            }
            result=await searchData(data);
            return Ok(result);

        }
        [HttpGet]
        [Route("searchForAdmin")]
        public async Task<IActionResult> searchForAdmin(string text)
        {
            var customer = db.Users.Where(x => x.IsActive == true && (x.RoleId == 1 ||x.RoleId==2)&& x.DisplayName.ToLower().Contains(text.ToLower())).ToList();
            var result = new List<AllChats>();
            var data = new List<VendorChat>();
            foreach (var v in customer)
            {
                var d = db.VendorChat.Where(x => (x.VendorId == v.Id|| x.CustomerId == v.Id)).ToList();
                data.AddRange(d);
            }
            result=await searchData(data);
            return Ok(result);

        }
        private async Task<List<AllChats>> searchData(List<VendorChat>data)
        {
            var result = new List<AllChats>();
            foreach (var d in data)
            {
                var r = new AllChats();
                var msg = await db.VendorChatMsg.Where(x => x.VendorChatId == d.Id).LastOrDefaultAsync();
                var detail = await db.ProductVariantDetails.Where(x => x.Id == d.ProductVariantDetailId).Include(x => x.Product.ProductImages).FirstOrDefaultAsync();
                r.CustomerId = d.CustomerId;
                r.Date = msg.DateTime;
                r.LastMsg = msg.CustomerMsg == null ? msg.VendorMsg : msg.CustomerMsg;
                r.ProductVariantDetailId = d.ProductVariantDetailId;
                r.ProductId = detail.ProductId;
                r.VendorId = detail.Product.VendorId;
                r.VendorName = db.Users.Where(x => x.Id == r.VendorId).FirstOrDefault().DisplayName;
                r.ProductImage = detail.Product.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? detail.Product.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : detail.Product.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
                result.Add(r);
            }
            return result;
        }

        [HttpPost]
        [Route("getOldMsgs")]
        public async Task<IActionResult> getOldMsgs(getOldMsgModel model)
        {
            var result = new List<OldMessage>();
            var data = new VendorChat();
            var data1 = db.VendorChat.Where(x => x.ProductVariantDetailId == model.ProductVariantDetailId && x.IsArchieved ==false).ToList();

            if (model.CustomerId != 0)
                 data = data1.Where(x => x.CustomerId == model.CustomerId ).FirstOrDefault();
            if (model.VendorId != 0)
                data = data1.Where(x => x.VendorId == model.VendorId).FirstOrDefault();
            
            if (data != null)
                {
                    var customer = await db.Users.Where(x => x.Id == data.CustomerId).FirstOrDefaultAsync();
                    var vendor = await db.Users.Where(x => x.Id == data.VendorId).FirstOrDefaultAsync();
                    var productdetail = await db.ProductVariantDetails.Where(x => x.Id == data.ProductVariantDetailId).Include(x => x.Product.ProductImages).FirstOrDefaultAsync();
                    var msg = await db.VendorChatMsg.Where(x => x.VendorChatId == data.Id).Include(x => x.VendorChat).ToListAsync();
                
                    foreach (var m in msg)
                    {
                        var r = new OldMessage();
                        r.CustomerId = data.CustomerId;
                        r.VendorId = data.VendorId;
                        r.CustomerMsg = m.CustomerMsg;
                        r.VendorMsg = m.VendorMsg;
                        r.Date = m.DateTime;
                        r.Id = m.Id;
                        r.IpAddress = data.IpAddress;
                        r.ProductVariantDetailId = data.ProductVariantDetailId;
                        r.CustomerName = customer.FirstName + " " + customer.LastName;
                        r.VendorName = vendor.DisplayName;
                        r.ProductName = productdetail.Product.Name;
                        r.ProductImage = productdetail.Product.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? productdetail.Product.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : productdetail.Product.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
                        result.Add(r);
                    }

                var IsRead=ReadMsg(msg,model.VendorId);
                var noti = readnotification(model.VendorId, model.CustomerId, model.ProductId, model.ProductVariantDetailId);


                }
            return Ok(result);
        }
        Boolean ReadMsg(List<VendorChatMsg> all,int VendorId)
        {
            if(VendorId!=0)
            {
                foreach (var a in all)
                {
                    a.IsVendorRead = true;
                }
        }
            else
            {
                foreach (var a in all) 
                {
                    a.IsCustomerRead = true;
                }
            }
            try
            {
                db.Entry(all).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        Boolean readnotification(int VendorId,int CustomerId,int ProductId,int VariantId)
        {
            var id = ProductId.ToString();
            var variantid = VariantId.ToString();
            var data =new List<Notification>();
            if (VendorId != 0)
                {
                 data = db.Notifications.Where(x => x.UserId == VendorId && x.TargetURL.Contains(id) &&x.TargetURL.Contains(variantid)).ToList();
            }
            else
                data = db.Notifications.Where(x => x.UserId == VendorId && x.TargetURL.Contains(id) && x.TargetURL.Contains(variantid)).ToList();
            var all = db.NotificationUser.Where(x => x.IsActive == true && x.IsDeleted == false && x.IsRead == false).ToList();
            try
            {
                foreach (var d in data)
                {
                    var check = all.Where(x => x.NotificationId == d.Id).FirstOrDefault();
                    if (check != null)
                    {
                        check.IsRead = true;
                        check.ReadDate = DateTime.Now;
                        db.SaveChanges();
                    }

                }


                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        [HttpPost]
        [Route("getInitalData")]
        public async Task<IActionResult> getInitalData(getOldMsgModel model)
        {
            var result = new OldMessage();
            var all = new List<OldMessage>();
            var customer = db.Users.Where(x => x.Id == model.CustomerId).FirstOrDefault();
            var detail = db.ProductVariantDetails.Where(x => x.IsActive == true && x.Id == model.ProductVariantDetailId).Include(x=>x.Product.ProductImages).FirstOrDefault();
            result.CustomerId = model.CustomerId;
            result.CustomerName = customer.FirstName + " " + customer.LastName;
            result.IpAddress = model.IpAddress;
            result.ProductVariantDetailId = model.ProductVariantDetailId;
            result.ProductName = detail.Product.Name;
            result.ProductImage = detail.Product.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault() == null ? detail.Product.ProductImages.Where(x => x.IsActive == true).FirstOrDefault().ImagePath150x150 : detail.Product.ProductImages.Where(x => x.IsActive == true && x.IsDefault == true).FirstOrDefault().ImagePath150x150;
            result.VendorId = detail.Product.VendorId;
            result.VendorName = db.Users.Where(x => x.Id == result.VendorId).FirstOrDefault().DisplayName;
            all.Add(result);
            return Ok(all);
        }
        [HttpPost]
        [Route("add")]
        public IActionResult create(Message msg)
        {
            var data = saveInDb(msg);
            return Ok();
        }
        public Boolean saveInDb(Message msg)
        {
            var model = new VendorChat();
            var vendorid = 0;
            if (msg.VendorId == 0)
                vendorid = db.Products.Where(x => x.Id == msg.ProductId).FirstOrDefault().VendorId;
            else
                vendorid = msg.VendorId;
            model = db.VendorChat.Where(x =>x.IsActive==true&&x.IsArchieved ==false&& x.VendorId == vendorid && x.ProductVariantDetailId == msg.ProductVariantDetailId && x.CustomerId == msg.CustomerId).FirstOrDefault();
            if (model == null)
            {
                 model = new VendorChat();
                model.CustomerId = msg.CustomerId;
                model.IsActive = true;
                model.ProductVariantDetailId = msg.ProductVariantDetailId;

                model.IpAddress = msg.IpAddress;
                model.VendorId = vendorid;
                model.IsArchieved = false;
                model.ProductVariantDetailId = msg.ProductVariantDetailId;
                db.VendorChat.Add(model);
            }
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
                var chat = new AllChats();
                chat.CustomerId = model.CustomerId;
                var cust = db.Users.Where(x => x.Id == model.CustomerId).FirstOrDefault();
                chat.CustomerName = cust.FirstName + " " + cust.LastName;
                var vendor = db.Users.Where(x => x.Id == model.VendorId).FirstOrDefault();
                chat.VendorName = vendor.DisplayName;
                chat.Date = data.DateTime;
                chat.LastMsg = data.CustomerMsg;
                chat.ProductId = msg.ProductId;
                chat.ProductVariantDetailId = model.ProductVariantDetailId;
                chat.VendorId = model.VendorId;
                if (msg.VendorId == 0)
                {
                    var booldata = saveNotification(chat);
                }
                else
                {
                    var bdata = saveNotiForCust(chat);
                        }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("resolve")]
        public bool resolve(int id)
        {
            var data1 = db.VendorChatMsg.Where(x => x.Id == id).FirstOrDefault();
            if (data1 != null)
            {
                var data = db.VendorChat.Where(x => x.Id == data1.VendorChatId).FirstOrDefault();
                if(data!=null)
                data.IsArchieved = true;

                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        //NOTIFICATION
        
             private bool saveNotiForCust(AllChats check)
        {
            var noti = new Notification();
            noti.CreatedDate = System.DateTime.Now;
            noti.DeletedDate = null;
            noti.ReadDate = null;
            noti.IsRead = false;
            noti.IsDeleted = false;
            noti.IsActive = true;
            noti.NotificationTypeId = Convert.ToInt32(Helper.NotificationType.chat);
            noti.Title = "Got message";
            noti.SpanishTitle = "tengo mensaje";
            noti.Description = ("you got a new message  from " + check.VendorName).ToString();
            noti.SpanishDescription = ("recibiste un nuevo mensaje de " + check.VendorName).ToString();

            var TargetURL = db.NotificationTypes.Where(b => b.Id == Convert.ToInt32(Helper.NotificationType.chat) && b.IsActive == true)
                 .FirstOrDefault()?.BaseURL + "?Id=" + check.ProductId + "&variantId=" + check.ProductVariantDetailId + "&CustomId=" + check.CustomerId;
            noti.TargetURL = TargetURL;
            noti.UserId = check.CustomerId;
            //----saving notification of purcahse order
            var users = new List<int>();
            users.Add(check.CustomerId);
            var status = NotificationHelper.saveNotification(noti, db, users);
            return status;

        }
        private bool saveNotification(AllChats check)
        {
            var noti = new Notification();
            noti.CreatedDate = System.DateTime.Now;
            noti.DeletedDate = null;
            noti.ReadDate = null;
            noti.IsRead = false;
            noti.IsDeleted = false;
            noti.IsActive = true;
            noti.NotificationTypeId = Convert.ToInt32(Helper.NotificationType.chat);
            noti.Title = "Got message";
            noti.SpanishTitle = "tengo mensaje";
            noti.Description = ("you got a new message  from "+check.CustomerName).ToString();
            noti.SpanishDescription = ("recibiste un nuevo mensaje de " + check.CustomerName).ToString();

            var TargetURL = db.NotificationTypes.Where(b => b.Id == Convert.ToInt32(Helper.NotificationType.chat) && b.IsActive == true)
                 .FirstOrDefault()?.BaseURL + "?Id=" + check.ProductId+"&variantId="+check.ProductVariantDetailId+"&CustomId="+check.CustomerId;
            noti.TargetURL = TargetURL;
            noti.UserId = check.VendorId ;
            //----saving notification of purcahse order
            var users = new List<int>();
            users.Add(check.VendorId);
            var status = NotificationHelper.saveNotification(noti, db, users);
            return status;

        }
    }
}