using FluentNHibernate.Mapping;
using SharingTransactionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Mapping
{
    public class TramsactionMapping : ClassMap<Transaction>
    {
        public TramsactionMapping()
        {
            Table(nameof(Transaction));
            Id(c => c.Id);
            HasMany(c => c.Shareholders);
            Map(c => c.File).Column("FileID");
            Map(c => c.Date);
            Map(c => c.Cash);
            References(c => c.Creator);
            Map(c => c.Description);
            Map(c => c.Title);
            

        }
    }
}
