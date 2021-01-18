using NUnit.Framework;
using Moq;
using SharingTransactionApp.Models.Services;
using SharingTransactionApp.Models.Inerfaces;
using SharingTransactionApp.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace NUnitTestProject1
{
    public class BalanceServicesTests
    {
        BalanceService service;
        
        [SetUp]
        public void Setup()
        {

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
                CashBalance = cashBalance 
            };
            var amount = BalanceService.GetBalance(namePlus, balance);
            Assert.AreEqual(cashBalance, amount);
            amount = BalanceService.GetBalance(nameMinus, balance);
            Assert.AreEqual(-cashBalance, amount);
        }
        [Test]
        public void GetBalancesTest()
        {
            var name = "TestName";
            var user = new AppUser { Name = name };
            var balanceName1 = "balanceName1";
            var balanceUser1 = new AppUser { Name = balanceName1};
            var balanceName2 = "balanceName2";
            var balanceUser2 = new AppUser { Name = balanceName2 };
            var balancePlus = 12.12;
            var balanceMinus = 34.34;
            var balance1 = new Balance { CashBalance = balancePlus, PersonPlus = user, PersonMinus = balanceUser1 };
            var balance2 = new Balance { CashBalance = balanceMinus, PersonPlus = balanceUser2, PersonMinus = user};
            var mockCursor = new Mock<IAsyncCursor<Balance>>();
            mockCursor.Setup(c => c.Current).Returns(new List<Balance> { balance1, balance2 });
            mockCursor.SetupSequence(c => c.MoveNext(default)).Returns(true).Returns(false);

            var mockCollectionMongo = new Mock<IMongoCollection<Balance>>();
            Expression<Func<Balance, bool>> expression = e => e.PersonMinus.Name == name || e.PersonPlus.Name == name;
            mockCollectionMongo.Setup(c => c.FindSync<Balance>(It.IsAny<FilterDefinition<Balance>>(), It.IsAny<FindOptions<Balance, Balance>>(), default))
                .Returns(mockCursor.Object);

            var mongoMoq = new Mock<IMongoService>();
            mongoMoq.Setup(m => m.BalanceCollection).Returns(mockCollectionMongo.Object);
            service = new BalanceService(mongoMoq.Object);
            IList<UserBalance> result = service.GetBalances(name);
            IList<UserBalance> expectedResult = new List<UserBalance> {
                new UserBalance { Cash=balancePlus,Name=balanceName1},
                new UserBalance{Cash=-balanceMinus,Name=balanceName2 } };

            Assert.AreEqual(expectedResult[0].Name,result[0].Name);
            Assert.AreEqual(expectedResult[0].Cash,result[0].Cash);
            Assert.AreEqual(expectedResult[1].Name,result[1].Name);
            Assert.AreEqual(expectedResult[1].Cash,result[1].Cash);
        }
    }
}