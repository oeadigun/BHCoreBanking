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
    public class Transaction : Entity, ITransaction
    {
       
        public long DebitAccountID { get; set; }
        public long CreditAccountID { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        public string FailureMessage { get; set; }
        [DataMember]
        public TransactionStatus Status { get; set; }
        [DataMember]
        public DateTime TransactionDate { get; set; }
        [DataMember]
        public Guid TransactionReference { get; set; }
    }
}
