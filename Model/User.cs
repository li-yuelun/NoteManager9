using SqlSugar;
using System;

namespace Model
{
    /// <summary>
    /// 用户模型
    /// </summary>
    [SugarTable("t_users")]
    public class User : IModel
    {

        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        //用户名
        public string Name { get; set; }
        //密码
        public string Password { get; set; }
        //删除标记
        public bool? IsDeleted { get; set; } = false;
        //创建时间
        public DateTime? CreateTime { get; set; } = DateTime.Now;
    }
}
