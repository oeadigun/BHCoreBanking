using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Services.Contracts;
using BHCoreBanking.Services.Contracts.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Implementations
{
    public class AccountService : IAccountService
    {
        public IAccount CreateAccount(IAccountCreationRequest request)
        {
            throw new NotImplementedException();
        }

        public IAccount GetAccountDetails(long id)
        {
            throw new NotImplementedException();
        }
    }
}
