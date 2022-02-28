using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Implementations;
using BHCoreBanking.Data.Contracts;
using BHCoreBanking.Services.Contracts;
using BHCoreBanking.Services.Contracts.ServiceRequests;
using BHCoreBanking.Services.Implementations.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<IAccount> _repo;
        private readonly ITransactionService _transactionService;
        public AccountService(IRepository<IAccount> repository, ITransactionService transactionService)
        {
            _repo = repository;
            _transactionService = transactionService;
        }

        public async Task<IAccount> CreateAccountAsync(IAccountCreationRequest request)
        {
            var account = new Account()
            {
                Type = request.GetAccountType(),
                CustomerID = request.CustomerID,
                AccountNumber = $"{request.GetAccountType().ToString().Substring(0,1)} {new Random().Next(1,2000)}{request.CustomerID}".PadLeft(10, '0'),
                Balance = new Balance() { CurrencyCode = request.CurrencyCode, Position = Core.Enums.PositionType.Credit, Amount = 0}
            };

            var newAccount = await _repo.InsertAsync(account);

            if (newAccount?.ID > 0 && request.InitialCreditDeposit > 0)
            {
                var postingRequest = new TransactionPostingRequest()
                {
                    Amount = request.InitialCreditDeposit,
                    CreditAccountID = newAccount.ID,
                    DebitAccountID = 1,
                }; 

                var postingResponse  = await _transactionService.PostTransactionAsync(postingRequest);
            };

            return newAccount;
        }

        public Task<IAccount> GetAccountDetailsAsync(long id)
        {
            return _repo.GetAsync(id);
        }

        public Task<IEnumerable<IAccount>> GetAccountsByCustomerID(long customerID)
        {
            return _repo.SearchAsync(t => t.CustomerID == customerID);
        }

        public async Task<bool> UpdateBalances(long creditAccount, long debitAccount, decimal amount)
        { 
            return await _repo.UpdateBatchAsync(t => 
            { 
                if (t.ID == debitAccount) { t.Balance.Amount -= amount; }
                if (t.ID == creditAccount) { t.Balance.Amount += amount; }
            });
        }
    }
}
