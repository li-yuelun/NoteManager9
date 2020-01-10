using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface INoteBLL
    {
        /// <summary>
        /// 单个日志新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(NoteDTO t);

        /// <summary>
        /// 单个日志删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteAsync(NoteDTO t);

        /// <summary>
        /// 单个日志删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteBySoftAsync(NoteDTO t);

        /// <summary>
        /// 日志动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Note, bool>> exp);

        /// <summary>
        /// 单个日志修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(NoteDTO t);

        /// <summary>
        /// 单个日志动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<NoteDTO> GetEntityAsync(Expression<Func<Note, bool>> exp);

        /// <summary>
        /// 多个日志动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<NoteDTO>> GetFiltersAsync(Expression<Func<Note, bool>> exp);
    }
}
