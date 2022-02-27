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
    public class CustomerService : ICustomerService
    { 
        public ICustomer CreateCustomer(ICustomerCreationRequest request)
        {
            throw new NotImplementedException();
        }

        public ICustomer GetCustomerDetails(long id)
        {
            throw new NotImplementedException();
        }
    }
}
