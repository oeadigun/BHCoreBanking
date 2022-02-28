using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Implementations;
using BHCoreBanking.Data;
using BHCoreBanking.Data.Contracts;
using BHCoreBanking.Data.DataStore;
using BHCoreBanking.Services.Implementations;
using BHCoreBanking.Services.Implementations.ServiceRequests;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BHCoreBanking.UnitTests
{
    public class CustomerServiceTest
    {
        private readonly CustomerService _customerService;

        public CustomerServiceTest()
        {
            _customerService = new CustomerService(new Repository<ICustomer>(new InMemoryStore<ICustomer>()));
        }
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task CreateCustomer_When_ValidRequest_Expect_NewCustomerWithIDAsync()
        {
            // Arrange
            CustomerCreationRequest request = new CustomerCreationRequest();
            request.FirstName = "Olanrewaju";
            request.LastName = "Adigun";

            //Act
            var actual = await _customerService.CreateCustomerAsync(request);

            //Assert
            Assert.Greater(actual.ID, 0);
            Assert.AreEqual(request.FirstName, actual.FirstName);
            Assert.AreEqual(request.LastName, actual.LastName);
        }

        [Test]
        public async Task GetCustomerDetailsAsync_When_ValidCustomerID_Expect_CustomerDetailsAsync()
        {
            // Arrange
            var stubStore = new Mock<IDataStore<ICustomer>>();
            var expectedCustomer = new Customer() { ID = 1, FirstName = "Lanre", LastName = "Adigun" };
            stubStore.Setup(t => t.GetAsync(typeof(ICustomer).Name)).ReturnsAsync(new List<ICustomer>() { expectedCustomer });  

            var customerService = new CustomerService(new Repository<ICustomer>(stubStore.Object)); 

            //Act
            var actual = await customerService.GetCustomerDetailsAsync(expectedCustomer.ID);

            //Assert
            Assert.AreEqual(expectedCustomer.ID, actual.ID);
            Assert.AreEqual(expectedCustomer.FirstName, actual.FirstName);
            Assert.AreEqual(expectedCustomer.LastName, actual.LastName);
        }
    }
}