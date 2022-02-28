using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Implementations;
using BHCoreBanking.Data.Contracts;
using BHCoreBanking.Services.Contracts;
using BHCoreBanking.Services.Contracts.ServiceRequests;
using BHCoreBanking.Services.Implementations.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<ITransaction> _repo;
        private readonly IRepository<IAccount> _accountRepo;
        public TransactionService(IRepository<ITransaction> repository, IRepository<IAccount> accountRepo)
        {
            _repo = repository;
            _accountRepo = accountRepo;
        }

        public async Task<IEnumerable<ITransaction>> GetTransactionsAsync(ITransactionSearchRequest request)
        {
            return await _repo.SearchAsync(t => t.ID > 0);
        }

        public async Task<IEnumerable<ITransaction>> GetTransactionsByAccountIDAsync(long accountID)
        {
            return await _repo.SearchAsync(t => t.CreditAccountID == accountID || t.DebitAccountID ==  accountID);
        }

        public async Task<TransactionResponse> PostTransactionAsync(ITransactionPostingRequest request)
        {
            var transaction = new Transaction()
            {
                CreditAccountID = request.CreditAccountID,
                DebitAccountID = request.DebitAccountID,
                Amount = request.Amount,
                TransactionDate = DateTime.Now,
                TransactionReference = Guid.NewGuid(),
                Status = Core.Enums.TransactionStatus.Pending
            };

            transaction = (Transaction)await _repo.InsertAsync(transaction) ?? transaction;
             
            var creditAccount = await _accountRepo.GetAsync(request.CreditAccountID); 
            var debitAccount = await _accountRepo.GetAsync(request.DebitAccountID);

            if (creditAccount == null || debitAccount == null)
            {
                transaction.Status = Core.Enums.TransactionStatus.Failed;
                transaction.FailureMessage = $"Account with ID {(creditAccount == null ? request.CreditAccountID : request.DebitAccountID)} is not valid";
            } 
            else
            {
                var response = PerformTransactionValidationChecks(creditAccount, debitAccount, request.Amount);
                if (response.Status == Core.Enums.TransactionStatus.Failed)
                {
                    transaction.Status = response.Status;
                    transaction.FailureMessage = response.ResponseMessage; 
                }
                else
                {
                    var successful = await  _accountRepo.UpdateBatchAsync(t =>
                    {
                        if (t.ID == request.DebitAccountID) { t.Balance.Amount -= request.Amount; }
                        if (t.ID == request.CreditAccountID) { t.Balance.Amount += request.Amount; }
                    });

                    if (successful)
                    {
                        transaction.Status = Core.Enums.TransactionStatus.Successful;
                    }
                    else
                    {
                        transaction.Status = Core.Enums.TransactionStatus.Failed;
                        transaction.FailureMessage = $"Could not update balances"; 
                    } 
                }
            } 

            transaction = (Transaction)await _repo.UpdateAsync(transaction);

            return await Task.FromResult(new TransactionResponse() 
            { 
                Status = transaction ==  null ? Core.Enums.TransactionStatus.Pending : transaction.Status,
                ResponseMessage = transaction?.FailureMessage, 
                TransactionReference = transaction?.TransactionReference.ToString() 
            });
        }

        private TransactionResponse PerformTransactionValidationChecks(IAccount creditAccount, IAccount debitAccount, decimal amount)
        {
            TransactionResponse response = new TransactionResponse();
            if (debitAccount.Balance.Position == Core.Enums.PositionType.Debit || debitAccount.Balance.Amount < amount)
            {
                response.Status = Core.Enums.TransactionStatus.Failed;
                response.ResponseMessage = $"Insufficient funds on debit account : {debitAccount.AccountNumber}";
            }
            else if (debitAccount.Balance.CurrencyCode != creditAccount.Balance.CurrencyCode)
            {
                // Implement currency exchange logic
                response.Status = Core.Enums.TransactionStatus.Failed;
                response.ResponseMessage = $"Accounts have different currency codes";
            }
            return response;
        }

        
    }
}
