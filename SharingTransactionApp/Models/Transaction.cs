using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class Transaction
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual AppUser Creator { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual IEnumerable<UserConfirmation> Shareholders { get; set; }
        public virtual double Cash { get; set; }
        public virtual Guid File { get; set; }
    }
}
