using BHCoreBanking.Core.Enums; 
using BHCoreBanking.Services.Contracts.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Implementations.ServiceRequests
{
    public class CurrentAccountCreationRequest : IAccountCreationRequest
    {
        public long CustomerID { get; set; }
        public decimal InitialCreditDeposit { get; set; }
        public string CurrencyCode { get; set; }

        public AccountType GetAccountType()
        {
            return AccountType.Current;
        }
    }
}
