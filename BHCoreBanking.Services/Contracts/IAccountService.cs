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
        IAccount CreateAccount(IAccountCreationRequest request);
        IAccount GetAccountDetails(long id);
    }
}
