using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using LookupT = System.Collections.Generic.KeyValuePair<string, bbsharp.Renderers.Html.HtmlRendererCallback>;

namespace bbsharp.Renderers.Html
{
    /// <summary>
    /// A delegate that allows you to register for your own BBCode tags. This is called when the parser encounters the respective custom tag
    /// </summary>
    /// <param name="Node">The node that is the custom tag</param>
    /// <param name="ThrowOnError">True if an exception should be thrown upon error. If set to false, no exceptions should be thrown and errors should be silently dealt with</param>
    /// <param name="LookupTable">Internal use only. Must be passed on to any further ToHtml() calls</param>
    /// <returns>A string containing the HTML code for Node and all children.</returns>
    public delegate string HtmlRendererCallback(BBCodeNode Node, bool ThrowOnError, object LookupTable);

    /// <summary>
    /// Static class containing HTML rendering methods
    /// </summary>
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
        /// <summary>
        /// Converts a collection of BBCodeNodes to HTML
        /// </summary>
        /// <param name="ThrowOnError">Whether or to throw an exception when an error is encountered. If false, errors will be silently ignored</param>
        /// <returns>The HTML source as a string</returns>
        public static string ToHtml(this IEnumerable<BBCodeNode> Nodes, bool ThrowOnError)
        {
            return Nodes.ToHtml(ThrowOnError, (object)convertLookup);
        }
        /// <summary>
        /// Converts a collection of BBCodeNodes to HTML
        /// </summary>
        /// <param name="ThrowOnError">Whether or to throw an exception when an error is encountered. If false, errors will be silently ignored</param>
        /// <param name="LookupTable">A KeyValuePair&lt;string,HtmlRendererCallback&gt;[] containing valid BBCode tags and their HTML renderers</param>
        /// <returns>The HTML source as a string</returns>
        public static string ToHtml(this IEnumerable<BBCodeNode> Nodes, bool ThrowOnError, object LookupTable)
        {
            StringBuilder html = new StringBuilder();

            foreach (var node in Nodes)
                html.Append(node.ToHtml(ThrowOnError, LookupTable));

            return html.ToString();
        }
        static readonly LookupT[] convertLookup = new Dictionary<string, HtmlRendererCallback>
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
            { "color",  RenderColor },
            { "colour", RenderColor },
        }.ToArray();

        /// <summary>
        /// The default lookup table. Contains renderers for [b], [i], [u], [code], [del], [ins], [hr], [body], [img] and [a] tags
        /// </summary>
        public static LookupT[] DefaultLookupTable { get { return convertLookup; } }

        /// <summary>
        /// Converts a BBCodeNode to HTML
        /// </summary>
        /// <returns>The HTML source as a string</returns>
        public static string ToHtml(this BBCodeNode Node)
        {
            return Node.ToHtml(false);
        }
        /// <summary>
        /// Converts a BBCodeNode to HTML
        /// </summary>
        /// <param name="ThrowOnError">Whether or to throw an exception when an error is encountered. If false, errors will be silently ignored</param>
        /// <returns>The HTML source as a string</returns>
        public static string ToHtml(this BBCodeNode Node, bool ThrowOnError)
        {
            return Node.ToHtml(ThrowOnError, (object)convertLookup);
        }
        /// <summary>
        /// Converts a BBCodeNode to HTML
        /// </summary>
        /// <param name="ThrowOnError">Whether or to throw an exception when an error is encountered. If false, errors will be silently ignored</param>
        /// <param name="LookupTable">A KeyValuePair&lt;string,HtmlRendererCallback&gt;[] containing valid BBCode tags and their HTML renderers</param>
        /// <returns>The HTML source as a string</returns>
        public static string ToHtml(this BBCodeNode Node, bool ThrowOnError, object LookupTable)
        {
            if ((Node as BBCodeTextNode) != null)
                return HttpUtility.HtmlEncode(Node.ToString()).Replace("\n", "<br />");

            var d = ((LookupT[])LookupTable).Where(x => ((LookupT)x).Key.ToLower() == Node.TagName.ToLower());
            if (d.Count() > 0)
                return ((LookupT)d.First()).Value(Node, ThrowOnError, LookupTable);

            if (ThrowOnError)
                throw new HtmlRenderException("Unknown tag name '" + Node.TagName + "'");

            return Error(Node, LookupTable);
        }
    }
}
