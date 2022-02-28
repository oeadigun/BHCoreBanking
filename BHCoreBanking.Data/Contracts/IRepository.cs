using BHCoreBanking.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BHCoreBanking.Data.Contracts
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> UpdateBatchAsync(Action<T> updateAction);
        Task<T> GetAsync(long id);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> query); 
    }
}
