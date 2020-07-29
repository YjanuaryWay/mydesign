using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WYJCore.Web
{
    public class WYJRouter : Router
    {
        private readonly RequestDelegate next;
        private readonly string area = Environment.CurrentDirectory + "/wwwroot";
        public WYJRouter(RequestDelegate requestDelegate, Action init = null)
        {
            next = requestDelegate;
            init = WYJ.Init + init;
            init();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "charset=UTF-8;";
            var path = httpContext.Request.Path;
            var where = RouteTo(path);
            try
            {
                switch (where)
                {
                    case RoutingPath.Static:
                        await UseStatics(httpContext, area + path);
                        break;
                    case RoutingPath.Api:
                        httpContext.Response.ContentType += "application/json;";
                        await UseApi(httpContext, path);
                        break;
                    case RoutingPath.RazorPage:
                        httpContext.Response.ContentType += "text/html;";
                        await MapRazor(httpContext, path);
                        break;
                    default:
                        await next(httpContext);
                        break;
                }
            }
            catch (WYJException e)
            {
                var res = new Response { IsSuccess = false, Msg = e.Message };
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(res));
            };
        }


    }

}