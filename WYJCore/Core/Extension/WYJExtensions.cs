using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WYJCore.Web;

namespace WYJCore.Extension
{
    public static class WYJExtensions
    {
        ///////////////////////////////////////* 1.MiddleWareExtensions*///////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWYJRouter(this IApplicationBuilder application, Action action = null)
        {
            return application.UseMiddleware<WYJRouter>(action);
        }

        public static IServiceCollection AddWYJRouter(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(option =>
            {
                option.ViewLocationFormats.Clear();
                option.ViewLocationFormats.Add("/Pages/{0}.cshtml");
                option.ViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
            });

            services.AddSingleton<SiteLauncher>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            return services;
        }

        ////////////////////////////////////* 2.EnumerableExtensions*//////////////////////////////////////////////////////
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> ts,Action<T,int> action)
        {            
            var t = ts.GetEnumerator();
            var l = ts.Count();
            for (int i = 0; i < l; i++)
            {
                action(t.Current, i);
                t.MoveNext();
            }
            t.Reset();
            return ts;
        }

        public static string JoinToString<T>(this IEnumerable<T> ts,string c = ",")
        {
            var separator = "";
            return ts.Aggregate("",(res,x) => {
                res += separator + x.ToString();
                separator = c;
                return res;
            });
        }


        /////////////////////////////////////* 3.TypeExtensions*/////////////////////////////////////////////////////
        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList<KeyValuePair<string, object>> EnumList(this Type type)
        {
            if (type.IsEnum)
            {
                return type.GetEnumNames().Select(x => 
                    new KeyValuePair<string, object> (x,Enum.Parse(type,x))
                ).ToList();
            }
            else if(type.IsArray){

            };
            return null;
        }


        /////////////////////////////////////* 4.StringExtensions*/////////////////////////////////////////////////////
        /// <summary>
        /// 字符串相等比较
        /// </summary>
        public static bool EqualTo(this string str, string Name, bool ignoreCase = true)
        {
            return string.Compare(str, Name, ignoreCase) == 0;
        }

        /// <summary>
        /// 默认首位大写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="indexcollection">大写位置的索引</param>
        /// <returns></returns>
        public static string ToMyUpper(this string str, IEnumerable<int> indexcollection = default)
        {
            indexcollection ??= new int[] { 0 };
            var chars = str.ToLower().ToCharArray();
            foreach (var i in indexcollection)
            {
                if (i < chars.Length)
                {
                    chars[i] = char.ToUpper(chars[i]);
                }
            }
            return new string(chars);
        }
        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || str.Length == 0;
        }

        public static List<int> SplitToInt(this string str,string separator=",")
        {
            return str.Split(separator).Select(x => int.Parse(x)).ToList();
        }

        ///////////////////////////////////////* 5.TagHelperOutputExtensions*///////////////////////////////////////////////////
        /// <summary>
        /// </summary>
        /// <param name="output"></param>
        /// <param name="menu"></param>
        public static void AddAttributes(this TagHelperOutput output, object menu)
        {
            Type type = menu.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                string val = (string)property.GetValue(menu);
                if (!val.IsNullOrEmpty())
                    output.Attributes.SetAttribute(property.Name, val);
            }
        }

        ///////////////////////////////////////* 6.DateTimeExtensions*///////////////////////////////////////////////////

        public static DateTime GetNextWeekday(this DateTime thisDate, DayOfWeek day)
        {
            var x = day - thisDate.DayOfWeek;
            if (x <= 0) x += 7;
            return thisDate.AddDays(x);
        }


    }

}
