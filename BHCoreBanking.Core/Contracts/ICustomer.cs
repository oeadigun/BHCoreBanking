using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Core.Contracts
{
    public interface ICustomer : IEntity
    {

        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
    }
}
