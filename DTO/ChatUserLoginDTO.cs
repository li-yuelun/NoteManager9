using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 聊天用户登录dto
    /// </summary>
    public class ChatUserLoginDTO
    {
        public string CurrentChatName { get; set; }
        public List<ChatUserDTO> onlineNamelist { get; set; }
    }
}
