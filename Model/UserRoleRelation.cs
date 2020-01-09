using SqlSugar;

namespace Model
{
    /// <summary>
    /// 用户角色关系模型
    /// </summary>
    [SugarTable("t_userrolerelations")]
    public class UserRoleRelation
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true)]
        public long User_Id { get; set; }

        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true)]
        public long Role_Id { get; set; }
    }
}
