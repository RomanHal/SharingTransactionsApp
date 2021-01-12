using FluentNHibernate.Mapping;
using SharingTransactionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Mapping
{
    public class ImageMapping:ClassMap<ImageJson>
    {
        public ImageMapping()
        {
            Id(c => c.Id);
            Map(c => c.Data).CustomSqlType("Text");
        }
    }
}
