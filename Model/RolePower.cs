using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 单个角色及其对应的权限
    /// </summary>
    public class RolePower
    {
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 角色对应的权限
        /// </summary>
        public List<Power> Powers { get; set; }
    }
}
