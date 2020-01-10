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

namespace WebFrontByCustomer.Controllers
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
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string Name,string Style,DateTime? StartDate,DateTime? EndDate)
        {
            var exp = ExpressionHelper.True<Note>();
            if (!string.IsNullOrEmpty(Name))
            {
                exp=exp.And(p=>p.Name==Name);
            }
            if (!string.IsNullOrEmpty(Style))
            {
                exp = exp.And(p => p.Style == Style);
            }
            if (StartDate != null)
            {
                exp = exp.And(p => DateTime.Compare(p.CreateTime ?? DateTime.Now, StartDate ?? DateTime.Now) >= 0);
            }
            if (EndDate != null)
            {
                exp = exp.And(p => DateTime.Compare(p.CreateTime ?? DateTime.Now, EndDate ?? DateTime.Now) <= 0);
            }
            var list = (await noteBLL.GetFiltersAsync(exp)).Where(e=>e.User_Id == 1);
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
            await noteBLL.DeleteAsync(e => e.Id == Id && e.IsDeleted == false);
            return Redirect("/Note/Index");
        }

        [HttpGet]
        public async Task<ActionResult> Update(long Id)
        {
            var note = await noteBLL.GetEntityAsync(e => e.Id == Id && e.IsDeleted == false);
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