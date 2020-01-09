using System;

namespace Model
{
    public interface IModel
    {
        long Id { get; set; }
        bool? IsDeleted { get; set; }
        DateTime? CreateTime { get; set; }
    }
}
