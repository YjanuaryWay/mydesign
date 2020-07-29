using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WYJCore;
using WYJCore.Extension;
using WYJCore.Web;
using WYJWeb.Models;

namespace WYJWeb.Apis
{
    public class RoleController : ControllerBase<Role>
    {

        public Response GetMenus(string form)
        {
            return Respond(() =>
            {
                var list = JsonConvert.DeserializeObject<List<int>>(form);
                return list.Select(i => WYJ.Menus.Find(m => m.Id == i));
            });
        }

        public Response GetList(string key,int pageIndex, int pageSize)
        {
            return Respond(() =>
            {
                var list = GetList();
                int startIndex = (pageIndex-1) * pageSize;
                int endIndex = list.Count-startIndex;
                if (endIndex > pageSize) endIndex = pageSize;
                return new
                {
                    pageSize,
                    Data = list.GetRange(startIndex , endIndex),
                    list.Count
                };
            });
        }

    }
}
