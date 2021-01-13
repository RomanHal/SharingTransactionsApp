using FluentNHibernate.Mapping;
using SharingTransactionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Mapping
{
    public class UserConfirmationMapping : ClassMap<UserConfirmation>
    {
        public UserConfirmationMapping()
        {
            Table(nameof(UserConfirmation));
            Id(c => c.Id);
            Map(c => c.Confirmation);
            References(c => c.Person);
        }
    }
}
