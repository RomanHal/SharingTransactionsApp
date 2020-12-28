using SharingTransactionApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class ImageJson
    {
        public Guid Id { get; set; }
        public FormatEnum Format { get; set; }
        public string Data { get; set; }

    }
}
