using SqlSugar;
using System;

namespace Model
{
    /// <summary>
    /// 日志模型
    /// </summary>
    [SugarTable("t_notes")]
    public class Note:IModel
    {

        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        //日志名字
        public string Name { get; set; }
        //日志内容
        public string Detail { get; set; }
        //删除标记
        public bool? IsDeleted { get; set; } = false;
        //创建时间
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        //创建人id
        public long User_Id { get; set; }

    }
}
