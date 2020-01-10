using System;

namespace DTO
{
    public class NoteDTO
    {
        public long Id { get; set; }
        /// <summary>
        /// 日志名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; } = false;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 创建人Id
        /// </summary>
        public long User_Id { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public string Style { get; set; }
    }
}
