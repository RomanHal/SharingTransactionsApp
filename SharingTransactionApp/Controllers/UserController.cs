using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NHibernate;
using SharingTransactionApp.Models;
using SharingTransactionApp.Models.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISession _session;

        public UserController(ISession session)
        {
            _session = session;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var list =  _session.Query<AppUser>().ToList();
            var slist = list.Select(u => u.Name);
            return slist;
        }
        [Route("activeUser")]
        [HttpGet]
        public string GetaActiveUser()
        {
            if (User.Claims.Where(c => c.Type == "name").FirstOrDefault() == null) return "Logged Out";
            return User.Claims.Where(c=>c.Type=="name").FirstOrDefault().Value;
        }
    }
}
