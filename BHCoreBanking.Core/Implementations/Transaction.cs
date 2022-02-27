using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Core.Implementations
{
    public class Transaction : Entity, ITransaction
    {
        public long AccountID { get; set; }
        public decimal Amount { get; set; }
        public PositionType RecordType { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
