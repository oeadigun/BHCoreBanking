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
    public class Balance : IBalance
    {
        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public PositionType Position { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
    }
}
