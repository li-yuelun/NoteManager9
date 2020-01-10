using BLL;
using DTO;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebFront.Controllers
{
    public class ChatMessageController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        //public IChatMessageBLL chatMessageBLL { get; set; }

        IChatMessageBLL chatMessageBLL = new ChatMessageBLL();

        public IChatUserBLL chatUserBLL { get; set; }

        /// <summary>
        /// 数据加载
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Receiver"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetDataLoad(string Sender,string Receiver)
        {
            var chatMessageDTO = new ChatMessageDTO() { Sender=Sender,Receiver=Receiver};
            var list = (await chatMessageBLL.GetFilterByBothsAsync(chatMessageDTO));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 数据加载测试
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetData(string Id)
        {
            var chatMessageDTO = new ChatMessageDTO() { Sender = "张飞", Receiver = "刘备" };
            var list = (await chatMessageBLL.GetFilterByBothsAsync(chatMessageDTO));
            if (!string.IsNullOrEmpty(Id))
            {
                list = list.Where(e => e.Id == Id).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            //所有的人
            var chatMessageDTO = new ChatMessageDTO();
            var onlinelist = chatUserBLL.GetFilters(e => true);
            return View(onlinelist);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Add(ChatMessageDTO chatMessageDTO)
        {
            chatMessageDTO.IsDeleted = false;
            chatMessageDTO.CreateTime = DateTime.Now;
            await chatMessageBLL.AddAsync(chatMessageDTO);
            return Content("ok");
        }

        /// <summary>
        /// 修改消息（测试）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
       [HttpGet]
        public async Task<ActionResult> Update(string Id)
        {
            var entity=await chatMessageBLL.GetEntityAsync(e=>e.Id==Id);
            return View(entity);
        }

        /// <summary>
        /// 修改消息（测试）
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(ChatMessageDTO chatMessageDTO)
        {
            string msg;
            try
            {
                await chatMessageBLL.UpdateAsync(chatMessageDTO);
                var responsrClassDTO = new ResponseClassDTO() { State = "success" };
                msg = JsonConvert.SerializeObject(responsrClassDTO);
            }
            catch (Exception ex)
            {
                var responsrClassDTO = new ResponseClassDTO() { State = "fail",Message=ex.Message };
                msg = JsonConvert.SerializeObject(responsrClassDTO);
            }
            return msg;
        }

        /// <summary>
        /// 删除指定的聊天内容(真实删除)
        /// </summary>
        /// <param name="Id">聊天消息的内容</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(string Id,string Sender,string Receiver)
        {
            try
            {
                ChatMessageDTO chatMessageDTO = new ChatMessageDTO() { Sender = Sender, Receiver = Receiver, Id = Id };
                await chatMessageBLL.DeleteAsync(chatMessageDTO);
                return Redirect("/ChatMessage/Index");
            }
            catch (Exception ex)
            {
                return Content("删除失败"+ex.Message);
            }
        }

        /// <summary>
        /// 删除指定的聊天内容(软删除)
        /// </summary>
        /// <param name="Id">聊天消息的内容</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> DeleteBySoft(string Id, string Sender, string Receiver)
        {
            try
            {
                ChatMessageDTO chatMessageDTO = new ChatMessageDTO() { Sender = Sender, Receiver = Receiver, Id = Id };
                await chatMessageBLL.DeleteBySoftAsync(chatMessageDTO);
                return Redirect("/ChatMessage/Index");
            }
            catch (Exception ex)
            {
                return Content("删除失败" + ex.Message);
            }
        }

        /// <summary>
        /// 获取指定的人员之间的聊天记录
        /// </summary>
        /// <param name="chatMessageDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetMessage(ChatMessageDTO chatMessageDTO)
        {
            var list=await chatMessageBLL.GetFilterByBothsAsync(chatMessageDTO);
            return Json(list,JsonRequestBehavior.AllowGet);
        }
    }
}