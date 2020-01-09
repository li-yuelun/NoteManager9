using System;

namespace DTO
{
    public class MusicDTO
    {
        public long Id { get; set; }
        //歌曲名字
        public string Name { get; set; }
        //语种
        public string Language { get; set; }
        //歌曲类型
        public string Style { get; set; }
        //主题
        public string Theme { get; set; }
        //歌词
        public string Lyrics { get; set; }
        //删除标记
        public bool? IsDeleted { get; set; } = false;
        //创建时间
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        //地址
        public string Url { get; set; }
        //歌手id
        public long Singer_Id { get; set; }
        //演唱者
        public string SingerName { get; set; }
    }
}
