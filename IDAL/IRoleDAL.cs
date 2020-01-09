using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IRoleDAL : IGeneralDAL<Role>
    {
        /// <summary>
        /// 根据role的id查询对应的role和powers
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<RolePower> GetRolePowerAsync(long Id);

        /// <summary>
        /// 修改role对应的信息和气对应的关联关系
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Power_Ids"></param>
        /// <returns></returns>
        Task<ResponseClass> UpdateAsync(Role role, long[] Power_Ids);

        /// <summary>
        /// 删除对应的role和与之关联的关联信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ResponseClass> DeletedAsync(long Id);
    }
}
