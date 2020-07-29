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
    public class UserController : ControllerBase<User>
    {
        private string sid;

        public Response GetList(string key,string LoginId,string Password, int pageIndex, int pageSize)
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



        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public Response Login(string form)
        {
            return Respond(() => {
                //Db.db.Saveable<Role>(new List<Role> { 
                //    new Role{Name="e1", Powers=new List<int>{1,2,3}}
                
                //}).ExecuteCommand();
                var o = JsonConvert.DeserializeObject<User>(form);
                o.GetHashCode();
                var user = GetModel(x => x.LoginId == o.LoginId);
                if (user is null) throw new WYJException("账号不存在");
                if (user.IsDisable) throw new WYJException("账号已禁用");
                if (user.Password != o.Password) throw new WYJException("密码错误");
                CurrentHttpContext.Request.Cookies.TryGetValue("sid", out sid);
                Session.AddUser(sid,user);
                return user;
            });


        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public Response Register(string form)
        {
            return Respond(() =>
            {
                var user = JsonConvert.DeserializeObject<User>(form);
                Db.Save(user).ExecuteCommand();
            }, "注册成功！");
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public Response Logout()
        {
            return Respond(() =>
            {
                CurrentHttpContext.Request.Cookies.TryGetValue("sid", out sid);
                Session.currentUsers.RemoveAll(x => x.Id.EqualTo(sid));
            });

        }

        //////////////       
        [HttpPost]
        public Response GetMenus(string form)
        {
            return Respond(() =>
            {
                var list = JsonConvert.DeserializeObject<List<int>>(form);
                return list.Select(i => WYJ.Menus.Find(m => m.Id == i));
            });
        }
    }
}
