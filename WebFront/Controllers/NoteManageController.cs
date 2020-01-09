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
    public class NoteManageController : Controller
    {
        public IUserBLL userBLL { get; set; }
        // GET: NoteManage
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        //用户登录
        [HttpPost]
        public async Task<string> Login(string Name, string Password)
        {
            try
            {
                var userdto = await userBLL.GetEntityAsync(e => e.Name == Name && e.Password == Password && e.IsDeleted == false);
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "ok", Message = "" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseClassDTO() { State = "fail", Message = ex.Message });
            }
        }
    }
}