using BHCoreBanking.Core.Constants;
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
    public class AccountServiceTest
    {
        private readonly AccountService _accountService;

        public AccountServiceTest()
        {
            var store = new InMemoryStore<IAccount>();
            var transactionStore = new InMemoryStore<ITransaction>();
            var result  = store.SaveAsync(typeof(IAccount).Name, new List<IAccount>() { new Account
            {   
                ID = 1, 
                Balance = new Balance() 
                {   Amount = 5000, 
                    CurrencyCode = CurrencyCodes.USD, 
                    Position = Core.Enums.PositionType.Credit 
                } 
            } }).Result;

            _accountService = new AccountService(new Repository<IAccount>(store),
                new TransactionService(new Repository<ITransaction>(transactionStore),
                new Repository<IAccount>(store)));
        }
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task CreateAccount_When_ValidCustomerID_Expect_NewAccountWithIDAsync()
        {
            // Arrange
            CurrentAccountCreationRequest request = new CurrentAccountCreationRequest()
            {
                CurrencyCode = CurrencyCodes.USD,
                CustomerID = 1,
                InitialCreditDeposit = 0
            }; 

            //Act
            var actual = await _accountService.CreateAccountAsync(request);

            //Assert
            Assert.Greater(actual.ID, 0);
            Assert.AreEqual(request.InitialCreditDeposit, actual.Balance.Amount);
            Assert.AreEqual(request.CurrencyCode, actual.Balance.CurrencyCode); 
        }

        [Test]
        public async Task CreateAccount_When_ValidCustomerID_And_InitialCredit_Expect_NewAccountWithCreditBalanceAsync()
        {
            // Arrange
            CurrentAccountCreationRequest request = new CurrentAccountCreationRequest()
            {
                CurrencyCode = CurrencyCodes.USD,
                CustomerID = 1,
                InitialCreditDeposit = 1000
            };

            //Act
            var newAccount = await _accountService.CreateAccountAsync(request);

            //Assert
            Assert.Greater(newAccount.ID, 0); 
            var actual = await _accountService.GetAccountDetailsAsync(newAccount.ID); 
            Assert.AreEqual(request.InitialCreditDeposit, actual.Balance.Amount);
            Assert.AreEqual(request.CurrencyCode, actual.Balance.CurrencyCode);
        }
    }
}