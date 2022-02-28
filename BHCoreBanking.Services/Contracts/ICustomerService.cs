using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Services.Contracts.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Contracts
{
    public interface ICustomerService
    {
        Task<ICustomer> CreateCustomerAsync(ICustomerCreationRequest request);
        Task<ICustomer> GetCustomerDetailsAsync(long id);
        Task<IEnumerable<ICustomer>> GetCustomersAsync();
    }
}
