using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class TransactionDetails
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<UserConfirmationDetail> Shareholders { get; set; }
        public double Cash { get; set; }
        public Guid File { get; set; }

        public TransactionDetails() { }
        public TransactionDetails(Transaction transaction)
        {
            Id = transaction.Id;
            Date = transaction.Date;
            Creator = transaction.Creator.Name;
            Title = transaction.Title;
            Description = transaction.Description;
            Shareholders = transaction.Shareholders.Select(sh => new UserConfirmationDetail {Person=sh.Person.Name,Confirmation= sh.Confirmation }).ToList();
            Cash = transaction.Cash;
            File = transaction.File;
        }
    }

    public class UserConfirmationDetail
    {
        public string Person { get; set; }
        public bool Confirmation { get; set; }
    }
}
