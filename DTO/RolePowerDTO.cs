using System.Collections.Generic;

namespace DTO
{
    /// <summary>
    /// 角色和器对应的权限
    /// </summary>
    public class RolePowerDTO
    {
        /// <summary>
        /// 角色
        /// </summary>
        public RoleDTO RoleDTO { get; set; }
        /// <summary>
        /// 角色对应的权限
        /// </summary>
        public List<PowerDTO> PowerDTOs { get; set; }
    }
}
