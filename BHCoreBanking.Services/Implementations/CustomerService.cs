using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Implementations;
using BHCoreBanking.Data.Contracts;
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
        private readonly IRepository<ICustomer> _repo;
        public CustomerService(IRepository<ICustomer> repository)
        {
            _repo = repository;
        }
        public async Task<ICustomer> CreateCustomerAsync(ICustomerCreationRequest request)
        {
            var customer = new Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            return await _repo.InsertAsync(customer);
        }

        public async Task<ICustomer> GetCustomerDetailsAsync(long id)
        {
            return await _repo.GetAsync(id);
        }

        public Task<IEnumerable<ICustomer>> GetCustomersAsync()
        {
            return _repo.SearchAsync(t => t.ID > 0);
        }
    }
}
