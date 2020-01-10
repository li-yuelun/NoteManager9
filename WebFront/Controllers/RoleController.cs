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
    public class RoleController : Controller
    {
        
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; } 

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var list = await roleBLL.GetFiltersAsync(e=> e.IsDeleted == false);
            return View(list);
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string Name,string Mark)
        {
            var exp = ExpressionHelper.True<Role>();
            if (!string.IsNullOrEmpty(Name))
            {
                exp = exp.And(p => p.Name == Name);
            }
            if(!string.IsNullOrEmpty(Mark))
            {
                exp = exp.And(p => p.Mark == Mark);
            }
            var list = (await roleBLL.GetFiltersAsync(exp));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Add(RoleDTO roleDTO)
        {
            try
            {
                await roleBLL.AddAsync(roleDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long Id)
        {
            try
            {
                await roleBLL.DeletedAsync(Id);
                return Redirect("/Role/Index");
            }
            catch (Exception ex)
            {
                return Redirect("删除失败"+ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(long Id)
        {
            var rolepowerDTO = await roleBLL.GetRolePowerAsync(Id);
            return View(rolepowerDTO);
        }

        [HttpPost]
        public async Task<string> Update(RoleDTO roleDTO,long[] Power_Ids)
        {
            var responseClass=await roleBLL.UpdateAsync(roleDTO, Power_Ids);
            return JsonConvert.SerializeObject(responseClass);
        }

        //根据角色id查询关联角色的权限
        [HttpGet]
        public async Task<ActionResult> ShowRolePower(long Id)
        {
            var list = await roleBLL.GetRolePowerAsync(Id);
            return View(list);
        }
    }
}