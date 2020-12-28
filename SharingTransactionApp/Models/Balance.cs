using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class Balance
    {
        public Guid Id { get; set; }
        public AppUser PersonPlus { get; set; }
        public AppUser PersonMinus { get; set; }
        public double CashBalance { get; set; }
    }
}
