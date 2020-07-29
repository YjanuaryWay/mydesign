using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using WYJCore.Extension;

namespace WYJCore.Web
{
    public class Session
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string Id;

        /// <summary>
        /// 有效时间
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// 最近访问时间
        /// </summary>
        public DateTime LastTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IUser User { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return DateTime.Now < LastTime + Timeout;
        }

        public static List<Session> currentUsers = new List<Session>();
        public static bool TryGetCurrentUser(string id, out Session currentUser)
        {
            currentUser = currentUsers.Find(x => x.Id.EqualTo(id));
            return currentUser != null;
        }

        public static void AddUser(string id,IUser user, TimeSpan? timeout = null)
        {

            currentUsers.Add(new Session { Id = id, LastTime = DateTime.Now, Timeout = timeout ?? TimeSpan.FromMinutes(20),User = user });
        }

        public static bool CheckLogin(HttpContext httpContext)
        {
            bool Login = httpContext.Request.Cookies.TryGetValue("SID", out string sid);
            if (Login)
            {
                Login = TryGetCurrentUser(sid, out Session currentUser);
                if (Login)
                {
                    Login = currentUser.IsValid();
                    if (Login)
                    { 
                        currentUser.LastTime = DateTime.Now;
                    }
                    else
                    {
                        currentUsers.Remove(currentUser);
                    }
                }
            }
            else
            {
                sid = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append("SID", sid);
            }
            return Login;
        }

    }
}