
using DAL;
using DTO;
using EmitMapper;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBLL : IUserBLL
    {
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IPowerDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public static ObjectsMapper<User, UserDTO> UserMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<User, UserDTO>();

        public static ObjectsMapper<UserDTO, User> UserDTOMapperToModel = ObjectMapperManager.DefaultInstance.GetMapper<UserDTO, User>();

        public static ObjectsMapper<Role, RoleDTO> RoleMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<Role, RoleDTO>();

        public static ObjectsMapper<UserRolePower, UserRolePowerDTO> UserRolePowerMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<UserRolePower, UserRolePowerDTO>();

        public static ObjectsMapper<UserRole, UserRoleDTO> UserRoleMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<UserRole, UserRoleDTO>();

        public static ObjectsMapper<ResponseClass, ResponseClassDTO> ResponseClassMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<ResponseClass, ResponseClassDTO>();

        /// <summary>
        /// 单个用户新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(UserDTO t)
        {
            var user = UserDTOMapperToModel.Map(t);
            await userDAL.AddAsync(user);
            await userDAL.CommitAsync();
        }

        /// <summary>
        /// 单个用户删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(UserDTO t)
        {
            var user = UserDTOMapperToModel.Map(t);
            await userDAL.DeleteAsync(user);
             await userDAL.CommitAsync();
        }

        /// <summary>
        /// 单个用户删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteBySoftAsync(UserDTO t)
        {
            var user = UserDTOMapperToModel.Map(t);
            await userDAL.DeleteBySoftAsync(user);
            await userDAL.CommitAsync();
        }

        /// <summary>
        /// 用户动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<User, bool>> exp)
        {
            await userDAL.DeleteAsync(exp);
            await userDAL.CommitAsync();
        }

        /// <summary>
        /// 单个用户动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetEntityAsync(Expression<Func<User, bool>> exp)
        {
            var user= await userDAL.GetEntityAsync(exp);
            return UserMapperToDTO.Map(user);
        }

        /// <summary>
        /// 多个用户动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<UserDTO>> GetFiltersAsync(Expression<Func<User, bool>> exp)
        {
            var list=(await userDAL.GetFiltersAsync(exp)).ToList();
            return UserMapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// 单个用户修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UserDTO t)
        {
            var user = UserDTOMapperToModel.Map(t);
            await userDAL.UpdateAsync(user);
            await userDAL.CommitAsync();
        }

        /// <summary>
        /// 显示用户的所有角色和权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<UserRolePowerDTO>> GetUserRolePowers(long Id)
        {
             var list=await userDAL.GetUserRolePowersAsync(Id);
             return UserRolePowerMapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// 显示用户的所有角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<UserRoleDTO> GetUserRoles(long Id)
        {
            //id对应的userrole
            var userrole= await userDAL.GetUserRoleAsync(Id);
            //数据库中所有的role信息
            var roles = (await roleDAL.GetFiltersAsync(e => true)).ToList();
            var roleDTOs = RoleMapperToDTO.MapEnum(roles).ToList();
            //如果user的id对应的role在数据库中的role中,设置ischoosed为true
            foreach (var roleDTO in roleDTOs)
            {
                if (userrole.Roles.Select(e => e.Id).Contains(roleDTO.Id))
                {
                    roleDTO.IsChoosed = true;
                }
                else
                {
                    roleDTO.IsChoosed = false;
                }
            }
            return new UserRoleDTO() { UserDTO=UserMapperToDTO.Map(userrole.User),RoleDTOs= roleDTOs };
        }

        public async Task<ResponseClassDTO> UpdateAsync(UserDTO userDTO, long[] Role_Ids)
        {
            var user = UserDTOMapperToModel.Map(userDTO);
            var responseClass=await userDAL.UpdateAsync(user,Role_Ids);
            return ResponseClassMapperToDTO.Map(responseClass);
        }

        /// <summary>
        /// 删除对应的user和与之关联的关联表
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        public async Task<ResponseClassDTO> DeletedAsync(long Id)
        {
            var responseClass = await userDAL.DeletedAsync(Id);
            return ResponseClassMapperToDTO.Map(responseClass);
        }
    }
}
