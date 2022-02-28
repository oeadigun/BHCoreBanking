using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Services.Contracts.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Contracts
{
    public interface IAccountService
    {
        Task<IAccount> CreateAccountAsync(IAccountCreationRequest request);
        Task<IAccount> GetAccountDetailsAsync(long id);
        Task<IEnumerable<IAccount>> GetAccountsByCustomerID(long customerID);
        Task<bool> UpdateBalances(long creditAccount, long debitAccount, decimal amount);
    }
}
