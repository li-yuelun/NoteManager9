using SqlSugar;
using System;

namespace Model
{
    /// <summary>
    /// 聊天对象模型
    /// </summary>
    [SugarTable("t_chatusers")]
    public class ChatUser:IModel
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// 聊天对象的connectionId
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// 对象名
        /// </summary>
        public string ChatName { get; set; }
        /// <summary>
        /// 对象密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; } = false;
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; } = false;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
    }
}
