using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public AppUser Creator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<UserConfirmation> Shareholders { get; set; }
        public double Cash { get; set; }
        public Guid File { get; set; }
    }
}
