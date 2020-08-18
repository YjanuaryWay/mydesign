using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WYJCore;
using WYJCore.Web;
using WYJWeb.Models;

namespace WYJWeb.Apis
{
    public class NotificationController : ControllerBase<Notification>
    {

        public Response GetList(string key, int pageIndex, int pageSize)
        {
            return Respond(() =>
            {
                var list = GetList();
                int startIndex = (pageIndex - 1) * pageSize;
                int endIndex = list.Count - startIndex;
                if (endIndex > pageSize) endIndex = pageSize;
                return new
                {
                    pageSize,
                    Data = list.GetRange(startIndex, endIndex),
                    list.Count
                };
            });
        }

    }
}
