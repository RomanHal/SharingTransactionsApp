using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SharingTransactionApp.Models.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models.Services
{
    public class BalanceService:IBalanceService
    {
        private readonly IMongoService _service;

        public BalanceService(IMongoService service)
        {
            _service = service;
        }

        public static double GetBalance(string name, Balance balance)
        {
            return name == balance.PersonPlus.Name ? balance.CashBalance : -balance.CashBalance;
        }
        public IList<UserBalance> GetBalances(string name)
        {
            var balances= _service.BalanceCollection.Find(e => e.PersonMinus.Name == name || e.PersonPlus.Name == name).ToList();
            var userBalances = balances.Select(b =>new UserBalance {
                Name=b.PersonMinus.Name==name?b.PersonPlus.Name:b.PersonMinus.Name, 
                Cash=GetBalance(name,b)
            }).ToList();
            return userBalances;
        }
    }
}
