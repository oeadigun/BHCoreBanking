using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Services.Implementations.ServiceResponses
{
    public class TransactionResponse
    {
        TransactionStatus Status { get; set; }
        string ResponseMessage { get; set; }
    }
}
