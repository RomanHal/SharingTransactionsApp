using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NHibernate;
using SharingTransactionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharingTransactionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ISession _session;

        public TransactionController(ILogger<TransactionController> logger,ISession session)
        {
            _logger = logger;
            _session = session;
        }
        [HttpGet]
        public IEnumerable<TransactionBasic> Get()
        {
            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;
            return _session.Query<Transaction>().Where(tr => tr.Creator.Name == activeUser || tr.Shareholders.Any(sh => sh.Person.Name == activeUser))
                .ToList().Select(tr => new TransactionBasic(tr,activeUser));
        }
        [HttpGet]
        [Route("getDetails")]
        public TransactionDetails GetDetails(Guid id)
        {
            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;
            var transaction = _session.Query<Transaction>()
                .Where(tr => (tr.Id == id) && (tr.Creator.Name == activeUser || tr.Shareholders.Any(sh => sh.Person.Name == activeUser))).First();
            return new TransactionDetails(transaction);
        }
    }
}
