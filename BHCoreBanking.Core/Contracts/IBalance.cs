using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Core.Contracts
{
    public interface IBalance
    {
        string CurrencyCode { get; set; }
        PositionType Position { get; set; }
        decimal Amount { get; set; }
    }
}
