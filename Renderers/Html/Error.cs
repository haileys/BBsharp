using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;

namespace bbsharp.Renderers.Html
{
    public static partial class HtmlRenderer
    {
        static string Error(BBCodeNode Node)
        {
            if (Node.Singular)
                return "["
                        + Node.TagName
                        + Node.Attribute != null &&
                            Node.Attribute.Trim() != ""
                            ? "=" + HttpUtility.HtmlEncode(Node.Attribute)
                            : ""
                        + "]";
            else
                return "["
                        + Node.TagName
                        + Node.Attribute != null &&
                            Node.Attribute.Trim() != ""
                            ? "=" + HttpUtility.HtmlEncode(Node.Attribute)
                            : ""
                        + "]"
                        + Node.ToHtml(false)
                        + "[/" + Node.TagName + "]";
        }
    }
}