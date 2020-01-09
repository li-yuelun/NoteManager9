using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFront.App_Start
{
    public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"] as string;
            string actionName = filterContext.RouteData.Values["action"] as string;
            //操作是登录界面，直接进入
            if (controllerName == "User" && actionName == "Login")
            {
            }
            //操作不是登录界面
            else
            {
                //先前没登录过
                if (filterContext.HttpContext.Session["Name"] == null)
                {
                    filterContext.RequestContext.HttpContext.Response.Write("大兄弟，请登录在进行操作");
                    //filterContext.Result = new RedirectResult("/User/Login");
                }
                //先前登录过
                else
                {
                    if (filterContext.HttpContext.Session["Name"].Equals("fj"))
                    {
                        if (controllerName == "User" && actionName == "UserUpdate")
                        {
                            filterContext.RequestContext.HttpContext.Response.Write("大兄弟，你没有权限-进行操作");
                        }
                    }
                }
            }
        }
    }
}