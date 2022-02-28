using BHCoreBanking.Core.Constants;
using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Implementations;
using BHCoreBanking.ResponseModels;
using BHCoreBanking.Services.Contracts;
using BHCoreBanking.Services.Contracts.ServiceRequests;
using BHCoreBanking.Services.Implementations.ServiceRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCoreBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;
        public CustomersController(
            ICustomerService customerService, 
            ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost] 
        public async Task<IActionResult> Create(CustomerCreationRequest request)
        {
            var customer = await _customerService.CreateCustomerAsync(request);

            if (customer == null)
            {
                _logger.LogDebug($"BAD Request: Url:{HttpContext.Request.Path} Message: {Newtonsoft.Json.JsonConvert.SerializeObject(request)}"); 

                return BadRequest();
            }

            _logger.LogInformation($"Customer created successfully: {customer.ID}");

            var response = new BaseGenericResponse<ICustomer>()
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Data = customer
            };

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(long id)
        {
            var customer = await _customerService.GetCustomerDetailsAsync(id);

            if (customer == null)
            {
                 var badRequestMessage = $"Customer with id {id} does not exist"; 

                _logger.LogDebug($"BAD Request: Url:{HttpContext.Request.Path} Message: {badRequestMessage}");

                return BadRequest(badRequestMessage);
            } 

            var response = new BaseGenericResponse<ICustomer>()
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Data = customer
            };
            
            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetCustomers()
        {
            var customer = await _customerService.GetCustomersAsync(); 

            var response = new BaseGenericResponse<IEnumerable<ICustomer>>()
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Data = customer
            };

            return Ok(response);
        }
    }
}
