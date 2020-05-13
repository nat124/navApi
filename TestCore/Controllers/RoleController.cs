using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Localdb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TestCore.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        //private PistisContext db = new PistisContext();
        private readonly PistisContext db;

        public RoleController(PistisContext pistis)
        {
            db = pistis;
        }
        //public RoleController()
        //{
        //    db.Configuration.LazyLoadingEnabled = true;
        //    db.Configuration.ProxyCreationEnabled = true;
        //}
        [HttpGet]
        [Route("getAll")]
        [AllowAnonymous]
        public List<Role> GetAll()
        {
            return db.Roles.Where(x => x.IsActive == true).ToList();
        }
    }
}
