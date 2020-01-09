using Common;
using DTO;
using IBLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExpressionHelper = Common.ExpressionHelper;

namespace WebFront.Controllers
{
    public class MusicController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        public IMusicBLL musicBLL { get; set; }

        public ISingerBLL singerBLL { get; set; }

        // GET: Music
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            MusicListDTO musicListDTO = new MusicListDTO();
            var list = await musicBLL.GetFiltersAsync(e => e.IsDeleted == false);
            musicListDTO.Styles = list.Select(e => e.Style).Distinct().ToList();
            musicListDTO.Themes = list.Select(e => e.Theme).Distinct().ToList();
            musicListDTO.Languages = list.Select(e => e.Language).Distinct().ToList();
            return View(musicListDTO);
        }

        /// <summary>
        /// 查询及初始化
        /// </summary>
        /// <param name="ChatName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetData(string Name,string Language,string Style, string Theme)
        {
            var exp = ExpressionHelper.True<Music>();
            if (!string.IsNullOrEmpty(Name))
            {
                exp = exp.And(p => p.Name == Name);
            }
            if (!string.IsNullOrEmpty(Language))
            {
                exp = exp.And(p => p.Language == Language);
            }
            if (!string.IsNullOrEmpty(Style))
            {
                exp = exp.And(p => p.Style == Style);
            }
            if (!string.IsNullOrEmpty(Theme))
            {
                exp = exp.And(p => p.Theme == Theme);
            }
            var list = (await musicBLL.GetFiltersAsync(exp));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> MusicLists(string Style,string Theme,string Language)
        {
            var list=await musicBLL.GetFiltersAsync(e=>e.IsDeleted==false);
            if (Style != "全部")
            {
                list = list.Where(e => e.Style == Style).ToList();
            }
            if (Theme != "全部")
            {
                list = list.Where(e => e.Theme == Theme).ToList();
            }
            if (Language != "全部")
            {
                list = list.Where(e => e.Language == Language).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var list = (await musicBLL.GetFiltersAsync(e => e.IsDeleted == false));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        //music新增
        [HttpPost]
        public async Task<string> Add(MusicDTO musicDTO)
        {
            try
            {
                await musicBLL.AddAsync(musicDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            } 
        }

        //music删除
        [HttpGet]
        public async Task<ActionResult> Delete(long Id)
        {
            await musicBLL.DeleteAsync(e=>e.Id==Id);
            return View("/Music/Index");
        }

        [HttpPost]
        public async Task<string> UpdateAsync(MusicDTO musicDTO)
        {
            try
            {
                await musicBLL.UpdateAsync(musicDTO);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "success", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }

        /// <summary>
        /// music修改
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Update(long Id)
        {
            var musicDTO = await musicBLL.GetEntityAsync(e=>e.Id==Id);
            return View(musicDTO);
        }

        //music上传
        [HttpPost]
        public ActionResult Upload()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                //获取文件名后缀
                string extName = Path.GetExtension(file.FileName).ToLower();
                //获取保存目录的物理路径
                if (System.IO.Directory.Exists(Server.MapPath("/Musics/")) == false)//如果不存在就创建images文件夹
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("/Musics/"));
                }
                string path = Server.MapPath("/Musics/"); //path为某个文件夹的绝对路径，不要直接保存到数据库
                //string path = "F:\\TgeoSmart\\Image\\";
                //生成新文件的名称，guid保证某一时刻内图片名唯一（文件不会被覆盖）
                string fileNewName = Guid.NewGuid().ToString();
                //string ImageUrl = path + fileNewName + extName;
                string MusicUrl = path + file.FileName;
                //SaveAs将文件保存到指定文件夹中
                file.SaveAs(MusicUrl);
                //此路径为相对路径，只有把相对路径保存到数据库中图片才能正确显示（不加~为相对路径）
                //string url = "\\Musics\\" + fileNewName + extName;
                string url = "\\Musics\\" + file.FileName;
                return Json(new
                {
                    Result = true,
                    Data = url
                });
            }
            catch (Exception exception)
            {
                return Json(new
                {
                    Result = false,
                    exception.Message
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> SingleMusic(long Id)
        {
            var musicDTO = await musicBLL.GetEntityAsync(e => e.Id == Id && e.IsDeleted == false);
            return View(musicDTO);
        }
    }
}