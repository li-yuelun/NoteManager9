using System;

namespace DTO
{
    public class RoleDTO
    {
        public long Id { get; set; }
        //角色名字
        public string Name { get; set; }
        //删除标记
        public bool? IsDeleted { get; set; } = false;
        //创建时间
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        //角色标志(标识)
        public string Mark { get; set; }
        //是否被选择
        public bool? IsChoosed { get; set; } = false;
    }
}
