using Model;
using SqlSugar;
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
        /// <summary>
        /// 单个对象新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(T t);

        void Add(T t);

       /// <summary>
       /// 单个对象删除(真实删除)
       /// </summary>
       /// <param name="t"></param>
       /// <returns></returns>
        Task DeleteAsync(T t);

        void Delete(T t);

        /// <summary>
        /// 单个对象删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteBySoftAsync(T t);

        void DeleteBySoft(T t);

        /// <summary>
        /// 对象动态删除(真实删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<T, bool>> exp);

        void Delete(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 单个对象修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(T t);

        void Update(T t);

        /// <summary>
        /// 单个对象动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<T> GetEntityAsync(Expression<Func<T,bool>> exp);

        T GetEntity(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 多个对象动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<ISugarQueryable<T>> GetFiltersAsync(Expression<Func<T, bool>> exp);

        ISugarQueryable<T> GetFilters(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 事务提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 事务提交(异步)
        /// </summary>
        Task CommitAsync();
    }
}
