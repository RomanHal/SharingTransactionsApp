using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class TransactionBasic
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public bool  Confirmed { get; set; }
        public double Cash { get; set; }

        public TransactionBasic() { }
        public TransactionBasic(Transaction transaction,string activeUser)
        {
            Id = transaction.Id;
            Date = transaction.Date;
            Creator = transaction.Creator.Name;
            Title = transaction.Title;
            Cash = transaction.Cash;
            Confirmed = transaction.Shareholders.Where(sh => sh.Person.Name == activeUser).Select(sh => sh.Confirmation).FirstOrDefault();
        }
    }
}
