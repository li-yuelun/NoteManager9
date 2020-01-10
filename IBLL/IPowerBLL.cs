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
    public interface IPowerBLL
    {
        /// <summary>
        /// 单个权限新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(PowerDTO t);

        /// <summary>
        /// 单个权限删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteAsync(PowerDTO t);

        /// <summary>
        /// 单个权限删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteBySoftAsync(PowerDTO t);

        /// <summary>
        /// 权限动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Power, bool>> exp);

        /// <summary>
        /// 单个权限修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(PowerDTO t);

        /// <summary>
        /// 单个权限动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<PowerDTO> GetEntityAsync(Expression<Func<Power, bool>> exp);

        /// <summary>
        /// 多个权限动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<PowerDTO>> GetFiltersAsync(Expression<Func<Power, bool>> exp);

        /// <summary>
        /// 删除对应的power和与之关联的关联表
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        Task<ResponseClassDTO> DeletedAsync(long Id);
    }
}
