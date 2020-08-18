using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belary
{
    public enum LayoutType
    {
        MainMenu,
        Header,
        Footer
    }

    public class LayoutsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string name)
        {
             var user = new WYJWeb.Models.User { LoginId = "南山必胜客" };
             return View(name, user);
        }

    }
}
