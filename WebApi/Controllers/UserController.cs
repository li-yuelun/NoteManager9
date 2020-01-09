using DTO;
using IBLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        public IUserBLL userBLL { get; set; }

        public IRoleBLL roleBLL { get; set; }

        public IPowerBLL powerBLL { get; set; }

        public INoteBLL noteBLL { get; set; }

        [HttpGet]
        public async Task<List<UserDTO>> Index()
        {
            var list = await userBLL.GetFiltersAsync(e => true);
            return list;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var list = await userBLL.GetFiltersAsync(e => true);
            var list_json = JsonConvert.SerializeObject(list);
            return list_json;
        }

        [HttpGet]
        public string Test() 
        {
            return "test";
        }
    }
}
