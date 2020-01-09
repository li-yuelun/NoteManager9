using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IUserDAL : IGeneralDAL<User>
    {
        /// <summary>
        /// 根据user的id查询所有的与之对应的UserRolePower集合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<UserRolePower>> GetUserRolePowersAsync(long Id);

        /// <summary>
        /// 根据user的id查询所有的与之对应的User和Role集合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<UserRole> GetUserRoleAsync(long Id);

        /// <summary>
        /// 修改用户及其对应的角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Role_Ids"></param>
        /// <returns></returns>
        Task<ResponseClass> UpdateAsync(User user, long[] Role_Ids);

        /// <summary>
        /// 删除对应的user和与之关联的关联表
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        Task<ResponseClass> DeletedAsync(long Id);

    }
}
