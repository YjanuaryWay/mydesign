using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using WYJCore.Extension;

namespace WYJCore.Web.TagHelpers
{
    public class SearchbarTagHelper : TagHelper
    {
        public string Key { get; set; }
        public string Placeholder { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            var input =
                new Input(new List<string>
                {
                   "wyj-fitem"
                })
                {
                    Name = "key",
                    Id = "toolbar",
                    Placeholder = "请输入搜索关键词"
                };
            output.AddAttributes(input);

            output.TagMode = TagMode.SelfClosing;
        }
    }
}
