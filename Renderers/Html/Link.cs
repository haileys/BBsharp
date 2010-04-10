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
        /// <summary>
        /// Internal method for rendering links
        /// </summary>
        public static string RenderLink(BBCodeNode Node, bool ThrowOnError, object LookupTable)
        {
            if ((Node.Attribute ?? "").Trim() == "")
                if (Node.Children.Length != 0 && (Node.Children[0] as BBCodeTextNode) == null)
                    if (ThrowOnError)
                        throw new HtmlRenderException("[url] tag does not contain a URL attribute");
                    else
                        return Error(Node, LookupTable);
                else
                {
                    // support self-links such as [url]http://google.com[/url]
                    Node = ((BBCodeNode)Node.Clone()); // Nodes are mutable, and we don't want to mess with the original Node.
                    Node.Attribute = Node.Children[0].ToString();
                }



            Uri src = null;
            try
            {
                src = new Uri(Node.Attribute);
            }
            catch (UriFormatException)
            {
                if (ThrowOnError)
                    throw;
                return Error(Node, LookupTable);
            }

            if (!src.IsWellFormedOriginalString())
                if (ThrowOnError)
                    throw new HtmlRenderException("URL in [url] tag not well formed or relative");
                else
                    return Error(Node, LookupTable);

            if (!src.Scheme.Contains("http"))
                if (ThrowOnError)
                    throw new HtmlRenderException("URL scheme must be either HTTP or HTTPS");
                else
                    return Error(Node, LookupTable);
            
            return "<a href=\"" + HttpUtility.HtmlEncode(src.ToString()) + "\">"
                    + Node.Children.ToHtml()
                    + "</a>";
        }
    }
}