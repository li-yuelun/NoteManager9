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

namespace WebFront.Controllers
{
    public class SingerController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        public IMusicBLL musicBLL { get; set; }

        public ISingerBLL singerBLL { get; set; }

        // GET: Singer
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            SingerListDTO singerListDTO = new SingerListDTO();
            var list = await singerBLL.GetFiltersAsync(e => true);
            singerListDTO.Countrys = list.Select(e => e.Country).Distinct().ToList();
            singerListDTO.Styles = list.Select(e => e.Style).Distinct().ToList();
            singerListDTO.FirstLetters = list.Select(e => e.FirstLetter).Distinct().ToList();
            singerListDTO.singerDTOs = null;
            return PartialView(singerListDTO);
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string Name,string Sex,string Style,string FirstLetter,string Country)
        {
            Expression<Func<Singer, bool>> exp = a => true;
           
            if (!string.IsNullOrEmpty(Name))
            {
                exp = a => a.Name == Name;
            }
            if (!string.IsNullOrEmpty(Sex))
            {
                exp = a => a.Sex == Sex;
            }
            if (!string.IsNullOrEmpty(Style))
            {
                exp = a => a.Style == Style;
            }
            if (!string.IsNullOrEmpty(FirstLetter))
            {
                exp = a => a.FirstLetter == FirstLetter;
            }
            if (!string.IsNullOrEmpty(Country))
            {
                exp = a => a.Country == Country;
            }
            var list =await singerBLL.GetFiltersAsync(exp);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SingerMusics(string Name)
        {
            return View("SingerMusics", (object)Name);
        }

        [HttpGet]
        public async Task<JsonResult> SingerMusicsData(string Name)
        {
            var singerDTO = await singerBLL.GetEntityAsync(e=>e.Name==Name&&e.IsDeleted==false);
            var list = await musicBLL.GetFiltersAsync(e => e.Singer_Id == singerDTO.Id && e.IsDeleted == false);
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Add(SingerDTO singerDTO)
        {
            try
            {
                singerDTO.FirstLetter = StringHelper.ChineseToEnglish(singerDTO.Name);
                await singerBLL.AddAsync(singerDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "ok", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "ok", Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(long Id)
        {
            var singerDTO = await singerBLL.GetEntityAsync(e=>e.Id==Id && e.IsDeleted == false);
            return View(singerDTO);
        }

        [HttpPost]
        public async Task<string> Update(SingerDTO singerDTO)
        {
            try
            {
                await singerBLL.UpdateAsync(singerDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "ok", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "ok", Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long Id)
        {
            await singerBLL.DeleteAsync(e => e.Id == Id && e.IsDeleted == false);
            return View("ok");
        }

    }
}