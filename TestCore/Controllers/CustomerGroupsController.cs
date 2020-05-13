using Localdb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCore.Extension_Method;

namespace Api.Controllers
{
    [Route("api/CustomerGroup")]
    public class CustomerGroupsController : ControllerBase
    {
        private readonly PistisContext db;
        public CustomerGroupsController(PistisContext pistis)
        {
            db = pistis;
        }
        [HttpGet]
        [Route("getOneCustomerGroup")]
        public IActionResult CustomerGroup(int Id)
        {
            var data = new CustomerGroup();
            var groupname = "";
            try
            {
                data = db.CustomerGroups.Where(x => x.Id == Id).FirstOrDefault();

                groupname = data.GroupName;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);

            }
            return Ok(data);

        }
        [HttpGet]
        [Route("getCustomerGroup")]
        public IActionResult CustomerGroup(int page, int pageSize, string search)
        {
            var skipData = pageSize * (page - 1);

            var data = new List<CustomerGroup>();
            try
            {
                data = db.CustomerGroups.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);

            }
            int Count = 0;
            if (search == null)
                Count = data.Count;
            else
            {
                try
                {
                    data = data.Where(v => v.GroupName.ToLower().Contains(search.ToLower())).ToList();
                    Count = data.Count;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            var response = new
            {
                data = data.Skip(skipData).Take(pageSize).ToList(),
                count = Count,
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("deleteGroup")]
        public int deleteCustomerGroup(int groupId)
        {
            var message = 0;
            var data = new CustomerGroup();
            if (groupId > 0)
                data = db.CustomerGroups.Where(x => x.Id == groupId).FirstOrDefault();
            if (data != null)
                data.IsActive = false;
            var obj = db.CustomerGroupUsers.Where(x => x.CustomerGroupId == groupId).ToList();
            if (obj != null)
            {
                foreach (var item in obj)
                {
                    item.IsActive = false;
                    message = 1;
                }
            }
            try
            {
                db.SaveChanges();


            }
            catch (Exception e)
            {
                Console.Write(e.Message);

            }
            return message;

        }
        [HttpPost]
        [Route("editGroup")]
        public IActionResult editCustomerGroup([FromBody]customGroup formData)
        {
            var obj = new CustomerGroup();
            try
            {
                obj = db.CustomerGroups.Where(x => x.IsActive == true && x.Id == formData.Id).FirstOrDefault();
                obj.GroupName = formData.GroupName;
                obj.IsActive = true;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);

            }
            return Ok(obj);

        }
        [HttpPost]
        [Route("createGroup")]
        public int createGroup([FromBody]customGroup1 formData)
        {
            var obj = new CustomerGroup();
            var group = new CustomerGroupUsers();
            var message = 0;
            try
            {
                var checkGroupName = db.CustomerGroups.Where(x => x.GroupName.ToLower() == formData.GroupName.ToLower()).FirstOrDefault();
                if (checkGroupName == null)
                {
                    obj.GroupName = formData.GroupName;
                    obj.IsActive = true;
                    db.CustomerGroups.Add(obj);
                    db.SaveChanges();
                    foreach (var item in formData.CustomerId)
                    {
                        var check = db.CustomerGroupUsers.Where(x => x.UserId == item && x.CustomerGroupId == obj.Id).FirstOrDefault();
                        if (check == null)
                        {
                            group = new CustomerGroupUsers();
                            group.UserId = item;
                            group.CustomerGroupId = obj.Id;
                            group.IsActive = true;
                            db.CustomerGroupUsers.Add(group);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    message = 1;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);

            }
            return message;
        }
        [HttpGet]
        [Route("MangegroupCustomers")]
        public IActionResult ManageGroup(int Id)
        {
            var data = new List<CustomerGroupUsers>();
            try
            {
                data = db.CustomerGroupUsers.Where(x => x.CustomerGroupId == Id && x.IsActive==true).Include(x => x.User).OrderByDescending(x=>x.Id).ToList().RemoveReferences();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return Ok(data);
        }
        [Route("deleteGroupCustomer")]
        public IActionResult DeleteCust(int Id,int GroupId)
        {
            var data = new CustomerGroupUsers();
            try
            {
                data = db.CustomerGroupUsers.Where(x => x.UserId == Id && x.CustomerGroupId==GroupId).FirstOrDefault();
                data.IsActive = false;
                db.SaveChanges();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return Ok(data);
        }
        [Route("filterGroupCustomers")]
        public IActionResult groupCustomers(int Id)

        {
            var newData = new List<User>();
            var data = db.CustomerGroupUsers.Where(c => c.CustomerGroupId == Id).ToList();
            var users = db.Users.Where(c => c.IsActive == true && c.RoleId==1 ).ToList();
            foreach (var item in data)
            {
                var i = users.Where(c => c.Id == item.UserId && c.IsActive==true).SingleOrDefault();
                if (i != null)
                    users.Remove(i);
            }

            return Ok(users);
        }
        [HttpPost]
        [Route("addtoCustomergroup")]
        public int addmoreCustomer([FromBody] customGroup Data)
        {
            var message = 0;
            var list = new List<CustomerGroupUsers>();
            if (Data != null)
            {
                if (Data.CustomerId!=null)
                {
                    foreach (var item in Data.CustomerId)
                    {
                        var data = new CustomerGroupUsers();
                        data.UserId = item;
                        data.CustomerGroupId = Data.Id;
                        data.IsActive = true;
                        list.Add(data);
                        message = 1;
                    }
                }
            }
            try
            {
                db.CustomerGroupUsers.AddRange(list);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return message;
        }
        [HttpGet]
        [Route("getCustomers")]
        public IActionResult Customers()
        {

            //var data = new List<User>();
            try
            {
                var data = db.Users.Where(x => x.RoleId == 1 && x.IsActive == true && x.IsVerified == true)
                    .Select(x => new
                   {
                       Email = x.Email,
                       Id = x.Id

                   }).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
        }
    }
    public class customGroup
    {
        public List<int> CustomerId { get; set; }
        public string GroupName { get; set; }
        public int Id { get; set; }
    }
    public class customGroup1
    {
        public List<int> CustomerId { get; set; }
        public string GroupName { get; set; }
        
    }
}
