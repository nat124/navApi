using Localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace TestCore
{
    public class SaveUserLog
    {
        private readonly PistisContext db;
        public SaveUserLog(PistisContext pistis)
        {
            db = pistis;
        }
        public int saveLog(Log log)
        {
            int response=0;
            try
            {
                
             var obj = new Log();
                if (log != null)
                {
                    obj.IpAddress = log.IpAddress ?? "";
                    obj.UserId = log.UserId ?? 0;
                    obj.LogtypeId = log.LogtypeId??0;
                    obj.Action = log.Action??"";
                    obj.Description = log.Description??"";
                    obj.Result = log.Result ?? "";
                    obj.ActionDateTime = DateTime.Now;
                    obj.IsActive =true;
                    db.Log.Add(obj);
                    db.SaveChanges();
                    response = 1;
                }
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
