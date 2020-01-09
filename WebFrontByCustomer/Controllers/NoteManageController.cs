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
    public class NoteManageController : Controller
    {
        public IUserBLL userBLL { get; set; }
        // GET: NoteManage
        [HttpGet]
        public ActionResult Index()
        {
            ResponseClassDTO responseClassDTO = new ResponseClassDTO();
            responseClassDTO.Message = Session["Name"].ToString();
            return View(responseClassDTO);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}