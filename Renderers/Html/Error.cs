using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using LookupT = System.Collections.Generic.KeyValuePair<string, System.Func<bbsharp.BBCodeNode, bool, string>>;

namespace bbsharp.Renderers.Html
{
    public static partial class HtmlRenderer
    {
        static string Error(BBCodeNode Node, object LookupTable)
        {
            if (Node.Singular)
                return "["
                        + Node.TagName
                        + (((Node.Attribute ?? "").Trim() != "")
                            ? ("=" + HttpUtility.HtmlEncode(Node.Attribute ?? ""))
                            : (""))
                        + "]";
            else
                return "["
                        + Node.TagName
                        + (((Node.Attribute ?? "").Trim() != "")
                            ? ("=" + (HttpUtility.HtmlEncode(Node.Attribute ?? "")))
                            : (""))
                        + "]"
                        + Node.Children.ToHtml(false, LookupTable)
                        + "[/" + Node.TagName + "]";
        }
    }
}