using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using WYJCore.Extension;

namespace WYJCore
{
    public class Classes
    {
        public Type[] Types { get; } = Assembly.GetEntryAssembly().GetExportedTypes();

        /// <summary>
        /// 获取全部类型
        /// </summary>
        public Classes() { }
        /// <summary>
        /// 获取指定类型
        /// </summary>
        /// <param name="filter"></param>
        public Classes(Func<Type, bool> filter)
        {
            Types = Types.Where(x => filter(x)).ToArray();
        }

        public Type GetClass(string name, bool ignoreCase = true)
        {
            return Types.SingleOrDefault(x => x.Name.EqualTo(name, ignoreCase));
        }

        public Type GetClass(Func<Type, bool> filter)
        {
            return Types.SingleOrDefault(x => filter(x));
        }

        //public static object CreateInstance(Type type)
        //{
        //    object Instance;
        //    IServiceProvider service = null;
        //    var ctor = type.GetConstructors()[0];
        //    //foreach (var ctor in ctors)
        //    //{
        //    var parameters = ctor.GetParameters();
        //    object[] vs = new object[parameters.Length];
        //    if (parameters.Length == 0)
        //    {
        //        Instance = ctor.Invoke(null);
        //    }
        //    else
        //    {
        //        object[] PS = new object[parameters.Length];
        //        for (int i = 0; i < parameters.Length; i++)
        //        {
        //            if (parameters[i].HasDefaultValue)
        //            {
        //                PS[i] = (parameters[i].DefaultValue);
        //            }
        //            else
        //            {
        //                if (parameters[i].ParameterType.IsInterface)
        //                {
        //                    var p = service.GetService(parameters[i].ParameterType);
        //                    if (p != null)
        //                        PS[i] = p;
        //                }
        //                else
        //                {
        //                    var p = Classes.CreateInstance(parameters[i].ParameterType);
        //                    if (p != null) PS[i] = p;
        //                }
        //            }
        //        }
        //        Instance = Activator.CreateInstance(type, PS);
        //    }

        //    return Instance;
        //    //}
        //}

    }

}