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
    public class CommentsBLL: ICommentsBLL
    {
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IPowerDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public ICommentsDAL commentsDAL { get; set; }

        public static ObjectsMapper<Comments, CommentsDTO> CommentsMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<Comments, CommentsDTO>();

        public static ObjectsMapper<CommentsDTO, Comments> CommentsDTOMapperToModel = ObjectMapperManager.DefaultInstance.GetMapper<CommentsDTO, Comments>();

        /// <summary>
        /// 单条评论新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(CommentsDTO t)
        {
            var comments = CommentsDTOMapperToModel.Map(t);
            await commentsDAL.AddAsync(comments);
        }

        /// <summary>
        /// 单条评论删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(CommentsDTO t)
        {
            var comments = CommentsDTOMapperToModel.Map(t);
            await commentsDAL.DeleteAsync(comments);
        }

        /// <summary>
        /// 动态删除评论
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Comments, bool>> exp)
        {
            await commentsDAL.DeleteAsync(exp);
        }

        /// <summary>
        /// 动态获取单条评论
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<CommentsDTO> GetEntityAsync(Expression<Func<Comments, bool>> exp)
        {
            var comments = await commentsDAL.GetEntityAsync(exp);
            return CommentsMapperToDTO.Map(comments);
        }

        /// <summary>
        /// 动态获取多条评论
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<CommentsDTO>> GetFiltersAsync(Expression<Func<Comments, bool>> exp)
        {
            var list = await commentsDAL.GetFiltersAsync(exp);
            return CommentsMapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// 单条评论修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CommentsDTO t)
        {
            var comments = CommentsDTOMapperToModel.Map(t);
            await commentsDAL.UpdateAsync(comments);
        }
    }
}
