using BHCoreBanking.Core.Enums;
using BHCoreBanking.Services.Contracts.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Implementations.ServiceRequests
{
    public class TransactionSearchRequest : ITransactionSearchRequest
    {
        public long AccountID { get; set; }
        public TransactionStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool OrderDesc { get; set; }
    }
}
