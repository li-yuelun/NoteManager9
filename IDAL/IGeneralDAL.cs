using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IGeneralDAL<T> where T:class,IModel
    {
        Task AddAsync(T t);

        void Add(T t);

        Task DeleteAsync(T t);

        void Delete(T t);

        Task DeleteAsync(Expression<Func<T, bool>> exp);

        void Delete(Expression<Func<T, bool>> exp);

        Task UpdateAsync(T t);

        void Update(T t);

        Task<T> GetEntityAsync(Expression<Func<T,bool>> exp);

        T GetEntity(Expression<Func<T, bool>> exp);

        Task<List<T>> GetFiltersAsync(Expression<Func<T, bool>> exp);

        List<T> GetFilters(Expression<Func<T, bool>> exp);
    }
}
