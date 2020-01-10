using SqlSugar;

namespace Model
{
    /// <summary>
    /// 角色权限关系模型
    /// </summary>
    [SugarTable("t_rolepowerrelations")]
    public class RolePowerRelation
    {
        /// <summary>
        /// 指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Role_Id { get; set; }

        /// <summary>
        /// 指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Power_Id { get; set; }
    }
}
