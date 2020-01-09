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
    public interface IChatMessageBLL
    {
        //单个ChatMessage新增
        Task AddAsync(ChatMessageDTO t);

        void Add(ChatMessageDTO t);

        //单个ChatMessage删除
        Task DeleteAsync(ChatMessageDTO t);

        void Delete(ChatMessageDTO t);

        //单个ChatMessage删除(软删除)
        Task DeleteBySoftAsync(ChatMessageDTO t);

        void DeleteBySoft(ChatMessageDTO t);

        //单个ChatMessage修改
        Task UpdateAsync(ChatMessageDTO t);

        void Update(ChatMessageDTO t);

        //单个ChatMessage查询
        Task<ChatMessageDTO> GetEntityAsync(ChatMessageDTO t);

        ChatMessageDTO GetEntity(ChatMessageDTO t);

        //单个ChatMessage动态查询
        Task<ChatMessageDTO> GetEntityAsync(Expression<Func<ChatMessage,bool>> exp);

        ChatMessageDTO GetEntity(Expression<Func<ChatMessage, bool>> exp);

        //查询指定发送者和接受者之间的所有聊天记录
        Task<List<ChatMessageDTO>> GetFiltersAsync(ChatMessageDTO t);

        List<ChatMessageDTO> GetFilters(ChatMessageDTO t);

        //查询指定发送者和接受者之间的指定日期的聊天记录
        Task<List<ChatMessageDTO>> GetFiltersAsync(ChatMessageDTO t, DateTime startTime,DateTime dateTime);

        List<ChatMessageDTO> GetFilters(ChatMessageDTO t, DateTime startTime, DateTime dateTime);

        //查询指定发送者和指定接受者之间的所有聊天记录（A和B）
        Task<List<ChatMessageDTO>> GetFilterByBothsAsync(ChatMessageDTO t);

        List<ChatMessageDTO> GetFilterByBoths(ChatMessageDTO t);
    }
}
