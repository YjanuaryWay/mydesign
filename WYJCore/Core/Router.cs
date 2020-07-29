using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WYJCore.Extension;

namespace WYJCore.Web
{
    public abstract class Router
    {
        public static RoutingPath RouteTo(PathString path)
        {
            if (UrlHelper.IsApi(path)) return RoutingPath.Api;
            if (UrlHelper.IsStatic(path)) return RoutingPath.Static;
            return RoutingPath.RazorPage;
        }

        public static async Task UseStatics(HttpContext httpContext, string path)
        {
            if (File.Exists(path))
            {
                if (path.EndsWith("css")) httpContext.Response.ContentType = "text/css;";
                var bs = File.ReadAllBytes(path);
                await httpContext.Response.Body.WriteAsync(bs);
                return;
            }
            httpContext.Response.StatusCode = 404;
        }

        public static async Task UseApi(HttpContext httpContext, PathString path)
        {
            var names = UrlHelper.ParsePathString(path);
            var controllerType = WYJ.GetServiceType(names[1] + "Controller");
            if (controllerType == null) throw new WYJException($"未找到控制器{names[1]}Controller");
            dynamic controller = Activator.CreateInstance(controllerType);
            if (controller == null) throw new WYJException($"创建{names[1]}Controller的实例失败");
            controller.CurrentHttpContext = httpContext;
            var methods = controllerType.GetMember(names[2], BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.InvokeMethod);
            if (methods.Length == 0) throw new WYJException($"{controllerType.FullName}没有名称为{names[2]}的方法");

            ///筛选最匹配的方法
            ///支持默认值
            MethodInfo method = (MethodInfo)methods.Single(method => RouterHelper.CheckParameterSignature_Optional((method as MethodInfo).GetParameters(), httpContext.Request));
            ///构建参数
            var args = RouterHelper.BuildArgs(method.GetParameters(), httpContext.Request);
            try
            {
                string JSON = JsonConvert.SerializeObject(method.Invoke(controller, args));
                await httpContext.Response.WriteAsync(JSON);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        
        
        }

        public static async Task MapRazor(HttpContext httpContext, string path)
        {
            path = path.Trim('/');
#if DEBUG
            if (!(Session.CheckLogin(httpContext) || path.EqualTo("login")))
            {
                httpContext.Response.Redirect("/Login");
                return;
            }
#endif
            if (path.IsNullOrEmpty()) path = "index";
            string jsonResult = "未知错误";
            if (httpContext.RequestServices.GetService(typeof(IViewRenderService)) is IViewRenderService viewRenderService) jsonResult = await viewRenderService.RenderToStringAsync(path, httpContext);
            await httpContext.Response.WriteAsync(jsonResult);
        }
    }

}