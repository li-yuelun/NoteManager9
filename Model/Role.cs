using SqlSugar;
using System;

namespace Model
{
    /// <summary>
    /// 角色模型
    /// </summary>
    [SugarTable("t_roles")]
    public class Role : IModel
    {
        /// <summary>
        /// 指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; } = false;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 角色标志(标识)
        /// </summary>
        public string Mark { get; set; }
    }
}
