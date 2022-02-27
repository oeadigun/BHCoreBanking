using BHCoreBanking.Services.Contracts.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Implementations.ServiceRequests
{
    public class CustomerCreationRequest : ICustomerCreationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
