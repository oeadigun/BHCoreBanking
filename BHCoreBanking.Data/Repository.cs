using BHCoreBanking.Core.Contracts;
using BHCoreBanking.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BHCoreBanking.Data
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly IDataStore<T> _store;
        private object obj = new object();
        public Repository(IDataStore<T> store)
        {
            _store = store;
        }
        public async Task<T> GetAsync(long id)
        {
            var dataSet = await _store.GetAsync(typeof(T).Name) as List<T> ?? new List<T>();
            return dataSet.Where(t => t.ID == id).FirstOrDefault();
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null) return entity;
            lock (obj)
            {
                var dataSet = _store.GetAsync(typeof(T).Name).Result as List<T>;
                entity.ID = dataSet.Count + 1;

                dataSet.Add(entity);

                var saved = _store.SaveAsync(typeof(T).Name, (IEnumerable<T>)dataSet).Result;
                if (saved)
                {
                    return entity;
                }
                return default(T);
            }
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> query)
        {
            var dataSet = await _store.GetAsync(typeof(T).Name) as List<T> ?? new List<T>();

            dataSet = dataSet.AsQueryable().Where(query).ToList();
            return dataSet;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var dataSet = await _store.GetAsync(typeof(T).Name) as List<T> ?? new List<T>();
            int index = dataSet.IndexOf(entity);

            if (index >= 0)
            {
                dataSet.RemoveAt(index);
                dataSet.Add(entity);
                var updated = await _store.SaveAsync(typeof(T).Name, (IEnumerable<T>)dataSet);
                if (updated)
                {
                    return entity;
                }
            }
            return default(T);
        }

        public async Task<bool> UpdateBatchAsync(Action<T> updateAction)
        {

            lock (obj)
            {
                var dataSet = _store.GetAsync(typeof(T).Name).Result as List<T> ?? new List<T>();
                dataSet.ForEach(updateAction);
                return _store.SaveAsync(typeof(T).Name, (IEnumerable<T>)dataSet).Result;
            }
        }
    }
}
