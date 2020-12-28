using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
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
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IMongoService _service;

        public TransactionController(ILogger<TransactionController> logger,IMongoService service )
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public IEnumerable<TransactionBasic> Get()
        {
            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;// "Roman";
            return _service.TransactionCollection.Find(tr => tr.Creator.Name == activeUser || tr.Shareholders.Any(sh => sh.Person.Name == activeUser))
                .ToList().Select(tr => new TransactionBasic(tr,activeUser));
        }
        [HttpGet]
        [Route("getDetails")]
        public TransactionDetails GetDetails(Guid id)
        {
            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;// "Roman";
            var transaction = _service.TransactionCollection.
                Find(tr => (tr.Id == id) && (tr.Creator.Name == activeUser || tr.Shareholders.Any(sh => sh.Person.Name == activeUser))).First();
            return new TransactionDetails(transaction);
        }
    }
}
