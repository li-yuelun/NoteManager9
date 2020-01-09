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

        Task DeleteAsync(SingerDTO t);

        Task DeleteAsync(Expression<Func<Singer, bool>> exp);

        Task UpdateAsync(SingerDTO t);

        Task<SingerDTO> GetEntityAsync(Expression<Func<Singer, bool>> exp);

        Task<List<SingerDTO>> GetFiltersAsync(Expression<Func<Singer, bool>> exp);

        //获取歌手集合（但是歌手的名字为拼音缩写）
        //Task<List<SingerDTO>> GetSingerDTOsToFirstEnglish(Expression<Func<Singer, bool>> exp);
    }
}
