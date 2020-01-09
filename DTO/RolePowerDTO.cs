using System.Collections.Generic;

namespace DTO
{
    public class RolePowerDTO
    {
        //角色
        public RoleDTO RoleDTO { get; set; }
        //角色对应的权限
        public List<PowerDTO> PowerDTOs { get; set; }
    }
}
