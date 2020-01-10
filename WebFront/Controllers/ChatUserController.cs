using Common;
using DTO;
using IBLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExpressionHelper = Common.ExpressionHelper;

namespace WebFront.Controllers
{
    public class ChatUserController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        public IChatUserBLL chatUserBLL { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 数据表加载
        /// </summary>
        /// <param name="ChatName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetData(string ChatName,string Password)
        {
            var exp = ExpressionHelper.True<ChatUser>();
            if (!string.IsNullOrEmpty(ChatName))
            {
                exp = exp.And(p=>p.ChatName==ChatName);
            }
            if (!string.IsNullOrEmpty(Password))
            {
                exp = exp.And(p => p.Password == Password);
            }
            var list = (await chatUserBLL.GetFiltersAsync(exp));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 聊天用户新增
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(ChatUserDTO chatUserDTO)
        {
            try
            {
                await chatUserBLL.AddAsync(chatUserDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success"});
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 修改显示
        /// </summary>
        /// <param name="Id">要修改的chatuser的id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Update(long Id)
        {
            var entity = await chatUserBLL.GetEntityAsync(e=>e.Id==Id);
            return View(entity);
        }

        /// <summary>
        /// 聊天用户修改
        /// </summary>
        /// <param name="chatUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Update(ChatUserDTO chatUserDTO)
        {
            try
            {
                await chatUserBLL.UpdateAsync(chatUserDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// 聊天用户删除(真实删除)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Delete(long Id)
        {
            try
            {
                await chatUserBLL.DeleteAsync(e => e.Id == Id && e.IsDeleted == false);
                return Redirect("/ChatUser/Index");
            }
            catch (Exception ex)
            {
                return Content("删除失败"+ex.Message);
            }
        }

        /// <summary>
        /// 聊天用户删除(软删除)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> DeleteBySoft(long Id)
        {
            try
            {
                //await chatUserBLL.DeleteBySoftAsync(e => e.Id == Id && e.IsDeleted == false);
                return Redirect("/ChatUser/Index");
            }
            catch (Exception ex)
            {
                return Content("删除失败" + ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Login(string ChatName,string Password)
        {
            try
            {
                var entity = await chatUserBLL.GetEntityAsync(e => e.ChatName == ChatName && e.Password == Password);
                if (entity != null)
                {
                    Session["ChatName"] = ChatName;
                    return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = "没有此人，请检查用户名和密码是否正确!" });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }
    }
}