
using log4net;
using System.Web.Mvc;

namespace WebFront.App_Start
{
    public class ExceptionFilter : IExceptionFilter
    {
        private static ILog log = LogManager.GetLogger(typeof(ExceptionFilter));

        public void OnException(ExceptionContext filterContext)
        {
            //将错误记录到日志当中
            log.Error("出现未处理异常", filterContext.Exception);
            //在页面上显示错误
            string controller = filterContext.RouteData.Values["controller"] as string;
            string action = filterContext.RouteData.Values["action"] as string;

            filterContext.RequestContext.HttpContext.Response.Write(string.Format("{0}:{1}发生异常!{2}",
            controller, action, filterContext.Exception.Message));
            filterContext.ExceptionHandled = true;

            /*
               File.AppendAllText("d:/error.log", filterContext.Exception.ToString());
               filterContext.ExceptionHandled = true;//如果有其他的 IExceptionFilter 不再执行
               filterContext.Result = new ContentResult() { Content= "error" }; 
             */
        }
    }
}