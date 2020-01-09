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
    public interface IMusicBLL
    {

        /// <summary>
        /// 单个音乐新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(MusicDTO t);

        /// <summary>
        /// 单个音乐删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteAsync(MusicDTO t);

        /// <summary>
        /// 根据条件动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Music, bool>> exp);

        /// <summary>
        /// 单个修改音乐
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(MusicDTO t);

        /// <summary>
        /// 根据条件动态单个获取音乐
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<MusicDTO> GetEntityAsync(Expression<Func<Music, bool>> exp);

        /// <summary>
        /// 根据条件动态查询音乐集合
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<MusicDTO>> GetFiltersAsync(Expression<Func<Music, bool>> exp);
    }
}
