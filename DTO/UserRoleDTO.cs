using System.Collections.Generic;

namespace DTO
{
    public class UserRoleDTO
    {
        //用户
        public UserDTO UserDTO { get; set; }
        //用户对应的角色
        public List<RoleDTO> RoleDTOs { get; set; }
    }
}
