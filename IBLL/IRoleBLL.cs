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
        Task AddAsync(RoleDTO t);

        Task DeleteAsync(RoleDTO t);

        Task DeleteAsync(Expression<Func<Role, bool>> exp);

        Task UpdateAsync(RoleDTO t);

        Task<RoleDTO> GetEntityAsync(Expression<Func<Role, bool>> exp);

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
