using IDAL;
using Model;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ChatMessageDAL : IChatMessageDAL
    {
        //redis的服务地址
        //private static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
        private static readonly ConnectionMultiplexer redis = RedisManager.GetConnection();

        //默认第一个数据库，
        private static readonly IDatabase db = redis.GetDatabase();

        //创建事务
        private static readonly ITransaction tran = db.CreateTransaction();

        //创建批量处理
        private static readonly IBatch batch = db.CreateBatch();

        //新增单个chatmessage
        public async Task AddAsync(ChatMessage t)
        {
            var jsonstring = JsonConvert.SerializeObject(t);
            await db.ListLeftPushAsync("ChatMessage-" + t.Sender + "To" + t.Receiver, jsonstring);
        }

        public void Add(ChatMessage t)
        {
            t.Id = Guid.NewGuid().ToString();
            var jsonstring = JsonConvert.SerializeObject(t);
            db.ListLeftPush("ChatMessage-" + t.Sender + "To" + t.Receiver, jsonstring);
        }

        //删除指定的chatmessage(真实删除)
        public async Task DeleteAsync(ChatMessage t)
        {
            var jsonstring = JsonConvert.SerializeObject(t);
            await db.ListRemoveAsync("ChatMessage-" + t.Sender + "To" + t.Receiver, jsonstring);
        }

        public void Delete(ChatMessage t)
        {
            var jsonstring = JsonConvert.SerializeObject(t);
            db.ListRemove("ChatMessage-" + t.Sender + "To" + t.Receiver, jsonstring);
        }

        //删除指定的chatmessage(软删除)
        public async Task DeleteBySoftAsync(ChatMessage t)
        {
            //将旧的数据删掉
            var jsonstring = JsonConvert.SerializeObject(t);
            await db.ListRemoveAsync("ChatMessage-" + t.Sender + "To" + t.Receiver, jsonstring);
            //将旧的对象的isdelete改为true，并重新插入redis中
            t.IsDeleted = true;
            var jsonstring_add = JsonConvert.SerializeObject(t);
            await db.ListLeftPushAsync("ChatMessage-" + t.Sender + "To" + t.Receiver, jsonstring_add);
        }

        public void DeleteBySoft(ChatMessage t)
        {
            //将旧的数据删掉
            var jsonstring = JsonConvert.SerializeObject(t);
            db.ListRemove("ChatMessage-" + t.Sender + "To" + t.Receiver, jsonstring);
            //将旧的对象的isdelete改为true，并重新插入redis中
            t.IsDeleted = true;
            var jsonstring_add = JsonConvert.SerializeObject(t);
            db.ListLeftPush("ChatMessage-" + t.Sender + "To" + t.Receiver, jsonstring_add);
        }

        //获取单个chatmessage
        public async Task<ChatMessage> GetEntityAsync(ChatMessage t)
        {
            var list = await GetFiltersAsync(t);
            return list.Where(e => e.Id == t.Id).FirstOrDefault();
        }

        public ChatMessage GetEntity(ChatMessage t)
        {
            var list = GetFilters(t);
            return list.Where(e => e.Id == t.Id).FirstOrDefault();
        }

        //动态获取单个chatmessage
        public async Task<ChatMessage> GetEntityAsync(Expression<Func<ChatMessage, bool>> exp)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            var pattern = "ChatMessage*";
            var keys = redis.GetServer("localhost:6379").Keys(0, pattern);
            foreach (var key in keys)
            {
                var redisValue = await db.ListRangeAsync(key);
                foreach (var value in redisValue)
                {
                    var entity = JsonConvert.DeserializeObject<ChatMessage>(value);
                    list.Add(entity);
                }
            }
            var chatmessage = list.AsQueryable().Where(exp).FirstOrDefault();
            return chatmessage;
        }

        public ChatMessage GetEntity(Expression<Func<ChatMessage, bool>> exp)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            var pattern = "ChatMessage*";
            var keys = redis.GetServer("localhost:6379").Keys(0, pattern);
            foreach (var key in keys)
            {
                var redisValue = db.ListRange(key);
                foreach (var value in redisValue)
                {
                    var entity = JsonConvert.DeserializeObject<ChatMessage>(value);
                    list.Add(entity);
                }
            }
            var chatmessage = list.AsQueryable().Where(exp).FirstOrDefault();
            return chatmessage;
        }

        //查询指定发送者到指定接受者之间的所有聊天记录（A到B）
        public async Task<List<ChatMessage>> GetFiltersAsync(ChatMessage t)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            var redisValues = await db.ListRangeAsync("ChatMessage-" + t.Sender + "To" + t.Receiver);
            foreach (var redisValue in redisValues)
            {
                var entity = JsonConvert.DeserializeObject<ChatMessage>(redisValue);
                list.Add(entity);
            }
            return list;
        }

        public List<ChatMessage> GetFilters(ChatMessage t)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            var redisValues = db.ListRange("ChatMessage-" + t.Sender + "To" + t.Receiver);
            foreach (var redisValue in redisValues)
            {
                var entity = JsonConvert.DeserializeObject<ChatMessage>(redisValue);
                list.Add(entity);
            }
            return list;
        }

        //动态查询指定发送者到指定接受者之间的所有聊天记录（A到B）
        public async Task<List<ChatMessage>> GetFiltersAsync(Expression<Func<ChatMessage, bool>> exp)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            var pattern = "ChatMessage*";
            var keys = redis.GetServer("localhost:6379").Keys(0, pattern);
            foreach (var key in keys)
            {
                var redisValue = await db.ListRangeAsync(key);
                foreach (var value in redisValue)
                {
                    var entity = JsonConvert.DeserializeObject<ChatMessage>(value);
                    list.Add(entity);
                }
            }
            return list.AsQueryable().Where(exp).ToList();
        }

        public List<ChatMessage> GetFilters(Expression<Func<ChatMessage, bool>> exp)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            var pattern = "ChatMessage*";
            var keys = redis.GetServer("localhost:6379").Keys(0, pattern);
            foreach (var key in keys)
            {
                var redisValue = db.ListRange(key);
                foreach (var value in redisValue)
                {
                    var entity = JsonConvert.DeserializeObject<ChatMessage>(value);
                    list.Add(entity);
                }
            }
            return list;
        }

        //查询指定发送者和指定接受者之间的所有聊天记录（A和B）
        public async Task<List<ChatMessage>> GetFilterByBothsAsync(ChatMessage t)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            //发送者到接受者的所有消息
            var SenderToReceiverRedisValues = await db.ListRangeAsync("ChatMessage-" + t.Sender + "To" + t.Receiver);
            //接收者到发送者的所有消息
            var ReceiverToSenderRedisValues = await db.ListRangeAsync("ChatMessage-" + t.Receiver + "To" + t.Sender);
            //发送和接受者之间往来的消息
            var redisValues = SenderToReceiverRedisValues.Union(ReceiverToSenderRedisValues);
            foreach (var redisValue in redisValues)
            {
                var entity = JsonConvert.DeserializeObject<ChatMessage>(redisValue);
                list.Add(entity);
            }
            return list;
        }

        public List<ChatMessage> GetFilterByBoths(ChatMessage t)
        {
            List<ChatMessage> list = new List<ChatMessage>();
            //发送者到接受者的所有消息
            var SenderToReceiverRedisValues = db.ListRange("ChatMessage-" + t.Sender + "To" + t.Receiver);
            //接收者到发送者的所有消息
            var ReceiverToSenderRedisValues = db.ListRange("ChatMessage-" + t.Receiver + "To" + t.Sender);
            //发送和接受者之间往来的消息
            var redisValues = SenderToReceiverRedisValues.Union(ReceiverToSenderRedisValues);
            foreach (var redisValue in redisValues)
            {
                var entity = JsonConvert.DeserializeObject<ChatMessage>(redisValue);
                list.Add(entity);
            }
            return list;
        }

        //查询指定发送者和接受者之间的指定日期的聊天记录
        public async Task<List<ChatMessage>> GetFiltersAsync(ChatMessage t, DateTime startTime, DateTime endTime)
        {
            var list = await GetFiltersAsync(t);
            return list.Where(e => DateTime.Compare(e.CreateTime, startTime) >= 0 && DateTime.Compare(e.CreateTime, endTime) <= 0).ToList();
        }

        public List<ChatMessage> GetFilters(ChatMessage t, DateTime startTime, DateTime endTime)
        {
            var list = GetFilters(t);
            return list.Where(e => DateTime.Compare(e.CreateTime, startTime) >= 0 && DateTime.Compare(e.CreateTime, endTime) <= 0).ToList();
        }

        //修改指定chatmessage的内容
        public async Task UpdateAsync(ChatMessage t)
        {
            //redis库中保存的对应数据
            var chatmessage = GetEntity(t);
            string oldChatMessageStr = JsonConvert.SerializeObject(chatmessage);
            //删除redis库中保存的对应数据
            await db.ListRemoveAsync("ChatMessage-" + t.Sender + "To" + t.Receiver, oldChatMessageStr);
            t.CreateTime = chatmessage.CreateTime;
            t.IsDeleted = chatmessage.IsDeleted;
            string newChatMessageStr = JsonConvert.SerializeObject(t);
            //将修改后的新的chatmessage保存在redis库中
            await db.ListLeftPushAsync("ChatMessage-" + t.Sender + "To" + t.Receiver, newChatMessageStr);
        }

        public void Update(ChatMessage t)
        {
            //redis库中保存的对应数据
            var chatmessage = GetEntity(t);
            string oldChatMessageStr = JsonConvert.SerializeObject(chatmessage);
            //删除redis库中保存的对应数据
            db.ListRemove("ChatMessage-" + t.Sender + "To" + t.Receiver, oldChatMessageStr);
            t.CreateTime = chatmessage.CreateTime;
            t.IsDeleted = chatmessage.IsDeleted;
            string newChatMessageStr = JsonConvert.SerializeObject(t);
            //将修改后的新的chatmessage保存在redis库中
            db.ListLeftPush("ChatMessage-" + t.Sender + "To" + t.Receiver, newChatMessageStr);
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            tran.Execute();
        }

        /// <summary>
        /// 事务提交异步
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            await tran.ExecuteAsync();
        }
    }
}
