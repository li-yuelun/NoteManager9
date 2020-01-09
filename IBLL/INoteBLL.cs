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
    public interface INoteBLL
    {
        /// <summary>
        /// 单个日志新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(NoteDTO t);

        Task DeleteAsync(NoteDTO t);

        Task DeleteAsync(Expression<Func<Note, bool>> exp);

        Task UpdateAsync(NoteDTO t);

        Task<NoteDTO> GetEntityAsync(Expression<Func<Note, bool>> exp);

        Task<List<NoteDTO>> GetFiltersAsync(Expression<Func<Note, bool>> exp);
    }
}
