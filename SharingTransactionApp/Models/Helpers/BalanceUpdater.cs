using MongoDB.Driver;
using SharingTransactionApp.Models.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models.Services
{
    public class BalanceUpdater: IBalanceUpdater
    {
        private readonly IMongoService _service;
        private IMongoCollection<Balance> _balanceColl;

        public BalanceUpdater(IMongoService service)
        {
            _service = service;
            _balanceColl = _service.BalanceCollection;
        }
        public bool UpdateBalance(Transaction transaction)
        {
            var division = transaction.Shareholders.Count();
            var creator = transaction.Creator;
            var cash = transaction.Cash;
            var shareholders = transaction.Shareholders.Where(sh=>sh.Person!=creator).Select(sh => sh.Person);
            foreach(var shareholder in shareholders)
            {
                System.Linq.Expressions.Expression<Func<Balance, bool>> filter = b =>
                                      (b.PersonMinus == creator && b.PersonPlus == shareholder) || (b.PersonPlus == creator && b.PersonMinus == shareholder);
                var balance = _balanceColl.Find(filter).FirstOrDefault();
                if (balance is null)
                {
                    balance = new Balance {Id=Guid.NewGuid(), PersonPlus = creator, PersonMinus = shareholder, CashBalance = cash / division };
                    _balanceColl.InsertOne(balance);
                    return true;
                }
                else if (balance.PersonPlus == creator) balance.CashBalance += cash / division;
                else balance.CashBalance -= cash / division;
                _balanceColl.ReplaceOne(filter, balance);
            }


            return true;
        }
    }
}
