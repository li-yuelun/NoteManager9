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
    public class PowerController : Controller
    {

        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var list = await powerBLL.GetFiltersAsync(e => true);
            return View(list);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var list = (await userBLL.GetFiltersAsync(e => e.IsDeleted == false));
            //var list_json = JsonConvert.SerializeObject(list);
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
            await powerBLL.DeletedAsync(Id);
            return Redirect("/Power/Index");
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