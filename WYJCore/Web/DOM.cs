using System.Collections.Generic;

namespace WYJCore.Web.TagHelpers
{
    public abstract class DOM
    {

        public DOM(List<string> classes = null)
        {
            if (classes != null)
                Class = string.Join(" ", classes);
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
    }

    public class Form : DOM
    {
        public Form(List<string> classes = null) : base(classes)
        {

        }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class Input : Form
    {
        public Input(List<string> classes = null) : base(classes)
        {

        }
        public string Placeholder { get; set; }
    }

    public class Select : Form
    {
        public Select(List<string> classes = null) : base(classes)
        {

        }
    }

}