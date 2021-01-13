using MongoDB.Driver;
using NHibernate;
using SharingTransactionApp.Models.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models.Services
{
    public class BalanceUpdater: IBalanceUpdater
    {
        private readonly ISession _session;

        public BalanceUpdater(ISession session)
        {
            _session = session;
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
                Balance balance;
                using(_session.BeginTransaction())
                {
                   balance = _session.Query<Balance>().Where(filter).FirstOrDefault();

                    if (balance is null)
                    {
                        balance = new Balance { Id = Guid.NewGuid(), PersonPlus = creator, PersonMinus = shareholder, CashBalance = cash / division };
                        _session.Save(balance);
                        return true;
                    }
                    else if (balance.PersonPlus == creator) balance.CashBalance += cash / division;
                    else balance.CashBalance -= cash / division;
                    _session.Update(balance);
                    _session.GetCurrentTransaction()?.Commit();
                }

            }


            return true;
        }
    }
}
