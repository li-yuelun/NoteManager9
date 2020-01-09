using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 单个角色及其对应的权限
    /// </summary>
    public class RolePower
    {
        //角色
        public Role Role { get; set; }
        //角色对应的权限
        public List<Power> Powers { get; set; }
    }
}
