using System.Collections.Generic;

namespace DTO
{
    /// <summary>
    /// 用户及其对应的角色
    /// </summary>
    public class UserRoleDTO
    {
        /// <summary>
        /// 用户
        /// </summary>
        public UserDTO UserDTO { get; set; }
        /// <summary>
        /// 用户对应的角色
        /// </summary>
        public List<RoleDTO> RoleDTOs { get; set; }
    }
}
