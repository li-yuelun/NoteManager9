using MongoDB.Bson;
using System;

namespace Model
{
    /// <summary>
    /// 音乐评论模型
    /// </summary>
    public class Comments : IEntity
    {
        public ObjectId Id { get; set; }
        //评论人
        public string UserName { get; set; }
        //评论人ID
        public long User_Id { get; set; }
        //评论歌曲
        public string MusicName { get; set; }
        //歌曲ID
        public long Music_Id { get; set; }
        //评论内容
        public string CommentDetail { get; set; }
        //删除标记
        public bool IsDeleted { get; set; } = false;
        //评论时间
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
