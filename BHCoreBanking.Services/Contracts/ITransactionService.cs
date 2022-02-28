using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Enums;
using BHCoreBanking.Services.Contracts.ServiceRequests;
using BHCoreBanking.Services.Implementations.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BHCoreBanking.Services.Contracts
{
    public interface ITransactionService
    {
        Task<IEnumerable<ITransaction>> GetTransactionsAsync(ITransactionSearchRequest request);
        Task<TransactionResponse> PostTransactionAsync(ITransactionPostingRequest request); 
        Task<IEnumerable<ITransaction>> GetTransactionsByAccountIDAsync(long accountID);
    }
}
