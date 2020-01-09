using System;

namespace DTO
{
    public class NoteDTO
    {
        public long Id { get; set; }
        //日志名字
        public string Name { get; set; }
        //日志内容
        public string Detail { get; set; }
        //删除标记
        public bool? IsDeleted { get; set; } = false;
        //创建时间
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        //创建人Id
        public long User_Id { get; set; }
    }
}
