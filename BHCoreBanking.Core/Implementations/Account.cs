using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Core.Implementations
{
    public class Account : Entity, IAccount
    {
        public long CustomerID { get; set; }
        public AccountType Type { get; set; } 
        public IBalance Balance { get; set; }
    }
}
