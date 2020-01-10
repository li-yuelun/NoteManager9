using SqlSugar;

namespace Model
{
    /// <summary>
    /// 音乐，用户关联关系模型
    /// </summary>
    [SugarTable("t_musicuserrelations")]
    public class MusicUserRelation
    {
        /// <summary>
        /// 音乐指定主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Music_Id { get; set; }
        /// <summary>
        /// 用户指定主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long User_Id { get; set; }
    }
}
