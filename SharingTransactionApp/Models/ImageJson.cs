using SharingTransactionApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class ImageJson
    {
        public virtual Guid Id { get; set; }
        public virtual FormatEnum Format { get; set; }
        public virtual string Data { get; set; }

    }
}
