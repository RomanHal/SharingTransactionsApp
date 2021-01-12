using FluentNHibernate.Mapping;
using SharingTransactionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Mapping
{
    public class AppUserMapping:ClassMap<AppUser>
    {
        public AppUserMapping()
        {
            Table(nameof(AppUser));
            Id(c => c.Id);
            Map(c => c.Name);
            Map(c => c.Email);
        }
    }
}
