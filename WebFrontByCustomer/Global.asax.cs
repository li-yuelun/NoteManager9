using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebFrontByCustomer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //autofac配置
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//把当前程序集中的 Controller 都注册
            //不要忘了.PropertiesAutowired()
            // 获取所有相关类库的程序集
            Assembly bll = Assembly.Load("BLL");
            builder.RegisterAssemblyTypes(bll).Where(type => !type.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            Assembly dal = Assembly.Load("DAL");
            builder.RegisterAssemblyTypes(dal).Where(type => !type.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            var container = builder.Build();
            //注册系统级别的 DependencyResolver，这样当 MVC 框架创建 Controller 等对象的时候都是管 Autofac 要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
