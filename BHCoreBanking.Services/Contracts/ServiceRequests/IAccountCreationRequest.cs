using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Contracts.ServiceRequests
{
    public interface IAccountCreationRequest
    {
        public long CustomerID { get; set; }
        public decimal InitialCreditDeposit { get; set; }
        public string CurrencyCode { get; set; } 
        public AccountType GetAccountType();
    }
}
