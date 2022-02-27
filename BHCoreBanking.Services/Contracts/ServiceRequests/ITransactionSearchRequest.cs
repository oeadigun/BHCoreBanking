using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Contracts.ServiceRequests
{
    public interface ITransactionSearchRequest
    {
        long AccountID { get; set; } 
        TransactionStatus? Status { get; set; } 
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        bool OrderDesc { get; set; }
    }
}
