using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;

namespace bbsharp.Renderers.Html
{
    public static partial class HtmlRenderer
    {
        /// <summary>
        /// Internal method for rendering images
        /// </summary>
        public static string RenderColor(BBCodeNode Node, bool ThrowOnError, object LookupTable)
        {
            if ((Node.Attribute ?? "").Trim() == "")
                if (ThrowOnError)
                    throw new HtmlRenderException("Missing attribute in [color] tag.");
                else
                    return Error(Node, LookupTable);

            if (!Regex.IsMatch(Node.Attribute.Trim(), "^#?[a-z0-9]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase))
                if (ThrowOnError)
                    throw new HtmlRenderException("Invalid color in [color] tag. Expected either color name or hexadecimal color");
                else
                    return Error(Node, LookupTable);

            return "<span style=\"color:" + Node.Attribute.Trim() + "\">"
                    + Node.Children.ToHtml(ThrowOnError, LookupTable)
                    + "</span>";
        }
    }
}