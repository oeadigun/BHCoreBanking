using BHCoreBanking.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Core.Implementations
{
    [DataContract]
    [Serializable]
    public class Entity : IEntity
    {
        [DataMember]
        public long ID { get; set; }
    }
}
