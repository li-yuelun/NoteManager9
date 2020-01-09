using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 单个用户及其对应的角色
    /// </summary>
    public class UserRole
    {
        //用户
        public User User { get; set; }
        //用户对应角色
        public List<Role> Roles { get; set; }
    }
}
