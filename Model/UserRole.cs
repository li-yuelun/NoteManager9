using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 单个用户及其对应的角色
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 用户对应角色
        /// </summary>
        public List<Role> Roles { get; set; }
    }
}
