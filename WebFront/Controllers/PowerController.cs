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
    public class PowerController : Controller
    {

        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }
  
        public INoteBLL noteBLL { get; set; } 

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var list = await powerBLL.GetFiltersAsync(e=>true);
            return View(list);
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string Name,string Mark)
        {
            var exp = ExpressionHelper.True<Power>();
            if (!string.IsNullOrEmpty(Name))
            {
                exp = exp.And(p => p.Name == Name);
            }
            if (!string.IsNullOrEmpty(Mark))
            {
                exp = exp.And(p => p.Name == Name);
            }
            var list = (await powerBLL.GetFiltersAsync(exp));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Add(PowerDTO powerDTO)
        {
            try
            {
                await powerBLL.AddAsync(powerDTO);
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
                await powerBLL.DeletedAsync(Id);
                return Redirect("/Power/Index");
            }
            catch (Exception ex)
            {
                return Content("删除失败"+ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(long Id)
        {
            var power = await powerBLL.GetEntityAsync(e => e.Id == Id && e.IsDeleted == false);
            return View(power);
        }

        [HttpPost]
        public async Task<string> Update(PowerDTO powerDTO)
        {
            try
            {
                await powerBLL.UpdateAsync(powerDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = ex.Message });
            }
        }
    }
}