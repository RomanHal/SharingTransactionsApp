using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class CreationController : ControllerBase
    {
        private readonly ILogger<CreationController> _logger;
        private readonly ITransactionRegistrar _registrar;
        private readonly ISession _session;

        public CreationController(ILogger<CreationController> logger,ITransactionRegistrar registrar,ISession session)
        {
            _logger = logger;
            _registrar = registrar;
            _session = session;
        }
        [HttpPost]
        public IActionResult CreateNew(TransactionInput transaction)
        {
            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;
            _registrar.Register(transaction,activeUser);
            return Created("",transaction);
        }
        [HttpGet]
        public IEnumerable<TransactionBasic> GetCreated()
        {
            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;
            return _session.Query<Transaction>().Where(t => t.Creator.Name == activeUser).ToList().Select(tr => new TransactionBasic(tr, activeUser));
        }
    }
}
