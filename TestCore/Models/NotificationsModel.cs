using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class NotificationsModel
    {
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

        public string Typename { get; set; }
        public int CountAll { get; set; }
    }
}
