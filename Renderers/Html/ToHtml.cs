using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using LookupT = System.Collections.Generic.KeyValuePair<string, System.Func<bbsharp.BBCodeNode, bool, object, string>>;

namespace bbsharp.Renderers.Html
{
    public static partial class HtmlRenderer
    {
        /// <summary>
        /// Converts a collection of BBCodeNodes to HTML
        /// </summary>
        /// <returns>The HTML source as a string</returns>
        public static string ToHtml(this IEnumerable<BBCodeNode> Nodes)
        {
            return Nodes.ToHtml(false);
        }
        public static string ToHtml(this IEnumerable<BBCodeNode> Nodes, bool ThrowOnError)
        {
            return Nodes.ToHtml(ThrowOnError, (object)convertLookup);
        }
        public static string ToHtml(this IEnumerable<BBCodeNode> Nodes, bool ThrowOnError, object LookupTable)
        {
            StringBuilder html = new StringBuilder();

            foreach (var node in Nodes)
                html.Append(node.ToHtml(ThrowOnError, LookupTable));

            return html.ToString();
        }
        static readonly LookupT[] convertLookup = new Dictionary<string, Func<BBCodeNode, bool, object, string>>
        {
            // The BBCode tags which correlate 1:1 with HTML tags
            { "b",      DirectConvert },
            { "i",      DirectConvert },
            { "u",      DirectConvert },
            { "code",   DirectConvert },
            { "del",    DirectConvert },
            { "ins",    DirectConvert },
            { "hr",     DirectConvert },
            { "body",   DirectConvert },

            // BBCode tags which require special handling
            { "img",    RenderImage },
            { "a",      RenderLink },
        }.ToArray();

        public static string ToHtml(this BBCodeNode Node)
        {
            return Node.ToHtml(false);
        }
        public static string ToHtml(this BBCodeNode Node, bool ThrowOnError)
        {
            return Node.ToHtml(ThrowOnError, (object)convertLookup);
        }
        public static string ToHtml(this BBCodeNode Node, bool ThrowOnError, object LookupTable)
        {
            if ((Node as BBCodeTextNode) != null)
                return HttpUtility.HtmlEncode(Node.ToString());

            var d = ((LookupT[])LookupTable).Where(x => ((LookupT)x).Key.ToLower() == Node.TagName.ToLower());
            if (d.Count() > 0)
                return ((LookupT)d.First()).Value(Node, ThrowOnError, LookupTable);

            if (ThrowOnError)
                throw new HtmlRenderException("Unknown tag name '" + Node.TagName + "'");

            return Error(Node, LookupTable);
        }
    }
}
