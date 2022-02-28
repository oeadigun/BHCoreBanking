using BHCoreBanking.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Data.Contracts
{
    public interface IDataStore<T> where T : IEntity
    {
        public Task<bool> SaveAsync(string key, IEnumerable<T> data);
        public Task<IEnumerable<T>> GetAsync(string key);
    }
}
