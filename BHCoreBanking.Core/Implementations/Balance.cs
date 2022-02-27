using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Core.Implementations
{
    public class Balance : IBalance
    {
        public string CurrencyCode { get; set; }
        public PositionType Position { get; set; }
        public decimal Amount { get; set; }
    }
}
