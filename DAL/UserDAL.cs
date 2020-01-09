using IDAL;
using Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL : GeneralDAL<User>, IUserDAL
    {

        /// <summary>
        /// 根据user的id查询用户的权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<List<UserRolePower>> GetUserRolePowersAsync(long Id)
        {
            return await Db.Queryable<User, UserRoleRelation, Role, RolePowerRelation, Power>((u, userRoleRelation, role, rolePowerRelation, power) => new object[]{
                  JoinType.Left,u.Id==userRoleRelation.User_Id,
                  JoinType.Left,userRoleRelation.Role_Id==role.Id,
                  JoinType.Left,role.Id==rolePowerRelation.Role_Id,
                  JoinType.Left,rolePowerRelation.Power_Id==power.Id,
            }).Where(u => u.Id==Id).Select((u, userRoleRelation, role, rolePowerRelation, power) => new UserRolePower() {
                Id=Id,
                UserName=u.Name,
                Password=u.Password,
                RoleName=role.Name,
                PowerName=power.Name
            }).ToListAsync();
        }

        /// <summary>
        /// 根据user的id查询用户的角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<UserRole> GetUserRoleAsync(long Id)
        {
            var user = await Db.Queryable<User, UserRoleRelation, Role>((u, userRoleRelation, role) => new object[]{
                JoinType.Left,u.Id==userRoleRelation.User_Id,
                JoinType.Left,userRoleRelation.Role_Id==role.Id,
            }).Where(u => u.Id == Id).Distinct().FirstAsync();
            var roles =await Db.Queryable<User, UserRoleRelation, Role>((u, userRoleRelation, role) => new object[]{
                JoinType.Left,u.Id==userRoleRelation.User_Id,
                JoinType.Left,userRoleRelation.Role_Id==role.Id,
            }).Where(u => u.Id == Id).Select((u, userRoleRelation, role)=>role).ToListAsync();
            return new UserRole() { User = user, Roles = roles };
        }

        /// <summary>
        /// 修改对应user的信息及其对应的角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Role_Ids"></param>
        /// <returns></returns>
        public async Task<ResponseClass> UpdateAsync(User user, long[] Role_Ids)
        {
            try
            {
                Db.Ado.BeginTran();
                await Db.Updateable(user).ExecuteCommandAsync();
                List<UserRoleRelation> list = new List<UserRoleRelation>();
                foreach (var Role_Id in Role_Ids)
                {
                    list.Add(new UserRoleRelation() { User_Id = user.Id, Role_Id = Role_Id });
                }
                await Db.Deleteable<UserRoleRelation>(e => e.User_Id == user.Id).ExecuteCommandAsync();
                await Db.Insertable(list).ExecuteCommandAsync();
                Db.Ado.CommitTran();
                return new ResponseClass() { State="success",Message="成功"};
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                return new ResponseClass() { State = "fail", Message = ex.Message };
            }
        }

        /// <summary>
        /// 删除对应的user和与之关联的关联表
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        public async Task<ResponseClass> DeletedAsync(long Id)
        {
            try
            {
                Db.Ado.BeginTran();
                //删除user表中的数据
                await Db.Deleteable(new User() { Id=Id}).ExecuteCommandAsync();
                //删除关联表userrolerelation中的数据
                await Db.Deleteable<UserRoleRelation>(e => e.User_Id == Id).ExecuteCommandAsync();
                Db.Ado.CommitTran();
                return new ResponseClass() { State = "success", Message = "成功" };
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                return new ResponseClass() { State = "fail", Message = ex.Message };
            }
        }
    }
}
