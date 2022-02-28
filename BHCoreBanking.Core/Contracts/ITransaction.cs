using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace BHCoreBanking.Core.Contracts
{
    public interface ITransaction : IEntity
    {
        long DebitAccountID { get; set; }
        long CreditAccountID { get; set; }
        decimal Amount { get; set; }
        string FailureMessage { get; set; }
        Guid TransactionReference { get; set; } 
        TransactionStatus Status { get; set; }
        DateTime TransactionDate { get; set; }
    }
}
