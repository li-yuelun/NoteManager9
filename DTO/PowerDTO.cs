using System;

namespace DTO
{
    public class PowerDTO
    {
        public long Id { get; set; }
        /// <summary>
        /// 权限名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; } = false;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 权限标志(标识)
        /// </summary>
        public string Mark { get; set; }
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool? IsChoosed { get; set; } = false;
    }
}
