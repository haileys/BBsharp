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
        /// <summary>
        /// Internal method for rendering a BBCode tag that corresponds 1:1 with an HTML tag
        /// </summary>
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