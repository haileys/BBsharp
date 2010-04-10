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
        /// Internal method for rendering images
        /// </summary>
        public static string RenderImage(BBCodeNode Node, bool ThrowOnError, object LookupTable)
        {
            if (Node.Children.Length != 1)
                if (ThrowOnError)
                    throw new HtmlRenderException("[img] tag does not to contain image URL");
                else
                    return Error(Node, LookupTable);

            if ((Node.Children[0] as BBCodeTextNode) == null)
                if (ThrowOnError)
                    throw new HtmlRenderException("[img] tag does not to contain image URL");
                else
                    return Error(Node, LookupTable);

            Uri src = null;
            try
            {
                src = new Uri(Node.Children[0].ToString(), UriKind.Absolute);
            }
            catch (UriFormatException)
            {
                if (ThrowOnError)
                    throw;
                return Error(Node, LookupTable);
            }

            if (!src.IsWellFormedOriginalString())
                if (ThrowOnError)
                    throw new HtmlRenderException("Image URL in [img] tag not well formed or relative");
                else
                    return Error(Node, LookupTable);

            if (!src.Scheme.Contains("http"))
                if (ThrowOnError)
                    throw new HtmlRenderException("Image URL scheme must be either HTTP or HTTPS");
                else
                    return Error(Node, LookupTable);
            
            return "<img src=\"" + HttpUtility.HtmlEncode(src.ToString()) + "\" alt=\"" + HttpUtility.HtmlEncode(src.ToString()) + "\" />";
        }
    }
}