namespace DTO
{
    /// <summary>
    /// 用户及其对应的角色，权限
    /// </summary>
    public class UserRolePowerDTO
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 权限名字
        /// </summary>
        public string PowerName { get; set; }
    }
}
