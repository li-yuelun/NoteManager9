using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface ICommentsDAL
    {
         /// <summary>
         /// 单个评论新增
         /// </summary>
         /// <param name="comments"></param>
         /// <returns></returns>
         Task AddAsync(Comments comments);
         void Add(Comments comments);

         /// <summary>
         /// 单个评论删除
         /// </summary>
         /// <param name="comments"></param>
         /// <returns></returns>
         Task DeleteAsync(Comments comments);

         void Delete(Comments comments);

         /// <summary>
         /// 评论动态删除
         /// </summary>
         /// <param name="exp"></param>
         /// <returns></returns>
         Task DeleteAsync(Expression<Func<Comments, bool>> exp);

         void Delete(Expression<Func<Comments, bool>> exp);

         /// <summary>
         /// 单个评论修改
         /// </summary>
         /// <param name="comments"></param>
         /// <returns></returns>
         Task UpdateAsync(Comments comments);

         void Update(Comments comments);

         /// <summary>
         /// 动态获取单个评论
         /// </summary>
         /// <param name="exp"></param>
         /// <returns></returns>
         Task<Comments> GetEntityAsync(Expression<Func<Comments, bool>> exp);

         Comments GetEntity(Expression<Func<Comments, bool>> exp);

         /// <summary>
         /// 动态获取多个评论
         /// </summary>
         /// <param name="exp"></param>
         /// <returns></returns>
         Task<List<Comments>> GetFiltersAsync(Expression<Func<Comments, bool>> exp);

         List<Comments> GetFilters(Expression<Func<Comments, bool>> exp);
    }
}
