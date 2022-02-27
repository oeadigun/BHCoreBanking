using BHCoreBanking.Core.Contracts;
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
        public IEnumerable<ITransaction> GetTransactions(ITransactionSearchRequest request)
        {
            throw new NotImplementedException();
        }

        public TransactionResponse PostTransaction(ITransactionPostingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
