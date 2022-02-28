using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Data.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Data.DataStore
{
    public class InMemoryStore<T> : IDataStore<T> where T: IEntity
    {
        public static ConcurrentDictionary<string, List<T>> _store = new ConcurrentDictionary<string, List<T>>();

        public async Task<IEnumerable<T>> GetAsync(string key)
        {
            return await Task.FromResult(_store.GetOrAdd(key, new List<T>()));
        }

        public async Task<bool> SaveAsync(string key, IEnumerable<T> data)
        {
            if(!_store.ContainsKey(key))
            {
                return await Task.FromResult(_store.TryAdd(key, data?.ToList() ?? new List<T>()));
            }
            else
            {
                _store.TryGetValue(key, out List<T> existingValue);
                return await Task.FromResult(_store.TryUpdate(key, data?.ToList() ?? new List<T>(), existingValue));
            } 
        }
    }
}
