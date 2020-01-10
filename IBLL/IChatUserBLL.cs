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
    public interface IChatUserBLL
    {
        /// <summary>
        /// 单个聊天用户新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddAsync(ChatUserDTO t);

        void Add(ChatUserDTO t);

        /// <summary>
        /// 单个聊天用户删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteAsync(ChatUserDTO t);

        void Delete(ChatUserDTO t);

        /// <summary>
        /// 单个聊天用户删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task DeleteBySoftAsync(ChatUserDTO t);

        void DeleteBySoft(ChatUserDTO t);
        /// <summary>
        /// 根据动态条件删除聊天用户
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<ChatUser, bool>> exp);

        void Delete(Expression<Func<ChatUser, bool>> exp);

        /// <summary>
        /// 单个聊天用户修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateAsync(ChatUserDTO t);

        void Update(ChatUserDTO t);

        /// <summary>
        /// 根据动态条件获取单个聊天用户
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<ChatUserDTO> GetEntityAsync(Expression<Func<ChatUser, bool>> exp);

        ChatUserDTO GetEntity(Expression<Func<ChatUser, bool>> exp);

        /// <summary>
        /// 根据动态条件获取聊天用户集合
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<List<ChatUserDTO>> GetFiltersAsync(Expression<Func<ChatUser, bool>> exp);

        List<ChatUserDTO> GetFilters(Expression<Func<ChatUser, bool>> exp);
    }
}
