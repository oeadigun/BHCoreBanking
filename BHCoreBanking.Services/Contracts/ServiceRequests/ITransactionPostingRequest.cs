using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Contracts.ServiceRequests
{
    public interface ITransactionPostingRequest
    {
        public long DebitAccountID { get; set; }
        public long CreditAccountID { get; set; }
        public decimal Amount { get; set; }
    }
}
