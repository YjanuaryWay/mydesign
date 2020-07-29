using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace WYJCore.Web
{
    internal class UrlHelper
    {
        public static string Api = @"^/[a|A][p|P][i|I][s|S]?(/[A-z|_]\w*){2}(/*)?";
        public static string[] ParsePathString(string pathString)
        {
            return pathString.Trim('/').Split("/");
        }

        public static string[] ParseQueryString(QueryString queryString)
        {
            return queryString.Value.Trim('?').Split("&");
        }

        public static bool IsStatic(string pathString)
        {
            return ParsePathString(pathString)[^1].Contains('\u002e');
        }

        public static bool IsApi(string pathString)
        {
            return new Regex(Api).IsMatch(pathString);
        }

    }

    internal class RegString
    {
        public string Variable { get; set; } = @"[A-z|_]\w*";
    }

}