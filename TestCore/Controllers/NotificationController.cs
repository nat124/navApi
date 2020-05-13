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
    [Route("api/notification")]
    [ApiController]
    [EnableCors("EnableCORS")]
    public class NotificationController : ControllerBase
    {
        private readonly PistisContext db;

        public NotificationController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("Count")]
        public IActionResult count(int UserId)
        {
            return Ok( db.NotificationUser.Where(z => z.UserId == UserId && z.IsActive == true).Count());
        }
        [HttpGet]
        [Route("getUsersNotifications")]
        public IActionResult getNotificationByUser(int userId, string action)
        {
            try
            {
                var Notifications = new List<Notification>();
                var data = new List<Notification>();
               
                if (action == null)
                    data = db.Notifications.Where(b => b.IsActive == true && b.IsRead == false && b.IsDeleted == false).Include(c => c.users).ToList();
                else
                    data = db.Notifications.Where(b => b.IsActive == true).Include(c => c.users).ToList();

                //-------users notifications
                foreach (var item in data)
                {
                    var users = new List<NotificationUser>();
                    if (action == null)
                        users = item.users.Where(c => c.UserId == userId && c.IsActive == true && c.IsRead == false && c.IsDeleted == false).ToList();
                    else
                        users = item.users.Where(c => c.UserId == userId && c.IsActive == true).ToList();
                    item.users = new List<NotificationUser>();
                    if (users.Count > 0)
                        item.users = users;
                }

                var myNoti = data.Where(v => v.users.Count > 0).ToList();


                Notifications.AddRange(myNoti);
                var allData = Notifications.Select(m => new NotificationsModel
                {
                    Id = m.users.FirstOrDefault()?.Id ?? 0,
                    CreatedDate = Convert.ToDateTime(m.CreatedDate.ToShortDateString()),
                    Description = m.Description,
                    SpanishDescription=m.SpanishDescription??m.Description,
                    SpanishTitle =m.SpanishTitle,
                    IsActive = m.IsActive,
                    IsDeleted = m.users.FirstOrDefault()?.IsDeleted ?? false,
                    IsRead = m.users.FirstOrDefault()?.IsRead ?? false,
                    ReadDate = m.users.FirstOrDefault()?.ReadDate,
                    NotificationTypeId = m.NotificationTypeId,
                    TargetURL = m.TargetURL,
                    Title = m.Title,
                    UserId = userId,
                    Typename = db.NotificationTypes.Where(b => b.Id == m.NotificationTypeId).FirstOrDefault().Type,
                }).OrderByDescending(b => b.Id).ToList();

                if (action == null)
                    allData = allData.Take(5).ToList();
                
                return Ok(allData);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpGet]
        [Route("readUserNotification")]
        public IActionResult readNotification(int id)
        {
            var data = db.NotificationUser.Where(b => b.Id == id).FirstOrDefault();
            if (data != null)
            {
                try
                {
                    data.IsRead = true;
                    data.ReadDate = System.DateTime.Now;
                    db.SaveChanges();
                    return Ok(true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
                return Ok(false);
        }


        [HttpGet]
        [Route("removeUserNotification")]
        public IActionResult removeUserNotification(int id)
        {
            var data = db.NotificationUser.Where(b => b.Id == id).FirstOrDefault();
            if (data != null)
            {
                try
                {
                    data.IsDeleted = true;
                    data.DeletedDate = System.DateTime.Now;
                    db.SaveChanges();
                    return Ok(true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return Ok(false);
        }

        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var data = db.NotificationUser.Where(b => b.Id == id).FirstOrDefault();
            if (data != null)
            {
                try
                {
                    data.IsActive = false;
                    data.IsDeleted = true;
                    data.DeletedDate = System.DateTime.Now;
                    db.SaveChanges();
                    return Ok(true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
                return Ok(false);
        }
    }
}