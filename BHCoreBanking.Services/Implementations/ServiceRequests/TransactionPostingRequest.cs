using BHCoreBanking.Services.Contracts.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Implementations.ServiceRequests
{
    public class TransactionPostingRequest : ITransactionPostingRequest
    {
        public long DebitAccountID { get; set; }
        public long CreditAccountID { get; set; }
        public decimal Amount { get; set; }
    }
}
