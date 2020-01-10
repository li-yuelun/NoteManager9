namespace DTO
{
    /// <summary>
    /// 用户角色关系中间表
    /// </summary>
    public class UserRoleRelationDTO
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public long User_Id { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>
        public long Role_Id { get; set; }
    }
}
