using System;

namespace SharingTransactionApp.Models
{
    public class UserConfirmation
    {
        public virtual Guid Id { get; set; }
        public virtual AppUser Person { get; set; }
        public virtual bool Confirmation { get; set; }
    }
}