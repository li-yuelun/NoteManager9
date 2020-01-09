using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IChatMessageDAL
    {
        //单个ChatMessage新增
        Task AddAsync(ChatMessage t);

        void Add(ChatMessage t);

        //单个ChatMessage删除(真实删除)
        Task DeleteAsync(ChatMessage t);

        void Delete(ChatMessage t);

        //单个ChatMessage删除(软删除)
        Task DeleteBySoftAsync(ChatMessage t);

        void DeleteBySoft(ChatMessage t);

        //单个ChatMessage修改
        Task UpdateAsync(ChatMessage t);

        void Update(ChatMessage t);

        //单个ChatMessage查询
        Task<ChatMessage> GetEntityAsync(ChatMessage t);

        ChatMessage GetEntity(ChatMessage t);

        //动态获取单个chatmessage
        Task<ChatMessage> GetEntityAsync(Expression<Func<ChatMessage, bool>> exp);

        ChatMessage GetEntity(Expression<Func<ChatMessage, bool>> exp);

        //查询指定发送者和接受者之间的所有聊天记录
        Task<List<ChatMessage>> GetFiltersAsync(ChatMessage t);

        List<ChatMessage> GetFilters(ChatMessage t);

        //查询指定发送者和接受者之间的指定日期的聊天记录
        Task<List<ChatMessage>> GetFiltersAsync(ChatMessage t, DateTime startTime, DateTime dateTime);

        List<ChatMessage> GetFilters(ChatMessage t, DateTime startTime, DateTime dateTime);

        //动态查询指定发送者和接受者之间的指定日期的聊天记录
        Task<List<ChatMessage>> GetFiltersAsync(Expression<Func<ChatMessage, bool>> exp);

        List<ChatMessage> GetFilters(Expression<Func<ChatMessage, bool>> exp);

        //查询指定发送者和指定接受者之间的所有聊天记录（A和B）
        Task<List<ChatMessage>> GetFilterByBothsAsync(ChatMessage t);

        List<ChatMessage> GetFilterByBoths(ChatMessage t);

        //事务提交异步
        Task CommitAsync();

        //事务提交
        void Commit();

        ////批量提交异步
        //void BatchCommitAsync();

        ////批量提交
        //void BatchCommit();

    }
}
