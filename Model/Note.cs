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
        /// <summary>
        /// 日志名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; } = false;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 创建人id
        /// </summary>
        public long User_Id { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public string Style { get; set; }
        
    }
}
