using BHCoreBanking.Core.Constants;
using BHCoreBanking.Core.Contracts;
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
    
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAccountService _accountService; 
        private readonly ICustomerService _customerService;
        private readonly ITransactionService _transactionService;
        public AccountsController(IAccountService accountService, ICustomerService customerService, ITransactionService transactionService,  ILogger<AccountsController> logger)
        {
            _accountService = accountService; 
            _customerService = customerService;
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Create(CurrentAccountCreationRequest request)
        {
            var account = await _accountService.CreateAccountAsync(request);

            if (account == null)
            {
                _logger.LogDebug($"BAD Request: Url:{HttpContext.Request.Path} Message: {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");

                return BadRequest();
            }

            _logger.LogInformation($"Account created successfully: {account.ID}");

            var response = new BaseGenericResponse<IAccount>()
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Data = account
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetAcountDetails(long id)
        {
            var account = await _accountService.GetAccountDetailsAsync(id);

            if (account == null)
            {
                var badRequestMessage = $"Account with id {id} does not exist";

                _logger.LogDebug($"BAD Request: Url:{HttpContext.Request.Path} Message: {badRequestMessage}");

                return BadRequest(badRequestMessage);
            }

            var customer = await _customerService.GetCustomerDetailsAsync(account.CustomerID);

            var transactionFilter = new TransactionSearchRequest()
            {
                AccountID = id,
                OrderDesc = true,
                Status = Core.Enums.TransactionStatus.Successful
            };

            var transactions = await _transactionService.GetTransactionsAsync(transactionFilter);

            var response = new BaseGenericResponse<object>()
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Data = new
                {
                    Account =  account,
                    Customer = customer,
                    Transactions = transactions
                }
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("api/[controller]/customer/{customerID}")] 
        public async Task<IActionResult> GetAccountsByCustomerID(long customerID)
        {
            var accounts = await _accountService.GetAccountsByCustomerID(customerID);
 
            var response = new BaseGenericResponse<object>()
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Data =  accounts
            };

            return Ok(response);
        }

    }
}
