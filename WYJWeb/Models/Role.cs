using SqlSugar;
using System;
using System.Collections.Generic;
using WYJCore;
using WYJCore.Extension;

namespace WYJWeb.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role:ModelBase<Role> ,IRole
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色权限
        /// </summary>
        public string Powers { get; set; }

        [SugarColumn(IsIgnore =true)]
        public List<int> powers => Powers.SplitToInt();

        public bool HasPower()
        {
            return true;
        }
    }
}