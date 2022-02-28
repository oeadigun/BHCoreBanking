using BHCoreBanking.Core.Constants;
using BHCoreBanking.Core.Contracts;
using BHCoreBanking.ResponseModels;
using BHCoreBanking.Services.Contracts;
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
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger _logger; 
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService, ILogger<TransactionsController> logger)
        { 
            _transactionService = transactionService;
            _logger = logger;
        }
        [HttpGet]
        [Route("api/[controller]/account/{accountID}")]
        public async Task<IActionResult> GetTransactionsByAccountID(long accountID)
        {
            var transactions = await _transactionService.GetTransactionsByAccountIDAsync(accountID);

            var response = new BaseGenericResponse<IEnumerable<ITransaction>>()
            {
                ResponseCode = ResponseCodes.SUCCESS,
                Data = transactions
            };

            return Ok(response);
        }
    }
}
