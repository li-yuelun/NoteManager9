using SqlSugar;
using System;

namespace Model
{
    /// <summary>
    /// 音乐模型
    /// </summary>
    [SugarTable("t_musics")]
    public class Music : IModel
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// 歌曲名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 语种
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 歌曲类型
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// 歌词
        /// </summary>
        public string Lyrics { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; } = false;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 歌手id
        /// </summary>
        public long Singer_Id { get; set; }
        /// <summary>
        /// 演唱者名字
        /// </summary>
        public string SingerName { get; set; }
    }
}
