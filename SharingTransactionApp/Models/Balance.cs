using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class Balance
    {
        public virtual Guid Id { get; set; }
        public virtual AppUser PersonPlus { get; set; }
        public virtual AppUser PersonMinus { get; set; }
        public virtual double CashBalance { get; set; }
    }
}
