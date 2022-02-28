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
        public TransactionStatus Status { get; set; }
        public string TransactionReference { get; set; }
        public string ResponseMessage { get; set; }
    }
}
