using Autofac;
using Autofac.Integration.WebApi;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //autofac的配置
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            //把当前程序集中的 Controller 都注册,不要忘了.PropertiesAutowired()
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).PropertiesAutowired();

            //获取所有相关类库的程序集
            Assembly[] assemblies = new Assembly[] { Assembly.Load("DAL"), Assembly.Load("BLL") };

            builder.RegisterAssemblyTypes(assemblies).Where(type => !type.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            var container = builder.Build();

            //注册系统级别的 DependencyResolver，这样当 api 框架创建 Controller 等对象的时候都是管 Autofac 要对象。
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
