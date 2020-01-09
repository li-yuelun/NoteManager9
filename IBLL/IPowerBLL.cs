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
    public interface IPowerBLL
    {
        Task AddAsync(PowerDTO t);

        Task DeleteAsync(PowerDTO t);

        Task DeleteAsync(Expression<Func<Power, bool>> exp);

        Task UpdateAsync(PowerDTO t);

        Task<PowerDTO> GetEntityAsync(Expression<Func<Power, bool>> exp);

        Task<List<PowerDTO>> GetFiltersAsync(Expression<Func<Power, bool>> exp);

        /// <summary>
        /// 删除对应的power和与之关联的关联表
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        Task<ResponseClassDTO> DeletedAsync(long Id);
    }
}
