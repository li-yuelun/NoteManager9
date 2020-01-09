
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

namespace WebFront.Controllers
{
    public class UserController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; } 

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var list = (await userBLL.GetFiltersAsync(e => e.Name==Name));
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (await userBLL.GetFiltersAsync(e => true));
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        //用户注册
        [HttpPost]
        public async Task<string> Add(UserDTO userDTO)
        {
            try
            {
                await userBLL.AddAsync(userDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        //根据用户id删除指定的用户信息
        [HttpGet]
        public async Task<ActionResult> Delete(long Id)
        {
            await userBLL.DeletedAsync(Id);
            return Redirect("/User/Index");
        }

        //根据用户id获取要修改的用户
        [HttpGet]
        public async Task<ActionResult> Update(long Id)
        {
            var userRoleDTO = await userBLL.GetUserRoles(Id);
            return View(userRoleDTO);
        }

        //修改用户信息和关联表的数据
        [HttpPost]
        public async Task<string> Update(UserDTO userDTO,long[] Role_Ids)
        {
            var responseclassDTO = await userBLL.UpdateAsync(userDTO, Role_Ids);
            return JsonConvert.SerializeObject(responseclassDTO);
        }

        //根据用户id查询用户的所有关联角色和权限
        [HttpGet]
        public async Task<ActionResult> ShowUserRolePower(long Id)
        {
            var list=await userBLL.GetUserRolePowers(Id);
            return View(list);
        }

        //进入用户登录页
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //用户登录
        [HttpPost]
        public async Task<string> Login(string Name,string Password)
        {
            try
            {
                var userdto = await userBLL.GetEntityAsync(e => e.Name == Name && e.Password == Password && e.IsDeleted==false);
                if (userdto != null)
                {
                    return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = "没有此人，请检查用户名或密码是否错误" });

                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message  });
            }
        }
    }
}