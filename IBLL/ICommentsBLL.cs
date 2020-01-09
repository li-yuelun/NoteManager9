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
    public interface ICommentsBLL
    {
        Task AddAsync(CommentsDTO t);

        Task DeleteAsync(CommentsDTO t);

        Task DeleteAsync(Expression<Func<Comments, bool>> exp);

        Task UpdateAsync(CommentsDTO t);

        Task<CommentsDTO> GetEntityAsync(Expression<Func<Comments, bool>> exp);

        Task<List<CommentsDTO>> GetFiltersAsync(Expression<Func<Comments, bool>> exp);
    }
}
