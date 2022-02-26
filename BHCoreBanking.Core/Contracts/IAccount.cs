using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Core.Contracts
{
    public interface IAccount : IEntity
    { 
        long CustomerID { get; set; }
        AccountType Type { get; set; }
    }
}
