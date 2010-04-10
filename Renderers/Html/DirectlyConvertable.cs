using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Collections.Generic;
using LookupT = System.Collections.Generic.KeyValuePair<string, System.Func<bbsharp.BBCodeNode, bool, string>>;

namespace bbsharp.Renderers.Html
{
    public static partial class HtmlRenderer
    {
        public static string DirectConvert(BBCodeNode Node, bool ThrowOnError, object LookupTable)
        {
            if (Node.Singular)
                return "<" + Node.TagName + " />";

            return "<" + Node.TagName + ">"
                    + Node.Children.ToHtml(ThrowOnError, LookupTable)
                    + "</" + Node.TagName + ">";
        }
    }
}