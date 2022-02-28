using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Core.Contracts
{ 
    
    public interface IEntity
    {
        [DataMember]
        long ID { get; set; }
    }
}
