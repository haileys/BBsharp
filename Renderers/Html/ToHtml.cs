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
        public static string ToHtml(this IEnumerable<BBCodeNode> Nodes)
        {
            return Nodes.ToHtml(false);
        }
        public static string ToHtml(this IEnumerable<BBCodeNode> Nodes, bool ThrowOnError)
        {
            StringBuilder html = new StringBuilder();

            foreach (var node in Nodes)
                html.Append(node.ToHtml(ThrowOnError));

            return html.ToString();
        }

        static const string[] directBbToHtml = new string[] {
            "b", "i", "u", "code", "del", "ins", "hr"
        };

        public static string ToHtml(this BBCodeNode Node)
        {
            return Node.ToHtml(false);
        }
        public static string ToHtml(this BBCodeNode Node, bool ThrowOnError)
        {
            if ((Node as BBCodeTextNode) != null)
                return HttpUtility.HtmlEncode(Node.ToString());

            if (directBbToHtml.Contains(Node.TagName))
                if (Node.Singular)
                    return "<" + Node.TagName + "/>";
                else
                    return "<" + Node.TagName + ">"
                            + Node.ToHtml(ThrowOnError)
                            + "</" + Node.TagName + ">";

            switch (Node.TagName)
            {
                case "img":
                    return RenderImage(Node, ThrowOnError);

                default:
                    return Error(Node);
            }
        }
    }
}
