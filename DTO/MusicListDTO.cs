using System.Collections.Generic;

namespace DTO
{
    //展示所有歌曲的风格集合，主题集合，语言集合（用于Index页面的初始化填充）
    public class MusicListDTO
    {
        /// <summary>
        /// 歌曲风格集合
        /// </summary>
        public List<string> Styles { get; set; }
        /// <summary>
        /// 歌曲主题集合
        /// </summary>
        public List<string> Themes { get; set; }
        /// <summary>
        /// 歌曲语言集合
        /// </summary>
        public List<string> Languages { get; set; }
        /// <summary>
        /// 歌曲风格集合
        /// </summary>
        public List<MusicDTO> MusicDTOs { get; set; }
    }
}
