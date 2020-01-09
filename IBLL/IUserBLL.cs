﻿using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IUserBLL
    {
        Task AddAsync(UserDTO t);

        Task DeleteAsync(UserDTO t);

        Task DeleteAsync(Expression<Func<User, bool>> exp);

        Task UpdateAsync(UserDTO t);

        Task<UserDTO> GetEntityAsync(Expression<Func<User, bool>> exp);

        Task<List<UserDTO>> GetFiltersAsync(Expression<Func<User, bool>> exp);

        /// <summary>
        /// 显示用户的所有角色和权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<List<UserRolePowerDTO>> GetUserRolePowers(long Id);

        /// <summary>
        /// 显示用户的所有角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<UserRoleDTO> GetUserRoles(long Id);

        /// <summary>
        /// 修改对应user的信息及其对应的角色
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="Role_Ids"></param>
        /// <returns></returns>
        Task<ResponseClassDTO> UpdateAsync(UserDTO userDTO, long[] Role_Ids);

        /// <summary>
        /// 删除对应的user和与之关联的关联表
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        Task<ResponseClassDTO> DeletedAsync(long Id);
    }
}