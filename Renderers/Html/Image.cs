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
        static string RenderImage(BBCodeNode Node, bool ThrowOnError)
        {
            if (Node.Children.Length != 1)
                if (ThrowOnError)
                    throw new HtmlRenderException("[img] tag does not to contain image URL");
                else
                    return Error(Node);

            if ((Node.Children[0] as BBCodeTextNode) == null)
                if (ThrowOnError)
                    throw new HtmlRenderException("[img] tag does not to contain image URL");
                else
                    return Error(Node);

            Uri src = null;
            try
            {
                src = new Uri(Node.Children[0].ToString(), UriKind.Absolute);
            }
            catch (UriFormatException)
            {
                if (ThrowOnError)
                    throw;
                return Error(Node);
            }

            if (!src.IsWellFormedOriginalString())
                if (ThrowOnError)
                    throw new HtmlRenderException("Image URL in [img] tag not well formed or relative");
                else
                    return Error(Node);

            if (!src.Scheme.Contains("http"))
                if (ThrowOnError)
                    throw new HtmlRenderException("Image URL scheme must be either HTTP or HTTPS");
                else
                    return Error(Node);
            
            // @todo
            throw new NotImplementedException();
        }
    }
}