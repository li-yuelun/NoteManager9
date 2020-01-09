using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DTO;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace WebFront
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        //所有在线用户
        public static List<ChatUserDTO> chatuserdtos = new List<ChatUserDTO>();

        //向指定的用户发送消息
        public void SendMessage(string connectionId, string message)
        {
            Clients.All.hello();
            var user = chatuserdtos.Where(s => s.ConnectionId == connectionId).FirstOrDefault();
            if (user != null)
            {
                Clients.Client(connectionId).addMessage(message + "" + DateTime.Now, Context.ConnectionId);
                //给自己发送，把用户的ID传给自己
                Clients.Client(Context.ConnectionId).addMessage(message + "" + DateTime.Now, connectionId);
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("该用户已离线");
            }
        }

        [HubMethodName("exitChat")]
        public void GetName(string name)
        {
            //查询用户
            var _chatuserdtos = chatuserdtos.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            if (chatuserdtos != null)
            {
                _chatuserdtos.ChatName = name;
                Clients.Client(Context.ConnectionId).showId(Context.ConnectionId);
            }
            GetUsers();
        }

        /// <summary>
        /// 重写连接事件
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            //查询用户
            var _chatuserdtos = chatuserdtos.Where(w => w.ConnectionId == Context.ConnectionId).SingleOrDefault();
            //判断用户是否存在，否则添加集合
            if (_chatuserdtos == null)
            {
                //var chatuserdto = new ChatUserDTO("",Context.ConnectionId);
                //chatuserdtos.Add(chatuserdto);
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = chatuserdtos.Where(p => p.ConnectionId == Context.ConnectionId).FirstOrDefault();
            //判断用户是否存在，存在则删除
            if (user != null)
            {
                //删除用户
                chatuserdtos.Remove(user);
            }
            GetUsers();//获取所有用户的列表
            return base.OnDisconnected(stopCalled);
        }

        //获取当前的connectionid
        public void GetCurrentConnectionId()
        {
             Clients.All.OnGetCurrentConnectionId(Context.ConnectionId);
        }

        //获取所有用户在线列表
        private void GetUsers()
        {
            var list = chatuserdtos.Select(s => new { s.ChatName, s.ConnectionId }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getUsers(jsonList);
        }
    }
}