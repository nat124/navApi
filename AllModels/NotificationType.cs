using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class NotificationType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string BaseURL { get; set; }
        public bool IsActive { get; set; }
    }

    public class Notification
    {
        public Notification()
        {
            users = new List<NotificationUser>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NotificationTypeId { get; set; }
        public string TargetURL { get; set; }
        public string Title { get; set; }
        public string SpanishTitle { get; set; }
        public string Description { get; set; }
        public string SpanishDescription { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
        public Nullable<DateTime> ReadDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public bool IsActive { get; set; }
        public List<NotificationUser> users { get; set; }
    }
    public class NotificationUser    {        public int Id { get; set; }        public int UserId { get; set; }        public int NotificationId { get; set; }        public DateTime CreatedDate { get; set; }        public bool IsRead { get; set; }        public Nullable<DateTime> ReadDate { get; set; }        public bool IsDeleted { get; set; }        public Nullable<DateTime> DeletedDate { get; set; }        public bool IsActive { get; set; }    }
}
