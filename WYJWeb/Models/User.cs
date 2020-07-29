using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WYJCore;
using WYJCore.Web;

namespace WYJWeb.Models
{
    public class User : ModelBase<User>, IUser
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginId { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public int Role { get; set; }


        /// <summary>
        /// 是否已禁用
        /// </summary>
        public bool IsDisable { get; set; }

        public Role GetRole(DbContext<Role> db)
        {
            return db.GetModel().InSingle(Role);
        }


        public List<Menu> GetMenus(DbContext<Role> db)
        {
            var role = GetRole(db);
            return role.powers.Select(x => WYJ.Menus.First(y => y.Id == x)).ToList();
            //return WYJ.Menus.SelectMany(menu=>role.Powers,(m,p) => new { 
            //    m,p
            //}).Where(x=>x.m.Id==x.p).Select(x=>x.m).ToList();

        }

    }
}
