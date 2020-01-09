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
        //聊天对象的connectionId
        public string ConnectionId { get; set; }
        //对象名
        public string ChatName { get; set; }
        //对象密码
        public string Password { get; set; }
        //是否在线
        public bool IsOnline { get; set; } = false;
        //删除标记
        public bool? IsDeleted { get; set; } = false;
        //创建时间
        public DateTime? CreateTime { get; set; } = DateTime.Now;
    }
}
