using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WYJCore.Extension;

namespace WYJCore.Web
{
    internal class RouterHelper
    {

        public static bool CheckParameterSignature_Optional(ParameterInfo[] parameterInfos, HttpRequest httpRequest)
        {
            IEnumerable<string> allArgs = parameterInfos.Select(x => x.Name.ToMyUpper());
            IEnumerable<string> needArgs = parameterInfos.Where(x => !x.IsOptional).Select(x => x.Name.ToMyUpper());
            IEnumerable<string> keys = httpRequest.Query.Keys.Select(x => x.ToMyUpper());
            if (httpRequest.Method != "GET") keys = keys.Concat(httpRequest.Form.Keys.Select(X => X.ToMyUpper()));
            return needArgs.Except(keys).Count() + keys.Except(allArgs).Count() == 0;
        }

        public bool CheckParameterSignature(ParameterInfo[] parameterInfos, HttpRequest httpRequest)
        {
            IEnumerable<string> allArgs = parameterInfos.Select(x => x.Name.ToMyUpper());
            IEnumerable<string> keys = httpRequest.Query.Keys.Select(X => X.ToMyUpper());
            if (httpRequest.Method != "GET") keys = keys.Concat(httpRequest.Form.Keys.Select(X => X.ToMyUpper()));
            return allArgs.Except(keys).Count() + keys.Except(allArgs).Count() == 0;
        }

        public static object[] BuildArgs(ParameterInfo[] parameterInfos, HttpRequest httpRequest)
        {
            object[] os = new object[parameterInfos.Length];
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                string name = parameterInfos[i].Name;
                bool succsess = httpRequest.Method != "GET" && name.StartsWith("form", StringComparison.OrdinalIgnoreCase) ?
                    httpRequest.Form.TryGetValue(name, out StringValues str) :
                    httpRequest.Query.TryGetValue(name, out str);
                if (!succsess)
                {
                    if (!parameterInfos[i].IsOptional) throw new WYJException($"参数{name}值缺失");
                    os[i] = parameterInfos[i].DefaultValue;
                    continue;
                }

                ///取值成功后，转换成匹配类型
                ///控制器方法参数只支持：string,int,bool三种

                if (parameterInfos[i].ParameterType == typeof(int) || parameterInfos[i].ParameterType.IsEnum)
                {
                    if (!int.TryParse(str, out int val)) throw new WYJException($"参数{name}转换int失败");
                    os[i] = val;
                }
                else if (parameterInfos[i].ParameterType == typeof(bool))
                {
                    if (!bool.TryParse(str, out bool val)) throw new WYJException($"参数{name}转换bool失败");
                    os[i] = val;
                }
                else
                {
                    os[i] = str.ToString();
                }
            }
            return os;
        }

    }
}