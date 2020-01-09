using SqlSugar;
using System;

namespace Model
{
    /// <summary>
    /// 歌手模型
    /// </summary>
    [SugarTable("t_singers")]
    public class Singer : IModel
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        //歌手名字
        public string Name { get; set; }
        //歌手名字首字母
        public string FirstLetter{get;set;}
        //风格
        public string Style { get; set; }
        //歌手国家
        public string Country { get; set; }
        //性别
        public string Sex { get; set; }
        //照片
        public string Picture { get; set; }
        //删除标记
        public bool? IsDeleted { get; set; }
        //创建时间
        public DateTime? CreateTime { get; set; }
    }
}
