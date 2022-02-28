using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCoreBanking.ResponseModels
{
    public class BaseGenericResponse<T>
    { 
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T Data { get; set; }
    }
}
