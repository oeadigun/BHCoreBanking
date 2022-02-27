using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Contracts.ServiceRequests
{
    public interface ICustomerCreationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
