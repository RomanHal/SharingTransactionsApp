using FluentNHibernate.Mapping;
using SharingTransactionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Mapping
{
    public class BalanceMapping:ClassMap<Balance>
    {
        public BalanceMapping()
        {
            Table(nameof(Balance));
            Id(c => c.Id);
            Map(c => c.CashBalance).Not.Nullable();
            References(c => c.PersonMinus);
            References(c => c.PersonPlus);
        }
    }
}
