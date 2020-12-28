using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models.Inerfaces
{
    public interface IMongoService
    {

        public IMongoCollection<AppUser> UsersCollection { get; }
        public IMongoCollection<Balance> BalanceCollection { get; }
        public IMongoCollection<Transaction> TransactionCollection { get; }
        public IMongoCollection<ImageJson> ImageJsonCollection { get; }

    }
}
