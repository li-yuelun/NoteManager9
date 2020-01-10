using Common;
using DTO;
using IBLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExpressionHelper = Common.ExpressionHelper;

namespace WebFront.Controllers
{
    public class NoteController : Controller
    {

        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; } 

        [HttpGet]
        public ActionResult Index()
        {
            //var list = await noteBLL.GetFiltersAsync(e=> e.IsDeleted == false);
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string Name,string Style)
        {
            var exp = ExpressionHelper.True<Note>();
            if (!string.IsNullOrEmpty(Name))
            {
                exp = exp.And(p => p.Name == Name);
            }
            if (!string.IsNullOrEmpty(Style))
            {
                exp = exp.And(p => p.Style == Style);
            }
            var list = (await noteBLL.GetFiltersAsync(exp));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Add(NoteDTO noteDTO)
        {
            try
            {
                await noteBLL.AddAsync(noteDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long Id)
        {
            try
            {
                await noteBLL.DeleteAsync(e => e.Id == Id && e.IsDeleted == false);
                return Redirect("/Note/Index");
            }
            catch (Exception ex)
            {
                return Redirect("删除失败"+ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(long Id)
        {
            var note = await noteBLL.GetEntityAsync(e=>e.Id==Id && e.IsDeleted == false);
            return View(note);
        }

        [HttpPost]
        public async Task<string> Update(NoteDTO noteDTO)
        {
            try
            {
                await noteBLL.UpdateAsync(noteDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }
    }
}