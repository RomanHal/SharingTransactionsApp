using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class TransactionInput
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public List<string> Shareholders { get; set; }
        public double Cost { get; set; }
        public string File { get; set; }
        public string Format { get; set; }
        
    }
}
