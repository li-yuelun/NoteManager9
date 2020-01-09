using System;

namespace DTO
{
    public class PowerDTO
    {
        public long Id { get; set; }
        //权限名字
        public string Name { get; set; }
        //删除标记
        public bool? IsDeleted { get; set; } = false;
        //创建时间
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        //权限标志(标识)
        public string Mark { get; set; }
        //是否被选择
        public bool? IsChoosed { get; set; } = false;
    }
}
