using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class ShippingPrice
    {
        public string DeliveryType { get; set; }
        public string ResponseFrom { get; set; }
        public double ShipPrice { get; set; }
        public string DeliveryDate { get; set; }

        public DateTime? ActualDeliveryDate
        {
            get
            {
                if (DeliveryDate != "")
                    return DateTime.ParseExact(DeliveryDate, "MM-dd-yyyy hh:mm", null);
                else
                    return null;
            }
        }
        public string showDate { get; set; }

        public string SpanishDate
        {
            get
            {
                if (ActualDeliveryDate != null)
                {
                    DateTime Actual = Convert.ToDateTime(ActualDeliveryDate);
                    return SpanishWeekDays[(int)Actual.DayOfWeek] + ", " + SpanishMonth[Actual.Month-1] + " " + Actual.Day + " ," + Actual.Year;
                }
                else
                    return "";
            }
        }

        public string EnglishDate
        {
            get
            {
                if (ActualDeliveryDate != null)
                {
                    DateTime Actual = Convert.ToDateTime(ActualDeliveryDate);
                    return EnglishWeekDays[(int)Actual.DayOfWeek] + ", " + EnglishMonth[Actual.Month-1] + " " + Actual.Day + " ," + Actual.Year;
                }
                else
                    return "";
            }
        }

        private String[] SpanishWeekDays = { "lunes", "martes", "miércoles", "jueves", "viernes", "sábado", "domingo" };
        private string[] SpanishMonth = { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };

        private String[] EnglishWeekDays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        private string[] EnglishMonth = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    }

   

    public class ErrorResponse
    {
        public string Note { get; set; }
        public string Message { get; set; }
    }

    public class ShippingResponse
    {
        public ShippingResponse()
        {
            SuccessData = new List<ShippingPrice>();
        }
        public List<ShippingPrice> SuccessData { get; set; }
        public ErrorResponse ErrorData { get; set; }
    }
}
