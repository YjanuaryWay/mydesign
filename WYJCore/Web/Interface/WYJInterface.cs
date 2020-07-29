using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WYJCore
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 登录账户
        /// </summary>
        public string LoginId { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

    }

    /// <summary>
    /// 角色
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }
    }

    public interface IModel
    {
        public int Id { get; set; }

        public string ModelType { get; }

        public bool IsEmpty();
    }

}
