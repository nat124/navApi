using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class StatusModel
    {
        public string action { get; set; } /*= "test.created";*/
        public string api_version  { get; set; }/*="v1";*/
        public string application_id { get; set; }/* = "3114510446021010";*/
        public DateTime date_created { get; set; } /*= DateTime.Now;*/
        public int id { get; set; } /*= 123;*/
        public bool live_mode { get; set; } /*= false;*/
        public string type { get; set; } /*= "test";*/
        public string user_id { get; set; } /*= "465375752";*/
        public Data data { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
    }
}
