using MongoDB.Driver;
using SharingTransactionApp.Models.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models
{
    public class MongoService: IMongoService
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<AppUser> _userCollection;
        private IMongoCollection<Balance> _balanceCollection;
        private IMongoCollection<Transaction> _transactionCollection;
        private IMongoCollection<ImageJson> _imageCollection;

        public MongoService(ISettingsDB settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            var collectionNamesList=  _database.ListCollectionNames().ToList();
            _userCollection = _database.GetCollection<AppUser>(settings.StorageCollectionName);
            _balanceCollection = _database.GetCollection<Balance>(collectionNamesList.Where(s => s.Contains(nameof(Balance))).FirstOrDefault());
            _transactionCollection = _database.GetCollection<Transaction>(collectionNamesList.Where(s => s.Contains(nameof(Transaction))).FirstOrDefault());
            _imageCollection = _database.GetCollection<ImageJson>(collectionNamesList.Where(s => s.Contains("File")).FirstOrDefault());
        }
        public IMongoCollection<AppUser> UsersCollection => _userCollection;
        public IMongoCollection<Balance> BalanceCollection => _balanceCollection;
        public IMongoCollection<Transaction> TransactionCollection => _transactionCollection;
        public IMongoCollection<ImageJson> ImageJsonCollection => _imageCollection;

    }
}
