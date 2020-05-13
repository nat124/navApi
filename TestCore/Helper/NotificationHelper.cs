using Localdb;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.Helper
{
    public static class NotificationHelper
    {

        public static bool saveNotification(Notification model, PistisContext context, List<int> users)
        {
            if (model != null)
            {
                try
                {
                    context.Notifications.Add(model);
                    context.SaveChanges();


                    if (users.Count > 0)
                    {
                        List<NotificationUser> allUsers = new List<NotificationUser>();
                        foreach (var item in users)
                        {
                            var userNotification = new NotificationUser();
                            userNotification.NotificationId = model.Id;
                            userNotification.UserId = item;
                            userNotification.IsActive = true;
                            userNotification.IsRead = false;
                            userNotification.IsDeleted = false;
                            userNotification.CreatedDate = System.DateTime.Now;
                            allUsers.Add(userNotification);
                        }
                        if (allUsers.Count == users.Count)
                        {
                            context.NotificationUser.AddRange(allUsers);
                            context.SaveChanges();
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }

        public static bool UpdateOrderStatus(string status, PistisContext db, int CheckoutID, int userID)
        {
            var noti = new Notification();
            noti.CreatedDate = System.DateTime.Now;
            noti.DeletedDate = null;
            noti.ReadDate = null;
            noti.IsRead = false;
            noti.IsDeleted = false;
            noti.IsActive = true;
            noti.NotificationTypeId = Convert.ToInt32(NotificationType.Shipping);
            noti.Title = "Order " + status + " successfully";
            noti.Description = "Congratulations your order has been " + status;
            var TargetURL = db.NotificationTypes.Where(b => b.Id == Convert.ToInt32(NotificationType.Shipping) && b.IsActive == true)
                 .FirstOrDefault()?.BaseURL + "?orderId=" + CheckoutID;
            noti.TargetURL = TargetURL;
            noti.UserId = 0;

            //----saving notification of purcahse order
            var users = new List<int>(); 
            users.Add(userID);
            return NotificationHelper.saveNotification(noti, db, users);
        }
    }
}
