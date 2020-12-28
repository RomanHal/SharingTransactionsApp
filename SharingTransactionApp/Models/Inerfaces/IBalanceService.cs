using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models.Inerfaces
{
    public interface IBalanceService
    {
        public IEnumerable<UserBalance> GetBalances(string name);
    }
}
