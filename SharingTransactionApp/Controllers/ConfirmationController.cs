using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SharingTransactionApp.Models;
using SharingTransactionApp.Models.Services;
using SharingTransactionApp.Models.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmationController : ControllerBase
    {
        private readonly ILogger<ConfirmationController> _logger;
        private readonly IBalanceUpdater _balanceUpdater;
        private IMongoCollection<Transaction> _transactionCollection;

        public ConfirmationController(ILogger<ConfirmationController> logger,IMongoService mongoService,IBalanceUpdater balanceUpdater)
        {
            _logger = logger;
            _balanceUpdater = balanceUpdater;
            _transactionCollection = mongoService.TransactionCollection;
        }

        [HttpGet]
        public IEnumerable<TransactionBasic> GetToConfirm()
        {
            if (User.Claims is null) return null;

            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;
            return _transactionCollection.Find(transaction => 
                transaction.Shareholders.Any(sh => sh.Person.Name == activeUser && sh.Confirmation == false)).ToList().Select(tr=>new TransactionBasic(tr,activeUser));
        }
        [HttpPost]
        public IActionResult Confirm(ResponseData<Guid> id)
        {
            if (User.Claims is null) return BadRequest();

            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;
            var transaction = _transactionCollection.Find(tr=> tr.Id == id.Data).ToEnumerable().First();
            if (transaction is null) return BadRequest();
            if (transaction.Shareholders.Any(s => s.Confirmation == true && s.Person.Name == activeUser)) return BadRequest();
            transaction.Shareholders = transaction.Shareholders.Select(sh =>
            {
                if (sh.Person.Name == activeUser) sh.Confirmation = true;
                return sh;
            });
            if (transaction.Shareholders is null) return BadRequest();

            _transactionCollection.ReplaceOne(tr => tr.Id == transaction.Id,transaction);
            if (transaction.Shareholders.All(sh => sh.Confirmation == true)) _balanceUpdater.UpdateBalance(transaction);

            return Ok();
        }

    }
}
