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
                    //CurrentDb.Insert(t);
                    Db.Insertable<T>(t).AddQueue();
                }
            });
        }

        public void Add(T t)
        {
            var _t = CurrentDb.GetSingle(e => e.Id == t.Id);
            if (_t == null)
            {
                //CurrentDb.Insert(t);
                Db.Insertable<T>(t).AddQueue();
            }
        }

        /// <summary>
        /// 单个对象删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T t)
        {
            await Task.Run(() => {
                //CurrentDb.Delete(t);
                Db.Deleteable<T>(t).AddQueue();
            });
        }

        public void Delete(T t)
        {
            //CurrentDb.Delete(t);
            Db.Deleteable<T>().AddQueue();
        }

        /// <summary>
        /// 单个对象删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteBySoftAsync(T t)
        {
            await Task.Run(() => {
                var _t = CurrentDb.GetSingle(e => e.Id == t.Id);
                if (_t != null)
                {
                    t.IsDeleted = true;
                    //CurrentDb.Update(t);
                    Db.Updateable<T>().AddQueue();
                }
            });
        }

        public void DeleteBySoft(T t)
        {
            var _t = CurrentDb.GetSingle(e => e.Id == t.Id);
            if (_t != null)
            {
                t.IsDeleted = true;
                //CurrentDb.Update(t);
                Db.Updateable<T>().AddQueue();
            }
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
                    //CurrentDb.Update(t);
                    Db.Updateable<T>(t).AddQueue();
                }
            });
        }

        public void Update(T t)
        {
            var _t = CurrentDb.GetSingle(e => e.Id == t.Id);
            if (_t != null)
            {
                //CurrentDb.Update(t);
                Db.Updateable<T>(t).AddQueue();
            }
        }

        /// <summary>
        /// 单个查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> exp)
        {
            //return CurrentDb.GetSingle(exp);
            return await Db.Queryable<T>().SingleAsync(exp);
        }

        public T GetEntity(Expression<Func<T, bool>> exp)
        {
            //return CurrentDb.GetSingle(exp);
            return Db.Queryable<T>().Single(exp);
        } 

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<T>> GetFiltersAsync(Expression<Func<T, bool>> exp)
        {
            //return await Task.Run(() => { return CurrentDb.GetList(exp); });
            return await Task.Run(() => {
                return Db.Queryable<T>().Where(exp);
            });
        }

        public ISugarQueryable<T> GetFilters(Expression<Func<T, bool>> exp)
        {
            //return CurrentDb.GetList(exp);
            return Db.Queryable<T>().Where(exp);
        }

        /// <summary>
        /// 动态删除 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<T, bool>> exp)
        {
            //await Task.Run(() => { return CurrentDb.Delete(exp); });
            await Task.Run(() => { Db.Deleteable<T>(exp).AddQueue(); });
        }

        public void Delete(Expression<Func<T, bool>> exp)
        {
            //CurrentDb.Delete(exp);
            Db.Deleteable<T>(exp).AddQueue() ;
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public async Task CommitAsync()
        {
            await Db.SaveQueuesAsync();
        }

        public void Commit()
        {
            Db.SaveQueues();
        }
    }
}
