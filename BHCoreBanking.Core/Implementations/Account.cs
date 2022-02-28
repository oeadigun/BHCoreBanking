using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Core.Implementations
{
    [DataContract]
    public class Account : Entity, IAccount
    {
        [DataMember]
        public long CustomerID { get; set; }
        [DataMember]
        public string AccountNumber { get; set; }
        [DataMember]
        public AccountType Type { get; set; }
        [DataMember]
        public IBalance Balance { get; set; }
    }
}
