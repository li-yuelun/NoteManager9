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
    public interface ISingerBLL
    {
        /// <summary>
        /// 单个歌手新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(SingerDTO t);

        /// <summary>
        /// 单个歌手删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteAsync(SingerDTO t);

        /// <summary>
        /// 歌手动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Singer, bool>> exp);

        /// <summary>
        /// 单个歌手新增单个修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(SingerDTO t);

        /// <summary>
        /// 单个歌手动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<SingerDTO> GetEntityAsync(Expression<Func<Singer, bool>> exp);

        /// <summary>
        /// 多个歌手动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<SingerDTO>> GetFiltersAsync(Expression<Func<Singer, bool>> exp);

        //获取歌手集合（但是歌手的名字为拼音缩写）
        //Task<List<SingerDTO>> GetSingerDTOsToFirstEnglish(Expression<Func<Singer, bool>> exp);
    }
}
