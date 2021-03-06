﻿using DAL;
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
    public class ChatUserBLL : IChatUserBLL
    {
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IPowerDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public IChatUserDAL chatUserDAL { get; set; }

        public static ObjectsMapper<ChatUser, ChatUserDTO> ChatUserMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<ChatUser, ChatUserDTO>();

        public static ObjectsMapper<ChatUserDTO, ChatUser> ChatUserDTOMapperToModel = ObjectMapperManager.DefaultInstance.GetMapper<ChatUserDTO, ChatUser>();

        /// <summary>
        /// 单个聊天用户新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(ChatUserDTO t)
        {
            var chatuser = ChatUserDTOMapperToModel.Map(t);
            await chatUserDAL.AddAsync(chatuser);
            await chatUserDAL.CommitAsync();
        }

        public void Add(ChatUserDTO t)
        {
            var chatuser = ChatUserDTOMapperToModel.Map(t);
            chatUserDAL.Add(chatuser);
            chatUserDAL.Commit();
        }

        /// <summary>
        /// 单个聊天用户删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(ChatUserDTO t)
        {
            var chatuser = ChatUserDTOMapperToModel.Map(t);
            await chatUserDAL.DeleteAsync(chatuser);
            await chatUserDAL.CommitAsync();
        }

        public void Delete(ChatUserDTO t)
        {
            var chatuser = ChatUserDTOMapperToModel.Map(t);
            chatUserDAL.Delete(chatuser);
            chatUserDAL.Commit();
        }

        /// <summary>
        /// 单个聊天用户删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteBySoftAsync(ChatUserDTO t)
        {
            var chatuser = ChatUserDTOMapperToModel.Map(t);
            await chatUserDAL.DeleteBySoftAsync(chatuser);
            await chatUserDAL.CommitAsync();
        }

        public void DeleteBySoft(ChatUserDTO t)
        {
            var chatuser = ChatUserDTOMapperToModel.Map(t);
            chatUserDAL.DeleteBySoft(chatuser);
            chatUserDAL.Commit();
        }

        /// <summary>
        /// 聊天用户动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<ChatUser, bool>> exp)
        {
            await chatUserDAL.DeleteAsync(exp);
            await chatUserDAL.CommitAsync();
        }

        public void Delete(Expression<Func<ChatUser, bool>> exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 动态获取单个聊天用户
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<ChatUserDTO> GetEntityAsync(Expression<Func<ChatUser, bool>> exp)
        {
            var chatuser = await chatUserDAL.GetEntityAsync(exp);
            return ChatUserMapperToDTO.Map(chatuser);
        }

        public ChatUserDTO GetEntity(Expression<Func<ChatUser, bool>> exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 动态获取多个聊天用户
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<ChatUserDTO>> GetFiltersAsync(Expression<Func<ChatUser, bool>> exp)
        {
            var list = (await chatUserDAL.GetFiltersAsync(exp)).ToList();
            return ChatUserMapperToDTO.MapEnum(list).ToList();
        }

        public List<ChatUserDTO> GetFilters(Expression<Func<ChatUser, bool>> exp)
        {
            var list = chatUserDAL.GetFilters(exp).ToList();
            return ChatUserMapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// 单个连天用户修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ChatUserDTO t)
        {
            var chatuser = ChatUserDTOMapperToModel.Map(t);
            await chatUserDAL.UpdateAsync(chatuser);
            await chatUserDAL.CommitAsync();
        }

        public void Update(ChatUserDTO t)
        {
            throw new NotImplementedException();
        }
    }
}
