using System.Collections.Generic;

namespace DTO
{
    //展示所有歌手的国家集合，风格集合（用于SingerList页面的初始化填充）
    public class SingerListDTO
    {
        /// <summary>
        /// 歌手的国家集合
        /// </summary>
        public List<string> Countrys { get; set; }
        /// <summary>
        /// 歌手的风格集合
        /// </summary>
        public List<string> Styles { get; set; }
        /// <summary>
        /// 歌手名字首字母集合
        /// </summary>
        public List<string> FirstLetters { get; set; }
        /// <summary>
        /// 歌手集合
        /// </summary>
        public List<SingerDTO> singerDTOs { get; set; }
    }
}
