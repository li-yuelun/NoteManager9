using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentsDAL :ICommentsDAL
    {
        MongoDBHelper<Comments> mHelp = new MongoDBHelper<Comments>("mongodb://127.0.0.1:27017", "fj", "Comments");

        /// <summary>
        /// 单个评论新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(Comments t)
        {
            await mHelp.AddAsync(t);
        }

        public void Add(Comments t)
        {
            mHelp.Add(t);
        }

        /// <summary>
        /// 单个评论删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Comments t)
        {
            await mHelp.DeleteAsync(t);
        }

        public void Delete(Comments t)
        {
            mHelp.Delete(t);
        }

        /// <summary>
        /// 动态删除评论
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Comments, bool>> exp)
        {
            await mHelp.DeleteAsync(exp);
        }

        public void Delete(Expression<Func<Comments, bool>> exp)
        {
            mHelp.Delete(exp);
        }

        /// <summary>
        /// 单个评论动态查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<Comments> GetEntityAsync(Expression<Func<Comments, bool>> exp)
        {
            return await mHelp.GetEntityAsync(exp);
        }

        public Comments GetEntity(Expression<Func<Comments, bool>> exp)
        {
            return mHelp.GetEntity(exp);
        }

        /// <summary>
        /// 多个评论动态查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<Comments>> GetFiltersAsync(Expression<Func<Comments, bool>> exp)
        {
            return await mHelp.GetFilterAsync(exp);
        }

        public List<Comments> GetFilters(Expression<Func<Comments, bool>> exp)
        {
            return mHelp.GetFilter(exp);
        }

        /// <summary>
        /// 单个评论修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Comments t)
        {
            await mHelp.UpdateAsync(t);
        }

        public void Update(Comments t)
        {
            mHelp.Update(t);
        }
    }
}
