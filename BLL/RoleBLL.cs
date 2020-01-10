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
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoleBLL : IRoleBLL
    {
        
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IPowerDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; } 

        public static ObjectsMapper<Role, RoleDTO> RolemapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<Role, RoleDTO>();

        public static ObjectsMapper<RoleDTO, Role> RoleDTOmapperToModel = ObjectMapperManager.DefaultInstance.GetMapper<RoleDTO, Role>();

        public static ObjectsMapper<Power, PowerDTO> PowermapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<Power, PowerDTO>();

        public static ObjectsMapper<RolePower, RolePowerDTO> RolePowermapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<RolePower, RolePowerDTO>();

        public static ObjectsMapper<ResponseClass, ResponseClassDTO> ResponseClassmapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<ResponseClass, ResponseClassDTO>();

        public static ObjectsMapper<ResponseClass, ResponseClassDTO> ResponseClassMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<ResponseClass, ResponseClassDTO>();

        /// <summary>
        /// role单个新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(RoleDTO t)
        {
            var role = RoleDTOmapperToModel.Map(t);
            await roleDAL.AddAsync(role);
            await roleDAL.CommitAsync();
        }

        /// <summary>
        /// role单个删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(RoleDTO t)
        {
            var role = RoleDTOmapperToModel.Map(t);
            await roleDAL.DeleteAsync(role);
            await roleDAL.CommitAsync();
        }

        /// <summary>
        /// role单个删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteBySoftAsync(RoleDTO t)
        {
            var role = RoleDTOmapperToModel.Map(t);
            await roleDAL.DeleteBySoftAsync(role);
            await roleDAL.CommitAsync();
        }

        /// <summary>
        /// role动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Role, bool>> exp)
        {
            await roleDAL.DeleteAsync(exp);
            await roleDAL.CommitAsync();
        }

        /// <summary>
        /// role单个动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<RoleDTO> GetEntityAsync(Expression<Func<Role, bool>> exp)
        {
            var role=await roleDAL.GetEntityAsync(exp);
            return RolemapperToDTO.Map(role);
        }

        /// <summary>
        /// role动态多个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<RoleDTO>> GetFiltersAsync(Expression<Func<Role, bool>> exp)
        {
            var list=(await roleDAL.GetFiltersAsync(exp)).ToList();
            return RolemapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// role单个修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(RoleDTO t)
        {
            var role = RoleDTOmapperToModel.Map(t);
            await roleDAL.UpdateAsync(role);
            await roleDAL.CommitAsync();
        }

        /// <summary>
        /// 获取role的id对应的role和powers
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RolePowerDTO> GetRolePowerAsync(long Id)
        {
            //角色id查找对应的所有role和power
            var rolepower = await roleDAL.GetRolePowerAsync(Id);
            //数据库中所有的role
            var powers = (await powerDAL.GetFiltersAsync(e => e.IsDeleted == false)).ToList();
            var powersDTO = PowermapperToDTO.MapEnum(powers).ToList();
            foreach (var powerDTO in powersDTO)
            {
                if (rolepower.Powers.Select(e => e.Id).Contains(powerDTO.Id))
                {
                    powerDTO.IsChoosed = true;
                }
                else
                {
                    powerDTO.IsChoosed = false;
                }
            }
            return new RolePowerDTO() { RoleDTO = RolemapperToDTO.Map(rolepower.Role), PowerDTOs = powersDTO };
        }

        /// <summary>
        /// 修改rolr的id对应的role和role-power关联表
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="Power_Ids"></param>
        /// <returns></returns>
        public async Task<ResponseClassDTO> UpdateAsync(RoleDTO roleDTO, long[] Power_Ids)
        { 
            var role = RoleDTOmapperToModel.Map(roleDTO);
            var ResponseClass = await roleDAL.UpdateAsync(role,Power_Ids);
            return ResponseClassmapperToDTO.Map(ResponseClass);
        }

        /// <summary>
        /// 删除对应的role和与之关联的关联表
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        public async Task<ResponseClassDTO> DeletedAsync(long Id)
        {
            var responseClass=await roleDAL.DeletedAsync(Id);
            return ResponseClassMapperToDTO.Map(responseClass);
        }
    }
}
