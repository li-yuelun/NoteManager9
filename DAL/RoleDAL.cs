using IDAL;
using Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RoleDAL : GeneralDAL<Role>, IRoleDAL
    {
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
                //删除role表中的数据
                await Db.Deleteable(new Role() { Id = Id }).ExecuteCommandAsync();
                //删除关联表userrolerelation中的数据
                await Db.Deleteable<UserRoleRelation>(e => e.Role_Id == Id).ExecuteCommandAsync();
                //删除关联表rolepowerrelation中的数据
                await Db.Deleteable<RolePowerRelation>(e => e.Role_Id == Id).ExecuteCommandAsync();
                Db.Ado.CommitTran();
                return new ResponseClass() { State = "success", Message = "成功" };
            }
            catch (Exception ex)
            {
                Db.Ado.RollbackTran();
                return new ResponseClass() { State = "fail", Message = ex.Message };
            }
        }

        /// <summary>
        /// 根据role的id查询对应的role和powers
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RolePower> GetRolePowerAsync(long Id)
        {
            var role = await Db.Queryable<Role, RolePowerRelation, Power>((r, rolepowerrelation, power) => new object[] {
                JoinType.Left,r.Id==rolepowerrelation.Role_Id,
                JoinType.Left,rolepowerrelation.Power_Id==power.Id,
            }).Where(r => r.Id == Id).Select(r => r).Distinct().FirstAsync();
            var powers = await Db.Queryable<Role, RolePowerRelation, Power>((r, rolepowerrelation, power) => new object[] {
                JoinType.Left,r.Id==rolepowerrelation.Role_Id,
                JoinType.Left,rolepowerrelation.Power_Id==power.Id,
            }).Where(r => r.Id == Id).Select((r, rolepowerrelation, power) => power).ToListAsync();
            return new RolePower() { Role = role, Powers = powers };
        }

        /// <summary>
        /// 修改role对应的信息和气对应的关联关系
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Power_Ids"></param>
        /// <returns></returns>
        public async Task<ResponseClass> UpdateAsync(Role role, long[] Power_Ids)
        {
            try
            {
                Db.Ado.BeginTran();
                await Db.Updateable(role).ExecuteCommandAsync();
                List<RolePowerRelation> list = new List<RolePowerRelation>();
                foreach (var Power_Id in Power_Ids)
                {
                    list.Add(new RolePowerRelation() { Power_Id = Power_Id, Role_Id = role.Id });
                }
                await Db.Deleteable<RolePowerRelation>(e => e.Role_Id == role.Id).ExecuteCommandAsync();
                await Db.Insertable(list).ExecuteCommandAsync();
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
