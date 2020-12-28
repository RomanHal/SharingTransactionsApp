using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models.Inerfaces
{
    public interface ITransactionRegistrar
    {
        public bool Register(TransactionInput transaction,string author);
    }
}
