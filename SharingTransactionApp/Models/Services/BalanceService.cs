using SharingTransactionApp.Models.Inerfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class BalanceService:IBalanceService
    {
        private readonly ISession _session;

        public BalanceService(ISession session)
        {
            _session = session;
        }

        public static double GetBalance(string name, Balance balance)
        {
            return name == balance.PersonPlus.Name ? balance.CashBalance : -balance.CashBalance;
        }
        public IEnumerable<UserBalance> GetBalances(string name)
        {
            IEnumerable<Balance> balances;
            using(_session.BeginTransaction())
            {
                balances = _session.Query<Balance>().Where(e => e.PersonMinus.Name == name || e.PersonPlus.Name == name).ToList();
                _session.GetCurrentTransaction()?.Commit();
            }
            var userBalances = balances.Select(b =>new UserBalance {
                Name=b.PersonMinus.Name==name?b.PersonPlus.Name:b.PersonMinus.Name, 
                Cash=GetBalance(name,b)
            });
            return userBalances;
        }
    }
}
