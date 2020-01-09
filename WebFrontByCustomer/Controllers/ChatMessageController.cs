using DTO;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebFrontByCustomer.Controllers
{
    public class ChatMessageController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        public IChatMessageBLL chatMessageBLL { get; set; }

        //IChatMessageBLL chatMessageBLL = new ChatMessageBLL();

        public IChatUserBLL chatUserBLL { get; set; }

        // GET: ChatMessage
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //发送消息的人
            string CurrentChatName = (Session["ChatName"]??"").ToString();
            //在线的人
            var onlineNamelist = await chatUserBLL.GetFiltersAsync(e => e.IsOnline == true);
            return View(new ChatUserLoginDTO { onlineNamelist = onlineNamelist, CurrentChatName = CurrentChatName });
        }

        [HttpPost]
        public async Task<string> SendMessage(ChatMessageDTO chatMessageDTO)
        {
            try
            {
                await chatMessageBLL.AddAsync(chatMessageDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(ChatMessageDTO chatMessageDTO)
        {
            chatMessageDTO.Id = Guid.NewGuid().ToString();
            await chatMessageBLL.AddAsync(chatMessageDTO);
            return Content("ok");
        }

        [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            var entity = await chatMessageBLL.GetEntityAsync(new ChatMessageDTO() { Sender = "刘备", Receiver = "张飞", Id = Id });
            return View(entity);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ChatMessageDTO chatMessageDTO)
        {
            await chatMessageBLL.GetEntityAsync(chatMessageDTO);
            return View();
        }

        //获取指定的人员之间的聊天记录
        [HttpPost]
        public async Task<JsonResult> GetMessage(ChatMessageDTO chatMessageDTO)
        {
            var list = await chatMessageBLL.GetFilterByBothsAsync(chatMessageDTO);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}