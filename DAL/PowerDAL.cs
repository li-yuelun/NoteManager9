using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PowerDAL : GeneralDAL<Power>, IPowerDAL
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
                //删除power表中的数据
                await Db.Deleteable(new Power() { Id = Id }).ExecuteCommandAsync();
                //删除关联表rolepowerrelation中的数据
                await Db.Deleteable<RolePowerRelation>(e => e.Power_Id == Id).ExecuteCommandAsync();
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
