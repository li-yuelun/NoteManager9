using DAL;
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
    public class ChatMessageBLL : IChatMessageBLL
    {
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IPowerDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        //public IChatMessageDAL chatMessageDAL { get; set; }

        IChatMessageDAL chatMessageDAL = new ChatMessageDAL();

        public static ObjectsMapper<ChatMessage, ChatMessageDTO> ChatMessageMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<ChatMessage, ChatMessageDTO>();

        public static ObjectsMapper<ChatMessageDTO, ChatMessage> ChatMessageDTOMapperToModel = ObjectMapperManager.DefaultInstance.GetMapper<ChatMessageDTO, ChatMessage>();

        /// <summary>
        /// 新增指定人员发送到指定接收人员的消息
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            await chatMessageDAL.AddAsync(chatmessage);
            await chatMessageDAL.CommitAsync();
        }

        public void Add(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            chatMessageDAL.Add(chatmessage);
            chatMessageDAL.Commit();
        }

        /// <summary>
        /// 删除指定人员发送的消息
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            //redis中对应的ChatMessage
            var entity =await chatMessageDAL.GetEntityAsync(chatmessage);
            //删除redis中指定的ChatMessage
            await chatMessageDAL.DeleteAsync(entity);
            //事务提交
            await chatMessageDAL.CommitAsync();
        }

        public void Delete(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            //redis中对应的ChatMessage
            var entity = chatMessageDAL.GetEntity(chatmessage);
            //删除redis中指定的ChatMessage
            chatMessageDAL.Delete(chatmessage);
            //事务提交
            chatMessageDAL.Commit();
        }

        /// <summary>
        /// 单个ChatMessage删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteBySoftAsync(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            //redis中对应的ChatMessage
            var entity = await chatMessageDAL.GetEntityAsync(chatmessage);
            //软删除redis中指定的ChatMessage
            await chatMessageDAL.DeleteBySoftAsync(chatmessage);
            //事务提交
            await chatMessageDAL.CommitAsync();
        }

        public void DeleteBySoft(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            //redis中对应的ChatMessage
            var entity = chatMessageDAL.GetEntity(chatmessage);
            //软删除redis中指定的ChatMessage
            chatMessageDAL.DeleteBySoft(chatmessage);
            //事务提交
            chatMessageDAL.Commit();
        }

        /// <summary>
        /// 修改指定的聊天信息
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            await chatMessageDAL.UpdateAsync(chatmessage);
            await chatMessageDAL.CommitAsync();
        }

        public void Update(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            chatMessageDAL.Update(chatmessage);
            chatMessageDAL.Commit();
        }

        /// <summary>
        /// 获取指定的人员的聊天信息(A到B)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<List<ChatMessageDTO>> GetFiltersAsync(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            var list = await chatMessageDAL.GetFiltersAsync(chatmessage);
            return ChatMessageMapperToDTO.MapEnum(list).ToList();
        }

        public List<ChatMessageDTO> GetFilters(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            var list = chatMessageDAL.GetFilters(chatmessage);
            return ChatMessageMapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// 获取指定的人员的单个聊天信息(A到B)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<ChatMessageDTO> GetEntityAsync(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            var result= await chatMessageDAL.GetEntityAsync(chatmessage);
            return ChatMessageMapperToDTO.Map(result);
        }

        public ChatMessageDTO GetEntity(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            var result = chatMessageDAL.GetEntity(chatmessage);
            return ChatMessageMapperToDTO.Map(result);
        }

        /// <summary>
        /// 动态获取指定的人员的单个聊天信息(A到B)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<ChatMessageDTO> GetEntityAsync(Expression<Func<ChatMessage,bool>> exp)
        {
            var result = await chatMessageDAL.GetEntityAsync(exp);
            return ChatMessageMapperToDTO.Map(result);
        }

        public ChatMessageDTO GetEntity(Expression<Func<ChatMessage, bool>> exp)
        {
            var result = chatMessageDAL.GetEntity(exp);
            return ChatMessageMapperToDTO.Map(result);
        }

        /// <summary>
        /// 查询指定日期范围内的人员之间的聊天信息（A和B彼此单向）
        /// </summary>
        /// <param name="t"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<ChatMessageDTO>> GetFiltersAsync(ChatMessageDTO t, DateTime startTime, DateTime endTime)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            var list = await chatMessageDAL.GetFiltersAsync(chatmessage,startTime, endTime);
            return ChatMessageMapperToDTO.MapEnum(list).ToList();
        }

        public List<ChatMessageDTO> GetFilters(ChatMessageDTO t, DateTime startTime, DateTime endTime)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            var list = chatMessageDAL.GetFilters(chatmessage, startTime, endTime);
            return ChatMessageMapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// 查询指定发送者和指定接受者之间的所有聊天记录（A和B彼此双向）
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<List<ChatMessageDTO>> GetFilterByBothsAsync(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            var list = await chatMessageDAL.GetFilterByBothsAsync(chatmessage);
            //按照创建日期排序
            list = list.OrderBy(e => e.CreateTime).ToList();
            return ChatMessageMapperToDTO.MapEnum(list).ToList(); 
        }

        public List<ChatMessageDTO> GetFilterByBoths(ChatMessageDTO t)
        {
            var chatmessage = ChatMessageDTOMapperToModel.Map(t);
            var list = chatMessageDAL.GetFilterByBoths(chatmessage);
            //按照创建日期排序
            list = list.OrderBy(e => e.CreateTime).ToList();
            return ChatMessageMapperToDTO.MapEnum(list).ToList();
        }
    }
}
