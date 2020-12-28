using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
        private readonly IMongoService _service;

        public UserController(IMongoService service)
        {
            _service = service;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var list =  _service.UsersCollection.Find(_=>true).ToList();
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
