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
    public interface IRoleBLL
    {
        /// <summary>
        /// 单个角色新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(RoleDTO t);

        /// <summary>
        /// 单个角色删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteAsync(RoleDTO t);

        /// <summary>
        /// 单个角色删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteBySoftAsync(RoleDTO t);

        /// <summary>
        /// 角色动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Role, bool>> exp);

        /// <summary>
        /// 单个角色修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(RoleDTO t);

        /// <summary>
        /// 单个角色动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<RoleDTO> GetEntityAsync(Expression<Func<Role, bool>> exp);

        /// <summary>
        /// 多个角色动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<RoleDTO>> GetFiltersAsync(Expression<Func<Role, bool>> exp);

        /// <summary>
        /// 获取role的id对应的role和powers
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<RolePowerDTO> GetRolePowerAsync(long Id);

        /// <summary>
        /// 修改role对应的信息和气对应的关联关系
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Power_Ids"></param>
        /// <returns></returns>
        Task<ResponseClassDTO> UpdateAsync(RoleDTO roleDTO, long[] Power_Ids);

        /// <summary>
        /// 删除对应的role和与之关联的关联表
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        Task<ResponseClassDTO> DeletedAsync(long Id);

    }
}
