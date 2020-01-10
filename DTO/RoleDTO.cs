using System;

namespace DTO
{
    public class RoleDTO
    {
        public long Id { get; set; }
        /// <summary>
        /// 角色名字
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
        /// 角色标志(标识)
        /// </summary>
        public string Mark { get; set; }
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool? IsChoosed { get; set; } = false;
    }
}
