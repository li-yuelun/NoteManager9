using System;
using System.Linq;

namespace Model
{
    /// <summary>
    /// 聊天信息模型
    /// </summary>
    public class ChatMessage
    {
        //主键id
        public string Id { get; set; }
        //发送的消息
        public string Message { get; set; }
        //发送者
        public string Sender { get; set; }
        //接受者
        public string Receiver { get; set; }
        //是否删除
        public bool IsDeleted { get; set; }
        //创建时间
        public DateTime CreateTime { get; set; } 
    }
}
