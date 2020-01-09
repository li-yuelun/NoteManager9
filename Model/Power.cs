using SqlSugar;
using System;

namespace Model
{
    [SugarTable("t_powers")]
    public class Power : IModel
    {
        /// <summary>
        /// 权限模型
        /// </summary>
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        //权限名字
        public string Name { get; set; }
        //删除标记
        public bool? IsDeleted { get; set; } = false;
        //创建时间
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        //标志
        public string Mark { get; set; }
    }
}
