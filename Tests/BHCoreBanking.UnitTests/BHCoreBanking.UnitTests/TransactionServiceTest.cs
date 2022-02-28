using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Implementations;
using BHCoreBanking.Data;
using BHCoreBanking.Data.Contracts;
using BHCoreBanking.Data.DataStore;
using BHCoreBanking.Services.Implementations;
using BHCoreBanking.Services.Implementations.ServiceRequests;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHCoreBanking.UnitTests
{
    public class TransactionServiceTest
    {
        private readonly TransactionService _transactionService;

        public TransactionServiceTest()
        {
            //var store = new InMemoryStore<ITransaction>(); 
            //var result = store.SaveAsync(typeof(ITransaction).Name, new List<ITransaction>() { new Transaction
            //{
            //    ID = 1, 
            //    Amount = 1000, 
            //    CreditAccountID = 1, 
            //    DebitAccountID = 1, 
            //    Status = Core.Enums.TransactionStatus.Successful,
            //    TransactionDate = System.DateTime.Now,
            //    TransactionReference = System.Guid.NewGuid()
            //} }).Result;

            //_transactionService = new TransactionService(new Repository<ITransaction>(store), );
        }
        [SetUp]
        public void Setup()
        {

        }

        //[Test]
        //public async Task CreateTransaction_When_ValidRequest_Expect_NewTransactionWithIDAsync()
        //{
        //    // Arrange
        //    TransactionCreationRequest request = new TransactionCreationRequest();
        //    request.FirstName = "Olanrewaju";
        //    request.LastName = "Adigun";

        //    //Act
        //    var actual = await _transactionService.CreateTransactionAsync(request);

        //    //Assert
        //    Assert.Greater(actual.ID, 0);
        //    Assert.AreEqual(request.FirstName, actual.FirstName);
        //    Assert.AreEqual(request.LastName, actual.LastName);
        //}

        //[Test]
        //public async Task GetTransactionDetailsAsync_When_ValidTransactionID_Expect_TransactionDetailsAsync()
        //{
        //    // Arrange
        //    var stubStore = new Mock<IDataStore>();
        //    var expectedTransaction = new Transaction() { ID = 1, FirstName = "Lanre", LastName = "Adigun" };
        //    stubStore.Setup(t => t.GetAsync(typeof(ITransaction).Name)).ReturnsAsync(new List<ITransaction>() { expectedTransaction });  

        //    var TransactionService = new TransactionService(new Repository<ITransaction>(stubStore.Object)); 

        //    //Act
        //    var actual = await TransactionService.GetTransactionDetailsAsync(expectedTransaction.ID);

        //    //Assert
        //    Assert.AreEqual(expectedTransaction.ID, actual.ID);
        //    Assert.AreEqual(expectedTransaction.FirstName, actual.FirstName);
        //    Assert.AreEqual(expectedTransaction.LastName, actual.LastName);
        //}
    }
}