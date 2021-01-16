using NUnit.Framework;
using Moq;
using SharingTransactionApp.Models.Services;
using SharingTransactionApp.Models.Inerfaces;
using SharingTransactionApp.Models;

namespace NUnitTestProject1
{
    public class BalanceServicesTests
    {
        BalanceService service;
        
        [SetUp]
        public void Setup()
        {
            var mongoMoq = new Mock<IMongoService>();
            service = new BalanceService(mongoMoq.Object);
            
        }

        [Test]
        public void GetBalanceTest()
        {
            var namePlus = "plus";
            var nameMinus = "minus";
            var cashBalance = 12.34;
            Balance balance = new Balance 
            { 
                PersonMinus = new AppUser { Name = nameMinus }, 
                PersonPlus = new AppUser { Name = namePlus }, 
                CashBalance = cashBalance };
            var amount = BalanceService.GetBalance(namePlus, balance);
            Assert.AreEqual(cashBalance, amount);
            amount = BalanceService.GetBalance(nameMinus, balance);
            Assert.AreEqual(-cashBalance, amount);
        }
    }
}