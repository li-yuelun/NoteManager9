using IDAL;
using Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAL<T> : MyDbContext<T>, IGeneralDAL<T> where T : class, IModel,new()
    {
        /// <summary>
        /// 单个新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(T t)
        {
            await Task.Run(()=> {
                var _t = CurrentDb.GetSingle(e => e.Id == t.Id);
                if (_t == null)
                {
                    CurrentDb.Insert(t);
                }
            });
        }

        public void Add(T t)
        {
            var _t = CurrentDb.GetSingle(e => e.Id == t.Id);
            if (_t == null)
            {
                CurrentDb.Insert(t);
            }
        }

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T t)
        {
            await Task.Run(() => { CurrentDb.Delete(t); });
        }

        public void Delete(T t)
        {
            CurrentDb.Delete(t);
        }

        /// <summary>
        /// 单个修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T t)
        {
            await Task.Run(() => {
                var _t = CurrentDb.GetSingle(e=>e.Id==t.Id);
                if (_t != null)
                {
                    CurrentDb.Update(t);
                }
            });
        }

        public void Update(T t)
        {
            var _t = CurrentDb.GetSingle(e => e.Id == t.Id);
            if (_t != null)
            {
                CurrentDb.Update(t);
            }
        }

        /// <summary>
        /// 单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> exp)
        {
            return await Task.Run(() => { return CurrentDb.GetSingle(exp); });
        }

        public T GetEntity(Expression<Func<T, bool>> exp)
        {
            return CurrentDb.GetSingle(exp);
        } 

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<T>> GetFiltersAsync(Expression<Func<T, bool>> exp)
        {
            return await Task.Run(() => { return CurrentDb.GetList(exp); });
        }

        public List<T> GetFilters(Expression<Func<T, bool>> exp)
        {
            return CurrentDb.GetList(exp);
        }

        /// <summary>
        /// 多条件删除 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<T, bool>> exp)
        {
            await Task.Run(() => { return CurrentDb.Delete(exp); });
        }

        public void Delete(Expression<Func<T, bool>> exp)
        {
            CurrentDb.Delete(exp);
        }
    }
}
