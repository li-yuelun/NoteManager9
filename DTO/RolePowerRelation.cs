namespace DTO
{
    /// <summary>
    /// 角色权限中间表
    /// </summary>
    public class RolePowerRelation
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        public long Role_Id { get; set; }
        /// <summary>
        /// 权限主键
        /// </summary>
        public long Power_Id { get; set; }
    }
}
