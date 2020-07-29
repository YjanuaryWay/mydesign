using System;
using System.Threading.Tasks;

namespace WYJCore
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model = null);
    }
}
